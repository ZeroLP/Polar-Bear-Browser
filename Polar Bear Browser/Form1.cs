/*
    A Light Weighted Custom Chromnium Browser
    Copyright (C) 2018  Zero

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using CefSharp.WinForms;
using CefSharp;

namespace Polar_Bear_Browser
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            Random rnd = new Random();
            int time = rnd.Next(1000, 3000);
            Thread t = new Thread(new ThreadStart(startform));
            t.Start();
            Thread.Sleep(time);
            InitializeComponent();

            t.Abort();
        }
        ChromiumWebBrowser chrome;

        public void startform()
        {
            Application.Run(new Loadform());
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(metroTextBox1.Text))
            {
                metroTextBox1.Text = "Please enter a web link";
                
            }
            else
            {
                 chrome.Load(metroTextBox1.Text);
            }
            
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            chrome.Load("https://www.google.com");
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            MetroFramework.MetroMessageBox.Show(this, "Developer: Zero\nSource status: Public(AGPL)\nDevelopment language: C#\nCopyright (C) 2018 PBB", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();
            //Init
            Cef.Initialize(settings);
            metroTextBox1.Text = "https://www.google.com";
            chrome = new ChromiumWebBrowser(metroTextBox1.Text);
            chrome.Parent = metroTabControl1.SelectedTab;
            chrome.Dock = DockStyle.Fill;
            chrome.AddressChanged += Chrome_AddressChanged;
            chrome.TitleChanged += Chrome_TitleChanged;
        }

        private void Chrome_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                metroTextBox1.Text = e.Address;
            }));
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            chrome.Refresh();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
           
                if (chrome.CanGoForward)
                    chrome.Forward();
            
                
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
                if (chrome.CanGoBack)
                chrome.Back();
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            TabPage tab = new TabPage();
            tab.Text = "New tab";
            metroTabControl1.Controls.Add(tab);
            metroTabControl1.SelectTab(metroTabControl1.TabCount - 1);
            ChromiumWebBrowser chrome = new ChromiumWebBrowser("https://www.google.com");
            chrome.Parent = tab;
            chrome.Dock = DockStyle.Fill;
            metroTextBox1.Text = "https://www.google.com";
            chrome.AddressChanged += Chrome_AddressChanged;
            chrome.TitleChanged += Chrome_TitleChanged;
        }

        private void Chrome_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                metroTabControl1.SelectedTab.Text = e.Title;
            }));
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            metroTabControl1.TabPages.Remove(metroTabControl1.SelectedTab);
            if (metroTabControl1.TabCount.Equals(0))
            {
                TabPage tab = new TabPage();
                tab.Text = "New tab";
                metroTabControl1.Controls.Add(tab);
                metroTabControl1.SelectTab(metroTabControl1.TabCount - 1);
                ChromiumWebBrowser chrome = new ChromiumWebBrowser("https://www.google.com");
                chrome.Parent = tab;
                chrome.Dock = DockStyle.Fill;
                metroTextBox1.Text = "https://www.google.com";
                chrome.AddressChanged += Chrome_AddressChanged;
                chrome.TitleChanged += Chrome_TitleChanged;

            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Information
            MetroFramework.MetroMessageBox.Show(this, "                                                              Polar Bear Browser                                                              \nDeveloper: Zero\nSource status: Private\nDevelopment language: C#\n*******************************************************\n* Copyright (C) 2017 Polar Bear Browser\n*\n* This file is part of Polar Bear Browser project.\n*\n* Polar Bear Browser can not be copied and/or distributed without the express permission of Zero\n*******************************************************", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ZeroLP/Polar-Bear-Browser");
        }
    }
}

