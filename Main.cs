using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChessGame
{
    class Program
    {
        public static string[,] board = new string[8,8];
        public static string[] whitePieces = new string[16];
        public static string[] blackPieces = new string[16];
        public static int[] cursor = new int[2];

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Clear();

            setPieces();
            placePieces();
            printBoard();
            cursorListening();

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
        }

        public static void cursorListening()
        {
            cursor[0] = 4;
            cursor[1] = 4;
            Console.SetCursorPosition(cursor[0], cursor[1]);
        }

        public static void placePieces()
        {
            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = "";
                }
            }

            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    if (i == 1)
                    {
                        board[i, j] = whitePieces[j];
                    }

                    if (i == 0)
                    {
                        board[i, j] = whitePieces[j + 8];
                    }

                    if (i == 6)
                    {
                        board[i, j] = blackPieces[j];
                    }

                    if (i == 7)
                    {
                        board[i, j] = blackPieces[j + 8];
                    }
                }
            }
        }

        public static void setPieces()
        {
            for (var i = 0; i < whitePieces.GetLength(0); i++)
            {
                if (i < 8)
                {
                    whitePieces[i] = "♙";
                } else if (i == 8 || i == 15)
                {
                    whitePieces[i] = "♖";
                } else if (i == 9 || i == 14)
                {
                    whitePieces[i] = "♘";
                } else if (i == 10 || i == 13)
                {
                    whitePieces[i] = "♗";
                } else if (i == 11)
                {
                    whitePieces[i] = "♕";
                } else
                {
                    whitePieces[i] = "♔";
                }
            }

            for (var i = 0; i < blackPieces.GetLength(0); i++)
            {
                if (i < 8)
                {
                    blackPieces[i] = "♟";
                }
                else if (i == 8 || i == 15)
                {
                    blackPieces[i] = "♜";
                }
                else if (i == 9 || i == 14)
                {
                    blackPieces[i] = "♞";
                }
                else if (i == 10 || i == 13)
                {
                    blackPieces[i] = "♝";
                }
                else if (i == 11)
                {
                    blackPieces[i] = "♛";
                }
                else
                {
                    blackPieces[i] = "♚";
                }
            }
        }

        public static void printBoard()
        {
            bool placeBlack = false;
            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    if (placeBlack && board[i,j] == "")
                    {
                        Console.Write("□ ");
                    } else if (board[i, j] == "")
                    {
                        Console.Write("■ ");
                    } else
                    {
                        Console.Write(board[i, j] + " ");
                    }
                    placeBlack = !placeBlack;
                }
                placeBlack = !placeBlack;
                Console.WriteLine();
            }
        }
    }
}
