namespace XmlGenerator
{
    partial class frmXmlGenerator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblItemName = new Label();
            txtItemName = new TextBox();
            lblQty = new Label();
            txtQty = new TextBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lalVatRate = new Label();
            txtVatRate = new TextBox();
            btnGenerate = new Button();
            lblUnit = new Label();
            txtUnit = new TextBox();
            SuspendLayout();
            // 
            // lblItemName
            // 
            lblItemName.AutoSize = true;
            lblItemName.Location = new Point(25, 20);
            lblItemName.Name = "lblItemName";
            lblItemName.Size = new Size(83, 17);
            lblItemName.TabIndex = 7;
            lblItemName.Text = "Item Name : ";
            // 
            // txtItemName
            // 
            txtItemName.Location = new Point(111, 18);
            txtItemName.Name = "txtItemName";
            txtItemName.Size = new Size(129, 25);
            txtItemName.TabIndex = 0;
            // 
            // lblQty
            // 
            lblQty.AutoSize = true;
            lblQty.Location = new Point(40, 56);
            lblQty.Name = "lblQty";
            lblQty.Size = new Size(67, 17);
            lblQty.TabIndex = 8;
            lblQty.Text = "Quentity : ";
            // 
            // txtQty
            // 
            txtQty.Location = new Point(111, 54);
            txtQty.Name = "txtQty";
            txtQty.Size = new Size(129, 25);
            txtQty.TabIndex = 1;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(60, 93);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(47, 17);
            lblPrice.TabIndex = 9;
            lblPrice.Text = "Price : ";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(111, 90);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(129, 25);
            txtPrice.TabIndex = 2;
            // 
            // lalVatRate
            // 
            lalVatRate.AutoSize = true;
            lalVatRate.Location = new Point(44, 161);
            lalVatRate.Name = "lalVatRate";
            lalVatRate.Size = new Size(63, 17);
            lalVatRate.TabIndex = 11;
            lalVatRate.Text = "VatRate : ";
            // 
            // txtVatRate
            // 
            txtVatRate.Location = new Point(111, 160);
            txtVatRate.Name = "txtVatRate";
            txtVatRate.Size = new Size(129, 25);
            txtVatRate.TabIndex = 4;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(99, 204);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(88, 28);
            btnGenerate.TabIndex = 5;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.Location = new Point(65, 128);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(42, 17);
            lblUnit.TabIndex = 10;
            lblUnit.Text = "Unit : ";
            // 
            // txtUnit
            // 
            txtUnit.Location = new Point(111, 125);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(129, 25);
            txtUnit.TabIndex = 3;
            // 
            // frmXmlGenerator
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(287, 253);
            Controls.Add(txtUnit);
            Controls.Add(lblUnit);
            Controls.Add(btnGenerate);
            Controls.Add(txtVatRate);
            Controls.Add(lalVatRate);
            Controls.Add(txtPrice);
            Controls.Add(lblPrice);
            Controls.Add(txtQty);
            Controls.Add(lblQty);
            Controls.Add(txtItemName);
            Controls.Add(lblItemName);
            Name = "frmXmlGenerator";
            Text = "E-Invoice XML Generator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblItemName;
        private TextBox txtItemName;
        private Label lblQty;
        private TextBox txtQty;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lalVatRate;
        private TextBox txtVatRate;
        private Button btnGenerate;
        private Label lblUnit;
        private TextBox txtUnit;
    }
}
