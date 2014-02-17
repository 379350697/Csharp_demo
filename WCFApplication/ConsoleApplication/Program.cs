using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections;
using System.Reflection;
using EmployeeModel;

namespace ConsoleProgram
{
    class EmployeeManagement
    {
        public int id = 0;
        public List<Employee> emps = new List<Employee>();

        public void addEmp()
        {
            id++;
            Employee emp = new Employee();
            emp.Id = id;
            Console.WriteLine("employee id:" + emp.Id);
            Console.Write("please enter employee lastname:");
            emp.LastName = Console.ReadLine();
            Console.Write("please enter employee firstname:");
            emp.FirstName = Console.ReadLine();
            this.validateGender(emp);
            validateBirth(emp);
            Console.Write("please enter employee address:");
            emp.Address = Console.ReadLine();
            validatePhone(emp);
            emps.Add(emp);
            this.showMessage("add successfully!", false);
        }

        private void validatePhone(Employee emp)
        {
            Console.Write("please enter employee phonenumber:");
            emp.Phone = Console.ReadLine();
            while (!Regex.IsMatch(emp.Phone, @"^\d{11}|\d{8}$"))
            {
                this.showMessage("wrong phonenumber fotmat,the length of phonenumber should be 11 or 8!", true);
                Console.Write("please enter employee phone:");
                emp.Phone = Console.ReadLine();
            }
        }

        private void validateBirth(Employee emp)
        {
            DateTime temp;
            Console.Write("please enter employee birth(eg:1990/11/11):");
            String date = Console.ReadLine();
            if (date.LastIndexOf("/") - date.IndexOf("/") < 3)
            {
                date = date.Insert(date.IndexOf("/") + 1, "0");
            }
            if (date.Length - date.LastIndexOf("/") < 3)
            {
                date = date.Insert(date.LastIndexOf("/") + 1, "0");
            }
            while (!DateTime.TryParseExact(date, "yyyy/MM/dd", null,
            System.Globalization.DateTimeStyles.None, out temp))
            {
                this.showMessage("wrong date fotmat!", true);
                Console.Write("please enter employee birth(eg:1990/11/11):");
                date = Console.ReadLine();
            }
            emp.BirthDay = temp.ToString();
        }

        private void validateGender(Employee emp)
        {
            Console.Write("please enter employee gender:(M – Male; F – Female)");
            emp.Gender = Console.ReadLine().ToUpper();
            while (!("M".Equals(emp.Gender) || "F".Equals(emp.Gender)))
            {
                this.showMessage("Wrong enters!Please enter M or F,M for male,F for female.", true);
                Console.Write("please enter employee gender:(M – Male; F – Female):");
                emp.Gender = Console.ReadLine().ToUpper();
            }
        }

        public void showMenu()
        {
            Console.WriteLine("[1].display all existing employees");
            Console.WriteLine("[2].add new employees");
            Console.WriteLine("[3].search existing employee info");
            Console.WriteLine("[4].update employee info");
            Console.WriteLine("[5].delete employee");
            Console.Write("Please enter a number to perform operations:");
        }

        public void searchEmpById()
        {
            Console.Write("please enter a id:");
            int empid = System.Int32.Parse(Console.ReadLine());
            Employee emp = emps.ElementAt(empid - 1);
            if (emp != null)
            {
                this.showEmp(emp);
            }
            else
            {
                this.showMessage("There is no employee who's id is " + empid, true);
            }
        }

        public void searchEmpsByFirstName()
        {
            Console.Write("please enter a firstname:");
            String firstname = Console.ReadLine();
            List<Employee> list = this.getEmpsByname(firstname, true);
            this.showEmps(firstname, list);
        }

        public void searchEmpsByLastName()
        {
            Console.Write("please enter a lastname:");
            String lastname = Console.ReadLine();
            List<Employee> list = this.getEmpsByname(lastname, false);
            this.showEmps(lastname, list);
        }

