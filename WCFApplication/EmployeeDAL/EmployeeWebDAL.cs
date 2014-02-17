using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using EmployeeModel;
using IEmployeeDALN;
using WebApplication;

namespace EmployeeDAL
{
    public class EmployeeWebDAL : IEmployeeDAL
    {
        public EmployeeWebDAL()
        {
        }

        public int AddEmployee(Employee emp)
        {
            String sql = "insert into employee(firstname,lastname,gender,birthday,phone,address) "
                    + "values('" + emp.FirstName + "','" + emp.LastName + "','" + emp.Gender + "','" + emp.BirthDay + "','" + emp.Phone + "','" + emp.Address + "')";

            int i = SqlHelper.ExecuteNoQuery(sql);
            SqlHelper.close();
            return i;
        }

        public int UpdateEmployee(Employee emp)
        {
            String sql = "update employee set birthday='" + emp.BirthDay + "',phone='" + emp.Phone + "',address='" + emp.Address + "' where id =" + emp.Id;
            int i = SqlHelper.ExecuteNoQuery(sql);
            SqlHelper.close();
            return i;
        }

        public int DeleteEmployee(String id)
        {
            String sql = "update employee set isAvailable='F' where id =" + id;
            int i = SqlHelper.ExecuteNoQuery(sql);
            SqlHelper.close();
            return i;
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
            String sql = null;
            if ("asc".Equals(asc))
            {
                sql = "Select * from employee where isAvailable='T'";
            }
            else
            {
                sql = "Select * from employee where isAvailable='T' order by id desc";
            }
            DataSet ds = SqlHelper.ExecuteQuery(sql);
            return ds;
        }

        public DataSet GetEmplpyeeById(String id)
        {
            String sql = null;
            sql = "Select * from employee where isAvailable='T' and id =" + id;
            DataSet ds = SqlHelper.ExecuteQuery(sql);
            return ds;
        }

        public DataSet GetEmplpyeeByFirstName(String firstname)
        {
            String sql = null;
            sql = "Select * from employee where isAvailable='T' and firstname ='" + firstname + "'";
            DataSet ds = SqlHelper.ExecuteQuery(sql);
            return ds;
        }

        public DataSet GetEmplpyeeByLastName(String lastname)
        {
            String sql = null;
            sql = "Select * from employee where isAvailable='T' and lastname ='" + lastname + "'";
            DataSet ds = SqlHelper.ExecuteQuery(sql);
            return ds;
        }
    }
}
