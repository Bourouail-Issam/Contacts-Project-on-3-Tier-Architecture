using System;

using ContactsBusinessLayer;

namespace ContactsProject
{
    internal class Program
    {
        static public void PrintContactInfo(clsContact contact)
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
        static void Main(string[] args)
        {
            for (int i = 1; i < 13; i++)
            {
                Console.WriteLine("------------------------");
                TestFindContact(i);
                Console.WriteLine("");
            }
        }    
    }
}
