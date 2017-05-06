using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChessGame {
  class Program {
    // Define Global Vars
    public static ConsoleKeyInfo keyPress;
    public static string[,] board = new string[8,8];
    public static string[] whitePieces = new string[16];
    public static string[] blackPieces = new string[16];
    public static int[] cursor = new int[2];

    // Main Structure
    static void Main(string[] args) {
      // Set Text Encoding
      Console.OutputEncoding = Encoding.UTF8;

      Console.Clear();

      setPieces();
      placePieces();
      printBoard();
      cursorListening();

      // To Display Console Text Correctly.
      Console.SetCursorPosition(0, Console.WindowHeight - 1);
    }

    // Obtain Piece String From X and Y in Matrix
    public static string getPiece(int x, int y) {
      return board[y, x];
    }

    // Pick A Piece and Do Movements
    public static void grabPiece() {
      if (board[cursor[1], cursor[0]] != "") {
        // Change Color When Pick
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(board[cursor[1], cursor[0]]);
        Console.ResetColor();
        Console.SetCursorPosition(cursor[0] * 2, cursor[1]);

        do {
          keyPress = Console.ReadKey(true);
          var piece = getPiece(cursor[0], cursor[1]);

          if (keyPress.Key == ConsoleKey.DownArrow) {

          }

        } while (keyPress.Key != ConsoleKey.Spacebar);
      }

      Console.SetCursorPosition(0, 0);
      printBoard();
      Console.SetCursorPosition(cursor[0] * 2, cursor[1]);
    }

    // Move Meta Side Cursor
    public static void moveCursor(int x, int y) {
      int boardRows = board.GetLength(0);
      int boardColumns = board.GetLength(1);

      // Set Cursor Limits
      if (cursor[0] + y > boardRows -1) {
        cursor[0] = 0;
        y = 0;
      } else if (cursor[0] + y < 0) {
        cursor[0] = boardRows -1;
        y = 0;
      }

      if (cursor[1] + x > boardColumns -1) {
        cursor[1] = 0;
        x = 0;
      } else if (cursor[1] + x < 0) {
        cursor[1] = boardColumns;
        y = 0;
      }

      // Move Cursor
      cursor[0] += y;
      cursor[1] += x;
      Console.SetCursorPosition(cursor[0] * 2, cursor[1]);
    }

    // Function to Listen to Inputs
    public static void cursorListening() {
      // Place Cursor in Center
      cursor[0] = 4;
      cursor[1] = 4;
      Console.SetCursorPosition(cursor[0] * 2, cursor[1]);

      do {
        keyPress = Console.ReadKey(true);

        // Input Definition to Move Cursor
        if(keyPress.Key == ConsoleKey.UpArrow)
          moveCursor(-1, 0);
        if(keyPress.Key == ConsoleKey.DownArrow)
          moveCursor(1, 0);
        if(keyPress.Key == ConsoleKey.LeftArrow)
          moveCursor(0, -1);
        if(keyPress.Key == ConsoleKey.RightArrow)
          moveCursor(0, 1);

        // Input Definition to Pick Piece
        if(keyPress.Key == ConsoleKey.Spacebar) {
          grabPiece();
        }

      } while (keyPress.Key != ConsoleKey.Escape);

    }

    // Set First Position in Board
    public static void placePieces() {
      // Clean Board
      for (var i = 0; i < board.GetLength(0); i++) {
        for (var j = 0; j < board.GetLength(1); j++) {
          board[i, j] = "";
      }}

      // Place Pieces
      for (var i = 0; i < board.GetLength(0); i++) {
        for (var j = 0; j < board.GetLength(1); j++) {
          if (i == 1) board[i, j] = whitePieces[j];
          if (i == 0) board[i, j] = whitePieces[j + 8];
          if (i == 6) board[i, j] = blackPieces[j];
          if (i == 7) board[i, j] = blackPieces[j + 8];
      }}
    }

    // Define Pieces in Correct Position for Easly Place in Board
    public static void setPieces() {
      for (var i = 0; i < whitePieces.GetLength(0); i++) {
        if (i < 8) {
          whitePieces[i] = "♙";
        } else if (i == 8 || i == 15) {
          whitePieces[i] = "♖";
        } else if (i == 9 || i == 14) {
          whitePieces[i] = "♘";
        } else if (i == 10 || i == 13) {
          whitePieces[i] = "♗";
        } else if (i == 11) {
          whitePieces[i] = "♕";
        } else {
          whitePieces[i] = "♔";
        }
      }

      for (var i = 0; i < blackPieces.GetLength(0); i++) {
        if (i < 8) {
          blackPieces[i] = "♟";
        } else if (i == 8 || i == 15) {
          blackPieces[i] = "♜";
        } else if (i == 9 || i == 14) {
          blackPieces[i] = "♞";
        } else if (i == 10 || i == 13) {
          blackPieces[i] = "♝";
        } else if (i == 11) {
          blackPieces[i] = "♛";
        } else {
          blackPieces[i] = "♚";
        }
      }
    }

    // Print Board
    public static void printBoard() {
      // Bool to Switch Grid Color
      bool placeBlack = false;
      for (var i = 0; i < board.GetLength(0); i++) {
        for (var j = 0; j < board.GetLength(1); j++) {
          // Set Black and White Grid When There's NO Pieces
          if (placeBlack && board[i, j] == "") {
            Console.Write("□ ");
          } else if (board[i, j] == "") {
            Console.Write("■ ");
          } else {
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
