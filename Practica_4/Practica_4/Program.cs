/*
     Nombres y apellidos: Reyner David Contreras Rojas.
     C.I: V.-26934400
     Materia: Programacion II
*/

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Practica_4
{
    class Program
    {
        //Method to process a line readed which should have queens, knights or pawns. Returns a flag with a message if an error was encountered.
        private static int piecesLocations(ref string[] l, ref List<Piece> list, char typeP, int n, int m)
        {
            int k, r, c;// k = number of pieces; r = rows; c = columns
            bool flag;
            flag = int.TryParse(l[0], out k);
            if (!flag) //if its NaN
                return 1;

            else if (k > 100) //more than 100 pieces
            {
                Console.WriteLine("Number of " + typeP + " piece exceeds 100, only 100 will be loaded.");
                k = 100;
            }

            if (k > (l.Length - 1) / 2) //incomplete data
                return 2;

            //process the line
            int j = 1;
            for (int i = 0; i < k; i++)
            {
                flag = int.TryParse(l[j++], out r);
                if (!flag) //if its NaN
                    return 1;

                flag = int.TryParse(l[j++], out c);
                if (!flag) //if its NaN
                    return 1;

                if ((r > 0 && c > 0) && (r <= n && c <= m)) //checking if its a valid position
                {
                    Piece p = new Piece(r, c, typeP);
                    flag = list.Any(x => x.r == r - 1 && x.c == c - 1);
                    if (flag)
                    {
                        Console.WriteLine("The piece " + typeP + "(" + r + " " + c + ") is not taken into account" +
                            " because there is one more in the same position.");
                    }
                    else
                        list.Add(p);
                }//if
                else
                {
                    return 5; //not is a valid position
                }
            }//for i

            return 0; //successful
        }//piecesLocations(string[] l, ref Piece[] vector)

        //Method which try to read a board. Returns a flag with a message if an error was encountered.
        private static int readABoard(out Board bb, ref StreamReader sr)
        {
            bb = null;
            string line = sr.ReadLine(); //read first line
            if (line == null)
                return 3;

            string[] l;
            int n, m, aux; //foo vars
            bool isNumber;

            //Processing the line with the dimensions of the board
            l = line.Split();
            isNumber = int.TryParse(l[0], out n);
            if (!isNumber) //if it's NaN
                return 1;
            isNumber = int.TryParse(l[1], out m);
            if (!isNumber) //if it's NaN
                return 1;

            if (n > 1000 || m > 1000 || n < 0 || m < 0) //n or m more than 1000 or less than 0
                return 4;

            if (n == m && m == 0) //successfully end of read
                return -1;

            //Reading locations of queens, knights and pawns
            char typeP = 'q'; //type of piece
            List<Piece> list = new List<Piece>(); //list of pieces
            for (int i = 0; i < 3; i++)
            {
                line = sr.ReadLine();
                if (line == null) //incomplete data for the board
                    return 2;
                
                l = line.Split();

                aux = piecesLocations(ref l, ref list, typeP, n, m); //method to process the line
                if (aux != 0) //error
                    return aux;

                switch (i) //assign pType depending on the number of line currently being read
                {
                    case 0:  typeP = 'k';   break;
                    case 1:  typeP = 'p';  break;
                }//switch(i)
            }//for i

            //creating the board object
            bb = new Board(n, m, ref list);
            list = null;
            return 0; //successful
        }//private static int readABoard(Board bb, ref StreamReader sr)

        //Method which try to read data from a file and create a queue of boards from it
        private static bool readFile(string filename, out Queue<Board> boards)
        {
            StreamReader sr = null;
            boards = null;
            try
            {
                //open the file
                sr = File.OpenText(filename);
                int flag = 0; //a flag which indicates if something went wrong or not
                boards = new Queue<Board>();

                //read the file
                bool running = true;
                Console.WriteLine("LOADING BOARDS:");
                while (running)
                {
                    Board bb;
                    Console.Write("Board " + (boards.Count + 1) + ": ");
                    flag = readABoard(out bb, ref sr); //Try to read a board and check if it was successful
                    switch (flag)
                    {
                        case -1: //successful end of read
                            Console.WriteLine("All boards was loaded successfully.");
                            running = false;
                            break;
                        case 0: //correct
                            Console.WriteLine("Succesful");
                            boards.Enqueue(bb);
                            break;
                        case 1:
                            Console.WriteLine("A value isn't an integer in the test case " + (boards.Count + 1) + ", skipping read of file.");
                            running = false;
                            break;
                        case 2:
                            Console.WriteLine("The data for the board is incomplete, the last board will not be loaded.");
                            running = false;
                            break;
                        case 3:
                            Console.WriteLine("Reached the end of the file and no \"0 0\" board detected, ending read of file.");
                            running = false;
                            break;
                        case 4:
                            Console.WriteLine("The dimensions of the board are not allowed, skipping read of file.");
                            running = false;
                            break;
                        case 5:
                            Console.WriteLine("One of the locations of a piece isn't valid, skipping read of file.");
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Oh oh, an unknown error. Skipping read of file.");
                            running = false;
                            break;
                    }//switch(flag)
                    Console.WriteLine();
                }//while(running)
            }//try
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Memory error.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File \'" + filename + "\' not found.");
            }
            catch (IOException)
            {
                Console.WriteLine("I/O error.");
            }
            finally
            {
                //if its opened, close the file
                if (sr != null)
                    sr.Close();
            }
            
            if (boards == null || boards.Count == 0) //Couldn't load any board.
                return false;

            //Successful
            return true;
        }//private static bool readFile(string filename, ref string[] paragraphs)

        //Method which try print the data of each board
        private static void printBoards(ref Queue<Board>boards)
        {
            if (boards == null || boards.Count == 0)
            {
                Console.WriteLine("Null board queue or doesn't have elements.");
                return;
            }

            Board[] b = boards.ToArray();
            int i = 1;
            foreach (Board bb in b)
            {
                Console.Write(i + ". "); bb.print();
                i++;
            }
        }//private static void printBoards(ref Queue<Board>boards)

        //Method which try to calculate the number of safe squares of each board.
        static bool calculate(ref Queue<Board> boards)
        {
            if (boards == null || boards.Count == 0) //the queue of boards is empty
            {
                Console.WriteLine("Null board queue or doesn't have elements.");
                return false;
            }

            int safeSquares, i = 1;
            foreach(Board b in boards)
            {
                safeSquares = b.calculateSafeSquares();
                Console.WriteLine("Board " + i + " has " + safeSquares + " safe squares.");
                ++i;
            }//foreach

            return true;
        }//static bool calculateSafeSquares(ref Queue<Board> boards)

        static void Main(string[] args)
        {
            //read the data from the file "queens.in"
            Queue<Board>boards;
            bool correct = readFile("queens.in", out boards);

            if (!correct)
                Console.WriteLine("An error has occurred while trying to load the data.");
            else
            {
                boards.TrimExcess();
                Console.WriteLine("------------------------------------------------------------");

                //print the data from the file
                Console.WriteLine("\nBOARDS LOADED:");
                printBoards(ref boards);

                Console.WriteLine("------------------------------------------------------------");
                //calculate the number of safe squares of every board from boards and print it.
                Console.WriteLine("\nOUTPUT:");
                calculate(ref boards);
            }///else if correct

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }//static void Main(string[] args)
    }//class Program
}//namespace Practica_4
