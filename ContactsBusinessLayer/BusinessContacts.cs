using System;

using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContact
    {
        public int ID { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryID { get; set; }
        public string ImagePath { get; set; }

        public clsContact() 
        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";
        }
         private clsContact(int ID, string FirstName, string LastName, string Email,
          string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;

        }
        static public clsContact Find(int ContactID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            int CountryID = -1;
            DateTime DateOfBirth = DateTime.Now;

            if (clsContactDataAccess.FindContactWithID(ContactID, ref FirstName,ref LastName,ref Email, ref Phone
                ,ref Address,ref DateOfBirth, ref CountryID, ref ImagePath))
            {
                return new clsContact(
                    ContactID,
                    FirstName, 
                    LastName, 
                    Email, 
                    Phone,
                    Address, 
                    DateOfBirth, 
                    CountryID, 
                    ImagePath);
            }
            return null;
        }
    }
}
