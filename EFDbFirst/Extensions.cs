using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDbFirst
{
    public static class Extensions
    {
        public static IQueryable<Person> OlderThan(this IQueryable<Person> q,
            int age)
        {
            return q
                .Where(p => (DbFunctions.DiffHours(p.DateOfBirth, DateTime.Today) / 8766) > age);
        }

        public static IQueryable<T> OlderThan<T>(this IQueryable<T> q, int age)
            where T:class,IDateOfBirth
        {
            return q
            .Where(p => (DbFunctions.DiffHours(p.DateOfBirth, DateTime.Today) / 8766) > age);
        }
    }
}
