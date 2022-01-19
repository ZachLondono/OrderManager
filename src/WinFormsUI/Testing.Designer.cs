namespace OrderManager.WinFormsUI;

partial class Testing {
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
            this.button1 = new System.Windows.Forms.Button();
            this.ScriptList = new System.Windows.Forms.ListView();
            this.button2 = new System.Windows.Forms.Button();
            this.ScriptParameter = new System.Windows.Forms.TextBox();
            this.GenerateReportBtn = new System.Windows.Forms.Button();
            this.CompanyList = new System.Windows.Forms.ListBox();
            this.LoadCompaniesBtn = new System.Windows.Forms.Button();
            this.CreateCompanyBtn = new System.Windows.Forms.Button();
            this.CompanyNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ProductList = new System.Windows.Forms.ListBox();
            this.LoadProductsBtn = new System.Windows.Forms.Button();
            this.ProductNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CreateProductBtn = new System.Windows.Forms.Button();
            this.AttributeList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RptPDFBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(397, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load Scripts";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoadScriptsFromDirectory);
            // 
            // ScriptList
            // 
            this.ScriptList.Location = new System.Drawing.Point(11, 65);
            this.ScriptList.Name = "ScriptList";
            this.ScriptList.Size = new System.Drawing.Size(396, 97);
            this.ScriptList.TabIndex = 1;
            this.ScriptList.UseCompatibleStateImageBehavior = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(14, 171);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(394, 45);
            this.button2.TabIndex = 2;
            this.button2.Text = "Run Selected Script";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ExecuteSelectedScript);
            // 
            // ScriptParameter
            // 
            this.ScriptParameter.Location = new System.Drawing.Point(11, 224);
            this.ScriptParameter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ScriptParameter.Name = "ScriptParameter";
            this.ScriptParameter.Size = new System.Drawing.Size(394, 27);
            this.ScriptParameter.TabIndex = 3;
            // 
            // GenerateReportBtn
            // 
            this.GenerateReportBtn.Location = new System.Drawing.Point(11, 263);
            this.GenerateReportBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GenerateReportBtn.Name = "GenerateReportBtn";
            this.GenerateReportBtn.Size = new System.Drawing.Size(394, 45);
            this.GenerateReportBtn.TabIndex = 4;
            this.GenerateReportBtn.Text = "Generate Report";
            this.GenerateReportBtn.UseVisualStyleBackColor = true;
            this.GenerateReportBtn.Click += new System.EventHandler(this.GenerateReportBtn_Click);
            // 
            // CompanyList
            // 
            this.CompanyList.FormattingEnabled = true;
            this.CompanyList.ItemHeight = 20;
            this.CompanyList.Location = new System.Drawing.Point(11, 503);
            this.CompanyList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CompanyList.Name = "CompanyList";
            this.CompanyList.Size = new System.Drawing.Size(389, 64);
            this.CompanyList.TabIndex = 5;
            // 
            // LoadCompaniesBtn
            // 
            this.LoadCompaniesBtn.Location = new System.Drawing.Point(11, 443);
            this.LoadCompaniesBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LoadCompaniesBtn.Name = "LoadCompaniesBtn";
            this.LoadCompaniesBtn.Size = new System.Drawing.Size(390, 52);
            this.LoadCompaniesBtn.TabIndex = 6;
            this.LoadCompaniesBtn.Text = "LoadCompanies";
            this.LoadCompaniesBtn.UseVisualStyleBackColor = true;
            this.LoadCompaniesBtn.Click += new System.EventHandler(this.LoadCompaniesBtn_Click);
            // 
            // CreateCompanyBtn
            // 
            this.CreateCompanyBtn.Location = new System.Drawing.Point(14, 316);
            this.CreateCompanyBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CreateCompanyBtn.Name = "CreateCompanyBtn";
            this.CreateCompanyBtn.Size = new System.Drawing.Size(390, 52);
            this.CreateCompanyBtn.TabIndex = 7;
            this.CreateCompanyBtn.Text = "Create Company";
            this.CreateCompanyBtn.UseVisualStyleBackColor = true;
            this.CreateCompanyBtn.Click += new System.EventHandler(this.CreateCompanyBtn_Click);
            // 
            // CompanyNameTextBox
            // 
            this.CompanyNameTextBox.Location = new System.Drawing.Point(11, 404);
            this.CompanyNameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CompanyNameTextBox.Name = "CompanyNameTextBox";
            this.CompanyNameTextBox.Size = new System.Drawing.Size(389, 27);
            this.CompanyNameTextBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 380);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Company Name";
            // 
            // ProductList
            // 
            this.ProductList.FormattingEnabled = true;
            this.ProductList.ItemHeight = 20;
            this.ProductList.Location = new System.Drawing.Point(11, 789);
            this.ProductList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ProductList.Name = "ProductList";
            this.ProductList.Size = new System.Drawing.Size(389, 64);
            this.ProductList.TabIndex = 10;
            this.ProductList.SelectedIndexChanged += new System.EventHandler(this.ProductList_SelectedIndexChanged);
            // 
            // LoadProductsBtn
            // 
            this.LoadProductsBtn.Location = new System.Drawing.Point(11, 711);
            this.LoadProductsBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LoadProductsBtn.Name = "LoadProductsBtn";
            this.LoadProductsBtn.Size = new System.Drawing.Size(390, 52);
            this.LoadProductsBtn.TabIndex = 11;
            this.LoadProductsBtn.Text = "Load Products";
            this.LoadProductsBtn.UseVisualStyleBackColor = true;
            this.LoadProductsBtn.Click += new System.EventHandler(this.LoadProductsBtn_Click);
            // 
            // ProductNameTextBox
            // 
            this.ProductNameTextBox.Location = new System.Drawing.Point(11, 672);
            this.ProductNameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ProductNameTextBox.Name = "ProductNameTextBox";
            this.ProductNameTextBox.Size = new System.Drawing.Size(389, 27);
            this.ProductNameTextBox.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 648);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Product Name";
            // 
            // CreateProductBtn
            // 
            this.CreateProductBtn.Location = new System.Drawing.Point(11, 592);
            this.CreateProductBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CreateProductBtn.Name = "CreateProductBtn";
            this.CreateProductBtn.Size = new System.Drawing.Size(390, 52);
            this.CreateProductBtn.TabIndex = 14;
            this.CreateProductBtn.Text = "Create Product";
            this.CreateProductBtn.UseVisualStyleBackColor = true;
            this.CreateProductBtn.Click += new System.EventHandler(this.CreateProductBtn_Click);
            // 
            // AttributeList
            // 
            this.AttributeList.FormattingEnabled = true;
            this.AttributeList.ItemHeight = 20;
            this.AttributeList.Location = new System.Drawing.Point(11, 883);
            this.AttributeList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AttributeList.Name = "AttributeList";
            this.AttributeList.Size = new System.Drawing.Size(389, 64);
            this.AttributeList.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 765);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Products";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 859);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Product Attributes";
            // 
            // RptPDFBtn
            // 
            this.RptPDFBtn.Location = new System.Drawing.Point(425, 16);
            this.RptPDFBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RptPDFBtn.Name = "RptPDFBtn";
            this.RptPDFBtn.Size = new System.Drawing.Size(320, 43);
            this.RptPDFBtn.TabIndex = 18;
            this.RptPDFBtn.Text = "Report PDF";
            this.RptPDFBtn.UseVisualStyleBackColor = true;
            this.RptPDFBtn.Click += new System.EventHandler(this.RptPDFBtn_Click);
            // 
            // Testing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 959);
            this.Controls.Add(this.RptPDFBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AttributeList);
            this.Controls.Add(this.CreateProductBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ProductNameTextBox);
            this.Controls.Add(this.LoadProductsBtn);
            this.Controls.Add(this.ProductList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CompanyNameTextBox);
            this.Controls.Add(this.CreateCompanyBtn);
            this.Controls.Add(this.LoadCompaniesBtn);
            this.Controls.Add(this.CompanyList);
            this.Controls.Add(this.GenerateReportBtn);
            this.Controls.Add(this.ScriptParameter);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ScriptList);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Testing";
            this.Text = "Testing";
            this.Load += new System.EventHandler(this.Testing_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Button button1;
    private ListView ScriptList;
    private Button button2;
    private TextBox ScriptParameter;
    private Button GenerateReportBtn;
    private ListBox CompanyList;
    private Button LoadCompaniesBtn;
    private Button CreateCompanyBtn;
    private TextBox CompanyNameTextBox;
    private Label label1;
    private ListBox ProductList;
    private Button LoadProductsBtn;
    private TextBox ProductNameTextBox;
    private Label label2;
    private Button CreateProductBtn;
    private ListBox AttributeList;
    private Label label3;
    private Label label4;
    private Button RptPDFBtn;
}
