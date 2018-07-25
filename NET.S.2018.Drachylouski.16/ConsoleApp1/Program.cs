using System;
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericCollection;
using BinarySearchTreeLogic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<string> bs = new BinarySearchTree<string>();

            bs.Add("hello");
            bs.Add("hello");
            bs.Add("omfg");
            bs.Add("A");
            bs.Add("a");
            


            foreach (var VARIABLE in bs.InOrderEnumerable())
            {
                Console.WriteLine(VARIABLE);
            }
        }


    }

    struct Book
    {
        private int year;
        public Book(int year):this()
        {
            Year = year;
        }

        public int Year { get => year; set => year = value; }
    }

    //class Book
    //{
    //    private int year;
    //    public Book(int year)
    //    {
    //        Year = year;
    //    }

    //    public int Year { get => year; set => year = value; }
    //}

    //class Com : IComparer<Book>
    //{
    //    public int Compare(Book x, Book y)
    //    {
    //        return x.Year - y.Year;
    //    }
    //}
}
