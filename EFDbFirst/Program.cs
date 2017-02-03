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
            Console.WriteLine("Update:");
            using (var context = new SucientlyExamplesEntities())
            {
                ////Update sandra
                //Person sandra = context.People.FirstOrDefault(p => p.FirstName == "Sandra");
                //sandra.FirstName = "John";

                ////create Google ceo
                //Person sunder = new Person();
                //sunder.FirstName = "Sundar";
                //sunder.LastName = "Pichai";
                //sunder.DateOfBirth = new DateTime(1972, 7, 12);
                //sunder.Genderld = 1;
                //context.People.Add(sunder);

                ////remove Steve Jobs
                ////Person jobs = context.People.FirstOrDefault(p => p.LastName == "Jobs");
                ////context.People.Remove(jobs);
                ////context.SaveChanges();
            }

            using (var context = new SucientlyExamplesEntities())
            {
                //using annonymous type
                var anon = context.People.Where(p => p.LastName == "Jacobs")
                    .Select(p => new
                    {
                        Id = p.id,
                        FirstName = p.FirstName,
                        LastName = p.LastName
                    }).SingleOrDefault();
                Console.WriteLine($"Annonymous type {anon.FirstName} {anon.LastName}");

                //PersonModel known = context.People.Where(p => p.FirstName == "Emma")
                //    .Select(p => new Person
                //    {
                //        id = p.id,
                //        FirstName = p.FirstName,
                //        LastName = p.LastName,
                //        DateOfBirth = p.DateOfBirth
                //    }).SingleOrDefault();
                //Console.WriteLine($"Known type {known.id} {known.FirstName}");
            }

            using (var context = new SucientlyExamplesEntities())
            {
                var sander = context.People.Where(p => p.FirstName == "Sundar")
                    .Select(p => new { Id = p.id }).SingleOrDefault();
                //update
                Person update = new Person();
                update.id = sander.Id;
                context.People.Attach(update);
                update.FirstName = "Tom";
                //delete
                var bill = context.People.Where(p => p.FirstName == "Bill")
                    .Select(p => new { Id = p.id }).SingleOrDefault();
                Person delete = new Person();
                delete.id = bill.Id;
                context.People.Attach(delete);
                context.People.Remove(delete);
                context.SaveChanges();
            }
                Console.ReadKey();
        }
    }
}
