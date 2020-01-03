using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Input;

namespace Expander
{
    public partial class ThisDocument
    {
        public AutomationElement m_oPanelDoc = null;

        private void ThisDocument_Startup(object sender, System.EventArgs e)
        {
            Automation.AddAutomationEventHandler(WindowPattern.WindowOpenedEvent, AutomationElement.RootElement, TreeScope.Children, OnWindowOpened);
        }

        private void OnWindowOpened(object sender, AutomationEventArgs automationEventArgs)
        {
            AutomationElement oWindow = AutomationElement.FromHandle(new IntPtr(Globals.ThisDocument.Application.ActiveWindow.Hwnd));
            m_oPanelDoc = oWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Document));

            if(m_oPanelDoc != null)
            {
                Automation.RemoveAutomationEventHandler(WindowPattern.WindowOpenedEvent, AutomationElement.RootElement, OnWindowOpened);
            }
        }

        private void ThisDocument_Shutdown(object sender, System.EventArgs e)
        {

        }

        #region Code généré par le Concepteur VSTO

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisDocument_Startup);
            this.Shutdown += new System.EventHandler(ThisDocument_Shutdown);
        }

        #endregion
    }
}
