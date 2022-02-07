namespace OrderManager.WinFormsUI;

partial class NewCompanyForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CompanyNameBox = new System.Windows.Forms.TextBox();
            this.ContactNameBox = new System.Windows.Forms.TextBox();
            this.Address1Box = new System.Windows.Forms.TextBox();
            this.Address2Box = new System.Windows.Forms.TextBox();
            this.Address3Box = new System.Windows.Forms.TextBox();
            this.CityBox = new System.Windows.Forms.TextBox();
            this.StateBox = new System.Windows.Forms.TextBox();
            this.ZipBox = new System.Windows.Forms.TextBox();
            this.CreateBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contact";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "City";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "State";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Zip";
            // 
            // CompanyNameBox
            // 
            this.CompanyNameBox.Location = new System.Drawing.Point(12, 33);
            this.CompanyNameBox.Name = "CompanyNameBox";
            this.CompanyNameBox.Size = new System.Drawing.Size(220, 27);
            this.CompanyNameBox.TabIndex = 6;
            // 
            // ContactNameBox
            // 
            this.ContactNameBox.Location = new System.Drawing.Point(12, 86);
            this.ContactNameBox.Name = "ContactNameBox";
            this.ContactNameBox.Size = new System.Drawing.Size(220, 27);
            this.ContactNameBox.TabIndex = 7;
            // 
            // Address1Box
            // 
            this.Address1Box.Location = new System.Drawing.Point(12, 139);
            this.Address1Box.Name = "Address1Box";
            this.Address1Box.Size = new System.Drawing.Size(220, 27);
            this.Address1Box.TabIndex = 8;
            // 
            // Address2Box
            // 
            this.Address2Box.Location = new System.Drawing.Point(12, 172);
            this.Address2Box.Name = "Address2Box";
            this.Address2Box.Size = new System.Drawing.Size(220, 27);
            this.Address2Box.TabIndex = 12;
            // 
            // Address3Box
            // 
            this.Address3Box.Location = new System.Drawing.Point(12, 205);
            this.Address3Box.Name = "Address3Box";
            this.Address3Box.Size = new System.Drawing.Size(220, 27);
            this.Address3Box.TabIndex = 13;
            // 
            // CityBox
            // 
            this.CityBox.Location = new System.Drawing.Point(61, 238);
            this.CityBox.Name = "CityBox";
            this.CityBox.Size = new System.Drawing.Size(171, 27);
            this.CityBox.TabIndex = 14;
            // 
            // StateBox
            // 
            this.StateBox.Location = new System.Drawing.Point(61, 271);
            this.StateBox.Name = "StateBox";
            this.StateBox.Size = new System.Drawing.Size(171, 27);
            this.StateBox.TabIndex = 15;
            // 
            // ZipBox
            // 
            this.ZipBox.Location = new System.Drawing.Point(61, 304);
            this.ZipBox.Name = "ZipBox";
            this.ZipBox.Size = new System.Drawing.Size(171, 27);
            this.ZipBox.TabIndex = 16;
            // 
            // CreateBtn
            // 
            this.CreateBtn.Location = new System.Drawing.Point(61, 377);
            this.CreateBtn.Name = "CreateBtn";
            this.CreateBtn.Size = new System.Drawing.Size(94, 29);
            this.CreateBtn.TabIndex = 17;
            this.CreateBtn.Text = "Create";
            this.CreateBtn.UseVisualStyleBackColor = true;
            this.CreateBtn.Click += new System.EventHandler(this.CreateBtn_Click);
            // 
            // Companies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 418);
            this.Controls.Add(this.CreateBtn);
            this.Controls.Add(this.ZipBox);
            this.Controls.Add(this.StateBox);
            this.Controls.Add(this.CityBox);
            this.Controls.Add(this.Address3Box);
            this.Controls.Add(this.Address2Box);
            this.Controls.Add(this.Address1Box);
            this.Controls.Add(this.ContactNameBox);
            this.Controls.Add(this.CompanyNameBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Companies";
            this.Text = "New Company";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private TextBox CompanyNameBox;
    private TextBox ContactNameBox;
    private TextBox Address1Box;
    private TextBox Address2Box;
    private TextBox Address3Box;
    private TextBox CityBox;
    private TextBox StateBox;
    private TextBox ZipBox;
    private Button CreateBtn;
}
