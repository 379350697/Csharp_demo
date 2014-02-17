using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeModel
{
    public class Employee
    {
        private int id;
        private String firstName;
        private String lastName;
        private String gender;
        private String birthday;
        private String address;
        private String phone;
        private String isAvailable;

        public String IsAvailable
        {
            get { return isAvailable; }
            set { isAvailable = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public String Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public String BirthDay
        {
            get { return birthday; }
            set { birthday = value; }
        }
        public String Address
        {
            get { return address; }
            set { address = value; }
        }

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }
    }
}

