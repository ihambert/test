﻿using System;
using System.Windows.Forms;
using Common;

namespace GradingFund
{
    internal static class Program
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Logger().Register();
            Application.Run(new Form1());
        }
    }
}