using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskApp
{
    public class Book:IComparable<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Pages { get; set; }

        public string Publisher { get; set; }

        public int CompareTo(Book? other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.Title.CompareTo(other.Title);
            }
        }

        //Print() that displays title, author, pages, and publisher

        public string Print()
        {
            return $"Title: {Title}, Author: {Author}, Pages: {Pages}, Publisher: {Publisher}";
        }

    }
}
