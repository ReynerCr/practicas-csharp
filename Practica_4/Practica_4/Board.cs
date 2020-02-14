using System;
using System.Collections.Generic;

namespace Practica_4
{
    public readonly struct Piece
    {
        public readonly int r, c;
        public readonly char typeP; //type of piece
        public Piece(int r, int c, char typeP)
        {
            this.r = r - 1;
            this.c = c - 1;
            this.typeP = typeP;
        }//constructor

        public void print() { Console.Write((r + 1) + " " + (c + 1) + " "); }
    }//struct Piece



    public class Board
    {
        public readonly int n, m; //dimensions of the board
        readonly List<Piece> pieces;

        public Board(int n, int m, ref List<Piece> pieces)
        {
            this.n = n;
            this.m = m;
            this.pieces = pieces;
            this.pieces.TrimExcess();
        }//constructor

        ////Method to mark the squares wich are attacked by the knight
        private static bool qMarkAttackRange(ref char[,] mat, Piece p)
        {
            if (p.typeP != 'q')
                return false;

            int i = p.r - 1, j = p.c - 1; //upper left
            while (i >= 0 && j >= 0 && mat[i, j] == ' ') { mat[i, j] = '#'; --i; --j; }

            i = p.r - 1; j = p.c; //up
            while (i >= 0 && mat[i, j] == ' ') { mat[i, j] = '#'; --i; }

            i = p.r - 1; j = p.c + 1; //upper right
            while (i >= 0 && j < mat.GetLength(1) && mat[i, j] == ' ') { mat[i, j] = '#'; --i; ++j; }

            i = p.r; j = p.c - 1; //left
            while (j >= 0 && mat[i, j] == ' ') { mat[i, j] = '#'; --j; }

            j = p.c + 1; //right
            while (j < mat.GetLength(1) && mat[i, j] == ' ') { mat[i, j] = '#'; ++j; }

            ++i; j = p.c - 1; //lower left
            while (i < mat.GetLength(0) && j >= 0 && mat[i, j] == ' ') { mat[i, j] = '#'; ++i; --j; }

            i = p.r + 1; j = p.c; //down
            while (i < mat.GetLength(0) && mat[i, j] == ' ') { mat[i, j] = '#'; ++i; }

            i = p.r + 1; ++j; //lower right
            while (i < mat.GetLength(0) && j < mat.GetLength(1) && mat[i, j] == ' ') { mat[i, j] = '#'; ++i; ++j; }

            return true;
        }//private static bool qMarkAttackRange(ref char[,] mat, Piece p)

        //Method to mark the squares wich are attacked by the knight
        private static bool kMarkAttackRange(ref char[,] mat, Piece p)
        {
            if (p.typeP != 'k')
                return false;

            int i = p.r, j = p.c;
            if (i - 1 >= 0) //opposite corner of rectangle 2x3, the knight is in one of the lower corners
            {
                if ((j - 2) >= 0 && mat[i - 1, j - 2] == ' ') //knight in right corner
                    mat[i - 1, j - 2] = '#';
                if ((j + 2) < mat.GetLength(1) && mat[i - 1, j + 2] == ' ') //knight in left corner
                    mat[i - 1, j + 2] = '#';
            }
            if (i + 1 < mat.GetLength(0)) //opposite corner of rectangle 2x3, the knight is in one of the upper corners
            {
                if ((j - 2) >= 0 && mat[i + 1, j - 2] == ' ') //knight in right corner
                    mat[i + 1, j - 2] = '#';

                if ((j + 2) < mat.GetLength(1) && mat[i + 1, j + 2] == ' ') //knight in left corner
                    mat[i + 1, j + 2] = '#';
            }
            if (i - 2 >= 0) //opposite corner of rectangle 3x2, the knight is in one of the lower corners
            {
                if ((j - 1) >= 0 && mat[i - 2, j - 1] == ' ') //knight in right corner
                    mat[i - 2, j - 1] = '#';
                if ((j + 1) <  mat.GetLength(1) && mat[i - 2, j + 1] == ' ') //knight in left corner
                    mat[i - 2, j + 1] = '#';
            }
            if (i + 2 < mat.GetLength(0)) //opposite corner of rectangle 3x2, the knight is in one of the upper corners
            {
                if ((j - 1) >= 0 && mat[i + 2, j - 1] == ' ') ///knight in right corner
                    mat[i + 2, j - 1] = '#';
                if ((j + 1) < mat.GetLength(1) && mat[i + 2, j + 1] == ' ') ///knight in left corner
                    mat[i + 2, j + 1] = '#';
            }

            return true;
        }//private static bool kMarkAttackRange(ref char[,] mat, Piece p)

        //Method to calculate the number of safe squares of the board.
        public int calculateSafeSquares()
        {
            if (pieces.Count == 0) //doesn't have elements
                return -1;

            int S = 0; //safeSquares
            char[,] mat = new char[n, m];
            
            //filling the matrix with ' '
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    mat[i, j] = ' ';
                }//for j
            }//for i

            //inserting the pieces in mat
            foreach (Piece p in pieces) { mat[p.r, p.c] = p.typeP; }

            //Determining the squares attacked by the pieces
            foreach (Piece p in pieces)
            {
                switch(p.typeP)
                {
                    case 'q':
                        qMarkAttackRange(ref mat, p);
                        break;
                    case 'k':
                        kMarkAttackRange(ref mat, p);
                        break;
                }//switch
            }//foreach

            //Determining the number of safe squares
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (mat[i, j] == ' ') //safe square
                        ++S;
                }//for j
            }//for i

            return S;
        }//public int calculateSafeSquares()

        //Method to print data of the board
        public void print()
        {
            Console.WriteLine("Dimensions of the board: " + n + " " + m);
            Console.WriteLine("Pieces: ");
            if (pieces != null && pieces.Count > 0)
            {
                char t = pieces[0].typeP;
                Console.Write(t + ": ");
                foreach (Piece p in pieces)
                {
                    if (t != p.typeP)
                    {
                        t = p.typeP;
                        Console.Write("\n" + t + ": ");
                    }
                    p.print();
                }//foreach p
            }//if
            else
                Console.WriteLine("The board doesn't have pieces.");
                           
            Console.WriteLine("\n");
        }//print()
    }//class Board
}//namespace Practica_4
