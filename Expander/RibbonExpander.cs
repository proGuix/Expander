using System;
using System.Collections.Generic;
using Microsoft.Office.Tools.Ribbon;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using System.Windows.Automation;

namespace Expander
{
    public partial class RibbonExpander
    {
        private Dictionary<String, String> m_oMap = null;
        private KeyboardListener m_lKeyBoard = null;

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
            Word.Find oFind = Globals.ThisDocument.Application.Selection.Find;

            foreach (KeyValuePair<String, String> oEntry in m_oMap)
            {
                oFind.Execute(oEntry.Key, true, true, false, false,
                              false, false, null, false, oEntry.Value,
                              Word.WdReplace.wdReplaceAll,
                              false, false, false, false);
            }
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
            if (Globals.ThisDocument.m_oPanelDoc != null && AutomationElement.FocusedElement == Globals.ThisDocument.m_oPanelDoc)
            {
                //Debug.WriteLine("Keys: " + e.KeyPressed + " Ctrl: " + e.Ctrl + " Alt: " + e.Alt);
                if (e.Ctrl == false && e.Alt == false &&
                    (e.KeyPressed == " " || 
                     e.KeyPressed == "." || 
                     e.KeyPressed == "," ||
                     e.KeyPressed == ";" ||
                     e.KeyPressed == "!" ||
                     e.KeyPressed == "?" ||
                     e.KeyPressed == "\r"))
                {
                    AutoExpand();
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
            Word.Selection currentSelection = Globals.ThisDocument.Application.Selection;
            // Test to see if selection is an insertion point.
            if (currentSelection.Type == Word.WdSelectionType.wdSelectionIP)
            {
                var oRng = currentSelection.Range;
                oRng.MoveStart(Word.WdUnits.wdWord, -1);
                if (oRng != null && m_oMap != null)
                {
                    String sText = oRng.Text;
                    if(sText != "")
                    {
                        String sKey = sText;
                        String sPref = "";
                        int nPosApos = sText.IndexOf('\'');
                        if (nPosApos == -1)
                        {
                            nPosApos = sText.IndexOf('’');
                        }
                        if (nPosApos != sText.Length - 1)
                        {
                            if (nPosApos != -1)
                            {
                                sKey = sText.Substring(nPosApos + 1, sText.Length - nPosApos - 1);
                                sPref = sText.Substring(0, nPosApos + 1);
                            }
                            if (m_oMap.ContainsKey(sKey))
                            {
                                oRng.Delete();
                                currentSelection.TypeText(sPref + m_oMap[sKey]);
                            }
                        }
                    }
                }
            }
        }
    }
}
