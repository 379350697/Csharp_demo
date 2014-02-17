using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using EmployeeModel;

namespace IEmployeeDALN
{
    public interface IEmployeeDAL
    {

        int AddEmployee(Employee emp);

        int UpdateEmployee(Employee emp);

        int DeleteEmployee(String id);

        DataSet GetAllEmplpoyee(String asc);

        DataSet GetEmplpyeeById(String id);

        DataSet GetEmplpyeeByFirstName(String firstname);

        DataSet GetEmplpyeeByLastName(String lastname);
    }
}
