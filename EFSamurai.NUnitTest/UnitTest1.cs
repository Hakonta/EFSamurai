using System;
using System.Collections.Generic;
using EFSamurai;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            using (var context = new SamuraiContext())
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
        }


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_AddOneSamuraiTwice()
        {
            EfMethods.AddOneSamurai("Zelda");
            EfMethods.AddOneSamurai("Link");
            string[] result = EfMethods.GetAllSamuraiNames();
            CollectionAssert.AreEqual(new[] { "Zelda", "Link"}, result);
        }

        [Test]
        public void Test_AddOneSamuraiWithSecretIdentity()
        {
            var samurai = new Samurai()
            {
                Name = "Ganondorf Dragmire",
                Hair = Hairstyle.Western
            };
            int samuraiId = EfMethods.AddOneSamurai(samurai);
            EfMethods.UpdateSamuraiSetSecretIdentity(samuraiId, "Ganon");

            Samurai actualSamurai = EfMethods.GetSamurai(samuraiId);
            Assert.AreEqual("Ganondorf Dragmire", actualSamurai.Name);
            Assert.AreEqual(Hairstyle.Western, actualSamurai.Hair);
            Assert.AreEqual("Ganon", actualSamurai.SecretIdentity.RealName);
        }

        [Test]
        public void Test_BattlesAndSamurais()
        {
            var samurai = new Samurai()
            {
                Name = "Leonardo",
                Hair = Hairstyle.Oicho,
                SecretIdentity = new SecretIdentity() { RealName = "Svangunn" },
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
                            Name = "Battle of Academy",
                            Description = "A fierce battle of wits!",
                            StartDate = new DateTime(2019,06,11), EndDate = new DateTime(2019,09,13), IsBrutal = true,
                            BattleLog = new BattleLog()
                            {
                                Name = "The Hackathon",
                                BattleEvents = new List<BattleEvent>()
                                {
                                    new BattleEvent()
                                    {
                                        Order = 1,
                                        Description = "What started as a friendly competition emerged into a deadly battle, where laptops were used as blunt weapons.",
                                        Summary = "The fight ended in a lot of tears and broken laptops. There were no victors..."
                                    }
                                }
                            }
                        },

                    },
                    new SamuraiBattle()
                    {
                        Battle = new Battle()
                        {
                            Name = "The revenge of the N.E.R.D.S",
                            Description = "Notable Enigmatic Revolting Dapping Students",
                            StartDate = new DateTime(2019,09,15), EndDate = new DateTime(2019,09,16), IsBrutal = true,
                            BattleLog = new BattleLog()
                            {
                                BattleEvents = new List<BattleEvent>()
                                {
                                    new BattleEvent()
                                    {
                                        Order = 2,
                                        Description = "As there were no laptops left. Pillows were used.",
                                        Summary = "The fight ended in a deadlock. There were no victors."
                                    },
                                    new BattleEvent()
                                    {
                                        Order = 3,
                                        Description = "Suddenly someone got the bright idea to stuff their pillows with rocks!",
                                        Summary = "It was a brutal fight with lots of casualties on all sides."
                                    }
                                }
                            }
                        }
                    }
                }

            };
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }

            string result = EfMethods.ListAllBattlesWithinPeriod(new DateTime(2019,01, 01), new DateTime(2019,09,14), true);
            // isBrutal,
            Assert.AreEqual("Leonardo alias Svangunn fought in Battle of Academy", result);

        }
    }
}