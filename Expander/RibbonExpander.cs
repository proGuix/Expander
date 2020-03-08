using System;
using System.Collections.Generic;
using Microsoft.Office.Tools.Ribbon;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace Expander
{
    public partial class RibbonExpander
    {
        private Dictionary<String, String> m_oMap = null;
        private KeyboardListener m_lKeyBoard = null;
        static public String sSeps = " \u00A0.…,;:!?\r\t\a\n";

        private void RibbonExpander_Load(object sender, RibbonUIEventArgs e)
        {
            m_lKeyBoard = new KeyboardListener();
            m_lKeyBoard.OnKeyPressed += Listener_OnKeyPressed;

            if (m_oCheckBoxAutoExpand.Checked == true)
            {
                m_lKeyBoard.HookKeyboard();
            }
        }

        private void AllExpand_Click(object sender, RibbonControlEventArgs e)
        {
            AllExpand();
            //if (m_oMap != null)
            //{
            //    Word.Find oFind = Globals.ThisDocument.Application.Selection.Find;
            //
            //    foreach (KeyValuePair<String, String> oEntry in m_oMap)
            //    {
            //        oFind.Execute(oEntry.Key, true, true, false, false,
            //                      true, false, null, false, oEntry.Value,
            //                      Word.WdReplace.wdReplaceAll,
            //                      false, false, false, false);
            //    }
            //}
        }

        private void AutoExpand_Click(object sender, RibbonControlEventArgs e)
        {
            if (m_oCheckBoxAutoExpand.Checked == true)
            {
                m_lKeyBoard.HookKeyboard();
            }
            else
            {
                m_lKeyBoard.UnHookKeyboard();
            }
        }

        void Listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            bool bKeysAutorized = e.Ctrl == false
                                  && e.Alt == false
                                  && sSeps.Contains(e.KeyPressed.ToString());
            if (bKeysAutorized)
            {
                //Debug.WriteLine("Keys: " + e.KeyPressed + " Ctrl: " + e.Ctrl + " Alt: " + e.Alt);
                AutomationElement oPanelDoc = null;
                AutomationElement oWindow = null;
                AutomationElement oFocus = null;
#if WORD2010
                Process oCurrentProc = Process.GetCurrentProcess();
                oWindow = AutomationElement.FromHandle(oCurrentProc.MainWindowHandle);
                oFocus = AutomationElement.FocusedElement;
                //word francais
                oPanelDoc = oWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Microsoft Word Document"));
                if (oPanelDoc == null)
                {
                    //word espagnol
                    oPanelDoc = oWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Documento de Microsoft Word"));
                }
                
#endif
#if WORD365
                oWindow = AutomationElement.FromHandle(new IntPtr(Globals.ThisDocument.Application.ActiveWindow.Hwnd));
                oFocus = AutomationElement.FocusedElement;
                oPanelDoc = oWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Document));
#endif
                if (oPanelDoc != null && oFocus == oPanelDoc)
                {
                    Thread myTh;
                    myTh = new Thread(new ThreadStart(AutoExpand));
                    myTh.Start();
                }
            }
        }

        private void LoadProfile_Click(object sender, RibbonControlEventArgs e)
        {
            OpenFileDialog oOpenFileDialog = new OpenFileDialog()
            {
                Filter = "XML files (*.xml)|*.xml",
                Title = "Open XML file"
            };

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                FormLoadProfile f = new FormLoadProfile();
                f.SetFilePath(oOpenFileDialog.FileName);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    m_oMap = f.GetMap();
                }
            }
        }

        private void SetProfileDefault_Click(object sender, RibbonControlEventArgs e)
        {
        }

        private void AutoExpand()
        {
#if WORD2010
            Thread.Sleep(100);
#endif
            if (m_oMap != null)
            {
                Word.Application oWordApp = Globals.ThisDocument.Application;
                try
                {
                    if (oWordApp.Selection.Type == Word.WdSelectionType.wdSelectionIP)
                    {
                        Word.Range oCurrentPosition = oWordApp.Selection.Range;
                        oWordApp.Selection.MoveStart(Word.WdUnits.wdLine, -1);
                        var oRngSel = oWordApp.Selection.Range;
                        oCurrentPosition.Select();
                        String sSel = oRngSel.Text;
                        if (sSel != null && sSel != "")
                        {
                            String sWordSeps = sSeps;
                            int nIdxText = sSel.Length - 1;
                            String sEndSel = "";
                            //End of selection
                            while (nIdxText >= 0 && sWordSeps.Contains(sSel[nIdxText].ToString()) == true)
                            {
                                sEndSel = sSel[nIdxText].ToString() + sEndSel;
                                nIdxText--;
                            }
                            String sLastWordSel = "";
                            //Last word
                            while (nIdxText >= 0 && sWordSeps.Contains(sSel[nIdxText].ToString()) == false)
                            {
                                sLastWordSel = sSel[nIdxText].ToString() + sLastWordSel;
                                nIdxText--;
                            }
                            String sKey = sLastWordSel;
                            String sPref = "";
                            int nPosApos = sLastWordSel.IndexOf('\'');
                            if (nPosApos == -1)
                            {
                                nPosApos = sLastWordSel.IndexOf('’');
                            }
                            if (nPosApos != sLastWordSel.Length - 1)
                            {
                                if (nPosApos != -1)
                                {
                                    sKey = sLastWordSel.Substring(nPosApos + 1, sLastWordSel.Length - nPosApos - 1);
                                    sPref = sLastWordSel.Substring(0, nPosApos + 1);
                                }
                                bool bIsLower = IsLower(sKey);
                                if (m_oMap.ContainsKey(sKey))
                                {
                                    oWordApp.Selection.Delete(Word.WdUnits.wdCharacter, -(sLastWordSel.Length) - (sEndSel.Length));
                                    oWordApp.Selection.TypeText(sPref + m_oMap[sKey] + sEndSel);
                                }
                                else if (!bIsLower)
                                {
                                    if (IsUpper(sKey))
                                    {
                                        if (m_oMap.ContainsKey(sKey.ToLower()))
                                        {
                                            oWordApp.Selection.Delete(Word.WdUnits.wdCharacter, -(sLastWordSel.Length) - (sEndSel.Length));
                                            oWordApp.Selection.TypeText(sPref + m_oMap[sKey.ToLower()].ToUpper() + sEndSel);
                                        }
                                    }
                                    else if (IsOnlyFirstCharUpper(sKey))
                                    {
                                        if (m_oMap.ContainsKey(sKey.ToLower()))
                                        {
                                            oWordApp.Selection.Delete(Word.WdUnits.wdCharacter, -(sLastWordSel.Length) - (sEndSel.Length));
                                            String sValue = m_oMap[sKey.ToLower()];
                                            if (sValue.Length == 1)
                                            {
                                                sValue = sValue.ToUpper();
                                            }
                                            else
                                            {
                                                sValue = Char.ToUpper(sValue[0]) + sValue.Substring(1);
                                            }
                                            oWordApp.Selection.TypeText(sPref + sValue + sEndSel);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                }
            }
        }

        private void AllExpand()
        {
            if (m_oMap != null)
            {
                Word.Application oWordApp = Globals.ThisDocument.Application;
                Word.Document oDoc = Globals.ThisDocument.Application.ActiveDocument;
                try
                {
                    Word.Range oCurrentPosition = oWordApp.Selection.Range;
                    oWordApp.Selection.Move(Word.WdUnits.wdStory, -1);
                    String sWordSeps = sSeps;

                    int nNbCharDoc = oWordApp.Selection.Document.Content.Characters.Count;
                    int nNbCharTravel = 0;
                    bool bFinish = (nNbCharDoc == nNbCharTravel);

                    //loop over each line in the doc
                    while (bFinish == false)
                    {
                        oWordApp.Selection.MoveEnd(Word.WdUnits.wdLine, 1);
                        var oRngSel = oWordApp.Selection.Range;
                        String sSel = oRngSel.Text;
                        oWordApp.Selection.Move(Word.WdUnits.wdLine, -1);
                        if (sSel != null && sSel != "")
                        {
                            int nIdxText = 0;
                            String sLastWordSel = "";
                            bool bWordMatch = false;
                            //loop over each word in the line
                            int nCountLastSepSel = 0;
                            int nCountLastCharInWord = 0;
                            while (nIdxText < sSel.Length)
                            {
                                while (nIdxText < sSel.Length)
                                {
                                    if (sWordSeps.Contains(sSel[nIdxText].ToString()) == true)
                                    {
                                        if (sSel[nIdxText] != '\a') //separateur de colonne dans les tableaux (ne compte pas comme un token)
                                        {
                                            nCountLastSepSel++;
                                            nNbCharTravel++;
                                        }
                                        nIdxText++;
                                    }
                                    else
                                    {
                                        sLastWordSel = sLastWordSel + sSel[nIdxText].ToString();
                                        nIdxText++;
                                        nCountLastCharInWord++;
                                        nNbCharTravel++;
                                        break;
                                    }
                                }
                                oWordApp.Selection.Move(Word.WdUnits.wdCharacter, nCountLastSepSel);
                                nCountLastSepSel = 0;
                                //Last word
                                while (nIdxText < sSel.Length)
                                {
                                    nNbCharTravel++;
                                    if (sWordSeps.Contains(sSel[nIdxText].ToString()) == true)
                                    {
                                        nCountLastSepSel++;
                                        nIdxText++;
                                        break;
                                    }
                                    else
                                    {
                                        sLastWordSel = sLastWordSel + sSel[nIdxText].ToString();
                                        nIdxText++;
                                        nCountLastCharInWord++;
                                    }
                                }
                                if (sLastWordSel != "")
                                {
                                    String sKey = sLastWordSel;
                                    String sPref = "";
                                    int nPosApos = sLastWordSel.IndexOf('\'');
                                    if (nPosApos == -1)
                                    {
                                        nPosApos = sLastWordSel.IndexOf('’');
                                    }
                                    if (nPosApos != sLastWordSel.Length - 1)
                                    {
                                        if (nPosApos != -1)
                                        {
                                            sKey = sLastWordSel.Substring(nPosApos + 1, sLastWordSel.Length - nPosApos - 1);
                                            sPref = sLastWordSel.Substring(0, nPosApos + 1);
                                        }
                                        bool bIsLower = IsLower(sKey);
                                        if (m_oMap.ContainsKey(sKey))
                                        {
                                            oWordApp.Selection.Move(Word.WdUnits.wdCharacter, sLastWordSel.Length);
                                            oWordApp.Selection.Delete(Word.WdUnits.wdCharacter, -(sLastWordSel.Length));
                                            oWordApp.Selection.TypeText(sPref + m_oMap[sKey]);
                                            bWordMatch = true;
                                        }
                                        else if (!bIsLower)
                                        {
                                            if (IsUpper(sKey))
                                            {
                                                if (m_oMap.ContainsKey(sKey.ToLower()))
                                                {
                                                    oWordApp.Selection.Move(Word.WdUnits.wdCharacter, sLastWordSel.Length);
                                                    oWordApp.Selection.Delete(Word.WdUnits.wdCharacter, -(sLastWordSel.Length));
                                                    oWordApp.Selection.TypeText(sPref + m_oMap[sKey.ToLower()].ToUpper());
                                                    bWordMatch = true;
                                                }
                                            }
                                            else if (IsOnlyFirstCharUpper(sKey))
                                            {
                                                if (m_oMap.ContainsKey(sKey.ToLower()))
                                                {
                                                    oWordApp.Selection.Move(Word.WdUnits.wdCharacter, sLastWordSel.Length);
                                                    oWordApp.Selection.Delete(Word.WdUnits.wdCharacter, -(sLastWordSel.Length));
                                                    String sValue = m_oMap[sKey.ToLower()];
                                                    if (sValue.Length == 1)
                                                    {
                                                        sValue = sValue.ToUpper();
                                                    }
                                                    else
                                                    {
                                                        sValue = Char.ToUpper(sValue[0]) + sValue.Substring(1);
                                                    }
                                                    oWordApp.Selection.TypeText(sPref + sValue);
                                                    bWordMatch = true;
                                                }
                                            }
                                        }
                                    }
                                    if (bWordMatch == false)
                                    {
                                        oWordApp.Selection.Move(Word.WdUnits.wdCharacter, nCountLastCharInWord);
                                    }
                                    nCountLastCharInWord = 0;
                                    if (nIdxText == sSel.Length)
                                    {
                                        oWordApp.Selection.Move(Word.WdUnits.wdCharacter, 1);
                                        nCountLastSepSel = 0;
                                    }
                                }
                                sLastWordSel = "";
                                bWordMatch = false;
                            }
                        }
                        bFinish = (nNbCharDoc == nNbCharTravel);
                        oWordApp.Selection.Move(Word.WdUnits.wdLine, -1);
                        oWordApp.Selection.Move(Word.WdUnits.wdLine, 1);
                    }
                    oCurrentPosition.Select();
                }
                catch (System.Runtime.InteropServices.COMException ce)
                {
                    Debug.WriteLine(ce);
                }
            }
        }

        public static String Reverse(String sText)
        {
            char[] tCharText = sText.ToCharArray();
            Array.Reverse(tCharText);
            return new String(tCharText);
        }

        public static bool IsUpper(String sText)
        {
            bool bIsUpper = true;
            for (int i = 0; i < sText.Length; i++)
            {
                if (Char.IsLetter(sText[i]) && Char.IsUpper(sText[i]) == false)
                {
                    bIsUpper = false;
                    break;
                }
            }
            return bIsUpper;
        }

        public static bool IsOnlyFirstCharUpper(String sText)
        {
            bool bIsOnlyFirstCharUpper = Char.IsUpper(sText[0]);
            if (bIsOnlyFirstCharUpper && sText.Length > 1)
            {
                for (int i = 1; i < sText.Length; i++)
                {
                    if (Char.IsUpper(sText[i]) == true)
                    {
                        bIsOnlyFirstCharUpper = false;
                        break;
                    }
                }
            }
            return bIsOnlyFirstCharUpper;
        }

        public static bool IsLower(String sText)
        {
            bool bIsLower = true;
            for (int i = 0; i < sText.Length; i++)
            {
                if (Char.IsLetter(sText[i]) && Char.IsLower(sText[i]) == false)
                {
                    bIsLower = false;
                    break;
                }
            }
            return bIsLower;
        }

        public static bool ConatainsSep(String sText, String sSeps)
        {
            bool bContainsSep = false;
            for (int i = 0; i < sText.Length; i++)
            {
                if (sSeps.Contains(sText[i].ToString()))
                {
                    bContainsSep = true;
                    break;
                }
            }
            return bContainsSep;
        }

        public static String DoubleAntiSlash(String sText)
        {
            String sResult = "";
            for (int i = 0; i < sText.Length; i++)
            {
                if (sText[i] == '\n')
                {
                    sResult += "\\n";
                }
                else if (sText[i] == '\r')
                {
                    sResult += "\\r";
                }
                else if (sText[i] == '\a')
                {
                    sResult += "\\a";
                }
                else
                {
                    sResult += sText[i];
                }
            }
            return sResult;
        }
    }
}
