using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new BasicTest();
            t.Initiaize();
            t.Test();
            t.Close();
        }
    }
}
