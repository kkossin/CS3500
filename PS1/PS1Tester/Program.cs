using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PS1Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] substrings = Regex.Split(" ", "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
        }
    }
}
