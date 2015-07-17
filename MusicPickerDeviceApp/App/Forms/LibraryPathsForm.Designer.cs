namespace MusicPickerDeviceApp
{
    partial class LibraryPathsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LibraryPathsForm));
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.folder = new System.Windows.Forms.LinkLabel();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.foldersLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Box = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folder
            // 
            this.folder.AutoSize = true;
            this.folder.Location = new System.Drawing.Point(16, 17);
            this.folder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.folder.Name = "folder";
            this.folder.Size = new System.Drawing.Size(75, 13);
            this.folder.TabIndex = 0;
            this.folder.TabStop = true;
            this.folder.Text = "Select a folder";
            this.folder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonLoad.Location = new System.Drawing.Point(280, 12);
            this.buttonLoad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(56, 19);
            this.buttonLoad.TabIndex = 1;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click_1);
            // 
            // foldersLabel
            // 
            this.foldersLabel.AutoSize = true;
            this.foldersLabel.Location = new System.Drawing.Point(2, 0);
            this.foldersLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.foldersLabel.Name = "foldersLabel";
            this.foldersLabel.Size = new System.Drawing.Size(0, 13);
            this.foldersLabel.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.foldersLabel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(18, 42);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(319, 81);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // Box
            // 
            this.Box.FormattingEnabled = true;
            this.Box.HorizontalScrollbar = true;
            this.Box.Location = new System.Drawing.Point(18, 145);
            this.Box.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Box.Name = "Box";
            this.Box.Size = new System.Drawing.Size(320, 108);
            this.Box.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(280, 275);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 24);
            this.button1.TabIndex = 5;
            this.button1.Text = "Delete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LibraryPathsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(346, 301);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Box);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.folder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LibraryPathsForm";
            this.Text = "Musicpicker - Music paths";
            this.Load += new System.EventHandler(this.LibraryPathsForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.LinkLabel folder;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Label foldersLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ListBox Box;
        private System.Windows.Forms.Button button1;
    }
}