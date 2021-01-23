using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class ImportStatus
    {
        public ImportStatus()
        {
            Imports = new HashSet<Import>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Import> Imports { get; set; }
    }
}