        public void showEmps(String name, List<Employee> list)
        {
            if (list.Count == 0)
            {
                this.showMessage("There is no employee who's name is " + name, true);
            }
            else
            {
                Console.Clear();
                foreach (Employee emp in list)
                {
                    Console.WriteLine("*****************");
                    this.showEmp(emp);
                }
            }
        }

        public void showEmp(Employee emp)
        {
            String gender = null;
            if (emp.Gender.Equals("M"))
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
            Console.WriteLine("Employee        id:" + emp.Id);
            Console.WriteLine("Employee firstname:" + emp.FirstName);
            Console.WriteLine("Employee  lastname:" + emp.LastName);
            Console.WriteLine("Employee    gender:" + gender);
            Console.WriteLine("Employee     birth:" + emp.BirthDay);
            Console.WriteLine("Employee   address:" + emp.Address);
            Console.WriteLine("Employee     phone:" + emp.Phone);
        }

        public List<Employee> getEmpsByname(String name, Boolean first)
        {
            List<Employee> list = new List<Employee>();
            Employee emp = null;
            for (int i = 1; i <= emps.Count; i++)
            {
                emp = emps.ElementAt(i - 1);
                if (first)
                {
                    if (name.Equals(emp.FirstName))
                    {
                        list.Add(emp);
                    }
                }
                else
                {
                    if (name.Equals(emp.LastName))
                    {
                        list.Add(emp);
                    }
                }
            }
            return list;
        }

        public void updateEmp()
        {
            Console.Write("Please enter the employee id:");
            int empid = System.Int32.Parse(Console.ReadLine());
            Employee emp = emps.ElementAt(empid - 1);
            if (emp != null)
            {
                this.validateBirth(emp);
                Console.Write("please enter employee address:");
                emp.Address = Console.ReadLine();
                this.validatePhone(emp);
                emps.RemoveAt(empid - 1);
                emps.Insert(empid - 1, emp);
                this.showMessage("update successfully!", false);
            }
            else
            {
                this.showMessage("Do not have this employee!", true);
            }

        }

        public void deleteEmp()
        {
            Console.Write("Please enter the employee id:");
            int empid = System.Int32.Parse(Console.ReadLine());
            if (emps.ElementAt(empid) != null)
            {
                emps.RemoveAt(empid - 1);
                this.showMessage("delete successfully!", false);
            }
            else
            {
                this.showMessage("Do not have this employee!", true);
            }

        }

        public void showMessage(String message, Boolean isRed)
        {
            if (isRed)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void operation()
        {
            String exit = null;
            Console.WriteLine("Hello " + Dns.GetHostName() + ", welcome to Employee Management System");
            Console.ReadLine();
            Console.Clear();
            while (!"exit".Equals(exit))
            {
                this.showMenu();
                String num = Console.ReadLine();
                Console.Clear();
                switch (num)
                {
                    case "1": Console.WriteLine("all existing employees:" + emps.Count); break;
                    case "2": this.addEmp(); break;
                    case "3":
                        Console.WriteLine("[1].search by id");
                        Console.WriteLine("[2].search by firstname");
                        Console.WriteLine("[3].search by lastname");
                        String search = Console.ReadLine();
                        Console.Clear();
                        switch (search)
                        {
                            case "1": this.searchEmpById(); break;
                            case "2": this.searchEmpsByFirstName(); break;
                            case "3": this.searchEmpsByLastName(); break;
                            default: Console.WriteLine("Do not have this option"); break;
                        }
                        break;
                    case "4": this.updateEmp(); break;
                    case "5": this.deleteEmp(); break;
                    default: Console.WriteLine("Do not have this option"); break;
                }
                Console.WriteLine("-----------------");
                Console.WriteLine("Please press enter-key to go back to the upper level!");
                Console.WriteLine("Or you can type exit to close system!");
                exit = Console.ReadLine();
                Console.Clear();
            }
        }

        static void Main(string[] args)
        {
            EmployeeManagement run = new EmployeeManagement();
            run.operation();
        }
    }
}
