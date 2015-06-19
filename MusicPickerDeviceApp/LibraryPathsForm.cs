﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicPickerDeviceApp.App;

namespace MusicPickerDeviceApp
{
    public partial class LibraryPathsForm : Form
    {
        public delegate void LibraryPathsEvent(List<string> paths);
        private LibraryPathsEvent callback;
        private ConfigurationModel configuration;

        public LibraryPathsForm(ConfigurationModel configuration, LibraryPathsEvent callback)
        {
            this.callback = callback;
            this.configuration = configuration;
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                configuration.Paths.Add(folderBrowser.SelectedPath);
                foldersLabel.Text += string.Format("- {0} \n", folderBrowser.SelectedPath);
            }
        }

        private void buttonLoad_Click_1(object sender, EventArgs e)
        {
            callback(configuration.Paths);
            this.Close();
        }

        private void LibraryPathsForm_Load(object sender, EventArgs e)
        {
            foldersLabel.Text = "";
            this.Box.Items.Clear();
            foreach (var path in configuration.Paths)
            {
                this.Box.Items.Add(new TextBox()
                {
                    Text = string.Format(path)
                }.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var path = this.Box.SelectedItem;
            configuration.Paths.Remove((string) path);

            callback(configuration.Paths);
            Box.Items.Remove(path);
        }
    }
}
