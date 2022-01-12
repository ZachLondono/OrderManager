namespace WinFormsUI;

partial class Products {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
            this.createProductBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nameInput = new System.Windows.Forms.TextBox();
            this.descriptionInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.attribute1Input = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.attribute2Input = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // createProductBtn
            // 
            this.createProductBtn.Location = new System.Drawing.Point(12, 284);
            this.createProductBtn.Name = "createProductBtn";
            this.createProductBtn.Size = new System.Drawing.Size(229, 23);
            this.createProductBtn.TabIndex = 0;
            this.createProductBtn.Text = "Create Product";
            this.createProductBtn.UseVisualStyleBackColor = true;
            this.createProductBtn.Click += new System.EventHandler(this.CreateProductBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // nameInput
            // 
            this.nameInput.Location = new System.Drawing.Point(12, 54);
            this.nameInput.Name = "nameInput";
            this.nameInput.Size = new System.Drawing.Size(229, 23);
            this.nameInput.TabIndex = 2;
            // 
            // descriptionInput
            // 
            this.descriptionInput.Location = new System.Drawing.Point(12, 109);
            this.descriptionInput.Name = "descriptionInput";
            this.descriptionInput.Size = new System.Drawing.Size(229, 23);
            this.descriptionInput.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Description";
            // 
            // attribute1Input
            // 
            this.attribute1Input.Location = new System.Drawing.Point(12, 169);
            this.attribute1Input.Name = "attribute1Input";
            this.attribute1Input.Size = new System.Drawing.Size(229, 23);
            this.attribute1Input.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Attribute 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Attribute 2";
            // 
            // attribute2Input
            // 
            this.attribute2Input.Location = new System.Drawing.Point(12, 227);
            this.attribute2Input.Name = "attribute2Input";
            this.attribute2Input.Size = new System.Drawing.Size(229, 23);
            this.attribute2Input.TabIndex = 7;
            // 
            // Products
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 315);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.attribute2Input);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.attribute1Input);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.descriptionInput);
            this.Controls.Add(this.nameInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createProductBtn);
            this.Name = "Products";
            this.Text = "Products";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Button createProductBtn;
    private Label label1;
    private TextBox nameInput;
    private TextBox descriptionInput;
    private Label label2;
    private TextBox attribute1Input;
    private Label label3;
    private Label label4;
    private TextBox attribute2Input;
}
