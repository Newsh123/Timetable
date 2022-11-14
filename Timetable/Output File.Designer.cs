
namespace Timetable
{
    partial class OutputFile
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
            this.download = new System.Windows.Forms.Button();
            this.Type = new System.Windows.Forms.ComboBox();
            this.FirstName = new System.Windows.Forms.TextBox();
            this.LastName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SearchPerson = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.backButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.SearchLesson = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.LessonCode = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // download
            // 
            this.download.BackColor = System.Drawing.Color.CornflowerBlue;
            this.download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.download.Location = new System.Drawing.Point(266, 17);
            this.download.Name = "download";
            this.download.Size = new System.Drawing.Size(156, 75);
            this.download.TabIndex = 0;
            this.download.Text = "Download Timetable";
            this.download.UseVisualStyleBackColor = false;
            this.download.Click += new System.EventHandler(this.download_Click);
            // 
            // Type
            // 
            this.Type.FormattingEnabled = true;
            this.Type.Items.AddRange(new object[] {
            "Teacher",
            "Student"});
            this.Type.Location = new System.Drawing.Point(119, 149);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(121, 21);
            this.Type.TabIndex = 1;
            // 
            // FirstName
            // 
            this.FirstName.Location = new System.Drawing.Point(119, 51);
            this.FirstName.Name = "FirstName";
            this.FirstName.Size = new System.Drawing.Size(121, 20);
            this.FirstName.TabIndex = 2;
            // 
            // LastName
            // 
            this.LastName.Location = new System.Drawing.Point(119, 98);
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(121, 20);
            this.LastName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "First Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Teacher or Student";
            // 
            // SearchPerson
            // 
            this.SearchPerson.BackColor = System.Drawing.Color.CornflowerBlue;
            this.SearchPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchPerson.Location = new System.Drawing.Point(119, 176);
            this.SearchPerson.Name = "SearchPerson";
            this.SearchPerson.Size = new System.Drawing.Size(121, 63);
            this.SearchPerson.TabIndex = 7;
            this.SearchPerson.Text = "Search for Person";
            this.SearchPerson.UseVisualStyleBackColor = false;
            this.SearchPerson.Click += new System.EventHandler(this.SearchPerson_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.download);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 414);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 123);
            this.panel1.TabIndex = 8;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panelTop.Controls.Add(this.backButton);
            this.panelTop.Controls.Add(this.label8);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(704, 94);
            this.panelTop.TabIndex = 26;
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.LightSteelBlue;
            this.backButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Location = new System.Drawing.Point(0, 0);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(116, 94);
            this.backButton.TabIndex = 2;
            this.backButton.Text = "Go Home";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(156, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(364, 54);
            this.label8.TabIndex = 0;
            this.label8.Text = "Timetable Creator";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.SearchPerson);
            this.panel2.Controls.Add(this.Type);
            this.panel2.Controls.Add(this.FirstName);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.LastName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(60, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(257, 258);
            this.panel2.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(47, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Search for a person";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 94);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(704, 62);
            this.panel3.TabIndex = 28;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 156);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(60, 258);
            this.panel4.TabIndex = 29;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(643, 156);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(61, 258);
            this.panel5.TabIndex = 30;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.SearchLesson);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.LessonCode);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(389, 156);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(254, 258);
            this.panel6.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(50, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 23);
            this.label6.TabIndex = 9;
            this.label6.Text = "Search for a lesson";
            // 
            // SearchLesson
            // 
            this.SearchLesson.BackColor = System.Drawing.Color.CornflowerBlue;
            this.SearchLesson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchLesson.Location = new System.Drawing.Point(96, 176);
            this.SearchLesson.Name = "SearchLesson";
            this.SearchLesson.Size = new System.Drawing.Size(121, 63);
            this.SearchLesson.TabIndex = 8;
            this.SearchLesson.Text = "Search for Person";
            this.SearchLesson.UseVisualStyleBackColor = false;
            this.SearchLesson.Click += new System.EventHandler(this.SearchLesson_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Lesson Code";
            // 
            // LessonCode
            // 
            this.LessonCode.Location = new System.Drawing.Point(96, 109);
            this.LessonCode.Name = "LessonCode";
            this.LessonCode.Size = new System.Drawing.Size(121, 20);
            this.LessonCode.TabIndex = 3;
            // 
            // OutputFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 537);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTop);
            this.Name = "OutputFile";
            this.Text = "Timetable Creator";
            this.Load += new System.EventHandler(this.Output_File_Load);
            this.panel1.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button download;
        private System.Windows.Forms.ComboBox Type;
        private System.Windows.Forms.TextBox FirstName;
        private System.Windows.Forms.TextBox LastName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SearchPerson;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button SearchLesson;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LessonCode;
        private System.Windows.Forms.Button backButton;
    }
}