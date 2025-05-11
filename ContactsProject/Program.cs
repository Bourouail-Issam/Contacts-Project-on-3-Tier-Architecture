using System;

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
        static void Main(string[] args)
        {
            //for (int i = 1; i < 13; i++)
            //{
            //    Console.WriteLine("------------------------");
            //    TestFindContact(i);
            //    Console.WriteLine("");
            //}

            TestAddNewContact();

        }    
    }
}
