using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using EmployeeModel;
using EmployeeDAL;
using IEmployeeDALN;

namespace EmployeeXml
{
    public partial class EmployeeView : Form
    {
        private IEmployeeDAL idal = new EmployeeXmlDAL();
        private EmployeeBll ebll = new EmployeeBll();
        private Employee employee;


        public EmployeeView()
        {
            MessageBox.Show("Hello " + Dns.GetHostName() + ", welcome to Employee Management System", "info", MessageBoxButtons.OK);
            this.InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(formClosed);
        }

        public Employee Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowEmp(ebll.GetAll(""), this.dataGridView1);
        }

        private void formClosed(object sender, EventArgs e)
        {
            if (MessageBox.Show("confirmed to close?", "warn", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
                Application.ExitThread();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if ("".Equals(textFN.Text.Trim()))
            {
                sb.Append("\nfirstname is required");
            }
            if ("".Equals(textLN.Text.Trim()))
            {
                sb.Append("\nlastname is required");
            }
            if (!Regex.IsMatch(textPhone.Text, @"^\d{8}|\d{11}$"))
            {
                sb.Append("\nwrong phonenumber fotmat!");
            }

            if (sb.Length == 0)
            {
                Employee emp = new Employee();
                emp.FirstName = textFN.Text;
                emp.LastName = textLN.Text;
                if (rButtonMale.Checked == true)
                {
                    emp.Gender = "M";
                }
                else
                {
                    emp.Gender = "F";
                }
                emp.BirthDay = dateTimeAdd.Value.ToString("MM/dd/yyyy");
                emp.Address = textAddress.Text;
                emp.Phone = textPhone.Text;
                ebll.Add(emp);
                ShowEmp(ebll.GetAll(""), this.dataGridView1);
                ShowMessage(labelValidate, "add successfully", Color.Green);
            }
            else
            {
                ShowMessage(labelValidate, sb.ToString(), Color.Red);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            labelValidate.ResetText();
            textFN.ResetText();
            textLN.ResetText();
            rButtonMale.Checked = true;
            textAddress.ResetText();
            textPhone.ResetText();
        }

        //修改员工页面的查找员工信息事件
        private void buttonIDU_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textIDU.Text, @"^\d+$"))
            {
                DataTable dt = idal.GetEmplpyeeById(textIDU.Text).Tables[0];
                if (dt != null && dt.Rows.Count != 0)
                {
                    Employee emp = DataRowToEmployee(dt.Rows[0]);
                    textFNU.Text = emp.FirstName;
                    textLNU.Text = emp.LastName;
                    if ("M".Equals(emp.Gender))
                    {
                        rButtonMU.Checked = true;
                    }
                    else
                    {
                        rButtonFMU.Checked = true;
                    }
                    DateTime time = new DateTime();
                    DateTime.TryParseExact(emp.BirthDay, "MM/dd/yyyy", null,
            System.Globalization.DateTimeStyles.None, out time);
                    dateTimeUpdate.Value = time;
                    textAddressU.Text = emp.Address;
                    textPhoneU.Text = emp.Phone;
                    panelUpdate.Visible = true;
                }
                else
                {
                    ShowMessage(labelUpdate,"can't find the employee", Color.Red);
                    panelUpdate.Visible = false;
                }
            }
            else
            {
                ShowMessage(labelUpdate, "please enter the right infomation", Color.Red);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DataTable dt = idal.GetEmplpyeeById(textIDU.Text).Tables[0];
            if (dt.Rows.Count==0)
            {
                ShowMessage(labelUpdate, "this employee was deleted", Color.Red);
                panelUpdate.Visible = false;
            }
            else
            {
                Employee emp = DataRowToEmployee(dt.Rows[0]);
                emp.BirthDay = dateTimeUpdate.Value.ToString("MM/dd/yyyy");
                emp.Address = textAddressU.Text;
                emp.Phone = textPhoneU.Text;
                if (!Regex.IsMatch(textPhoneU.Text, @"^(\d{8}|\d{11})$"))
                {
                    ShowMessage(labelUpdate, "phonenumber is invalid!", Color.Red);
                }else if (ebll.Update(emp))
                {
                    ShowMessage(labelUpdate, "update successfully!", Color.Green);
                }
                else
                {
                    ShowMessage(labelUpdate, "update failed!", Color.Red);
                }
                ShowEmp(ebll.GetAll(""), this.dataGridView1);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("confirmed to delete?", "warn", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (!"".Equals(textIDD.Text))
                {
                    if (ebll.Delete(textIDD.Text))
                    {
                        ShowMessage(labelDelete, "delete successfully", Color.Green);
                    }
                    else
                    {
                        ShowMessage(labelDelete, "delete failed", Color.Red);
                    }
                }
                else
                {
                    ShowMessage(labelDelete, "id is required", Color.Red);
                }
                
            }
            ShowEmp(ebll.GetAll(""), this.dataGridView1);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            String str = comboBoxSearch.Text;
            DataSet ds = new DataSet();
            if (!Regex.IsMatch(textSearch.Text,@"^\w+$"))
            {
                this.dataGridView2.Rows.Clear();
                ShowMessage(labelSearch, "please enter the infomation", Color.Red);
            }
            else { 
                switch (str)
                {
                    case "id":
                        ds = ebll.SearchById(textSearch.Text);
                        ds = SearchValidate(ds);
                        break;
                    case "firstname":
                        ds = ebll.SearchByFN(textSearch.Text);
                        ds = SearchValidate(ds);
                        break;
                    case "lastname":
                        ds = ebll.SearchByLN(textSearch.Text);
                        ds = SearchValidate(ds);
                        break;
                    default:
                        ShowMessage(labelSearch,"please select the type", Color.Red); break;
                }
            }
        }

        private DataSet SearchValidate(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                ShowEmp(ds, this.dataGridView2);
            }
            else
            {
                this.dataGridView2.Rows.Clear();
                ShowMessage(labelSearch, "can not find this employee", Color.Red);
            }
            return ds;
        }

        public static void ShowEmp(DataSet ds, DataGridView dgv)
        {
            DataTable dt = ds.Tables[0];
            dgv.Rows.Clear();
            int index = 0;
            DataRow row = null;
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                row = dt.Rows[i];
                index = dgv.Rows.Add();
                dgv.Rows[index].Cells[0].Value = row["id"];
                dgv.Rows[index].Cells[1].Value = row["firstname"];
                dgv.Rows[index].Cells[2].Value = row["lastname"];
                dgv.Rows[index].Cells[3].Value = row["gender"];
                dgv.Rows[index].Cells[4].Value = row["birthday"];
                dgv.Rows[index].Cells[5].Value = row["address"];
                dgv.Rows[index].Cells[6].Value = row["phone"];
            }
        }

        public static void ShowMessage(Label label,String message,Color color)
        {
            label.ForeColor = color;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = message;
        }

        public static Employee DataRowToEmployee(DataRow row)
        {
            Employee emp = new Employee();
            emp.Id = Convert.ToInt32(row["id"].ToString());
            emp.FirstName = row["firstname"].ToString();
            emp.LastName = row["lastname"].ToString();
            emp.Gender = row["gender"].ToString();
            emp.BirthDay = row["birthday"].ToString();
            emp.Phone = row["phone"].ToString();
            emp.Address = row["address"].ToString();
            return emp;
        }

    }
}
