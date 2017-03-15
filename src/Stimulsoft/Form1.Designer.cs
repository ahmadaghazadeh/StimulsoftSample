namespace Stimulsoft
{
    partial class Form1
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
            this.stiReportDataSource2 = new Stimulsoft.Report.Design.StiReportDataSource("Form1", this);
            this.stiReportDataSource1 = new Stimulsoft.Report.Design.StiReportDataSource("Form1", this);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stiReportDataSource2
            // 
            this.stiReportDataSource2.Item = this;
            this.stiReportDataSource2.Name = "Form1";
            // 
            // stiReportDataSource1
            // 
            this.stiReportDataSource1.Item = this;
            this.stiReportDataSource1.Name = "Form1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Simple Reoprt";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private Report.Design.StiReportDataSource stiReportDataSource1;
        private System.Windows.Forms.Button button1;
        private Report.Design.StiReportDataSource stiReportDataSource2;
    }
}

