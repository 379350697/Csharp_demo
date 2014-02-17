using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeXml;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class EmployeeBllTest
    {
        EmployeeBll eb = new EmployeeBll();
        [TestMethod]
        public void TestAdd()
        {

        }

        [TestMethod]
        public void TestUpdate()
        {
            Employee emp = new Employee();
            emp.Id = 1;
            emp.LastName = "a";
            emp.FirstName = "b";
            emp.Gender = "M";
            emp.Birth = new DateTime();
            emp.Address = "aa";
            emp.Phone = "22222222";
            var update = new Mock<IEmployeeDAL>();
            update.Setup(p => p.UpdateEmployee(emp)).Returns(true);
            Assert.IsTrue(eb.Update(emp));
        }

        [TestMethod]
        public void TestDelete()
        {
            var delete = new Mock<IEmployeeDAL>();
            delete.Setup(p => p.DeleteEmployee("1")).Returns(true);
            Assert.IsTrue(eb.Delete("1"));
        }

        [TestMethod]
        public void TestShowAll()
        {

        }

        [TestMethod]
        public void TestSearchById()
        {

        }

        [TestMethod]
        public void TestSearchByFN()
        {

        }

        [TestMethod]
        public void TestSearchByLN()
        {

        }
    }
}
