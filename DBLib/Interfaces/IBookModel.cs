using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDataAccess.Interfaces
{
    public interface IBookModel
    {
        // Strings
        public string Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string book_title { get; set; }
        public string book_category { get; set; }
        public string book_language { get; set; }
        public string book_salesman { get; set; }

        // Datetime
        public DateTime year_publication { get; set; }

        // Ints
        public int book_price { get; set; }
        public int book_amount { get; set; }

        // Bools
        public bool book_new { get; set; }

    }
}
