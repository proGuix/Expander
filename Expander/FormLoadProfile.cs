using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using System.Xml.Schema;
using System.Xml;
using System.Globalization;
using System.Diagnostics;

namespace Expander
{
    public partial class FormLoadProfile : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private Dictionary<String, String> m_oMap = null;
        private List<String> m_oLstDoublon = null;
        private List<String> m_oLstBadWord = null;
        private String m_sFilePathXML = "";
        private XDocument m_oDoc = null;
        private int m_nHeightTextBoxLog = 0;
        private bool m_bIsDetail = false;
        private bool m_bToCancel = false;
        private bool m_bIsCompleted = false;
        private bool m_bIsError = false;

        public FormLoadProfile()
        {
            InitializeComponent();
            m_nHeightTextBoxLog = m_oTextBoxLog.Height;
            m_oTextBoxLog.Height = 0;
            this.Height -= m_nHeightTextBoxLog;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public void SetFilePath(String sFilePathXML)
        {
            m_sFilePathXML = sFilePathXML;
        }

        public Dictionary<String, String> GetMap()
        {
            return m_oMap;
        }

        private void FormLoadProfile_Shown(object sender, EventArgs e)
        {
            m_oLabelStatus.Text = "Lecture du fichier";
            m_oProgressBar.Style = ProgressBarStyle.Marquee;
            m_oProgressBar.MarqueeAnimationSpeed = 10;
            m_oBackWorkLoadFile.RunWorkerAsync();
        }

        private void BackWorkLoadFile_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                if (m_bToCancel == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    m_oDoc = XDocument.Load(m_sFilePathXML);
                    e.Result = 0;
                    worker.ReportProgress(100);
                }
            }
            catch (XmlException)
            {
                e.Result = 1;
            }
            Thread.Sleep(500);
        }

        private void BackWorkLoadFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                m_oLabelStatus.Text = "Annulé";
                Log("Vous avez annulé la lecture du fichier");
                this.Refresh();
                Thread.Sleep(500);
                this.DialogResult = DialogResult.Cancel;
            }
            else if (e.Error != null)
            {
                m_oLabelStatus.Text = "Erreur";
                Log(e.Error.Message);
                m_oProgressBar.Style = ProgressBarStyle.Continuous;
                m_oProgressBar.Value = 0;
                m_bIsError = true;
            }
            else
            {
                switch ((int)e.Result)
                {
                    case 0:
                        Log("Lecture du fichier OK");
                        m_oLabelStatus.Text = "Validation du fichier";
                        m_oProgressBar.Style = ProgressBarStyle.Marquee;
                        m_oProgressBar.MarqueeAnimationSpeed = 10;
                        m_oBackWorkValidateFile.RunWorkerAsync();
                        break;
                    case 1:
                        m_oLabelStatus.Text = "Erreur";
                        Log("Le fichier \"" + m_sFilePathXML + "\" n'est pas un XML valide");
                        m_oProgressBar.Style = ProgressBarStyle.Continuous;
                        m_oProgressBar.Value = 0;
                        m_bIsError = true;
                        break;
                }
            }
        }

        private void BackWorkValidateFile_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                if (m_bToCancel == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Expander.SchemaProfile.xsd");
                    StreamReader reader = new StreamReader(stream);
                    String sXsdMarkup = reader.ReadToEnd();
                    XmlSchemaSet oSchemas = new XmlSchemaSet();
                    oSchemas.Add("", XmlReader.Create(new StringReader(sXsdMarkup)));
                    m_oDoc.Validate(oSchemas, null);
                    e.Result = 0;
                    worker.ReportProgress(100);
                }
            }
            catch (XmlSchemaValidationException)
            {
                e.Result = 1;
            }
            Thread.Sleep(500);
        }

        private void BackWorkValidateFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                m_oLabelStatus.Text = "Annulé";
                Log("Vous avez annulé la validation du fichier");
                this.Refresh();
                Thread.Sleep(500);
                this.DialogResult = DialogResult.Cancel;
            }
            else if (e.Error != null)
            {
                m_oLabelStatus.Text = "Erreur";
                Log(e.Error.Message);
                m_oProgressBar.Style = ProgressBarStyle.Continuous;
                m_oProgressBar.Value = 0;
                m_bIsError = true;
            }
            else
            {
                switch ((int)e.Result)
                {
                    case 0:
                        Log("Validation du fichier OK");
                        m_oLabelStatus.Text = "Chargement des données";
                        m_oProgressBar.Style = ProgressBarStyle.Continuous;
                        m_oProgressBar.Maximum = 100;
                        m_oProgressBar.Minimum = 0;
                        m_oProgressBar.Value = 0;
                        m_oBackWorkLoadData.RunWorkerAsync();
                        break;
                    case 1:
                        m_oLabelStatus.Text = "Erreur";
                        Log("Le fichier \"" + m_sFilePathXML + "\" ne valide pas le schéma XSD");
                        m_oProgressBar.Style = ProgressBarStyle.Continuous;
                        m_oProgressBar.Value = 0;
                        m_bIsError = true;
                        break;
                }
            }
        }

        private void BackWorkLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            IEnumerable<KeyValuePair<String, String>> tResult = from snippet in m_oDoc.Descendants("snippet")
                                                                select new KeyValuePair<String, String>
                                                                (
                                                                    snippet.Element("text-unexpanded").Value,
                                                                    snippet.Element("text-expanded").Value
                                                                );
            m_oMap = new Dictionary<String, String>();
            m_oLstDoublon = new List<String>();
            m_oLstBadWord = new List<String>();
            int nCountResult = tResult.Count();
            e.Result = 0;
            bool bBreak = false;
            for (int i = 0; i < nCountResult && bBreak == false; i++)
            {
                if (m_bToCancel == true || worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    String key = tResult.ElementAt(i).Key.Trim();
                    if (RibbonExpander.ConatainsSep(key, RibbonExpander.sSeps) == false)
                    {
                        if (m_oMap.ContainsKey(key) == false)
                        {
                            String value = tResult.ElementAt(i).Value.Trim();
                            m_oMap.Add(key, value);
                        }
                        else if (m_oLstDoublon.Count < 50)
                        {
                            if (m_oLstDoublon.Contains(key) == false)
                            {
                                e.Result = 1;
                                m_oLstDoublon.Add(key);
                            }
                        }
                        else
                        {
                            bBreak = true;
                        }
                    }
                    else if (m_oLstBadWord.Count < 50)
                    {
                        if (m_oLstBadWord.Contains(key) == false)
                        {
                            e.Result = 2;
                            m_oLstBadWord.Add(key);
                        }
                    }
                    else
                    {
                        bBreak = true;
                    }
                    worker.ReportProgress(((i + 1) * 100) / nCountResult);
                }
            }
            Thread.Sleep(500);
        }

        private void BackWorkLoadData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            m_oProgressBar.Value = e.ProgressPercentage;
        }

        private void BackWorkLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                m_oLabelStatus.Text = "Annulé";
                Log("Vous avez annulé le chargement des données");
                this.Refresh();
                Thread.Sleep(500);
                this.DialogResult = DialogResult.Cancel;
            }
            else if (e.Error != null)
            {
                m_oLabelStatus.Text = "Erreur";
                Log(e.Error.Message);
                m_bIsError = true;
            }
            else
            {
                String sLog = "";
                switch ((int)e.Result)
                {
                    case 0:
                        m_oLabelStatus.Text = "Succès";
                        Log("Chargement des données OK");
                        m_oButtonOK.Enabled = true;
                        m_oButtonOK.Focus();
                        m_bIsCompleted = true;
                        break;
                    case 1:
                        m_oLabelStatus.Text = "Erreur";
                        if (m_oLstDoublon.Count == 1)
                        {
                            sLog = "Les doublons sont interdits, corriger la clef suivante : ";
                            sLog += m_oLstDoublon[0];
                        }
                        else
                        {
                            sLog = "Les doublons sont interdits, ci-dessous la liste des ";
                            sLog += m_oLstDoublon.Count;
                            sLog += " premières clefs en erreur :";
                            for (int i = 0; i < m_oLstDoublon.Count; i++)
                            {
                                sLog += "\r\n";
                                sLog += i + 1;
                                sLog += ". ";
                                sLog += m_oLstDoublon[i];
                            }
                        }
                        Log(sLog);
                        m_bIsError = true;
                        break;
                    case 2:
                        m_oLabelStatus.Text = "Erreur";
                        if (m_oLstBadWord.Count == 1)
                        {
                            sLog = "Les ponctuations, espaces et sauts de ligne sont interdits, corriger la clef suivante : ";
                            sLog += RibbonExpander.DoubleAntiSlash(m_oLstBadWord[0]);
                        }
                        else
                        {
                            sLog = "Les ponctuations, espaces et sauts de ligne sont interdits, ci-dessous la liste des ";
                            sLog += m_oLstBadWord.Count;
                            sLog += " premières clefs en erreur :";
                            for (int i = 0; i < m_oLstBadWord.Count; i++)
                            {
                                sLog += "\r\n";
                                sLog += i + 1;
                                sLog += ". ";
                                sLog += RibbonExpander.DoubleAntiSlash(m_oLstBadWord[i]);
                            }
                        }
                        Log(sLog);
                        m_bIsError = true;
                        break;
                }
            }
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            m_oButtonCancel.Enabled = false;
            m_bToCancel = true;
            m_oBackWorkLoadData.CancelAsync();
            if (m_bIsCompleted == true || m_bIsError == true)
            {
                m_oLabelStatus.Text = "Annulé";
                Log("Vous avez annulé les opérations");
                this.Refresh();
                Thread.Sleep(500);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void ButtonDetails_Click(object sender, EventArgs e)
        {
            m_bIsDetail = !m_bIsDetail;
            if (m_bIsDetail == true)
            {
                m_oTextBoxLog.Enabled = true;
                m_oButtonDetails.Text = "Cacher détails";
                for (int i = 0; i < 10; i++)
                {
                    m_oTextBoxLog.Height += m_nHeightTextBoxLog / 10;
                    this.Height += m_nHeightTextBoxLog / 10;
                    this.Refresh();
                }
            }
            else
            {
                m_oTextBoxLog.Enabled = false;
                m_oButtonDetails.Text = "Voir détails";
                for (int i = 0; i < 10; i++)
                {
                    m_oTextBoxLog.Height -= m_nHeightTextBoxLog / 10;
                    this.Height -= m_nHeightTextBoxLog / 10;
                    this.Refresh();
                }
            }
        }

        private void Log(String sLog)
        {
            m_oTextBoxLog.AppendText("[");
            m_oTextBoxLog.AppendText(DateTime.Now.ToString(new CultureInfo("fr-FR")));
            m_oTextBoxLog.AppendText("] ");
            m_oTextBoxLog.AppendText(sLog);
            m_oTextBoxLog.AppendText(Environment.NewLine);
        }
    }
}
