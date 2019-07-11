namespace PetSittingUserInterface
{
    partial class OwnersForm
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
            this.lvOwners = new System.Windows.Forms.ListView();
            this.EmpOwnerID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EmpOwnerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EmpPetName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EmpPetAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EmpContactPhone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PetYears = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClear = new System.Windows.Forms.Button();
            this.txtOwnerId = new System.Windows.Forms.TextBox();
            this.txtOwnerName = new System.Windows.Forms.TextBox();
            this.lblOwnerName = new System.Windows.Forms.Label();
            this.txtPetName = new System.Windows.Forms.TextBox();
            this.lblPetName = new System.Windows.Forms.Label();
            this.txtPetAge = new System.Windows.Forms.TextBox();
            this.txtContactPhone = new System.Windows.Forms.TextBox();
            this.lblContactPhone = new System.Windows.Forms.Label();
            this.lblPetAge = new System.Windows.Forms.Label();
            this.txtPetYears = new System.Windows.Forms.TextBox();
            this.lblPetYears = new System.Windows.Forms.Label();
            this.btnGetById = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblOwnerId = new System.Windows.Forms.Label();
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
            this.btnList.Text = "List Owners";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // lvOwners
            // 
            this.lvOwners.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EmpOwnerID,
            this.EmpOwnerName,
            this.EmpPetName,
            this.EmpPetAge,
            this.EmpContactPhone,
            this.PetYears});
            this.lvOwners.FullRowSelect = true;
            this.lvOwners.GridLines = true;
            this.lvOwners.Location = new System.Drawing.Point(9, 210);
            this.lvOwners.Name = "lvOwners";
            this.lvOwners.Size = new System.Drawing.Size(615, 232);
            this.lvOwners.TabIndex = 4;
            this.lvOwners.UseCompatibleStateImageBehavior = false;
            this.lvOwners.View = System.Windows.Forms.View.Details;
            this.lvOwners.Click += new System.EventHandler(this.lvOwners_Click);
            // 
            // EmpOwnerID
            // 
            this.EmpOwnerID.Text = "Owner ID";
            this.EmpOwnerID.Width = 40;
            // 
            // EmpOwnerName
            // 
            this.EmpOwnerName.Text = "Owner Name";
            this.EmpOwnerName.Width = 120;
            // 
            // EmpPetName
            // 
            this.EmpPetName.Text = "Pet Name";
            this.EmpPetName.Width = 120;
            // 
            // EmpPetAge
            // 
            this.EmpPetAge.Text = "Pet Age";
            this.EmpPetAge.Width = 40;
            // 
            // EmpContactPhone
            // 
            this.EmpContactPhone.Text = "Contact Phone";
            this.EmpContactPhone.Width = 93;
            // 
            // PetYears
            // 
            this.PetYears.Text = "Pet Years";
            this.PetYears.Width = 80;
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
            // txtOwnerId
            // 
            this.txtOwnerId.Location = new System.Drawing.Point(109, 12);
            this.txtOwnerId.Name = "txtOwnerId";
            this.txtOwnerId.Size = new System.Drawing.Size(100, 20);
            this.txtOwnerId.TabIndex = 1;
            this.txtOwnerId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOwnerId_KeyPress);
            // 
            // txtOwnerName
            // 
            this.txtOwnerName.Location = new System.Drawing.Point(75, 62);
            this.txtOwnerName.Name = "txtOwnerName";
            this.txtOwnerName.Size = new System.Drawing.Size(134, 20);
            this.txtOwnerName.TabIndex = 3;
            // 
            // lblOwnerName
            // 
            this.lblOwnerName.Location = new System.Drawing.Point(4, 68);
            this.lblOwnerName.Name = "lblOwnerName";
            this.lblOwnerName.Size = new System.Drawing.Size(75, 20);
            this.lblOwnerName.TabIndex = 8;
            this.lblOwnerName.Text = "Owner Name:";
            // 
            // txtPetName
            // 
            this.txtPetName.Location = new System.Drawing.Point(75, 36);
            this.txtPetName.Name = "txtPetName";
            this.txtPetName.Size = new System.Drawing.Size(134, 20);
            this.txtPetName.TabIndex = 2;
            // 
            // lblPetName
            // 
            this.lblPetName.Location = new System.Drawing.Point(9, 39);
            this.lblPetName.Name = "lblPetName";
            this.lblPetName.Size = new System.Drawing.Size(70, 20);
            this.lblPetName.TabIndex = 8;
            this.lblPetName.Text = "Pet Name:";
            // 
            // txtPetAge
            // 
            this.txtPetAge.Location = new System.Drawing.Point(75, 88);
            this.txtPetAge.Name = "txtPetAge";
            this.txtPetAge.Size = new System.Drawing.Size(100, 20);
            this.txtPetAge.TabIndex = 4;
            this.txtPetAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPetAge_KeyPress);
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Location = new System.Drawing.Point(98, 136);
            this.txtContactPhone.Multiline = true;
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(105, 24);
            this.txtContactPhone.TabIndex = 5;
            // 
            // lblContactPhone
            // 
            this.lblContactPhone.Location = new System.Drawing.Point(4, 140);
            this.lblContactPhone.Name = "lblContactPhone";
            this.lblContactPhone.Size = new System.Drawing.Size(88, 20);
            this.lblContactPhone.TabIndex = 23;
            this.lblContactPhone.Text = "Contact Phone:";
            // 
            // lblPetAge
            // 
            this.lblPetAge.Location = new System.Drawing.Point(6, 91);
            this.lblPetAge.Name = "lblPetAge";
            this.lblPetAge.Size = new System.Drawing.Size(70, 20);
            this.lblPetAge.TabIndex = 10;
            this.lblPetAge.Text = "Pet Age:";
            // 
            // txtPetYears
            // 
            this.txtPetYears.BackColor = System.Drawing.SystemColors.Info;
            this.txtPetYears.ForeColor = System.Drawing.Color.Maroon;
            this.txtPetYears.Location = new System.Drawing.Point(75, 111);
            this.txtPetYears.Name = "txtPetYears";
            this.txtPetYears.ReadOnly = true;
            this.txtPetYears.Size = new System.Drawing.Size(100, 20);
            this.txtPetYears.TabIndex = 17;
            // 
            // lblPetYears
            // 
            this.lblPetYears.Location = new System.Drawing.Point(6, 111);
            this.lblPetYears.Name = "lblPetYears";
            this.lblPetYears.Size = new System.Drawing.Size(70, 20);
            this.lblPetYears.TabIndex = 16;
            this.lblPetYears.Text = "Pet Years:";
            // 
            // btnGetById
            // 
            this.btnGetById.Location = new System.Drawing.Point(268, 13);
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
            // lblOwnerId
            // 
            this.lblOwnerId.AutoSize = true;
            this.lblOwnerId.Location = new System.Drawing.Point(12, 13);
            this.lblOwnerId.Name = "lblOwnerId";
            this.lblOwnerId.Size = new System.Drawing.Size(52, 13);
            this.lblOwnerId.TabIndex = 24;
            this.lblOwnerId.Text = "Owner ID";
            // 
            // OwnersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 487);
            this.Controls.Add(this.lblOwnerId);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGetById);
            this.Controls.Add(this.txtPetYears);
            this.Controls.Add(this.lblPetYears);
            this.Controls.Add(this.txtPetAge);
            this.Controls.Add(this.lblPetAge);
            this.Controls.Add(this.lblContactPhone);
            this.Controls.Add(this.txtContactPhone);
            this.Controls.Add(this.txtOwnerName);
            this.Controls.Add(this.lblOwnerName);
            this.Controls.Add(this.txtPetName);
            this.Controls.Add(this.lblPetName);
            this.Controls.Add(this.txtOwnerId);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lvOwners);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnInsert);
            this.Name = "OwnersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Owners";
            this.Load += new System.EventHandler(this.OwnersForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.ListView lvOwners;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtOwnerId;
        private System.Windows.Forms.TextBox txtOwnerName;
        private System.Windows.Forms.Label lblOwnerName;
        private System.Windows.Forms.TextBox txtPetName;
        private System.Windows.Forms.Label lblPetName;
        private System.Windows.Forms.TextBox txtPetAge;
        private System.Windows.Forms.Label lblContactPhone;
        private System.Windows.Forms.TextBox txtContactPhone;
        private System.Windows.Forms.Label lblPetAge;
        private System.Windows.Forms.TextBox txtPetYears;
        private System.Windows.Forms.Label lblPetYears;
        private System.Windows.Forms.Button btnGetById;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ColumnHeader EmpOwnerID;
        private System.Windows.Forms.ColumnHeader EmpOwnerName;
        private System.Windows.Forms.ColumnHeader EmpPetName;
        private System.Windows.Forms.ColumnHeader EmpPetAge;
        private System.Windows.Forms.ColumnHeader EmpContactPhone;
        private System.Windows.Forms.ColumnHeader PetYears;
        private System.Windows.Forms.Label lblOwnerId;
    }
}

