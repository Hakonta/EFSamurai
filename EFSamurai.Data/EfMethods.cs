using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFSamurai
{
    public static class EfMethods
    {
        public static Samurai GetSamurai(int samuraiId)
        {

            using (var context = new SamuraiContext())
            {
                var samurai = context.Samurais
                    .Include(s => s.SecretIdentity)
                    .Where(s => s.Id == samuraiId);
                foreach (var s in samurai)
                {
                    return s;
                }
            }

            return null;
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
                    Console.WriteLine(s.Name);
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
