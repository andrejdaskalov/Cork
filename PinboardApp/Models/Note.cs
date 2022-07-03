using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PinboardApp.Models
{
    public class Note
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Data { get; set; }
        public Type Type { get; set; }
        public virtual Pinboard Pinboard { get; set; }

    }
}