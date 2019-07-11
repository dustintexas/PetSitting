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
    public partial class SittersForm : Form
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;

        #endregion
        public SittersForm()
        {
            InitializeComponent();
        }
        private void ClearScreen()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFee.Text = string.Empty;
            txtBio.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtHiringDate.Text = string.Empty;
            txtGrossSalary.Text = string.Empty;
            txtNetSalary.Text = string.Empty;
            lvSitters.Items.Clear();
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
                MessageBox.Show("UserInterface:SittersForm::InsertRowToListView::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Insert(string name, int age, decimal fee, string bio, DateTime? hiringDate, decimal grossSalary)
        {
            try
            {
                using (var sitters = new SittersBusiness())
                {
                    var entity = new SittersEntity();
                    entity.Name = name;
                    entity.Age = age;
                    entity.Bio = bio;
                    entity.Fee = fee;
                    entity.HiringDate = hiringDate;
                    entity.GrossSalary = grossSalary;
                    var opSuccessful = sitters.InsertSitter(entity);

                    var resultMessage = opSuccessful ? "Done Successfully" : "Error happened!";

                    MessageBox.Show(resultMessage, "Success", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                MessageBox.Show("UserInterface:SittersForm::Insert::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Update(int id, string name, int age, decimal fee, string bio, DateTime? hiringDate, decimal grossSalary)
        {
            try
            {
                using (var sitters = new SittersBusiness())
                {
                    var entity = new SittersEntity();
                    entity.SitterID = id;
                    entity.Name = name;
                    entity.Age = age;
                    entity.Fee = fee;
                    entity.Bio = bio;
                    entity.HiringDate = hiringDate;
                    entity.GrossSalary = grossSalary;
                    var opSuccessful = sitters.UpdateSitter(entity);

                    var resultMessage = opSuccessful ? "Done Successfully" : "Error happened or no Sitter found to update";

                    MessageBox.Show(resultMessage, "Success", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                MessageBox.Show("UserInterface:SittersForm::Update::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private void Delete(int id)
        {
            try
            {
                using (var sitters = new SittersBusiness())
                {
                    bool opSuccessful = sitters.DeleteSitterById(id);

                    var resultMessage = opSuccessful ? "Done Successfully" : "Error happened or no Sitter found to delete";

                    MessageBox.Show(resultMessage, "Success", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                MessageBox.Show("UserInterface:SittersForm::Delete::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private SittersEntity SelectOne(int id)
        {
            try
            {
                using (var sitters = new SittersBusiness())
                {
                    return sitters.SelectSitterById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                MessageBox.Show("UserInterface:SittersForm::SelectOne::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
            return null;
        }
        private List<SittersEntity> SelectAll()
        {
            try
            {
                using (var sitters = new SittersBusiness())
                {
                    return sitters.SelectAllSitters();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                MessageBox.Show("UserInterface:SittersForm::SelectAll::Error occured." +
                    Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK);
            }
            return null;
        }

        private void SittersForm_Load(object sender, EventArgs e)
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
                txtName.Text,
                int.Parse(txtAge.Text),
                decimal.Parse(txtFee.Text),
                txtBio.Text,
                txtHiringDate.Text.Trim().Length == 0 ? (DateTime?)null : DateTime.ParseExact(txtHiringDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                Math.Round(Decimal.Parse(txtGrossSalary.Text), 3));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Update(
                int.Parse(txtId.Text),
                txtName.Text,
                int.Parse(txtAge.Text),
                decimal.Parse(txtFee.Text),
                txtBio.Text,
                txtHiringDate.Text.Trim().Length == 0 ? (DateTime?)null : DateTime.ParseExact(txtHiringDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                Math.Round(Decimal.Parse(txtGrossSalary.Text), 3));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete(int.Parse(txtId.Text));
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            var entity = SelectOne(int.Parse(txtId.Text));
            if (entity != null)
            {
                txtName.Text = entity.Name;
                txtAge.Text = entity.Age.ToString();
                txtFee.Text = entity.Fee.ToString();
                txtBio.Text = entity.Bio;
                txtHiringDate.Text = string.IsNullOrEmpty(entity.HiringDate.ToString())
                    ? string.Empty : Convert.ToDateTime(entity.HiringDate).ToString("MM/dd/yyyy");
                txtGrossSalary.Text = entity.GrossSalary.ToString();
                txtNetSalary.Text = entity.NetSalary.ToString();
            }
            else
            {
                MessageBox.Show("No Sitters Found!", "Warning", MessageBoxButtons.OK);
            }
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            lvSitters.Items.Clear();

            var sitters = SelectAll();

            if (sitters != null)
            {
                foreach (var emp in sitters)
                {
                    var strListItems = new List<string>();
                    strListItems.Add(emp.SitterID.ToString());
                    strListItems.Add(emp.Name.ToString());
                    strListItems.Add(emp.Age.ToString());
                    strListItems.Add(emp.Fee.ToString());
                    strListItems.Add(emp.Bio.ToString());
                    strListItems.Add(string.IsNullOrEmpty(emp.HiringDate.ToString()) ? string.Empty : Convert.ToDateTime(emp.HiringDate).ToString("MM/dd/yyyy"));
                    strListItems.Add(emp.GrossSalary.ToString());
                    strListItems.Add(emp.NetSalary.ToString());

                    InsertRowToListView(lvSitters, strListItems);
                }
            }
            else
            {
                MessageBox.Show("No Sitters Found!", "Warning", MessageBoxButtons.OK);
            }

        }
        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFee_KeyPress(object sender, KeyPressEventArgs e)
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

        private void lvSitters_Click(object sender, EventArgs e)
        {
            if (lvSitters.SelectedItems.Count == 1)
            {
                txtId.Text = this.lvSitters.SelectedItems[0].SubItems[0].Text;
                txtName.Text = this.lvSitters.SelectedItems[0].SubItems[1].Text;
                txtAge.Text = this.lvSitters.SelectedItems[0].SubItems[2].Text;
                txtFee.Text = this.lvSitters.SelectedItems[0].SubItems[3].Text;
                txtBio.Text = this.lvSitters.SelectedItems[0].SubItems[4].Text;
                txtHiringDate.Text = this.lvSitters.SelectedItems[0].SubItems[5].Text;
                txtGrossSalary.Text = this.lvSitters.SelectedItems[0].SubItems[6].Text;
                txtNetSalary.Text = this.lvSitters.SelectedItems[0].SubItems[7].Text;
            }
        }

    }
}
