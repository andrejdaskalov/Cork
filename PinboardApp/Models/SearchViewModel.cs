using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinboardApp.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Pinboard> List { get; set; }
        public Pinboard CurrentPinboard { get; set; }
        public string query { get; set; }
    }
}