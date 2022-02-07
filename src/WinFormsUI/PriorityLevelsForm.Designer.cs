namespace OrderManager.WinFormsUI;

partial class PriorityLevelsForm {
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
            this.NewPriorityName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EditBox = new System.Windows.Forms.GroupBox();
            this.CreatePriorityBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PriorityLevelsBox = new System.Windows.Forms.ListBox();
            this.DragHint = new System.Windows.Forms.Label();
            this.EditBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // NewPriorityName
            // 
            this.NewPriorityName.Location = new System.Drawing.Point(5, 44);
            this.NewPriorityName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NewPriorityName.Name = "NewPriorityName";
            this.NewPriorityName.Size = new System.Drawing.Size(185, 23);
            this.NewPriorityName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Priority Levels";
            // 
            // EditBox
            // 
            this.EditBox.Controls.Add(this.CreatePriorityBtn);
            this.EditBox.Controls.Add(this.label2);
            this.EditBox.Controls.Add(this.NewPriorityName);
            this.EditBox.Location = new System.Drawing.Point(10, 39);
            this.EditBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditBox.Name = "EditBox";
            this.EditBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditBox.Size = new System.Drawing.Size(201, 112);
            this.EditBox.TabIndex = 2;
            this.EditBox.TabStop = false;
            this.EditBox.Text = "New Priority";
            // 
            // CreatePriorityBtn
            // 
            this.CreatePriorityBtn.Location = new System.Drawing.Point(52, 76);
            this.CreatePriorityBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CreatePriorityBtn.Name = "CreatePriorityBtn";
            this.CreatePriorityBtn.Size = new System.Drawing.Size(82, 22);
            this.CreatePriorityBtn.TabIndex = 2;
            this.CreatePriorityBtn.Text = "Create";
            this.CreatePriorityBtn.UseVisualStyleBackColor = true;
            this.CreatePriorityBtn.Click += new System.EventHandler(this.CreatePriorityBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // PriorityLevelsBox
            // 
            this.PriorityLevelsBox.FormattingEnabled = true;
            this.PriorityLevelsBox.ItemHeight = 15;
            this.PriorityLevelsBox.Location = new System.Drawing.Point(227, 13);
            this.PriorityLevelsBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PriorityLevelsBox.Name = "PriorityLevelsBox";
            this.PriorityLevelsBox.Size = new System.Drawing.Size(171, 139);
            this.PriorityLevelsBox.TabIndex = 3;
            this.PriorityLevelsBox.UseWaitCursor = true;
            // 
            // DragHint
            // 
            this.DragHint.AutoSize = true;
            this.DragHint.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DragHint.ForeColor = System.Drawing.Color.Gray;
            this.DragHint.Location = new System.Drawing.Point(227, 154);
            this.DragHint.Name = "DragHint";
            this.DragHint.Size = new System.Drawing.Size(167, 13);
            this.DragHint.TabIndex = 4;
            this.DragHint.Text = "Drag levels to reorder priorities";
            // 
            // PriorityLevels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 171);
            this.Controls.Add(this.DragHint);
            this.Controls.Add(this.PriorityLevelsBox);
            this.Controls.Add(this.EditBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PriorityLevels";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.EditBox.ResumeLayout(false);
            this.EditBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private TextBox NewPriorityName;
    private Label label1;
    private GroupBox EditBox;
    private Label label2;
    private Button CreatePriorityBtn;
    private ListBox PriorityLevelsBox;
    private Label DragHint;
}
