using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EFSamurai
{
    class Program
    {
        static void Main(string[] args)
        {
            // AddOneSamurai("Zelda");
            //AddSomeSamurais("Leonardo", "Donatello", "Michelangelo");
            //AddSomeBattles();
            // AddOneSamuraiWithRelatedData();
            // ClearDatabase();
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
                    BattleLog = new BattleLog(){Name = "TheToughAndLongBattle", 
                        BattleEvents = new List<BattleEvent>()
                        {
                            new BattleEvent(){Order = 1,
                                Description = "It was a long Battle, it was also tough.",
                                Summary = "The samurais won!"}}
                        }},
                new Battle()
                {
                    Name = "The Battle of 1000 samurais", Description = "A Battle to end it all.",
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
