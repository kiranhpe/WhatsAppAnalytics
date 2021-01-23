using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Import
    {
        public Import()
        {
            RawData = new HashSet<RawDatum>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string ImportedBy { get; set; }
        public DateTime ImpotedDateTime { get; set; }
        public int ImportStatusId { get; set; }

        public virtual ImportStatus ImportStatus { get; set; }
        public virtual ICollection<RawDatum> RawData { get; set; }
    }
}
