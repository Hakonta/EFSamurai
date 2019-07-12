using System;

namespace EFSamurai
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Hairstyle? Hair { get; set; }
        public SecretIdentity SecretIdentity { get; set; }
    }
}
