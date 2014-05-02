namespace HazeronMapper
{
    partial class selectSystemPrompt
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
            this.dataGridView_systems = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_ok = new System.Windows.Forms.Button();
            this.numericUpDown_depth = new System.Windows.Forms.NumericUpDown();
            this.label_depth = new System.Windows.Forms.Label();
            this.checkBox_resetloc = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_systems)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_depth)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_systems
            // 
            this.dataGridView_systems.AllowUserToAddRows = false;
            this.dataGridView_systems.AllowUserToDeleteRows = false;
            this.dataGridView_systems.AllowUserToOrderColumns = true;
            this.dataGridView_systems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView_systems, 4);
            this.dataGridView_systems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_systems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_systems.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_systems.MultiSelect = false;
            this.dataGridView_systems.Name = "dataGridView_systems";
            this.dataGridView_systems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_systems.Size = new System.Drawing.Size(410, 277);
            this.dataGridView_systems.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_systems, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_ok, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown_depth, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_depth, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_resetloc, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 321);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.Location = new System.Drawing.Point(3, 290);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(74, 23);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // numericUpDown_depth
            // 
            this.numericUpDown_depth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_depth.Location = new System.Drawing.Point(282, 291);
            this.numericUpDown_depth.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.numericUpDown_depth.Name = "numericUpDown_depth";
            this.numericUpDown_depth.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown_depth.TabIndex = 2;
            this.numericUpDown_depth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label_depth
            // 
            this.label_depth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_depth.AutoSize = true;
            this.label_depth.Location = new System.Drawing.Point(240, 295);
            this.label_depth.Margin = new System.Windows.Forms.Padding(3, 12, 0, 0);
            this.label_depth.Name = "label_depth";
            this.label_depth.Size = new System.Drawing.Size(39, 13);
            this.label_depth.TabIndex = 3;
            this.label_depth.Text = "Depth:";
            // 
            // checkBox_resetloc
            // 
            this.checkBox_resetloc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_resetloc.AutoSize = true;
            this.checkBox_resetloc.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_resetloc.Location = new System.Drawing.Point(338, 286);
            this.checkBox_resetloc.Name = "checkBox_resetloc";
            this.checkBox_resetloc.Size = new System.Drawing.Size(75, 32);
            this.checkBox_resetloc.TabIndex = 5;
            this.checkBox_resetloc.Text = "Reset Loc";
            this.checkBox_resetloc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_resetloc.UseVisualStyleBackColor = true;
            // 
            // selectSystemPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 321);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "selectSystemPrompt";
            this.Text = "Select System";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_systems)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_depth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_systems;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.NumericUpDown numericUpDown_depth;
        private System.Windows.Forms.Label label_depth;
        private System.Windows.Forms.CheckBox checkBox_resetloc;
    }
}