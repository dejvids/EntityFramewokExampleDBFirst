using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EFDbFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SucientlyExamplesEntities())
            {
                List<Person> people = context.People.ToList();
                foreach (Person person in people)
                    Console.WriteLine(person.FirstName);
            }
            Console.WriteLine("Women:");
            using (var context = new SucientlyExamplesEntities())
            {
                IQueryable<Person> query = context.People
                    .Where(p => p.Genderld == 2)
                    .OrderBy(p => p.LastName);
                List<Person> women = query.ToList();
                foreach (var p in query)
                    Console.WriteLine(p.LastName);
            }
            Console.WriteLine("Mens:");
            using (var context = new SucientlyExamplesEntities())
            {
                Gender g = context.Genders.Find(1);
                
                    foreach(Person p in g.People)
                    {
                        Console.WriteLine($"{p.FirstName} is {g.Description}.");
                    }
                
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine("Using extension class");
            using (var context = new SucientlyExamplesEntities())
            {
                List<Person> people = context.People.OlderThan(45).ToList();
                foreach (var p in people)
                    Console.WriteLine($"First name: {p.FirstName} Last name: {p.LastName}");
            }
            Console.WriteLine("Using generic extension people older than 20");
            using (var context = new SucientlyExamplesEntities())
            {
                List<Person> people = context.People.OlderThan<Person>(20).ToList();
                foreach (var p in people)
                    Console.WriteLine($"{p.FirstName} {p.LastName} {p.DateOfBirth}");
            }
                Console.ReadKey();
        }
    }
}
