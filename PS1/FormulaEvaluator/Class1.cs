﻿using System;
using System.Collections;
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

        public static int Evaluate(String exp, Lookup variableEvaluator)
        /// <summary>
        /// Takes in an expression in the form of an expression; returns answer as integer
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        {
            exp = exp.Replace(" ", "");
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            Stack<double> values = new Stack<double>();
            Stack<string> operators = new Stack<string>();
            int i = 0;
            int ssize = substrings.Count();
            int number = 0;
            for (i = 0; i < ssize; i++)
            {
                string check = substrings[i];
                if (Int32.TryParse(substrings[i], out number)) //Checks for integer
                {
                    Convert.ToDouble(number);
                    if (operators.Count() != 0)
                    {
                        if (operators.Peek() == "*" || operators.Peek() == "/")
                        {
                            if (operators.Peek() == "*")
                            {
                                string now = operators.Pop();
                                double first = values.Pop();
                                values.Push(first * number);
                            }
                            else
                            {
                                string now = operators.Pop();
                                double first = values.Pop();
                                values.Push(first / number);
                            }
                        }
                        else
                        {
                            values.Push(number);
                        }
                    }
                    else
                    {
                        values.Push(number);
                    }
                }

                else if (substrings[i].Any(x => char.IsLetter(x))) //Checks for letter(variable)
                {

                }

                else if (substrings[i].Equals("+") || substrings[i].Equals("-")) //Checks for addition/subtraction
                {
                    if (operators.Count() != 0)
                    {
                        if (operators.Peek() == "+")
                        {
                            string now = operators.Pop();
                            double second = values.Pop();
                            double first = values.Pop();
                            values.Push(first + second);
                        }
                        else if (operators.Peek() == "-")
                        {
                            string now = operators.Pop();
                            double second = values.Pop();
                            double first = values.Pop();
                            values.Push(first - second);
                        }
                    }
                    operators.Push(substrings[i]);
                }

                else if (substrings[i].Equals("*") || substrings[i].Equals("/")
                    || substrings[i].Equals("(")) //Checks for multiplication/division/opening parentheses
                {
                    operators.Push(substrings[i]);
                }

                else if (substrings[i].Equals(")")) //Checks for closing parentheses
                {

                    if (operators.Peek() == "+" || operators.Peek() == "-")
                    {
                        if (operators.Peek() == "+")
                        {
                            string now = operators.Pop();
                            double second = values.Pop();
                            double first = values.Pop();
                            values.Push(first + second);
                        }
                        else
                        {
                            string now = operators.Pop();
                            double second = values.Pop();
                            double first = values.Pop();
                            values.Push(first - second);
                        }
                    }

                    if (operators.Peek() == "(")
                    {
                        operators.Pop();
                    }

                   if (operators.Peek() == "*" || operators.Peek() == "/")
                    {
                        if (operators.Peek() == "*")
                        {
                            string now = operators.Pop();
                            double second = values.Pop();
                            double first = values.Pop();
                            values.Push(first * second);
                        }
                        else
                        {
                            string now = operators.Pop();
                            double second = values.Pop();
                            if (second == 0) { throw new ArgumentException("Division by zero encountered"); }
                            double first = values.Pop();
                            values.Push(first / second);
                        }

                    }

                    else
                    {
                        throw new System.ArgumentException("Expression contains invalid character");
                    }
                }
            }


            if (operators.Count() == 0) //will return answer if no operators remain
            {
                if (values.Count() == 1)
                {
                    int answer = Convert.ToInt32(values.Pop());
                    return answer;
                }
                else
                    throw new System.ArgumentException("Operators/values don't add up");
            }
            else if (operators.Count() == 1 && values.Count() == 2) ; //will carry out final operation if one operator remains
            {
                String now = operators.Pop();
                double second = values.Pop();
                double first = values.Pop();
                if (now == "+")
                {
                    int answer = Convert.ToInt32(first + second);
                    return answer;
                }
                else if (now == "-")
                {
                    int answer = Convert.ToInt32(first - second);
                    return answer;
                }
                else throw new System.ArgumentException("Operators/values don't add up");
            }
        }
    }
}