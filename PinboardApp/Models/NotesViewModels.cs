using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinboardApp.Models
{
    public class CreateViewModel
    {
        public Note Note { get; set; }
        public int? PinboardId { get; set; }
    }
}