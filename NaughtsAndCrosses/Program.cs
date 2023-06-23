using System;

namespace NaughtsAndCrosses
{
    static class Program
    {
        static int[] board = new int[9];
        static int[,] lines = new int[8, 3]
        {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 },
        };
        static char[] playerChars = new char[3] { ' ', 'O', 'X' };
        static int winner = 0;
        static Random rand = new Random();
        static int turnsPlayed = 0;

        static void Main(string[] args)
        {
            while (winner == 0 && turnsPlayed < 9)
            {
                DisplayIntro();
                Console.WriteLine("\nWhere would you like to play? (0-8):");
                int target = GetIndexFromInput(Console.ReadLine());
                if (target == -1)
                {
                    Console.Write("Input is invalid. Press enter to continue.");
                    Console.ReadLine();
                }
                else if (board[target] != 0)
                {
                    Console.Write("Square is already taken. Press enter to continue.");
                    Console.ReadLine();
                }
                else
                {
                    board[target] = 1;
                    turnsPlayed++;
                    winner = GetWinner();
                    
                    if (winner == 0 && turnsPlayed < 9)
                    {
                        MakeOpponentMove();
                        turnsPlayed++;

                        winner = GetWinner();
                    }
                }
            }

            DisplayIntro();

            if (winner == 0)
            {
                Console.WriteLine("\nIt's a draw!");
            }
            else
            {
                Console.WriteLine($"\nThe winner is '{playerChars[winner]}'!");
            }
        }

        static void DisplayIntro()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Naughts and Crosses!\n");
            Console.WriteLine(" 0 | 1 | 2 ");
            Console.WriteLine("-----------");
            Console.WriteLine(" 3 | 4 | 5 ");
            Console.WriteLine("-----------");
            Console.WriteLine(" 6 | 7 | 8 ");
            Console.WriteLine("\nYour Turn:\n");
            DisplayBoard();
        }

        static int GetIndexFromInput(string? input)
        {
            if (input is null || 
                input.Length != 1 ||
                input[0] < '0' || 
                input[0] > '8') return -1;

            return Convert.ToInt32(input);
        }

        static void MakeOpponentMove()
        {
            int target = rand.Next(0, 9);
            while (board[target] != 0)
            {
                target = rand.Next(0, 9);
            }
            board[target] = 2;
        }

        static void DisplayBoard()
        {
            Console.WriteLine($" {playerChars[board[0]]} | {playerChars[board[1]]} | {playerChars[board[2]]} ");
            Console.WriteLine("-----------");
            Console.WriteLine($" {playerChars[board[3]]} | {playerChars[board[4]]} | {playerChars[board[5]]} ");
            Console.WriteLine("-----------");
            Console.WriteLine($" {playerChars[board[6]]} | {playerChars[board[7]]} | {playerChars[board[8]]} ");
        }

        static int GetWinner()
        {
            int winner = 0;

            int lineIndex = 0;
            while (winner == 0 && lineIndex < lines.GetLength(0))
            {
                int a = board[lines[lineIndex, 0]];
                int b = board[lines[lineIndex, 1]];
                int c = board[lines[lineIndex, 2]];

                if (a != 0 && a == b && a == c)
                {
                    winner = a;
                }

                lineIndex++;
            }

            return winner;
        }
    }
}