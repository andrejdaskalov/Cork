using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Note Title")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Note Contents")]
        public string Data { get; set; }
        public Type Type { get; set; }
        public string X { get; set; }
        public  string Y { get; set; }
        public virtual Pinboard Pinboard { get; set; }

    }
}