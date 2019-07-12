using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFSamurai.Domain
{
    class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuoteStyle? Quality { get; set; }
        [ForeignKey("Samurai")]
        public int SamuraiID { get; set; }
        public Samurai  Samurai { get; set; }
    }
}
