// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using System.Diagnostics;
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

            webBrowser1.LoadingStateChanged += WebBrowser1_LoadingStateChanged;
            webBrowser1.TitleChanged += WebBrowser1_TitleChanged;

            btnBack.Enabled = false;
            btnForward.Enabled = false;
        }

        private void WebBrowser1_TitleChanged(object sender, CefSharp.TitleChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke( new Action( () => { 
                                                 this.Title = e.Title;
                                                 ContentChanged(this);
                                                } ));
            }
        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs args)
        {
            txtUrl.Text = args.Url.ToString();
        }

        void webBrowser1_CanGoForwardChanged(object sender, EventArgs e)
        {
        }

        void webBrowser1_CanGoBackChanged(object sender, EventArgs e)
        {
            btnBack.Enabled = webBrowser1.CanGoBack;
        }

        private void WebBrowser1_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => {
                    btnForward.Enabled = e.CanGoForward;
                    btnBack.Enabled = e.CanGoBack;
                }));
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

        public ResourceBrowserView(IResourceBrowserController controller) :
            this()
        {
            Controller = controller;
        }

        #region IResourceBrowserView

        public void GotoResource(string resourceUriString)
        {
            webBrowser1.LoadUrl(resourceUriString);
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

        public string Title { get; private set; }

        public event Action<object> ContentChanged;
        public event Action<object> ContentSaved { add { } remove { } }
        public event Action<object> ContentLoaded { add { } remove { } }
        #endregion

        public void Back()
        {
            webBrowser1.BrowserCore.GoBack();
        }

        public void Forward()
        {
            webBrowser1.BrowserCore.GoForward();
        }

        public void ReloadPage()
        {
            webBrowser1.BrowserCore.Reload();
        }

        public void OpenCurrentPageInBrowser()
        {
            Process.Start(webBrowser1.Address);
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

        private void chromiumWebBrowser1_TextChanged(object sender, EventArgs e)
        {
            if (ContentChanged != null)
            {
                ContentChanged(this);
            }
        }
    }
}
