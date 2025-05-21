using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practricee
{
    public delegate void del();
    public class green
    {
        public void green1()
        {
            Console.WriteLine("You have received green");

            Console.ReadKey();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            green g = new green();
            del d = g.green1;
            d();

        }
    }
}