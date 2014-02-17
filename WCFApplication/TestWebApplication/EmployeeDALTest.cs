using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication;

namespace TestWebApplication
{
    [TestClass]
    public class EmployeeDALTest
    {
        IEmployeeDAL idal = new EmployeeDAL();
        [TestMethod]
        public void TestAddEmployee()
        {
            Employee emp = new Employee();
            emp.FirstName = "aaa";
            emp.LastName = "bbb";
            emp.Gender = "M";
            emp.BirthDay = "1991/01/01";
            emp.Phone = "11111111";
            emp.Address = "asdasd";
            emp.IsAvailable = "T";
            Assert.IsTrue(idal.AddEmployee(emp));
        }

        [TestMethod]
        public void TestUpdateEmployee()
        {
            Employee emp = new Employee();
            emp.FirstName = "aaa";
            emp.LastName = "bbb";
            emp.Gender = "M";
            emp.BirthDay = "1991/01/01";
            emp.Phone = "22222222";
            emp.Address = "asdasd";
            emp.IsAvailable = "T";
            Assert.IsTrue(idal.UpdateEmployee(emp));
        }

        [TestMethod]
        public void TestDeleteEmployee()
        {
            Assert.IsTrue(idal.DeleteEmployee("1"));
        }

        [TestMethod]
        public void TestGetAllEmplpoyee()
        {
            Assert.IsNotNull(idal.GetAllEmplpoyee()[0]);
        }

        [TestMethod]
        public void TestGetEmplpyeeById()
        {
            Assert.IsNotNull(idal.GetEmplpyeeById("5")[0]);
        }

        [TestMethod]
        public void TestGetEmplpyeeByFirstName()
        {
            Assert.IsNotNull(idal.GetEmplpyeeByFirstName("derk")[0]);
        }

        [TestMethod]
        public void TestGetEmplpyeeByLastName()
        {
            Assert.IsNotNull(idal.GetEmplpyeeByLastName("wang")[0]);
        }
    }
}
