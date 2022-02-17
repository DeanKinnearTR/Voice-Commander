using System.Windows.Forms;
using VoiceCommander.Resources.Wav;

namespace VoiceCommander
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private bool showForm = false;
        private bool exiting = false;

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(showForm ? value : showForm);
        }

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
        //this.FormClosing += MainForm_FormClosing;
        //this.ShowDialog.Click += ShowDialog_Click;
        //this.ExitApplication.Click += ExitApplication_Click;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowDialog = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator = new System.Windows.Forms.ToolStripSeparator();
            this.ExitApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.GridMain = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.ContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridMain)).BeginInit();
            this.SuspendLayout();
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.ContextMenuStrip;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "Voice Commander";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.DoubleClick += new System.EventHandler(this.NotifyIcon_DoubleClick);
            this.NotifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDown);
            // 
            // ContextMenuStrip
            // 
            this.ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowDialog,
            this.Separator,
            this.ExitApplication});
            this.ContextMenuStrip.Name = "ContextMenuStrip";
            this.ContextMenuStrip.Size = new System.Drawing.Size(95, 54);
            // 
            // ShowDialog
            // 
            this.ShowDialog.Name = "ShowDialog";
            this.ShowDialog.Size = new System.Drawing.Size(94, 22);
            this.ShowDialog.Text = "Edit";
            this.ShowDialog.Click += new System.EventHandler(this.ShowDialog_Click);
            // 
            // Separator
            // 
            this.Separator.Name = "Separator";
            this.Separator.Size = new System.Drawing.Size(91, 6);
            // 
            // ExitApplication
            // 
            this.ExitApplication.Name = "ExitApplication";
            this.ExitApplication.Size = new System.Drawing.Size(94, 22);
            this.ExitApplication.Text = "Exit";
            this.ExitApplication.Click += new System.EventHandler(this.ExitApplication_Click);
            // 
            // GridMain
            // 
            this.GridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.GridMain.Location = new System.Drawing.Point(0, 0);
            this.GridMain.Name = "GridMain";
            this.GridMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridMain.Size = new System.Drawing.Size(751, 519);
            this.GridMain.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(801, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.TemporaryHack);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GridMain);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridMain)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void NotifyIcon_DoubleClick(object sender, System.EventArgs e)
        {
            //ShowForm();
            TemporaryHack(null, null);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exiting) return;
            e.Cancel = true;
            showForm = false;
            SetVisibleCore(false);
            _controller.Dispose();
        }

        private void ShowDialog_Click(object sender, System.EventArgs e)
        {
            //ShowForm();
        }

        private void ShowForm()
        {
            //showForm = true;
            //SetVisibleCore(true);
        }

        private void ShutdowmCommander()
        {
            Audio.PlaySound("holodeck_end_program.wav", false);
            exiting = true;
            Application.Exit();
        }

        private void ExitApplication_Click(object sender, System.EventArgs e)
        {
            ShutdowmCommander();
        }

        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ShowDialog;
        private System.Windows.Forms.ToolStripMenuItem ExitApplication;
        private ToolStripSeparator Separator;
        private DataGridView GridMain;
        private Button button1;
    }
}