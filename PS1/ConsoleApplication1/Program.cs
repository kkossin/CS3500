using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        private static int variableEvaluator(String v)
        {
            var variables = new Dictionary<string, int>
            {
                { "X6", 7 }, { "K", 6 }
            };
            return 0;

            if (variables.ContainsKey(v))
            {
                return variables[v];
            }
            else throw new ArgumentException("This variable has no value");
        }
        static void Main(string[] args)
        {
            Regex r = new Regex("[abc]*");
            r.IsMatch("a");

            string test1 = "3 * (3 + 3)";
            string test2 = "3 - 3";
            string test3 = "3 * 3";
            string test4 = "3 / 3";
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate(test1, variableEvaluator));
        }
    }
}
