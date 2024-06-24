using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace SqliteUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqliteCrud sql = new SqliteCrud(GetConnectionString());

            //ReadAllContacts(sql);

            //ReadContact(sql, 2);

            //CreateNewContact(sql);

            //UpdateContact(sql);

            //RemovePhoneNumberFromContact(sql, 1, 2);

            Console.WriteLine("done processing Sqlite");
            Console.ReadLine();
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

        private static void UpdateContact(SqliteCrud sql)
        {
            BasicContactModel contact = new BasicContactModel
            {
                Id = 1,
                FirstName = "Timmy",
                LastName = "storm"
            };
            sql.UpdateContactName(contact);

        }
        private static void RemovePhoneNumberFromContact(SqliteCrud sql, int contactId, int phoneNumberId)
        {
            sql.DeletePhoneNumber(contactId, phoneNumberId);
        }
        private static void CreateNewContact(SqliteCrud sql)
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
        private static void ReadAllContacts(SqliteCrud sql)
        {
            var rows = sql.GetAllContacts();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
            }
        }
        private static void ReadContact(SqliteCrud sql, int contactId)
        {
            var contact = sql.GetFullContactById(contactId);

            Console.WriteLine($"{contact.BasicInfo.Id} {contact.BasicInfo.FirstName} {contact.BasicInfo.LastName}");
        }
    }
}
