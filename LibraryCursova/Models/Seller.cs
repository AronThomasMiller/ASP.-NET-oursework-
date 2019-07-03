using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryCursova.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartWorkTime { get; set; }
        public int EndWorkTime { get; set; }
    }
}