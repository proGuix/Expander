using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Tools.Ribbon;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using System.Xml.Schema;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Windows.Automation;
using System.Windows;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Input;

namespace Expander
{
    public partial class RIBBON_EXPANDER
    {
        Dictionary<String, String> m_oMap = null;
        private KeyboardListener m_lKeyBoard = null;

        private void RibbonExpander_Load(object sender, RibbonUIEventArgs e)
        {
            m_lKeyBoard = new KeyboardListener();
            m_lKeyBoard.OnKeyPressed += Listener_OnKeyPressed;

            if (CHECKBOX_AUTOEXPAND.Checked == true)
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
            if (CHECKBOX_AUTOEXPAND.Checked == true)
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
                String sFilePath = oOpenFileDialog.FileName;
                try
                {
                    XDocument oDoc = XDocument.Load(sFilePath);

                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Expander.SchemaProfile.xsd");
                    StreamReader reader = new StreamReader(stream);
                    String sXsdMarkup = reader.ReadToEnd();

                    XmlSchemaSet oSchemas = new XmlSchemaSet();
                    oSchemas.Add("", XmlReader.Create(new StringReader(sXsdMarkup)));
                    oDoc.Validate(oSchemas, null);

                    var tResult = (from snippet in oDoc.Descendants("snippet")
                                  select new
                                  {
                                      sTextUnexpand = snippet.Element("text-unexpanded").Value,
                                      sTextExpand = snippet.Element("text-expanded").Value
                                  }
                                 );
                    m_oMap = new Dictionary<String, String>();
                    foreach (var tSnippet in tResult)
                    {
                        m_oMap.Add(tSnippet.sTextUnexpand.Trim(), tSnippet.sTextExpand.Trim());
                    }
                }
                catch (XmlException)
                {
                    MessageBox.Show("Le fichier \"" + sFilePath + "\" n'est pas un XML valide", "Expander Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (XmlSchemaValidationException) 
                {
                    MessageBox.Show("Le fichier \"" + sFilePath + "\" ne valide pas le schéma XSD", "Expander Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
