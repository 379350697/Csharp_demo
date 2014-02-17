using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;
using System.IO;
using WebApplication;
using EmployeeModel;
using IEmployeeDALN;
using System.Data;

namespace EmployeeDAL
{
    public class EmployeeXmlDAL : IEmployeeDAL
    {
        XmlDocument xmlDoc = null;
        string xmlFilePath = "../../temp.xml";

        public EmployeeXmlDAL()
        {
            if (!File.Exists(this.xmlFilePath))
            {
                this.CreateXml();
            }
        }

        public void CreateXml()
        {
            FileStream fs = new FileStream(xmlFilePath, FileMode.Create);
            fs.Close();
            xmlDoc = new XmlDocument();

            XmlElement root = xmlDoc.CreateElement("employees");
            xmlDoc.AppendChild(root);

            XmlElement employee = xmlDoc.CreateElement("employee");
            employee.SetAttribute("isAvailable", "TrueOrFalse");
            employee.SetAttribute("phonenumber", "PhoneNumber");
            employee.SetAttribute("address", "Address");
            employee.SetAttribute("birth", "Birth");
            employee.SetAttribute("gender", "Gender");
            employee.SetAttribute("lastname", "LastName");
            employee.SetAttribute("firstname", "FirstName");
            employee.SetAttribute("id", "ID");

            root.AppendChild(employee);
            xmlDoc.Save(@xmlFilePath);
        }

        public int AddEmployee(Employee emp)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            XmlNode lastEmp = xmlDoc.SelectSingleNode("employees/employee[last()]");
            XmlElement employee = xmlDoc.CreateElement("employee");
            if (!"ID".Equals(lastEmp.Attributes[7].Value))
            {
                emp.Id = System.Int32.Parse(lastEmp.Attributes[7].Value) + 1;
            }
            Hashtable ht = this.GetEmpHashtable(emp);

            foreach (XmlNode node in lastEmp.Attributes)
            {
                employee.SetAttribute(node.Name, ht[node.Name].ToString());
            }
            lastEmp.ParentNode.AppendChild(employee);

            xmlDoc.Save(xmlFilePath);
            return 1;
        }

        public int UpdateEmployee(Employee emp)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            XmlNode employee = xmlDoc.SelectSingleNode("employees/employee[@id=" + emp.Id + "]");
            if (employee == null)
            {
                return 0;
            }
            foreach (XmlNode node in employee.Attributes)
            {
                if ("birth".Equals(node.Name))
                {
                    node.Value = emp.BirthDay;
                }
                if ("address".Equals(node.Name))
                {
                    node.Value = emp.Address;
                }
                if ("phonenumber".Equals(node.Name))
                {
                    node.Value = emp.Phone;
                }
            }
            xmlDoc.Save(xmlFilePath);
            return 1;
        }

        public int DeleteEmployee(String id)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            XmlNode employee = xmlDoc.SelectSingleNode("employees/employee[@id=" + id + "]");
            if (employee == null)
            {
                return 0;
            }
            foreach (XmlNode node in employee.Attributes)
            {
                if ("isAvailable".Equals(node.Name))
                {
                    node.Value = "false";
                }
            }
            xmlDoc.Save(xmlFilePath);
            return 1;
        }

        public Hashtable GetEmpHashtable(Employee emp)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", emp.Id.ToString());
            ht.Add("firstname", emp.FirstName);
            ht.Add("lastname", emp.LastName);
            ht.Add("gender", emp.Gender);
            ht.Add("birth", emp.BirthDay);
            ht.Add("address", emp.Address);
            ht.Add("phonenumber", emp.Phone);
            ht.Add("isAvailable", "true");
            return ht;
        }



        public DataSet GetAllEmplpoyee(String asc)
        {
            DataSet ds = CreateDataTable();
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("/employees");
            foreach (XmlNode node in root.ChildNodes)
            {
                if ("false".Equals(node.Attributes[0].Value) || "TrueOrFalse".Equals(node.Attributes[0].Value))
                {
                    continue;
                }
                AddRow(ds, node);
            }
            return ds;
        }

        private static void AddRow(DataSet ds, XmlNode node)
        {
            DataRow row = ds.Tables[0].NewRow();
            row["id"] = System.Int32.Parse(node.Attributes[7].Value);
            row["firstname"] = node.Attributes[6].Value;
            row["lastname"] = node.Attributes[5].Value;
            row["gender"] = node.Attributes[4].Value;
            row["birthday"] = node.Attributes[3].Value;
            row["address"] = node.Attributes[2].Value;
            row["phone"] = node.Attributes[1].Value;
            ds.Tables[0].Rows.Add(row);
        }

        private static DataSet CreateDataTable()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("firstname");
            dt.Columns.Add("lastname");
            dt.Columns.Add("gender");
            dt.Columns.Add("birthday");
            dt.Columns.Add("phone");
            dt.Columns.Add("address");
            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet GetEmplpyeeById(String id)
        {
            DataSet ds = CreateDataTable();
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("/employees");
            foreach (XmlNode node in root.ChildNodes)
            {
                if ("false".Equals(node.Attributes[0].Value) || "TrueOrFalse".Equals(node.Attributes[0].Value))
                {
                    continue;
                }
                if (id.Equals(node.Attributes[7].Value))
                {
                    AddRow(ds, node);
                }
            }
            return ds;
        }

        public DataSet GetEmplpyeeByFirstName(String firstname)
        {
            DataSet ds = CreateDataTable();
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("/employees");
            foreach (XmlNode node in root.ChildNodes)
            {
                if ("false".Equals(node.Attributes[0].Value) || "TrueOrFalse".Equals(node.Attributes[0].Value))
                {
                    continue;
                }
                if (firstname.Equals(node.Attributes[6].Value))
                {
                    AddRow(ds, node);
                }
            }
            return ds;
        }

        public DataSet GetEmplpyeeByLastName(String lastname)
        {
            DataSet ds = CreateDataTable();
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("/employees");
            foreach (XmlNode node in root.ChildNodes)
            {
                if ("false".Equals(node.Attributes[0].Value) || "TrueOrFalse".Equals(node.Attributes[0].Value))
                {
                    continue;
                }
                if (lastname.Equals(node.Attributes[5].Value))
                {
                    AddRow(ds, node);
                }
            }
            return ds;
        }
    }
}
