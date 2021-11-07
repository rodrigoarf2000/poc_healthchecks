using System;
using System.Collections.Generic;

#nullable disable

namespace FastCore.Repositories.Entities
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
