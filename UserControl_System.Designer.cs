namespace HazeronMapper
{
    partial class UserControl_System
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl_System));
            this.label_SysName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_sysnotes = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_SysName
            // 
            this.label_SysName.AutoEllipsis = true;
            this.label_SysName.AutoSize = true;
            this.label_SysName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_SysName.ForeColor = System.Drawing.Color.Yellow;
            this.label_SysName.Location = new System.Drawing.Point(23, 0);
            this.label_SysName.Name = "label_SysName";
            this.label_SysName.Size = new System.Drawing.Size(102, 20);
            this.label_SysName.TabIndex = 0;
            this.label_SysName.Text = "Sysname";
            this.label_SysName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_SysName.Click += new System.EventHandler(this.label_SysName_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label_SysName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_sysnotes, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(128, 40);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(14, 14);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseDown);
            this.button1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseMove);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseUp);
            // 
            // textBox_sysnotes
            // 
            this.textBox_sysnotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_sysnotes.BackColor = System.Drawing.Color.Black;
            this.textBox_sysnotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.textBox_sysnotes, 2);
            this.textBox_sysnotes.ForeColor = System.Drawing.Color.White;
            this.textBox_sysnotes.Location = new System.Drawing.Point(0, 20);
            this.textBox_sysnotes.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_sysnotes.Multiline = true;
            this.textBox_sysnotes.Name = "textBox_sysnotes";
            this.textBox_sysnotes.Size = new System.Drawing.Size(128, 20);
            this.textBox_sysnotes.TabIndex = 2;
            this.textBox_sysnotes.Visible = false;
            this.textBox_sysnotes.TextChanged += new System.EventHandler(this.textBox_sysnotes_TextChanged);
            // 
            // UserControl_System
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinimumSize = new System.Drawing.Size(128, 40);
            this.Name = "UserControl_System";
            this.Size = new System.Drawing.Size(128, 40);
            this.DoubleClick += new System.EventHandler(this.UserControl_System_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseUp);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_SysName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_sysnotes;
    }
}
