using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

//Kyle Kossin   u0743196
//CS 3500 - Assignment 1
//September 10th, 2015

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
        /// Takes in an expression in the form of a string; returns answer as integer
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        {
            exp = exp.Replace(" ", ""); //Cut out empty space
            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)"); //Separates pieces of equation
            Stack<double> values = new Stack<double>();
            Stack<string> operators = new Stack<string>();
            int i = 0;
            int ssize = substrings.Count();
            int number = 0;
            for (i = 0; i < ssize; i++) //Will iterate once for each piece of equation
            {
                //Check for integer first
                if (Int32.TryParse(substrings[i], out number))
                {
                    Convert.ToDouble(number);
                    if (operators.Count() != 0)
                    {
                        //Multi/divi carried out first; we'll do that now with the current and previous number if needed
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
                        else //If another operator waits, number goes to stack; we'll come back
                        {
                            values.Push(number);
                        }
                    }
                    else //Number also goes to stack if there are no operators
                    {
                        values.Push(number);
                    }
                }

                //Check for variable (has a letter in it) and put it on stack
                else if (substrings[i].Any(x => char.IsLetter(x))) //Checks for letter(variable)
                {
                    values.Push(Convert.ToDouble(variableEvaluator(substrings[i])));
                }

                //Check for addition/subtraction
                else if (substrings[i].Equals("+") || substrings[i].Equals("-")) //Checks for addition/subtraction
                {
                    if (operators.Count() != 0)
                    {
                        //Carry out previous addition/subtraction (other operations will be handled by this point)
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
                    operators.Push(substrings[i]); //Put on stack if there aren't other operators there
                }

                //Check for multiplication/division/opening parenthesis and put on stack
                else if (substrings[i].Equals("*") || substrings[i].Equals("/")
                    || substrings[i].Equals("("))
                {
                    operators.Push(substrings[i]);
                }

                //Check for closing parenthesis
                else if (substrings[i].Equals(")"))
                {
                    //if parentheses contain add/sub, we carry it out 
                    //(add/sub expected to be reason for parentheses)
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

                    //Take the opening parenthesis off the stack
                    if (operators.Peek() == "(")
                    {
                        operators.Pop();
                    }

                    //Carry out the multi/divi waiting outside parentheses
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
                }

                //Exception thrown if another symbol is used
                else if (substrings[i] != "") //Regex leaves some empty strings
                {
                    {
                        throw new System.ArgumentException("Expression contains invalid character");
                    }
                }
            }


            //Return answer if no operators remain
            if (operators.Count() == 0) 
            {
                if (values.Count() == 1)
                {
                    int answer = Convert.ToInt32(values.Pop());
                    return answer;
                }
                else
                    throw new System.ArgumentException("Operators/values don't add up");
            }

            //Carry out final operation if one operator remains
            else if (operators.Count() == 1 && values.Count() == 2) ; 
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
                //Exception if there's a structure problem in expression
                else throw new System.ArgumentException("Problem with expression construction"); 
            }
        }
    }
}