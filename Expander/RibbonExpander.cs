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

namespace Expander
{
    public partial class RIBBON_EXPANDER
    {
        Dictionary<String, String> m_oMap;

        private void RibbonExpander_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void AllExpand_Click(object sender, RibbonControlEventArgs e)
        {
            Word.Find oFind = Globals.ThisDocument.Application.Selection.Find;
            oFind.ClearFormatting();

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
    }
}
