using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models
{
    public partial class Import
    {
        public Import()
        {
            RawData = new HashSet<RawData>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string ImportedBy { get; set; }
        public DateTime ImpotedDateTime { get; set; }

        public virtual ICollection<RawData> RawData { get; set; }
    }
}
