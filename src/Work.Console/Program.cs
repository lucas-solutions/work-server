using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    using Lucas.Solutions.Configuration;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new ConfigurationContainer();

            Console.WriteLine("Hosts:");
            foreach (var host in container.Hosts)
            {
                Console.WriteLine("* Host (Name: {0}, Address: {1}, Port: {2}, Protocol: {3})", host.Name, host.Address, host.Port, host.Protocol);
            }
            Console.WriteLine();
            Console.WriteLine("Users:");
            foreach (var user in container.Users)
            {
                Console.WriteLine("* User (Email: {0}, Roles: {1})", user.Email, string.Join(", ", user.Roles.Select(role => role.Name)));
            }
            Console.WriteLine();
            Console.WriteLine("Roles:");
            foreach (var role in container.Roles)
            {
                Console.WriteLine("* Role (Name: {0})", role.Name);
            }
            Console.WriteLine();
            Console.WriteLine("Transfers:");
            foreach (var transfer in container.Transfers)
            {
                Console.WriteLine("* Transfer (Name: {0}, Start: {1}, Parties:", transfer.Name, transfer.Start);
                foreach (var party in transfer.Parties)
                {
                    Console.WriteLine("    * Party (Name: {0}, Direction: {1}, Email: {2}, Host: {3})", party.Name, party.Direction, party.Email, party.Host);
                }
                Console.WriteLine("  )");
            }
            Console.WriteLine();

            Console.WriteLine("Enter any key:");
            Console.ReadKey();
        }
    }
}
