using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EmployeeModel;
using System.Data;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeBll" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeBll
    {
        [OperationContract]
        void Init(String type);

        [OperationContract]
        String GetHostName();

        [OperationContract]
        Boolean Add(Employee emp);

        [OperationContract]
        Boolean Update(Employee emp);

        [OperationContract]
        Boolean Delete(String id);

        [OperationContract]
        DataSet GetAll(String asc);

        [OperationContract]
        DataSet SearchById(String id);

        [OperationContract]
        DataSet SearchByFN(String firstname);

        [OperationContract]
        DataSet SearchByLN(String lastname);    
    }
}
