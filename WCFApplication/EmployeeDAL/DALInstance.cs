using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEmployeeDALN;

namespace EmployeeDAL
{
    public class DALInstance
    {
        public static IEmployeeDAL GetInstance(String type)
        {
            if ("Console".Equals(type))
            {
                return new EmployeeWebDAL();
            }
            else if ("WinForm".Equals(type))
            {
                return new EmployeeWebDAL();
            }
            else if ("Web".Equals(type))
            {
                return new EmployeeWebDAL();
            }
            else
            {
                return null;
            }
        }
    }
}
