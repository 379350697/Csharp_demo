using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EmployeeXml;
using System.IO;
using System.Xml;
using System.Collections;

namespace UnitTest
{

    [TestClass]
    public class EmployeeDalTest
    {

        IEmployeeDAL edl = new EmployeeDAL();
        XmlDocument xmlDoc = null;
        string xmlFilePath = "../../temp.xml";

        public int getNodeCount()
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("employees");
            int count = root.ChildNodes.Count;
            return count;
        }

        [TestMethod]
        public void TestCreateXml()
        {

        }

        [TestMethod]
        public void TestAddEmployee()
        {

            int count = this.getNodeCount();
            Employee emp = new Employee();
            emp.Id=count+1;
            emp.LastName ="a";
            emp.FirstName="b";
            emp.Gender="M";
            emp.Birth=new DateTime();
            emp.Address="aa";
            emp.Phone = "11111111";
            edl.AddEmployee(emp);
            Assert.AreEqual(count, this.getNodeCount() - 1);
        }

        [TestMethod]
        public void TestUpdateEmployee()
        {
            int count = this.getNodeCount();
            Employee emp = new Employee();
            emp.Id = count-1;
            emp.LastName = "a";
            emp.FirstName = "b";
            emp.Gender = "M";
            emp.Birth = new DateTime();
            emp.Address = "aa";
            emp.Phone = "22222222";
            edl.UpdateEmployee(emp);
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("employees");
            Assert.AreEqual("22222222", root.LastChild.Attributes[1].Value);
        }

        [TestMethod]
        public void TestDeleteEmployee()
        {
            int count = this.getNodeCount();
            edl.DeleteEmployee((count - 1).ToString());
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("employees");
            Assert.AreEqual("false", root.LastChild.Attributes[0].Value);
        }
        [TestMethod]
        public void TestGetEmpHashtable()
        {
            Employee emp = new Employee();
            emp.Id = 1;
            emp.LastName = "a";
            emp.FirstName = "b";
            emp.Gender = "M";
            emp.Birth = new DateTime();
            emp.Address = "aa";
            emp.Phone = "22222222";
            Hashtable ht = edl.GetEmpHashtable(emp);
            Assert.AreEqual("1", ht["id"].ToString());
        }

        [TestMethod]
        public void TestGetAllEmplpoyee()
        {
            Assert.IsNotNull(edl.GetAllEmplpoyee()[0]);
        }

        [TestMethod]
        public void TestGetEmplpyeeById()
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("employees");
            Assert.IsNotNull(edl.GetEmplpyeeById("5")[0]);
        }

        [TestMethod]
        public void TestGetEmplpyeeByFirstName()
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("employees");
            Assert.IsNotNull(edl.GetEmplpyeeByFirstName(root.LastChild.Attributes[6].Value)[0]);
        }

        [TestMethod]
        public void TestGetEmplpyeeByLastName()
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("employees");
            Assert.IsNotNull(edl.GetEmplpyeeByLastName(root.LastChild.Attributes[5].Value)[0]);
        }
    }
}
