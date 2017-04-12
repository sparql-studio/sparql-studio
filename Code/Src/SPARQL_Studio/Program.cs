// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using dk.ModularSystems.Sparql.Contracts;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace dk.ModularSystems.Sparql
{
    static class Program
    {
        static IAppController   _appController;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _appController = new AppController();
            AppView appView = new AppView(_appController);
            _appController.Initialize(args);

            Application.ThreadException += Application_ThreadException;
            Application.Run(appView);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Debug.Assert(_appController != null);
            _appController.OnException(e.Exception);
        }
    }
}
