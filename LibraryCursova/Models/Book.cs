using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryCursova.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AvtorName { get; set; }
        public string Vudavnuctvo { get; set; }
        public int YearOfVudannya { get; set; }
        public int Price { get; set; }
        public string Genre { get; set; }
        public int CompanyId { get; set; }
        public int Count { get; set; }
        public Company Company { get; set; }
    }
}