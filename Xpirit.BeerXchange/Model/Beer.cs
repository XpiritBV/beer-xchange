using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

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

        public int? SwitchedForId { get; set; }

        [JsonIgnore]
        public virtual Beer SwitchedFor { get; set; }
    }
}
