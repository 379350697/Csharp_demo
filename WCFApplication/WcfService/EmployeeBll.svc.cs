using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net;
using EmployeeDAL;
using EmployeeModel;
using IEmployeeDALN;
using System.Data;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeBll" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmployeeBll.svc or EmployeeBll.svc.cs at the Solution Explorer and start debugging.
    public class EmployeeBll : IEmployeeBll
    {
        private IEmployeeDAL idal = DALInstance.GetInstance("Web");

        public void Init(String type)
        {
            //idal = DALInstance.GetInstance(type);
        }

        public String GetHostName()
        {
            return Dns.GetHostName();
        }

        public Boolean Add(Employee emp)
        {
            Boolean flag = false;
            if (idal.AddEmployee(emp) > 0)
            {
                flag = true;
            }
            return flag;
        }

        public Boolean Update(Employee emp)
        {
            Boolean flag = false;
            if (idal.UpdateEmployee(emp) > 0)
            {
                flag = true;
            }
            return flag;
        }

        public Boolean Delete(String id)
        {
            Boolean flag = false;
            if (idal.DeleteEmployee(id) > 0)
            {
                flag = true;
            }
            return flag;
        }

        public DataSet GetAll(String asc)
        {
            return idal.GetAllEmplpoyee(asc);
        }

        public DataSet SearchById(String id)
        {
            return idal.GetEmplpyeeById(id);
        }

        public DataSet SearchByFN(String firstname)
        {
            return idal.GetEmplpyeeByFirstName(firstname);
        }

        public DataSet SearchByLN(String lastname)
        {
            return idal.GetEmplpyeeByLastName(lastname);
        }
    }
}
