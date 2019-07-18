using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace EFSamurai
{
    public static class EfMethods
    {

        public static string ListAllBattlesWithinPeriod(DateTime fromTime, DateTime toTime, bool? isBrutal)
        {
            using (var context = new SamuraiContext())
            {
                var withinPeriod = context.SamuraiBattles
                    .Include(sb => sb.Samurai)
                    .ThenInclude(s => s.SecretIdentity)
                    .Include(sb => sb.Battle)
                    .Where(s => s.Battle.EndDate >= fromTime && s.Battle.StartDate <= toTime);

                foreach (var s in withinPeriod)
                {
                    return $"{s.Samurai.Name} alias {s.Samurai.SecretIdentity.RealName} fought in {s.Battle.Name}";
                }

                return null;
            }



        }

        public static Samurai GetSamurai(int samuraiId)
        {

            using (var context = new SamuraiContext())
            {
                return context.Samurais
                    .Include(s => s.SecretIdentity)
                    .First(s => s.Id == samuraiId); // .First makes this into [Samurai]
            }
        }

        public static void UpdateSamuraiSetSecretIdentity(int samuraiId, string name)
        {
            using (var context = new SamuraiContext())
            {
                var samurai = context.Samurais
                    .Include(s => s.SecretIdentity)
                    .Where(s => s.Id == samuraiId)
                    .ToList();
                foreach (var s in samurai)
                {
                    s.SecretIdentity = new SecretIdentity();
                    s.SecretIdentity.RealName = name;
                        context.Samurais.Update(s);
                        context.SaveChanges();
                }
            }
        }
        public static int AddOneSamurai(string name)
        {
            var samurai = new Samurai()
            {
                Name = name
            };
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
                return samurai.Id;
            }

        }
        public static int AddOneSamurai(Samurai samurai)
        {
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
                return samurai.Id;
            }

        }

        public static string[] GetAllSamuraiNames()
        {
            string[] arrayOfNames;
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais
                    .Select(s => s.Name)
                    .ToArray();
                arrayOfNames = samurais;
            }
            return arrayOfNames;
        }
    }
}
