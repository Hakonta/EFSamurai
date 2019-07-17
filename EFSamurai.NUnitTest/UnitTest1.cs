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
    }
}