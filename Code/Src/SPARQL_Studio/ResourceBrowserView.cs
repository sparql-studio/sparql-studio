using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dk.ModularSystems.Sparql.Contracts;

namespace dk.ModularSystems.Sparql
{
    public partial class ResourceBrowserView : UserControl, IResourceBrowserView
    {
        // public IResourceBrowserController Controller { get; set; }
        public IController Controller { get; set; }

        public ResourceBrowserView()
        {
            InitializeComponent();

            webBrowser1.DocumentTitleChanged += webBrowser1_DocumentTitleChanged;
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            webBrowser1.CanGoBackChanged += webBrowser1_CanGoBackChanged;
            webBrowser1.CanGoForwardChanged += webBrowser1_CanGoForwardChanged;
            webBrowser1.Navigated += webBrowser1_Navigated;

            btnBack.Enabled = false;
            btnForward.Enabled = false;
        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs args)
        {
            txtUrl.Text = args.Url.ToString();
        }

        void webBrowser1_CanGoForwardChanged(object sender, EventArgs e)
        {
            btnForward.Enabled = webBrowser1.CanGoForward;
        }

        void webBrowser1_CanGoBackChanged(object sender, EventArgs e)
        {
            btnBack.Enabled = webBrowser1.CanGoBack;
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.MouseOver += Document_MouseOver;
            }
        }

        void Document_MouseOver(object sender, HtmlElementEventArgs e)
        {
            if (e.ToElement != null)
            {
                String link = e.ToElement.GetAttribute("href");
                if (String.IsNullOrEmpty(link))
                {
                    link = "";
                    // ToolTip tt = new ToolTip(e.ToElement);
                }
                else
                {
                    link = link.ToLower();
                }
                lbLink.Text = link;
            }
        }

        void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            if (ContentChanged != null)
            {
                ContentChanged(this);
            }
        }

        public ResourceBrowserView(IResourceBrowserController controller) :
            this()
        {
            Controller = controller;
        }

        #region IResourceBrowserView

        public void GotoResource(string resourceUriString)
        {
            webBrowser1.Navigate(resourceUriString);
            if (ContentChanged != null)
            {
                ContentChanged(this);
            }
        }

        #endregion

        #region IView

        public Boolean CanClose()
        {
            return true;
        }

        public bool IsDirty
        {
            get { return false; }
        }

        public bool IsUntitled
        {
            get { return false; }
        }

        public string Title
        {
            get
            {
                if (!String.IsNullOrEmpty(webBrowser1.DocumentTitle))
                {
                    return webBrowser1.DocumentTitle;
                }
                return (webBrowser1.Url != null)
                    ? webBrowser1.Url.ToString()
                    : "<>";

            }
        }

        public event Action<object> ContentChanged;
        public event Action<object> ContentSaved { add { } remove { } }
        public event Action<object> ContentLoaded { add { } remove { } }
        #endregion

        public void Back()
        {
            webBrowser1.GoBack();
        }

        public void Forward()
        {
            webBrowser1.GoForward();
        }

        public void ReloadPage()
        {
            webBrowser1.Refresh();
        }

        public void OpenCurrentPageInBrowser()
        {
            Process.Start(webBrowser1.Url.ToString());
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Back();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            this.Forward();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.ReloadPage();
        }

        private void btnOpenInBrowser_Click(object sender, EventArgs e)
        {
            OpenCurrentPageInBrowser();
        }


    }
}
