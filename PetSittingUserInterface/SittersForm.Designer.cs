namespace PetSittingUserInterface
{
    partial class SittersForm
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
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnList = new System.Windows.Forms.Button();
            this.lvSitters = new System.Windows.Forms.ListView();
            this.EmpID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EmpName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EmpAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EmpFee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EmpBio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HiringDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GrossSalary = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NetSalary = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblId = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtFee = new System.Windows.Forms.TextBox();
            this.lblFee = new System.Windows.Forms.Label();
            this.txtBio = new System.Windows.Forms.TextBox();
            this.lblBio = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtHiringDate = new System.Windows.Forms.TextBox();
            this.lblHiringDate = new System.Windows.Forms.Label();
            this.txtGrossSalary = new System.Windows.Forms.TextBox();
            this.lblGrossSalary = new System.Windows.Forms.Label();
            this.txtNetSalary = new System.Windows.Forms.TextBox();
            this.lblNetSalary = new System.Windows.Forms.Label();
            this.btnGetById = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(9, 181);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 0;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(90, 181);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(171, 181);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(252, 181);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(105, 23);
            this.btnList.TabIndex = 3;
            this.btnList.Text = "List Sitters";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // lvSitters
            // 
            this.lvSitters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EmpID,
            this.EmpName,
            this.EmpAge,
            this.EmpFee,
            this.EmpBio,
            this.HiringDate,
            this.GrossSalary,
            this.NetSalary});
            this.lvSitters.FullRowSelect = true;
            this.lvSitters.GridLines = true;
            this.lvSitters.Location = new System.Drawing.Point(9, 210);
            this.lvSitters.Name = "lvSitters";
            this.lvSitters.Size = new System.Drawing.Size(615, 232);
            this.lvSitters.TabIndex = 4;
            this.lvSitters.UseCompatibleStateImageBehavior = false;
            this.lvSitters.View = System.Windows.Forms.View.Details;
            this.lvSitters.Click += new System.EventHandler(this.lvSitters_Click);
            // 
            // EmpID
            // 
            this.EmpID.Text = "ID";
            this.EmpID.Width = 40;
            // 
            // EmpName
            // 
            this.EmpName.Text = "Name";
            this.EmpName.Width = 120;
            // 
            // EmpAge
            // 
            this.EmpAge.Text = "Age";
            this.EmpAge.Width = 40;
            // 
            // EmpFee
            // 
            this.EmpFee.Text = "Fee";
            this.EmpFee.Width = 40;
            // 
            // EmpBio
            // 
            this.EmpBio.Text = "Bio";
            this.EmpBio.Width = 93;
            // 
            // HiringDate
            // 
            this.HiringDate.Text = "Hire Date";
            this.HiringDate.Width = 100;
            // 
            // GrossSalary
            // 
            this.GrossSalary.Text = "Gross Salary";
            this.GrossSalary.Width = 80;
            // 
            // NetSalary
            // 
            this.NetSalary.Text = "Net Salary";
            this.NetSalary.Width = 80;
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(9, 13);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(70, 20);
            this.lblId.TabIndex = 5;
            this.lblId.Text = "ID:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(418, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(75, 13);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(100, 20);
            this.txtId.TabIndex = 7;
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtId_KeyPress);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(75, 39);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(300, 20);
            this.txtName.TabIndex = 9;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(9, 39);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(70, 20);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Name:";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(75, 65);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(100, 20);
            this.txtAge.TabIndex = 11;
            this.txtAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAge_KeyPress);
            // 
            // txtFee
            // 
            this.txtFee.Location = new System.Drawing.Point(275, 65);
            this.txtFee.Name = "txtFee";
            this.txtFee.Size = new System.Drawing.Size(100, 20);
            this.txtFee.TabIndex = 20;
            this.txtFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFee_KeyPress);
            // 
            // lblFee
            // 
            this.lblFee.Location = new System.Drawing.Point(239, 65);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(30, 20);
            this.lblFee.TabIndex = 21;
            this.lblFee.Text = "Fee:";
            // 
            // txtBio
            // 
            this.txtBio.Location = new System.Drawing.Point(193, 88);
            this.txtBio.Multiline = true;
            this.txtBio.Name = "txtBio";
            this.txtBio.Size = new System.Drawing.Size(182, 75);
            this.txtBio.TabIndex = 22;
            // 
            // lblBio
            // 
            this.lblBio.Location = new System.Drawing.Point(190, 68);
            this.lblBio.Name = "lblBio";
            this.lblBio.Size = new System.Drawing.Size(34, 20);
            this.lblBio.TabIndex = 23;
            this.lblBio.Text = "Bio:";
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(9, 65);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(70, 20);
            this.lblAge.TabIndex = 10;
            this.lblAge.Text = "Age:";
            // 
            // txtHiringDate
            // 
            this.txtHiringDate.Location = new System.Drawing.Point(75, 91);
            this.txtHiringDate.Name = "txtHiringDate";
            this.txtHiringDate.Size = new System.Drawing.Size(100, 20);
            this.txtHiringDate.TabIndex = 13;
            // 
            // lblHiringDate
            // 
            this.lblHiringDate.Location = new System.Drawing.Point(9, 91);
            this.lblHiringDate.Name = "lblHiringDate";
            this.lblHiringDate.Size = new System.Drawing.Size(70, 20);
            this.lblHiringDate.TabIndex = 12;
            this.lblHiringDate.Text = "Hiring Date:";
            // 
            // txtGrossSalary
            // 
            this.txtGrossSalary.Location = new System.Drawing.Point(75, 117);
            this.txtGrossSalary.Name = "txtGrossSalary";
            this.txtGrossSalary.Size = new System.Drawing.Size(100, 20);
            this.txtGrossSalary.TabIndex = 15;
            // 
            // lblGrossSalary
            // 
            this.lblGrossSalary.Location = new System.Drawing.Point(9, 117);
            this.lblGrossSalary.Name = "lblGrossSalary";
            this.lblGrossSalary.Size = new System.Drawing.Size(75, 20);
            this.lblGrossSalary.TabIndex = 14;
            this.lblGrossSalary.Text = "Gross Salary:";
            // 
            // txtNetSalary
            // 
            this.txtNetSalary.BackColor = System.Drawing.SystemColors.Info;
            this.txtNetSalary.ForeColor = System.Drawing.Color.Maroon;
            this.txtNetSalary.Location = new System.Drawing.Point(75, 143);
            this.txtNetSalary.Name = "txtNetSalary";
            this.txtNetSalary.ReadOnly = true;
            this.txtNetSalary.Size = new System.Drawing.Size(100, 20);
            this.txtNetSalary.TabIndex = 17;
            // 
            // lblNetSalary
            // 
            this.lblNetSalary.Location = new System.Drawing.Point(9, 143);
            this.lblNetSalary.Name = "lblNetSalary";
            this.lblNetSalary.Size = new System.Drawing.Size(75, 20);
            this.lblNetSalary.TabIndex = 16;
            this.lblNetSalary.Text = "Net Salary:";
            // 
            // btnGetById
            // 
            this.btnGetById.Location = new System.Drawing.Point(193, 10);
            this.btnGetById.Name = "btnGetById";
            this.btnGetById.Size = new System.Drawing.Size(105, 23);
            this.btnGetById.TabIndex = 18;
            this.btnGetById.Text = "Get by ID";
            this.btnGetById.UseVisualStyleBackColor = true;
            this.btnGetById.Click += new System.EventHandler(this.btnGetById_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(418, 181);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // SittersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 487);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGetById);
            this.Controls.Add(this.txtNetSalary);
            this.Controls.Add(this.lblNetSalary);
            this.Controls.Add(this.txtGrossSalary);
            this.Controls.Add(this.lblGrossSalary);
            this.Controls.Add(this.txtHiringDate);
            this.Controls.Add(this.lblHiringDate);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblFee);
            this.Controls.Add(this.txtFee);
            this.Controls.Add(this.lblBio);
            this.Controls.Add(this.txtBio);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lvSitters);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnInsert);
            this.Name = "SittersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sitters";
            this.Load += new System.EventHandler(this.SittersForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.ListView lvSitters;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblFee;
        private System.Windows.Forms.TextBox txtFee;
        private System.Windows.Forms.Label lblBio;
        private System.Windows.Forms.TextBox txtBio;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox txtHiringDate;
        private System.Windows.Forms.Label lblHiringDate;
        private System.Windows.Forms.TextBox txtGrossSalary;
        private System.Windows.Forms.Label lblGrossSalary;
        private System.Windows.Forms.TextBox txtNetSalary;
        private System.Windows.Forms.Label lblNetSalary;
        private System.Windows.Forms.Button btnGetById;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ColumnHeader EmpID;
        private System.Windows.Forms.ColumnHeader EmpName;
        private System.Windows.Forms.ColumnHeader EmpAge;
        private System.Windows.Forms.ColumnHeader EmpFee;
        private System.Windows.Forms.ColumnHeader EmpBio;
        private System.Windows.Forms.ColumnHeader HiringDate;
        private System.Windows.Forms.ColumnHeader GrossSalary;
        private System.Windows.Forms.ColumnHeader NetSalary;
    }
}

