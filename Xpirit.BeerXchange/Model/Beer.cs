using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Xpirit.BeerXchange.Model
{
    public class Beer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        public string Brewery { get; set; }
        public string CreatedBy { get; set; }
        public string Picture { get; set; }
        public DateTime? AddedDate { get; set; }
        
        public DateTime? RemovedDate { get; set; }
        public string RemovedBy { get; set; }

    }
}
