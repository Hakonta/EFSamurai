using System;
using System.Collections;
using System.Collections.Generic;

namespace EFSamurai
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Hairstyle? Hair { get; set; }
        public SecretIdentity SecretIdentity { get; set; }
        private ICollection<Quote> Quotes { get; set; }
        private ICollection<SamuraiBattle> SamuraiBattles { get; set; }
    }
}
