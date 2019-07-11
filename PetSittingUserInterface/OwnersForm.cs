using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using PetSitting.BusinessLogic;
using PetSitting.Common;
using PetSitting.Model;

namespace PetSittingUserInterface
{
    public partial class OwnersForm : Form
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;

        #endregion
        public OwnersForm()
        {
            InitializeComponent();
        }
        private void ClearScreen()
        {
            txtOwnerId.Text = string.Empty;
            txtOwnerName.Text = string.Empty;
            txtPetName.Text = string.Empty;
            txtContactPhone.Text = string.Empty;
            txtPetAge.Text = string.Empty;
            txtPetYears.Text = string.Empty;
            lvOwners.Items.Clear();
        }
        private void InsertRowToListView(ListView lv, List<string> strValues)
        {
            var lvRowItem = new ListViewItem();

            try
            {
                lvRowItem.Text = strValues[0];
                for (int iCounter = 1; iCounter < strValues.Count; iCounter++)
                {
                    lvRowItem.SubItems.Add(strValues[iCounter]);
                }
                lv.Items.Add(lvRowItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show("UserInterface:OwnersForm::InsertRowToListView::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Insert(string ownername, int age, string petname, string contactphone)
        {
            try
            {
                using (var owners = new OwnersBusiness())
                {
                    var entity = new OwnersEntity();
                    entity.OwnerName = ownername;
                    entity.PetName = petname;
                    entity.PetAge = age;
                    entity.ContactPhone = contactphone;
                    var opSuccessful = owners.InsertOwner(entity);

                    var resultMessage = opSuccessful ? "Done Successfully" : "Error happened!";

                    MessageBox.Show(resultMessage, "Success", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                MessageBox.Show("UserInterface:OwnersForm::Insert::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Update(int id, string ownername, string petname, int age, string contactphone)
        {
            try
            {
                using (var owners = new OwnersBusiness())
                {
                    var entity = new OwnersEntity();
                    entity.OwnerID = id;
                    entity.OwnerName = ownername;
                    entity.PetName = petname;
                    entity.PetAge = age;
                    entity.ContactPhone = contactphone;
                    var opSuccessful = owners.UpdateOwner(entity);

                    var resultMessage = opSuccessful ? "Done Successfully" : "Error happened or no Owner found to update";

                    MessageBox.Show(resultMessage, "Success", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                MessageBox.Show("UserInterface:OwnersForm::Update::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private void Delete(int id)
        {
            try
            {
                using (var owners = new OwnersBusiness())
                {
                    bool opSuccessful = owners.DeleteOwnerById(id);

                    var resultMessage = opSuccessful ? "Done Successfully" : "Error happened or no Owner found to delete";

                    MessageBox.Show(resultMessage, "Success", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                MessageBox.Show("UserInterface:OwnersForm::Delete::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private OwnersEntity SelectOne(int id)
        {
            try
            {
                using (var owners = new OwnersBusiness())
                {
                    return owners.SelectOwnerById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                MessageBox.Show("UserInterface:OwnersForm::SelectOne::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
            return null;
        }
        private List<OwnersEntity> SelectAll()
        {
            try
            {
                using (var owners = new OwnersBusiness())
                {
                    return owners.SelectAllOwners();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                MessageBox.Show("UserInterface:OwnersForm::SelectAll::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
            return null;
        }

        private void OwnersForm_Load(object sender, EventArgs e)
        {
            _loggingHandler = new LoggingHandler();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Insert(
                txtOwnerName.Text,
                int.Parse(txtPetAge.Text),
                txtPetName.Text,
                txtContactPhone.Text);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Update(
                int.Parse(txtOwnerId.Text),
                txtOwnerName.Text,
                txtPetName.Text,
                int.Parse(txtPetAge.Text),
                txtContactPhone.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete(int.Parse(txtOwnerId.Text));
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            var entity = SelectOne(int.Parse(txtOwnerId.Text));
            if (entity != null)
            {
                txtOwnerName.Text = entity.OwnerName;
                txtPetName.Text = entity.PetName;
                txtPetAge.Text = entity.PetAge.ToString();
                txtContactPhone.Text = entity.ContactPhone;
                txtPetYears.Text = entity.PetYears.ToString();
            }
            else
            {
                MessageBox.Show("No Owners Found!", "Warning", MessageBoxButtons.OK);
            }
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            lvOwners.Items.Clear();

            var owners = SelectAll();

            if (owners != null)
            {
                foreach (var emp in owners)
                {
                    var strListItems = new List<string>();
                    strListItems.Add(emp.OwnerID.ToString());
                    strListItems.Add(emp.OwnerName.ToString());
                    strListItems.Add(emp.PetName.ToString());
                    strListItems.Add(emp.PetAge.ToString());
                    strListItems.Add(emp.ContactPhone.ToString());
                    strListItems.Add(emp.PetYears.ToString());

                    InsertRowToListView(lvOwners, strListItems);
                }
            }
            else
            {
                MessageBox.Show("No Owners Found!", "Warning", MessageBoxButtons.OK);
            }

        }
        private void txtOwnerId_KeyPress(object sender, KeyPressEventArgs e)
        {
            //accepts only numbers
            if (e.KeyChar != 8 && e.KeyChar != 127)
            {
                if (e.KeyChar < 48 || e.KeyChar > 57)
                {
                    e.Handled = true;
                }
            }
        }
        private void txtPetAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            //accepts only numbers
            if (e.KeyChar != 8 && e.KeyChar != 127)
            {
                if (e.KeyChar < 48 || e.KeyChar > 57)
                {
                    e.Handled = true;
                }
            }
        }

        private void lvOwners_Click(object sender, EventArgs e)
        {
            if (lvOwners.SelectedItems.Count == 1)
            {
                txtOwnerId.Text = this.lvOwners.SelectedItems[0].SubItems[0].Text;
                txtOwnerName.Text = this.lvOwners.SelectedItems[0].SubItems[1].Text;
                txtPetName.Text = this.lvOwners.SelectedItems[0].SubItems[2].Text;
                txtPetAge.Text = this.lvOwners.SelectedItems[0].SubItems[3].Text;
                txtContactPhone.Text = this.lvOwners.SelectedItems[0].SubItems[4].Text;
                txtPetYears.Text = this.lvOwners.SelectedItems[0].SubItems[5].Text;
            }
        }

    }
}
