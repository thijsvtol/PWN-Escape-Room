namespace pwnUI
{
    partial class scoreUI
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
            this.dataGridViewScore = new System.Windows.Forms.DataGridView();
            this.teamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speeldatum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speeltijd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.straftijd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totaalTijd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScore)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewScore
            // 
            this.dataGridViewScore.AllowUserToAddRows = false;
            this.dataGridViewScore.AllowUserToDeleteRows = false;
            this.dataGridViewScore.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewScore.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewScore.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewScore.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.teamName,
            this.speeldatum,
            this.speeltijd,
            this.straftijd,
            this.totaalTijd});
            this.dataGridViewScore.Cursor = System.Windows.Forms.Cursors.No;
            this.dataGridViewScore.Location = new System.Drawing.Point(16, 90);
            this.dataGridViewScore.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewScore.Name = "dataGridViewScore";
            this.dataGridViewScore.ReadOnly = true;
            this.dataGridViewScore.RowHeadersVisible = false;
            this.dataGridViewScore.Size = new System.Drawing.Size(606, 362);
            this.dataGridViewScore.TabIndex = 4;
            // 
            // teamName
            // 
            this.teamName.HeaderText = "Team";
            this.teamName.Name = "teamName";
            this.teamName.ReadOnly = true;
            this.teamName.Width = 200;
            // 
            // speeldatum
            // 
            this.speeldatum.HeaderText = "Speeldatum";
            this.speeldatum.Name = "speeldatum";
            this.speeldatum.ReadOnly = true;
            // 
            // speeltijd
            // 
            this.speeltijd.HeaderText = "Speeltijd";
            this.speeltijd.Name = "speeltijd";
            this.speeltijd.ReadOnly = true;
            // 
            // straftijd
            // 
            this.straftijd.HeaderText = "Straftijd";
            this.straftijd.Name = "straftijd";
            this.straftijd.ReadOnly = true;
            // 
            // totaalTijd
            // 
            this.totaalTijd.HeaderText = "Tijd over";
            this.totaalTijd.Name = "totaalTijd";
            this.totaalTijd.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(481, 7);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "Terug naar begin";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 39);
            this.label1.TabIndex = 5;
            this.label1.Text = "Scorebord";
            // 
            // scoreUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 460);
            this.Controls.Add(this.dataGridViewScore);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "scoreUI";
            this.Text = "scoreUI";
            this.Load += new System.EventHandler(this.scoreUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn teamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn speeldatum;
        private System.Windows.Forms.DataGridViewTextBoxColumn speeltijd;
        private System.Windows.Forms.DataGridViewTextBoxColumn straftijd;
        private System.Windows.Forms.DataGridViewTextBoxColumn totaalTijd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}