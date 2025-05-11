using System;
using System.Data;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContact
    {
        private enum _enMode { Empty =0,AddNew = 1, Update=2 };
        public int ID { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryID { get; set; }
        public string ImagePath { get; set; }

        private _enMode _Mode;
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

            _Mode = _enMode.AddNew;
        }
         private clsContact(int ID, string FirstName, string LastName, string Email,
                                 string Phone, string Address, DateTime DateOfBirth, 
                                 int CountryID, string ImagePath)
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

            _Mode = _enMode.Update;
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

        private bool _AddNewContact()
        {
            this.ID = clsContactDataAccess.AddNewContact(this.FirstName,this.LastName,this.Email, this.Phone,
                this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
            return (this.ID != -1);
        }
        private bool _UpdateContact()
        {
            return clsContactDataAccess.UpdateContact(this.ID,this.FirstName, this.LastName, this.Email, this.Phone,
                this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
        }
        static public bool IsContactExist(int contactID)
        {
            return clsContactDataAccess.ExistContact(contactID);
        }

        static public bool DeleteContact(int contactID)
        {
            return clsContactDataAccess.DeleteContactWithID(contactID);
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case _enMode.AddNew:
                    if (_AddNewContact())
                    {
                        _Mode = _enMode.Empty;
                        return true;                        
                    };
                    return  false;
                case _enMode.Update:

                    if (_UpdateContact())
                    {
                        _Mode = _enMode.Empty;
                        return true;
                    }
                    return false;

            }
            return false;
        }

        static public DataTable GetAllContacts()
        {
            return clsContactDataAccess.GetAllContacts();
        }
    }
}
