using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace SQLServerUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlCrud sql = new SqlCrud(GetConnectionString());

            //ReadAllContacts(sql);

            //ReadContact(sql, 3);

            //CreateNewContact(sql);

            //UpdateContact(sql);

            //RemovePhoneNumberFromContact(sql, 1, 2);

            Console.WriteLine("done processing");
            Console.ReadLine();
        }

        private static void UpdateContact(SqlCrud sql)
        {
            BasicContactModel contact = new BasicContactModel
            {
                Id = 1,
                FirstName = "Timmy",
                LastName = "storm"
            };
            sql.UpdateContactName(contact);

        }
        private static void RemovePhoneNumberFromContact(SqlCrud sql, int contactId, int phoneNumberId)
        {
            sql.DeletePhoneNumber(contactId, phoneNumberId);
        }
        private static void CreateNewContact(SqlCrud sql)
        {
            FullContactModel user = new FullContactModel
            {
                BasicInfo = new BasicContactModel
                {
                    FirstName = "Sue",
                    LastName = "Storm"
                }
            };

            user.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "sue@gmail.com" });
            user.EmailAddresses.Add(new EmailAddressModel { Id = 2, EmailAddress = "me@gmail.com" });

            user.PhoneNumbers.Add(new PhoneNumberModel { Id = 2, PhoneNumber = "40400404" });
            user.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "55555555" });

            sql.CreateContact(user);
        }
        private static void ReadAllContacts(SqlCrud sql)
        {
            var rows = sql.GetAllContacts();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
            }
        }
        private static void ReadContact(SqlCrud sql, int contactId)
        {
            var contact = sql.GetFullContactById(contactId);

            Console.WriteLine($"{contact.BasicInfo.Id} {contact.BasicInfo.FirstName} {contact.BasicInfo.LastName}");
        }
        private static string GetConnectionString(string ConnectionStringName = "Default")
        {
            var output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(ConnectionStringName);

            return output;
        }
    }
}
