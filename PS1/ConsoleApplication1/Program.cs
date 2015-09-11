using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static int Main(string[] args)
        {
            string test1 = "3 / (3 - 3)";
            string test2 = "3 - 3";
            string test3 = "3 * 3";
            string test4 = "3 / 3";
            int answer = FormulaEvaluator.Evaluator.Evaluate(test1, FormulaEvaluator.Evaluator.variableEvaluator);
            System.Diagnostics.Debug.Write(answer);
            return answer;
        }
    }
}
