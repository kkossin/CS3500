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
                { "X17", 7 }, { "K", 3 }
            };

            if (variables.ContainsKey(v))
            {
                return variables[v];
            }
            else throw new ArgumentException("A variable has no value");
        }
        static void Main(string[] args)
        {
            string test1 = "X17 + 3 * (3 - (3 / 3))";
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate(test1, variableEvaluator));
        }
    }
}
