using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PinboardApp.Models
{
    public class Pinboard
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<Note> Notes { get; set; }
    }
}