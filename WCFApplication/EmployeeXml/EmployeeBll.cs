using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EmployeeDAL;
using EmployeeModel;
using IEmployeeDALN;

namespace EmployeeXml
{
    class EmployeeBll
    {
        IEmployeeDAL idal = new EmployeeXmlDAL();
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
