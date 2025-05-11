using System;
using System.Data;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using ContactsBusinessLayer;

namespace ContactsProject
{
    internal class Program
    {
        static  void PrintContactInfo(clsContact contact)
        {
            Console.WriteLine($"ContactID   : {contact.ID}");
            Console.WriteLine($"FirstName   : {contact.FirstName}");
            Console.WriteLine($"LastName    : {contact.LastName}");
            Console.WriteLine($"Email       : {contact.Email}");
            Console.WriteLine($"Phone       : {contact.Phone}");
            Console.WriteLine($"Address     : {contact.Address}");
            Console.WriteLine($"DateOfBirth : {contact.DateOfBirth}");
            Console.WriteLine($"CountryID   : {contact.CountryID}");
            Console.WriteLine($"ImagePath   : {contact.ImagePath}");
        }
        static void TestFindContact(int ContactID)
        {
            clsContact contact = clsContact.Find(ContactID);

            if (contact != null)
            {
                PrintContactInfo(contact);
            }
            else
            {
                Console.WriteLine($"Contact with ID = {ContactID} Not Found");
            }
        }
        static void ReadContact(ref clsContact contact)
        {
            Console.Write("Please Enter FirstName : ");
            while (true)
            {
                if ((contact.FirstName = Console.ReadLine()) != "")
                    break;
                Console.Write("We dont Accept empty value Please Enter FirstName : ");
            }

            Console.Write("Please Enter LastName : ");
            while (true)
            {
                if ((contact.LastName = Console.ReadLine()) != "")
                    break;
                Console.Write("We dont Accept empty value Please Enter LastName : ");
            }

            Console.Write("Please Enter Email : ");
            while (true)
            {
                if ((contact.Email = Console.ReadLine()) != "")
                    break;
                Console.Write("We dont Accept empty value Please Enter Email : ");
            }

            Console.Write("Please Enter a Phone : ");
            while (true)
            {
                if ((contact.Phone = Console.ReadLine()) != "")
                    break;
                Console.Write("We dont Accept empty value Please Enter a Phone : ");
            }

            Console.Write("Please Enter an Address : ");
            while (true)
            {
                if ((contact.Address = Console.ReadLine()) != "")
                    break;
                Console.Write("We dont Accept empty value Please Enter an Address : ");
            }


            Console.Write("Enter DateOfBirth (Example: 2000-05-07) : ");
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    contact.DateOfBirth = date;
                    break;
                }
                Console.Write("you need to enter rigth Date (Example: 2000-05-07) : ");
            }

            Console.Write("Enter CountryID  : ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int countryid))
                {
                    contact.CountryID = countryid;
                    break;
                }
                Console.Write("We dont Accept empty value Please Enter a CountryID : ");
            }

            Console.Write("Please Enter ImagePath : ");
            contact.ImagePath = Console.ReadLine();
        }
        static void TestAddNewContact()
        {
            clsContact newConatct = new clsContact();
            ReadContact(ref newConatct);

            if(newConatct.Save())
            {
                Console.WriteLine($"\nAdding New Contact is successfullt with ID = {newConatct.ID}");
            }
            else
            {
                Console.WriteLine("Adding New Contact is Failed");
            }
        }
        static void TestUpdateContact(int contactID)
        {
            clsContact contact = clsContact.Find(contactID);
            if (contact != null)
            {
                PrintContactInfo(contact);
                
                Console.Write("Do you wanat Update This Enter 1 if yes , 2 if No : ");
                short isUpdate = 0;
                while(true)
                {
                    if(short.TryParse(Console.ReadLine(), out short number))
                    {
                        if (number == 1 || number ==2)
                        {
                           isUpdate = number;
                           break;
                        }
                    }
                    Console.Write(@"We do no Accept any value other than (1,2) Please Enter 1 if yes , 2 if No : ");
                }

                if(isUpdate == 1)
                {
                    ReadContact(ref contact);
                    if(contact.Save())
                        Console.WriteLine($"Updating Contact is successfullt");
                    else
                        Console.WriteLine($"Updating Contact is Failed");
                }
            }
            else
                Console.WriteLine($"contact with ID = {contactID} Not Found");

        }
        static void TestIsExistContact(int contactID)
        {
            if (clsContact.IsContactExist(contactID))
                Console.WriteLine("Yes , contact is there");
            else
                Console.WriteLine("No, Contact Is Not there");
        }
        static void TestDeleteContact(int ContactID)
        {
            Console.Write("Do you wanat Delete This Enter 1 if yes , 2 if No : ");
            short isDeleted = 0;
            while (true)
            {
                if (short.TryParse(Console.ReadLine(), out short number))
                {
                    if (number == 1 || number == 2)
                    {
                        isDeleted = number;
                        break;
                    }
                }
                Console.Write(@"We do no Accept any value other than (1,2) Please Enter 1 if yes , 2 if No : ");
            }
            if (isDeleted == 1)
            {
                if (clsContact.IsContactExist(ContactID))
                {
                    if (clsContact.DeleteContact(ContactID))
                        Console.WriteLine("Deleting Contact successfullt ");
                    else
                        Console.WriteLine("Deletingh Contact is Failed ");
                }
                else
                    Console.WriteLine($"The Contact with ID = {ContactID} is Not Found");
            }
        }

        static void TestGetAllContacts()
        {
            DataTable contacts = clsContact.GetAllContacts();

            if (contacts != null)
            {
                Console.WriteLine("{0,-10} | {1,-10} | {2,-12} | {3,-25} | {4,-15} | {5,-25} | {6,-25} | {7,-10} | {8,-30}",
                    "ContactID","FirstName", "LastName", "Email", "Phone", "Address", "DateOfBirth", "CountryID", "ImagePath");
                foreach (DataRow row in contacts.Rows)
                {
                    Console.WriteLine("{0,-10} | {1,-10} | {2,-12} | {3,-25} | {4,-15} | {5,-25:yyyy-mm-dd} | {6,-25} | {7,-10} | {8,-30}",
                                       row["ContactID"]?.ToString() ?? "N/A",
                                       row["FirstName"]?.ToString() ?? "N/A",
                                       row["LastName"]?.ToString() ?? "N/A",
                                       row["Email"]?.ToString() ?? "N/A",
                                       row["Phone"]?.ToString() ?? "N/A",
                                       row["Address"]?.ToString() ?? "N/A",
                                       row["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(row["DateOfBirth"]).ToString("yyyy,mm,dd"): "N/A",
                                       row["CountryID"]?.ToString() ?? "N/A",
                                       (row["ImagePath"] != DBNull.Value) && (!string.IsNullOrWhiteSpace(row["ImagePath"].ToString()))
                                       ? row["ImagePath"].ToString()
                                       :"N/A"
                                       );
                }
            }
            else
                Console.WriteLine("Empty Contacts");
        }
        static void Main(string[] args)
        {
            //for (int i = 1; i < 13; i++)
            //{
            //    Console.WriteLine("------------------------");
            //    TestFindContact(i);
            //    Console.WriteLine("");
            //}

            //TestAddNewContact();
            //TestUpdateContact(15);
            //TestUpdateContact(25);
            //TestIsExistContact(15);
            //TestIsExistContact(25);
            //TestDeleteContact(15);
            //TestDeleteContact(25);
            TestGetAllContacts();
        }   
        
    }
}
