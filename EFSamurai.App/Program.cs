using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http.Headers;
using ConsoleTables.Core;
using Remotion.Linq.Clauses;

namespace EFSamurai
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddOneSamurai("Zelda");
            //AddSomeSamurais("Leonardo", "Donatello", "Michelangelo");
            //AddSomeBattles();
            //AddOneSamuraiWithRelatedData();
            // ClearDatabase();
            // ListAllSamuraiNames();
            // ListAllSamuraiNames_OrderByDescending();
            // FindSamurainWithRealName("Splinter");
            // ListQuoteAndSamurai(QuoteStyle.Cheesy);
            // ListQuoteAndSamurai(QuoteStyle.Cheesy); // Uses join
            // ListAllBattles(new DateTime(1515 - 01 - 01), new DateTime(1900 - 01 - 01), true);
            // ListAllBattlesWithinPeriod(new DateTime(1515 - 01 - 01), new DateTime(1900 - 01 - 01), null);
            // var listOfAliasesAndNames = AllSamurainNamesWithAliases();
            // ListAllBattles_WithLog(new DateTime(1400 - 01 - 1), new DateTime(1900 - 01 - 01), true);
            // FindSamuraiWithRealName_WithDifferentQuery("Splinter"); // Join/Include with Entity Queries
            // ListAllSamuraiNames_WithLinq();
             //var listOfSamuraiInfo = GetSamuraiInfo();
             //PrintSamuraiInfo(listOfSamuraiInfo);

        }

        private static void PrintSamuraiInfo(ICollection<SamuraiInfo> listOfSamurais)
        {
            var table = new ConsoleTable("Name", "RealName", "Battles");

            foreach (var samurai in listOfSamurais)
            {
                table.AddRow(samurai.Name, samurai.RealName, samurai.BattleNames);
            }
            Console.WriteLine(table);
        }

        private static ICollection<SamuraiInfo> GetSamuraiInfo()
        {
            List<SamuraiInfo> listOfSamuraiInfo = new List<SamuraiInfo>();

            using (var context = new SamuraiContext())
            {
                var samuraisInformation =
                    context.SamuraiBattles
                        .Include(sb => sb.Samurai)
                        .ThenInclude(s => s.SecretIdentity)
                        .Include(b => b.Battle)
                        .ToList();
                foreach (var s in samuraisInformation)
                {
                    SamuraiInfo samuraiInfo = new SamuraiInfo();
                    samuraiInfo.Name = s.Samurai.Name;
                    samuraiInfo.RealName = s.Samurai.SecretIdentity.RealName;
                    samuraiInfo.BattleNames = s.Battle.Name;
                    listOfSamuraiInfo.Add(samuraiInfo);
                }
            }

            return listOfSamuraiInfo;

        }

        private static void ListAllBattles_WithLog(DateTime from, DateTime to, bool isBrutal)
        {
            using (var context = new SamuraiContext())
            {
                var battlesAndLogs =
                    from s in context.BattleEvents
                    join Battle in context.Battles on s.BattleLogId equals Battle.Id
                    where Battle.IsBrutal == isBrutal
                    select new {s.Summary, Battle.Name};
                var table = new ConsoleTable("Name of Battle", "Summary");

                foreach (var battle in battlesAndLogs)
                {
                    table.AddRow(battle.Name, battle.Summary);
                }
                Console.WriteLine(table);

            }

        }

        private static ICollection<string> AllSamurainNamesWithAliases()
        {
            {
                List<string> listOfSamurais = new List<string>();

                using (var context = new SamuraiContext())
                {
                    var samurais =
                        from s in context.SecretIdentities
                        join Samurai in context.Samurais on s.SamuraiID equals Samurai.Id
                        select new {s.RealName, Samurai.Name};
                    foreach (var name in samurais)
                    {
                        listOfSamurais.Add($"{name.RealName} alias {name.Name}");
                    }
                }
                return listOfSamurais;
            }
        }

        private static void ListAllBattlesWithinPeriod(DateTime fromTime, DateTime toTime, bool? isBrutal)
        {
            using (var context = new SamuraiContext())
            {
                var withinPeriod =
                    from s in context.Battles
                    where s.EndDate <= toTime && s.StartDate <= toTime
                    select s;
                if (withinPeriod.Count() == 0)
                {
                    Console.WriteLine("Didn't find any battles within this time period");
                }

                Console.WriteLine("Found the following battles: ");
                if (isBrutal == null)
                    foreach (var battle in withinPeriod)
                    {
                        Console.WriteLine(battle.Name);
                    }
                else if (isBrutal == true)
                {
                    foreach (var battle in withinPeriod)
                    {
                        if (battle.IsBrutal)
                        {
                            Console.WriteLine(battle.Name);
                        }
                    }
                }
                else if (isBrutal == false)
                {
                    foreach (var battle in withinPeriod)
                    {
                        if (battle.IsBrutal == false)
                        {
                            Console.WriteLine(battle.Name);
                        }
                    }
                }
            }

        }

        private static void ListAllBattles(DateTime fromTime, DateTime toTime, bool? IsBrutal)
        {
            using (var context = new SamuraiContext())
            {
                var withinPeriod = 
                    from s in context.Battles
                    where s.EndDate <= toTime && s.StartDate >= toTime
                    select s;
                if (withinPeriod.Count() == 0)
                {
                    var newQuery =
                        from s in context.Battles
                        where s.IsBrutal.Equals(true)
                        select s;
                    if (newQuery.Count() == 0)
                    {
                        Console.WriteLine("No battles were found.");
                    }
                    else
                    {
                        Console.WriteLine("Found the following battles: ");
                        foreach (var battle in newQuery)
                        {
                            Console.WriteLine(battle.Name);
                        }
                    }
                
                }
                else
                {
                    Console.WriteLine("Found the following battles: ");
                    foreach (var battle in withinPeriod)
                    {
                        Console.WriteLine(battle.Name);
                    }
                }
            }

        }

        private static void ListAllQuotesOfType(QuoteStyle quoteStyle)
        {
            using (var context = new SamuraiContext())
            {
                var quotes = from s in context.Quotes
                    where s.Quality == quoteStyle
                    select s;
                foreach (var quote in quotes)
                {
                    Console.WriteLine(quote.Text);
                }
            }
        }

        private static void ListQuoteAndSamurai(QuoteStyle quoteStyle)
        {
            using (var context = new SamuraiContext())
            {
                var quotes = from s in context.Quotes
                    join Samurai in context.Samurais on s.SamuraiId equals Samurai.Id
                    where s.Quality.Equals(quoteStyle)
                    select new {s.Text, Samurai.Name};

                foreach (var quote in quotes)
                {
                    Console.WriteLine($"\"{quote.Text}\" is a {quoteStyle} quote by {quote.Name}");
                }
            }
        }

        private static void FindSamuraiWithRealName_WithDifferentQuery(string name)
        {
            using (var context = new SamuraiContext())
            {
                var samuraiIdentities =
                    context.Samurais
                        .Include(s => s.SecretIdentity)
                        .Where(n => n.Name.Contains(name))
                        .ToList();
                foreach (var samurai in samuraiIdentities)
                {
                    Console.WriteLine($"{samurai.Name}'s real name is {samurai.SecretIdentity.RealName}");
                }
            }
        }

        private static void FindSamurainWithRealName(string name)
        {
            using (var context = new SamuraiContext())
            {
                var secretNumber = 0;
                var nameQuery = context.Samurais.Where(s => s.Name == name);
                if (nameQuery.Count() == 0)
                {
                    Console.WriteLine("There's no samurai by that name.");
                }
                else
                {
                    foreach (var IdNumber in nameQuery)
                    {
                        secretNumber = IdNumber.Id;
                    }

                    var secretName = context.SecretIdentities.Where(s => s.SamuraiID == secretNumber);
                    foreach (var realName in secretName)
                    {
                        Console.WriteLine(name + "'s real name is " + realName.RealName);
                    }
                }

            }
        }

        private static void ListAllSamuraiNames_WithLinq()
        {
            using (var context = new SamuraiContext())
            {
                var listOfSamurais =
                    context.Samurais
                        //.OrderByDescending(s => s.Name)
                        .OrderBy(s => s.Name) // Alfabetisk
                        .ToList();
                foreach (var name in listOfSamurais)
                {
                    Console.WriteLine(name.Name);
                }
            }


        }


        private static void ListAllSamuraiNames_OrderByDescending()
        {
            using (var ctx = new SamuraiContext())
            {
                var samurais = from s in ctx.Samurais
                    orderby s.Id descending
                    select s;
                foreach (var samurai in samurais)
                {
                    Console.WriteLine(samurai.Id + " " + samurai.Name);
                }
            }
        }

        private static void ListAllSamuraiNames()
        {
            using (var ctx = new SamuraiContext())
            {
                var samurais = from s in ctx.Samurais
                               orderby s.Name ascending 
                               select s.Name;
                foreach (var samurai in samurais)
                {
                    Console.WriteLine(samurai);
                }
            }


        }

        private static void ClearDatabase() // Clearing all the related entities
        {

            using (var context = new SamuraiContext())
            {
                context.RemoveRange(context.Samurais);
                context.RemoveRange(context.Battles);

                context.SaveChanges();
            }
        }

        private static void AddOneSamuraiWithRelatedData()
        {
            var Samurai = new Samurai()
            {
                Name = "Splinter", Hair = Hairstyle.Oicho,
                SecretIdentity = new SecretIdentity() {RealName = "Haakon"},
                Quotes = new List<Quote>()
                {
                    new Quote()
                    {
                        Quality = QuoteStyle.Cheesy,
                        Text = "You can always die. It's living that takes real courage."
                    }
                },
                SamuraiBattles = new List<SamuraiBattle>()
                {
                    new SamuraiBattle()
                    {
                        Battle = new Battle()
                        {
                            Name = "Battle of EndAll",
                            Description = "A battle to end all battles.",
                            BattleLog = new BattleLog()
                            {
                                Name = "EndAll",
                                BattleEvents = new List<BattleEvent>()
                                {
                                    new BattleEvent()
                                    {
                                        Order = 3,
                                        Description = "A fierce battle to decide the true owners of the district Alphadia.",
                                        Summary = "The Skaven clan won the battle and was claimed as the true rulors of Alphadia."
                                    }
                                }
                            }
                        },
                        
                    }
                }

            };
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(Samurai);
                context.SaveChanges();
            }

        }

        private static void AddSomeBattles() // Test method
        {
            IList<Battle> newBattles = new List<Battle>()
            {
                new Battle() {Name = "The big clash!", Description = "A long and tough Battle!",
                    StartDate = new DateTime(1515-02-02), EndDate = new DateTime(1530-01-01), IsBrutal = true,
                    BattleLog = new BattleLog(){Name = "TheToughAndLongBattle", 
                        BattleEvents = new List<BattleEvent>()
                        {
                            new BattleEvent(){Order = 1,
                                Description = "It was a long Battle, it was also tough.",
                                Summary = "The samurais won!"}}
                        }},
                new Battle()
                {
                    Name = "The Battle of 1000 samurais", Description = "A Battle to end it all.", IsBrutal = true,
                    StartDate = new DateTime(1532-01-01), EndDate = new DateTime(1545-01-02),
                    BattleLog = new BattleLog()
                    {
                        Name = "1000 Samurais",
                        BattleEvents = new List<BattleEvent>()
                        {
                            new BattleEvent()
                            {
                                Order = 2, 
                                Description = "There were 1000 samurais who fought.",
                                Summary = "There were less than 1000 samurais left. Quite a few actually..."
                            }
                        }
                    }
                },
            };
            using (var context = new SamuraiContext())
            {
                context.Battles.AddRange(newBattles);
                context.SaveChanges();
            }

        }

        private static void AddSomeSamurais(string name1, string name2, string name3) // Test method
        {
            IList<Samurai> newSamurais = new List<Samurai>()
            {
                new Samurai() {Name = name1},
                new Samurai() {Name = name2},
                new Samurai() {Name = name3}
            };
            using (var context = new SamuraiContext())
            {
                context.Samurais.AddRange(newSamurais);
                context.SaveChanges();
            }
        }

        private static void AddOneSamurai(string name) // Test method to verify that things works as intended
        {
            var samurai = new Samurai()
            {
                Name = name
            };
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }

        }
    }
}
