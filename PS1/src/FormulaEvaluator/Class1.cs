using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

//Kyle Kossin   u0743196
//CS 3500 - Assignment 1

namespace FormulaEvaluator
{
    // Evaluator reads an equation in string form, 
    // carries out the calculations, and returns the answer
    public static class Evaluator
    {
        /// <summary>
        /// Evaluates an expression containing integers/operators/variables
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public delegate int Lookup(String v);

        public static int Evaluate(String exp, Lookup va)
        /// <summary>
        /// Takes in an expression in the form of an expression; returns answer as integer
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        {

            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            Stack<double> values = new Stack<double>();
            Stack<string> operators = new Stack<string>();
            int i = 0;
            int vsize = 0;
            int osize = 0;
            int ssize = substrings.GetLength(1);
            int number = 0;
            for (i = 0; i < ssize; i++)
            {
                if (Int32.TryParse(substrings[i], out number)) //Checks for integer
                {
                    Convert.ToDouble(number);
                    if (substrings[i].Equals("*") || substrings[i].Equals("/"))
                    {
                        if (operators.Peek() == "*")
                        {
                            string now = operators.Pop();
                            osize--;
                            double first = values.Pop();
                            values.Push(first * number);
                        }
                        else
                        {
                            string now = operators.Pop();
                            osize--;
                            double first = values.Pop();
                            values.Push(first / number);
                        }
                    }
                    else
                    {

                        values.Push(number);
                        vsize++;
                    }
                }

                else if (substrings[i].Any(x => char.IsLetter(x))) //Checks for letter(variable)
                {
                    
                }

                else if (substrings[i].Equals("+") || substrings[i].Equals("-")) //Checks for addition/subtraction
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
                    else
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

                else if (substrings[i].Equals("*") || substrings[i].Equals("/")
                    || substrings[i].Equals("(")) //Checks for multiplication/division/opening parentheses
                {
                    operators.Push(substrings[i]);
                    osize++;
                }

                else if (substrings[i].Equals(")")) //Checks for closing parentheses
                {
                    if (operators.Peek() == "+" || operators.Peek() == "-")
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
                        else
                        {
                            string now = operators.Pop();
                            osize--;
                            double second = values.Pop();
                            vsize--;
                            double first = values.Pop();
                            values.Push(first - second);
                        }

                        if (operators.Peek() == "(")
                        {
                            operators.Pop();
                            osize--;
                        }

                        if (operators.Peek() == "*" || operators.Peek() == "/")
                        {
                            if (operators.Peek() == "*")
                            {
                                string now = operators.Pop();
                                osize--;
                                double second = values.Pop();
                                vsize--;
                                double first = values.Pop();
                                values.Push(first * second);
                            }
                            else
                            {
                                string now = operators.Pop();
                                osize--;
                                double second = values.Pop();
                                vsize--;
                                double first = values.Pop();
                                values.Push(first / second);
                            }
                        }
                    }
                    else if (operators.Peek() == " ")
                    {
                    }
                    else
                    {
                        throw new System.ArgumentException("Expression contains invalid character");
                    }
                }
            }


            if (osize == 0) //will return answer if no operators remain
            {
                if (vsize == 1)
                {
                    int answer = Convert.ToInt32(values.Pop());
                    return answer;
                }
                else
                    throw new System.ArgumentException("Operators/values don't add up");
            }
            else if (osize == 1 && vsize == 2) ; //will carry out final operation if one operator remains
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
                else throw new System.ArgumentException("Operators/values don't add up");
            }
        }
    }
}

