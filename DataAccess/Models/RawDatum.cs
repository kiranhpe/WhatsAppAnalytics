using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class RawDatum
    {
        public long Id { get; set; }
        public int ImportId { get; set; }
        public DateTime ChatDateTime { get; set; }
        public string Chat { get; set; }

        public virtual Import Import { get; set; }
    }
}
