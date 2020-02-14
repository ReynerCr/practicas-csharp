/*
     Nombres y apellidos: Reyner David Contreras Rojas.
     C.I: V.-26934400
     Materia: Programacion II
*/

using System;
using System.IO;
using System.Collections.Generic;

namespace Practica_3
{
    class Program
    {
        private static bool readFile(string filename, ref string[] paragraphs)
        {
            bool open = false;
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(filename);
                string l = sr.ReadToEnd();
                Console.WriteLine("Data contained in the file " + filename + ":\n" + l);
                Console.WriteLine("\n\n");
                paragraphs = l.Split('#');
                open = true;
            }
            catch (OutOfMemoryException) { Console.WriteLine("Memory error."); }
            catch (FileNotFoundException) { Console.WriteLine("File \'" + filename+ "\' not found."); }
            catch (IOException) { Console.WriteLine("I/O error."); }
            finally
            {
                if (sr != null)
                    sr.Close();
            }

            if (!open)
                return false;

            for (int i = 0; i < paragraphs.Length; i++)
            {
                paragraphs[i] = paragraphs[i].Trim(' ', '\n', '\r');
                if (paragraphs[i].Equals(""))
                {
                    Array.Resize(ref paragraphs, i);
                    break;
                }//if
            }//for

            return true;
        }//private static void readFile(string filename, ref string[] paragraphs)

        private static void verifyTags(ref string[] paragraphs)
        {
            Stack<char> tags;
            bool error;

            for (int i = 0; i < paragraphs.Length; i++)
            {
                tags = new Stack<char>();
                string l = paragraphs[i];
                int pos = l.IndexOf('<');
                error = false;
                while (pos != -1)
                {
                    pos++; //it should be a uppercase letter or a backslash character for a tag
                    if (pos + 1 < l.Length)
                    {
                        if (Char.IsUpper(l[pos]) && l[pos + 1] == '>') //upercase letter
                        {
                            tags.Push(l[pos]);
                            if (pos + 1 < l.Length)
                                l = l.Substring(pos + 1);
                        }//if
                        else if (l[pos] == '/' && pos + 2 < l.Length
                            && l[pos + 2] == '>') //backslash character
                        {
                            pos++; //now it should be a uppercase letter
                            if (Char.IsUpper(l[pos]))
                            {
                                try
                                {
                                    if (l[pos] == tags.Peek())
                                    {
                                        tags.Pop();
                                        if (pos + 1 < l.Length)
                                            l = l.Substring(pos + 1);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Expected </" + tags.Pop() + "> found </" + l[pos] + ">");
                                        error = true;
                                        break;
                                    }//else
                                }//try
                                catch (InvalidOperationException)
                                {
                                    Console.WriteLine("Expected # found </" + l[pos] + ">");
                                    error = true;
                                    break;
                                }
                            }//if
                        }//else if
                        else
                        {
                            l = l.Substring(pos + 1);
                        }
                    }//if (pos + 1 < l.Length)

                    pos = l.IndexOf('<');
                }//while
                if (!error)
                {
                    try
                    {
                        tags.Peek();
                        Console.WriteLine("Expected </" + tags.Pop() + "> found #");
                    }
                    catch (InvalidOperationException) { Console.WriteLine("Correctly tagged paragraph"); }
                }//if !error it will seek for some unclosed tag
            }//for i
        }//private static void verifyTags(ref string[] paragraphs)

        static void Main(string[] args)
        {
            string[] paragraphs = null;
            bool correct = readFile("tag.in", ref paragraphs);
            
            if (correct)
            {
                Console.WriteLine("OUTPUT:");
                verifyTags(ref paragraphs);
            }
            Console.Write("\nPress any key to exit.");
            Console.ReadKey();
        }//static void Main(string[] args)
    }//class Program
}//namespace Practica_3
