using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Kyle Kossin   u0743196
//CS 3500 - Assignment 1

namespace FormulaEvaluator
{
    // Evaluator reads an equation in string form, 
    // carries out the calculations, and returns the answer
    public static class Evaluator
    {
        public delegate int Lookup(String v);

        public static int Evaluate(String exp, Lookup va)
        {
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            Stack<double> values = new Stack<double>();
            Stack<string> operators = new Stack<string>();
            int i = 0;
            int vsize = 0;
            int osize = 0;
            int ssize = substrings.GetLength(1);
            for (i = 0; i < ssize; i++)
            {
                if (substrings[i].Equals("+") || substrings[i].Equals("-"))
                {
                    if (operators.Peek() == "+")
                    {
                        string now = operators.Pop();
                        osize--;
                        double second = values.Pop();
                        vsize--;
                        double first = values.Pop();
                        values.Push(first + second);
                    }
                    else if (operators.Peek() == "-")
                    {
                        string now = operators.Pop();
                        osize--;
                        double second = values.Pop();
                        vsize--;
                        double first = values.Pop();
                        values.Push(first - second);
                    }
                    operators.Push(substrings[i]);
                    osize++;
                }

                else if (substrings[i].Equals("*") || substrings[i].Equals("/") || substrings[i].Equals("("))
                {
                    operators.Push(substrings[i]);
                    osize++;
                }

            }


            if (osize == 0)
            {
                if (vsize == 1)
                {
                    int answer = Convert.ToInt32(values.Pop());
                    return answer;
                }
            }
            else if (osize == 1 && vsize == 2) ;
            {
                String now = operators.Pop();
                double second = values.Pop();
                double first = values.Pop();
                if (now == "+")
                {
                    int answer = Convert.ToInt32(first + second);
                    return answer;
                }
                else if (now == "i")
                {
                    int answer = Convert.ToInt32(first - second);
                    return answer;
                }
            }
        }
    }
}
