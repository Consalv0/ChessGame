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
    public static bool turn = false;
    public static int whiteCaptured = 0;
    public static int blackCaptured = 0;
    public static bool gameIsOver = false;

    // Main Structure
    static void Main(string[] args) {
      // Set Text Encoding
      Console.OutputEncoding = Encoding.UTF8;

      Console.Clear();

      setPieces();
      placePieces();
      printBoard();
      cursorListening();

      // To Display Console Text Correctly At the End of Program.
      Console.SetCursorPosition(0, Console.WindowHeight - 1);
    }

    // Know IF Number Is In Board Limits
    public static bool numberInBoard(int x, int y = 0) {
      if (x >= 0 && x < 8) {
        if (y >= 0 && y < 8) {
          return true;
        }
      }
      return false;
    }

    // Known IF the String Is A White Piece
    public static string isWhite(string piece) {
      if (piece == "♙") return "false";
      if (piece == "♖") return "false";
      if (piece == "♘") return "false";
      if (piece == "♗") return "false";
      if (piece == "♕") return "false";
      if (piece == "♔") return "false";

      if (piece == "♟") return "true";
      if (piece == "♜") return "true";
      if (piece == "♞") return "true";
      if (piece == "♝") return "true";
      if (piece == "♛") return "true";
      if (piece == "♚") return "true";

      return "NaN";
    }

    // Obtain Piece String From X and Y in Matrix
    public static string getPiece(int x, int y) {
      return board[y, x];
    }

    // Create All Posible Movements
    public static int[,] getPosibleMoves(string piece, int x, int y) {
      int[,] moves = new int[1,1];
      // Movements for Black Pawn
      if (piece == "♙") {
        moves = new int[4,2];
        // Set Position of All Movents to -1. -1
        for (var i = 0; i < moves.GetLength(0); i++) {
          moves[i,0] = -1; moves[i,1] = -1;
        }
        // Simple Step Foward
        if (numberInBoard(1 + y, 0 + x)) {
          if (board[1 + y, 0 + x] == "") {
            moves[0,0] = 0 + x; moves[0,1] = 1 + y;
          }
        }
        // Sided Step IF White Piece
        if (numberInBoard(1 + y, 1 + x)) {
          if (isWhite(board[1 + y, 1 + x]) == "true") {
            moves[1,0] = 1 + x; moves[1,1] = 1 + y;
          }
        }
        if (numberInBoard(1 + y, -1 + x)) {
          if (isWhite(board[1 + y, -1 + x]) == "true") {
            moves[2,0] = -1 + x; moves[2,1] = 1 + y;
          }
        }
        // Doble Step IF Inicial Position
        if (y == 1) {
          moves[3,0] = 0 + x; moves[3,1] = 2 + y;
        }
      }

      // Movements for White Pawn
      if (piece == "♟") {
        moves = new int[4,2];
        // Set Position of All Movents to -1. -1
        for (var i = 0; i < moves.GetLength(0); i++) {
          moves[i,0] = -1; moves[i,1] = -1;
        }
        // Simple Step Foward
        if (numberInBoard(-1 + y, 0 + x)) {
          if (board[-1 + y, 0 + x] == "") {
            moves[0,0] = 0 + x; moves[0,1] = -1 + y;
          }
        }
        // Sided Step IF Black Piece
        if (numberInBoard(-1 + y, 1 + x)) {
          if (isWhite(board[-1 + y, 1 + x]) == "false") {
            moves[1,0] = 1 + x; moves[1,1] = -1 + y;
          }
        }
        if (numberInBoard(-1 + y, -1 + x)) {
          if (isWhite(board[-1 + y, -1 + x]) == "false") {
            moves[2,0] = -1 + x; moves[2,1] = -1 + y;
          }
        }
        // Doble Step IF Inicial Position
        if (y == 6) {
          moves[3,0] = 0 + x; moves[3,1] = -2 + y;
        }
      }

      // Movements for Rook
      if (piece == "♜" || piece == "♖") {
        moves = new int[16,2];
        // Set Position of All Movents to -1. -1
        for (var i = 0; i < moves.GetLength(0); i++) {
          moves[i,0] = -1; moves[i,1] = -1;
        }

        // Make Moves in X Axis
        // Make Moves From the Piece to the Top
        for (var i = x; i >= 0; i--) {
          if (i == x) continue;
          if (isWhite(board[y, i]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[y, i]) != isWhite(piece)
                && isWhite(board[y, i]) != "NaN") {
              moves[i,0] = i; moves[i,1] = y;
              break;
            }
            moves[i,0] = i; moves[i,1] = y;
          }
        }
        // Make Moves From the Piece to the Bottom
        for (var i = x; i < 8; i++) {
          if (i == x) continue;
          if (isWhite(board[y, i]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[y, i]) != isWhite(piece)
                && isWhite(board[y, i]) != "NaN") {
              moves[i,0] = i; moves[i,1] = y;
              break;
            }
            moves[i,0] = i; moves[i,1] = y;
          }
        }

        // Make Moves in Y Axis
        // Make Moves From the Piece to the Left
        for (var i = y; i >= 0; i--) {
          if (i == y) continue;
          if (isWhite(board[i, x]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[i, x]) != isWhite(piece)
                && isWhite(board[i, x]) != "NaN") {
              moves[i+8,0] = x; moves[i+8,1] = i;
              break;
            }
            moves[i+8,0] = x; moves[i+8,1] = i;
          }
        }
        // Make Moves From the Piece to the Rigth
        for (var i = y; i < 8; i++) {
          if (i == y) continue;
          if (isWhite(board[i, x]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[i, x]) != isWhite(piece)
                && isWhite(board[i, x]) != "NaN") {
              moves[i+8,0] = x; moves[i+8,1] = i;
              break;
            }
            moves[i+8,0] = x; moves[i+8,1] = i;
          }
        }
      }

      // Movements for Knight
      if (piece == "♞" || piece == "♘") {
        moves = new int[8,2];
        // Set Position of All Movents to -1. -1
        for (var i = 0; i < moves.GetLength(0); i++) {
          moves[i,0] = -1; moves[i,1] = -1;
        }

        // Set Posible Positions
        if (numberInBoard(-2 + y, 1 + x)) {
          if (isWhite(board[-2 + y, 1 + x]) != isWhite(piece)) {
            moves[0,0] = 1 + x; moves[0,1] = -2 + y;
          }
        }
        if (numberInBoard(2 + y, 1 + x)) {
          if (isWhite(board[2 + y, 1 + x]) != isWhite(piece)) {
            moves[1,0] = 1 + x; moves[1,1] = 2 + y;
          }
        }
        if (numberInBoard(-2 + y, -1 + x)) {
          if (isWhite(board[-2 + y, -1 + x]) != isWhite(piece)) {
            moves[2,0] = -1 + x; moves[2,1] = -2 + y;
          }
        }
        if (numberInBoard(2 + y, -1 + x)) {
          if (isWhite(board[2 + y, -1 + x]) != isWhite(piece)) {
            moves[3,0] = -1 + x; moves[3,1] = 2 + y;
          }
        }
        if (numberInBoard(1 + y, -2 + x)) {
          if (isWhite(board[1 + y, -2 + x]) != isWhite(piece)) {
            moves[4,0] = -2 + x; moves[4,1] = 1 + y;
          }
        }
        if (numberInBoard(1 + y, 2 + x)) {
          if (isWhite(board[1 + y, 2 + x]) != isWhite(piece)) {
            moves[5,0] = 2 + x; moves[5,1] = 1 + y;
          }
        }
        if (numberInBoard(-1 + y, -2 + x)) {
          if (isWhite(board[-1 + y, -2 + x]) != isWhite(piece)) {
            moves[6,0] = -2 + x; moves[6,1] = -1 + y;
          }
        }
        if (numberInBoard(-1 + y, 2 + x)) {
          if (isWhite(board[-1 + y, 2 + x]) != isWhite(piece)) {
            moves[7,0] = 2 + x; moves[7,1] = -1 + y;
          }
        }
      }

      // Movements for Bishop
      if (piece == "♝" || piece == "♗") {
        moves = new int[32,2];
        // Set Position of All Movents to -1. -1
        for (var i = 0; i < moves.GetLength(0); i++) {
          moves[i,0] = -1; moves[i,1] = -1;
        }

        // Make Moves From the Piece to the Negative Bottom
        for (var i = x; i >= 0; i--) {
          if (i == x) continue;
          if (!numberInBoard(i, x - i + y)) continue;
          if (isWhite(board[x - i + y, i]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[x - i + y, i]) != isWhite(piece)
                && isWhite(board[x - i + y, i]) != "NaN") {
              moves[i,0] = i; moves[i,1] = x - i + y;
              break;
            }
            moves[i,0] = i; moves[i,1] = x - i + y;
          }
        }
        // Make Moves From the Piece to the Positive Bottom
        for (var i = y; i >= 0; i--) {
          if (i == y) continue;
          if (!numberInBoard(y - i + x, i)) continue;
          if (isWhite(board[i, y - i + x]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[i, y - i + x]) != isWhite(piece)
                && isWhite(board[i, y - i + x]) != "NaN") {
              moves[i+16,0] = y - i + x; moves[i+16,1] = i;
              break;
            }
            moves[i+16,0] = y - i + x; moves[i+16,1] = i;
          }
        }
        // Make Moves From the Piece to the Positive Top
        for (var i = x; i < 8; i++) {
          if (i == x) continue;
          if (!numberInBoard(i, i - x + y)) continue;
          if (isWhite(board[i - x + y, i]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[i - x + y, i]) != isWhite(piece)
                && isWhite(board[i - x + y, i]) != "NaN") {
              moves[i+8,0] = i; moves[i+8,1] = i - x + y;
              break;
            }
            moves[i+8,0] = i; moves[i+8,1] = i - x + y;
          }
        }
        // Make Moves From the Piece to the Negative Top
        for (var i = x; i >= 0; i--) {
          if (i == x) continue;
          if (!numberInBoard(i, i - x + y)) continue;
          if (isWhite(board[i - x + y, i]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[i - x + y, i]) != isWhite(piece)
                && isWhite(board[i - x + y, i]) != "NaN") {
              moves[i+24,0] = i; moves[i+24,1] = i - x + y;
              break;
            }
            moves[i+24,0] = i; moves[i+24,1] = i - x + y;
          }
        }
      }

      // Movements for Queen
      if (piece == "♛" || piece == "♕") {
        moves = new int[48,2];
        // Set Position of All Movents to -1. -1
        for (var i = 0; i < moves.GetLength(0); i++) {
          moves[i,0] = -1; moves[i,1] = -1;
        }

        // Make Moves From the Piece to the Negative Bottom
        for (var i = x; i >= 0; i--) {
          if (i == x) continue;
          if (!numberInBoard(i, x - i + y)) continue;
          if (isWhite(board[x - i + y, i]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[x - i + y, i]) != isWhite(piece)
                && isWhite(board[x - i + y, i]) != "NaN") {
              moves[i,0] = i; moves[i,1] = x - i + y;
              break;
            }
            moves[i,0] = i; moves[i,1] = x - i + y;
          }
        }
        // Make Moves From the Piece to the Positive Bottom
        for (var i = y; i >= 0; i--) {
          if (i == y) continue;
          if (!numberInBoard(y - i + x, i)) continue;
          if (isWhite(board[i, y - i + x]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[i, y - i + x]) != isWhite(piece)
                && isWhite(board[i, y - i + x]) != "NaN") {
              moves[i+16,0] = y - i + x; moves[i+16,1] = i;
              break;
            }
            moves[i+16,0] = y - i + x; moves[i+16,1] = i;
          }
        }
        // Make Moves From the Piece to the Positive Top
        for (var i = x; i < 8; i++) {
          if (i == x) continue;
          if (!numberInBoard(i, i - x + y)) continue;
          if (isWhite(board[i - x + y, i]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[i - x + y, i]) != isWhite(piece)
                && isWhite(board[i - x + y, i]) != "NaN") {
              moves[i+8,0] = i; moves[i+8,1] = i - x + y;
              break;
            }
            moves[i+8,0] = i; moves[i+8,1] = i - x + y;
          }
        }
        // Make Moves From the Piece to the Negative Top
        for (var i = x; i >= 0; i--) {
          if (i == x) continue;
          if (!numberInBoard(i, i - x + y)) continue;
          if (isWhite(board[i - x + y, i]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[i - x + y, i]) != isWhite(piece)
                && isWhite(board[i - x + y, i]) != "NaN") {
              moves[i+24,0] = i; moves[i+24,1] = i - x + y;
              break;
            }
            moves[i+24,0] = i; moves[i+24,1] = i - x + y;
          }
        }

        // Make Moves in X Axis
        // Make Moves From the Piece to the Top
        for (var i = x; i >= 0; i--) {
          if (i == x) continue;
          if (isWhite(board[y, i]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[y, i]) != isWhite(piece)
                && isWhite(board[y, i]) != "NaN") {
              moves[i+32,0] = i; moves[i+32,1] = y;
              break;
            }
            moves[i+32,0] = i; moves[i+32,1] = y;
          }
        }
        // Make Moves From the Piece to the Bottom
        for (var i = x; i < 8; i++) {
          if (i == x) continue;
          if (isWhite(board[y, i]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[y, i]) != isWhite(piece)
                && isWhite(board[y, i]) != "NaN") {
              moves[i+32,0] = i; moves[i+32,1] = y;
              break;
            }
            moves[i+32,0] = i; moves[i+32,1] = y;
          }
        }

        // Make Moves in Y Axis
        // Make Moves From the Piece to the Left
        for (var i = y; i >= 0; i--) {
          if (i == y) continue;
          if (isWhite(board[i, x]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[i, x]) != isWhite(piece)
                && isWhite(board[i, x]) != "NaN") {
              moves[i+40,0] = x; moves[i+40,1] = i;
              break;
            }
            moves[i+40,0] = x; moves[i+40,1] = i;
          }
        }
        // Make Moves From the Piece to the Rigth
        for (var i = y; i < 8; i++) {
          if (i == y) continue;
          if (isWhite(board[i, x]) == isWhite(piece)) {
            break;
          } else {
            if (isWhite(board[i, x]) != isWhite(piece)
                && isWhite(board[i, x]) != "NaN") {
              moves[i+40,0] = x; moves[i+40,1] = i;
              break;
            }
            moves[i+40,0] = x; moves[i+40,1] = i;
          }
        }
      }

      // Movements for King
      if (piece == "♚" || piece == "♔") {
        moves = new int[12,2];
        // Set Position of All Movents to -1. -1
        for (var i = 0; i < moves.GetLength(0); i++) {
          moves[i,0] = -1; moves[i,1] = -1;
        }

        for (var i = -1; i < 2; i++) {
          for (var j = -1; j < 2; j++) {
            if (numberInBoard(i + y, j + x)) {
              if (isWhite(board[i + y, j + x]) != isWhite(piece)) {
                moves[(i+1)*3 + j+1,0] = j + x;
                moves[(i+1)*3 + j+1,1] = i + y;
            } }
        } }
      }

      return moves;
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

        // Set Posible Movements
        int[] piecePos = {cursor[0], cursor[1]};
        var piece = getPiece(piecePos[0], piecePos[1]);
        int[,] posibleMoves = getPosibleMoves(piece, piecePos[0], piecePos[1]);

        bool doBreak = false;
        do {
          keyPress = Console.ReadKey(true);

          // Prevent Pick Piece of Other Player
          if (isWhite(piece) == "true" && turn) doBreak = true;
          if (isWhite(piece) == "false" && !turn) doBreak = true;

          // Input Definition to Move Cursor
          if (keyPress.Key == ConsoleKey.UpArrow)
            moveCursor(-1, 0);
          if (keyPress.Key == ConsoleKey.DownArrow)
            moveCursor(1, 0);
          if (keyPress.Key == ConsoleKey.LeftArrow)
            moveCursor(0, -1);
          if (keyPress.Key == ConsoleKey.RightArrow)
            moveCursor(0, 1);

          // Place New Position of Piece
          if (keyPress.Key == ConsoleKey.Spacebar) {
            // Check All Posible Moves Concurrences
            for (var i = 0; i < posibleMoves.GetLength(0); i++) {
              // Display All Posible Movements
              if (numberInBoard(posibleMoves[i, 0], posibleMoves[i, 1])) {
                Console.SetCursorPosition(posibleMoves[i, 0] * 2, posibleMoves[i, 1]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓");
                Console.ResetColor();
                System.Threading.Thread.Sleep(50);
              }
              // Movement Is Possible
              if (posibleMoves[i, 0] == cursor[0] && posibleMoves[i, 1] == cursor[1]) {
                board[piecePos[1], piecePos[0]] = "";
                // Movement IS Capturing Piece
                if (board[posibleMoves[i, 1], posibleMoves[i, 0]] != "") {
                  // Check Game Over
                  if (board[posibleMoves[i, 1], posibleMoves[i, 0]] == "♚") {
                    gameOver("white");
                  }
                  if (board[posibleMoves[i, 1], posibleMoves[i, 0]] == "♔") {
                    gameOver("black");
                  }
                  // Display Captured Pieces
                  if (isWhite(board[posibleMoves[i, 1], posibleMoves[i, 0]]) == "true") {
                    Console.SetCursorPosition(8 * 2 + whiteCaptured, 0);
                    whiteCaptured++;
                  } else {
                    Console.SetCursorPosition(8 * 2 + blackCaptured, 7);
                    blackCaptured++;
                  }
                  Console.Write(board[posibleMoves[i, 1], posibleMoves[i, 0]]);
                }
                board[posibleMoves[i, 1], posibleMoves[i, 0]] = piece;
                turn = !turn;
                doBreak = true;
            } }
          }

        } while (keyPress.Key != ConsoleKey.Spacebar && !doBreak);
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
        if (keyPress.Key == ConsoleKey.UpArrow)
          moveCursor(-1, 0);
        if (keyPress.Key == ConsoleKey.DownArrow)
          moveCursor(1, 0);
        if (keyPress.Key == ConsoleKey.LeftArrow)
          moveCursor(0, -1);
        if (keyPress.Key == ConsoleKey.RightArrow)
          moveCursor(0, 1);

        // Input Definition to Pick Piece
        if(keyPress.Key == ConsoleKey.Spacebar) {
          grabPiece();
        }

      } while (keyPress.Key != ConsoleKey.Escape && !gameIsOver);
    }

    // Set First Position in Board
    public static void placePieces() {
      // Clean Board
      for (var i = 0; i < board.GetLength(0); i++) {
        for (var j = 0; j < board.GetLength(1); j++) {
          board[i, j] = "";
      } }

      // Place Pieces
      for (var i = 0; i < board.GetLength(0); i++) {
        for (var j = 0; j < board.GetLength(1); j++) {
          if (i == 1) board[i, j] = whitePieces[j];
          if (i == 0) board[i, j] = whitePieces[j + 8];
          if (i == 6) board[i, j] = blackPieces[j];
          if (i == 7) board[i, j] = blackPieces[j + 8];
      } }
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
            Console.Write("  ");
          } else if (board[i, j] == "") {
            Console.Write("◆ ");
          } else {
            Console.Write(board[i, j] + " ");
          }
            placeBlack = !placeBlack;
        }
        placeBlack = !placeBlack;
        Console.WriteLine();
      }
    }

    public static void gameOver(string playerColor) {
      Console.SetCursorPosition(0, Console.WindowHeight - 1);
      if (playerColor == "white") {
        Console.WriteLine("White WINS");
      } else {
        Console.WriteLine("Black WINS");
      }
      gameIsOver = true;

      Console.WriteLine("Restart Game Y/N");
      do {
        keyPress = Console.ReadKey(true);

        // Restart Game?
        if (keyPress.Key == ConsoleKey.Y) {
          board = new string[8,8];
          whitePieces = new string[16];
          blackPieces = new string[16];
          cursor = new int[2];
          turn = false;
          whiteCaptured = 0;
          blackCaptured = 0;
          gameIsOver = false;

          Console.Clear();

          setPieces();
          placePieces();
          printBoard();
          cursorListening();
        }
      } while (keyPress.Key != ConsoleKey.Escape && keyPress.Key != ConsoleKey.N);
    }
  }
}
