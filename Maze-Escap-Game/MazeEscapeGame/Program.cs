using System;

namespace MazeRunner
{
    public class MazeGame
    {
        private char[,] maze;
        private int playerX;
        private int playerY;

        public MazeGame(char[,] mazeGrid)
        {
            maze = mazeGrid;
            playerX = -1;
            playerY = -1;
        }

        public void StartGame()
        {
            Console.WriteLine("Welcome to the Maze Escape Challenge!\r\n");
            Console.WriteLine("Generated Maze:\r");
            FindStartingPoint();

            if (playerX == -1 || playerY == -1)
            {
                Console.WriteLine("Starting point not found in the maze.");
                return;
            }

            while (true)
            {
                DisplayMaze();

                Console.WriteLine("Use W, A, S, D to move. Your goal is to reach the Exit (E) !");
                Console.WriteLine($"Current Position: ({playerX + 1}, {playerY + 1})");
                Console.WriteLine("Enter your move (U/L/D/R):");
                char move = Console.ReadKey().KeyChar;

                if (!MovePlayer(move))
                {
                    Console.WriteLine("Invalid move. Try again.");
                }

                if (IsGameWon())
                {
                    Console.WriteLine("Congratulations! You've reached the Exit (E)!\r\n");
                    break;
                }
            }

            Console.Write("Do you want to play again? (Y/N): ");
            char playAgain = char.ToUpper(Console.ReadKey().KeyChar);
            if (playAgain == 'Y')
            {
                Console.WriteLine();
                StartGame();
            }
            else
            {
                Console.WriteLine("\nThank you for playing the Maze Escape Challenge!");
            }
        }

        private void FindStartingPoint()
        {
            int rows = maze.GetLength(0);
            int columns = maze.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (maze[i, j] == 'S')
                    {
                        playerX = i;
                        playerY = j;
                        return;
                    }
                }
            }
        }

        private bool MovePlayer(char move)
        {
            int newX = playerX;
            int newY = playerY;

            switch (move)
            {
                case 'U':
                    newX--;
                    break;
                case 'L':
                    newY--;
                    break;
                case 'D':
                    newX++;
                    break;
                case 'R':
                    newY++;
                    break;
                default:
                    return false;
            }

            if (IsValidMove(newX, newY))
            {
                playerX = newX;
                playerY = newY;
                return true;
            }

            return false;
        }

        private bool IsValidMove(int newX, int newY)
        {
            return newX >= 0 && newX < maze.GetLength(0) &&
                   newY >= 0 && newY < maze.GetLength(1) &&
                   maze[newX, newY] != '#';
        }

        private void DisplayMaze()
        {
            int rows = maze.GetLength(0);
            int columns = maze.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i == playerX && j == playerY)
                    {
                        Console.Write("P "); // Player position.
                    }
                    else
                    {
                        Console.Write(maze[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        private bool IsGameWon()
        {
            return maze[playerX, playerY] == 'E';
        }
    }

    public class Program
    {
        public static void Main()
        {
            char[,] mazeGrid = {
                { '#', '#', '#', '#', '#', '#', '#' },
                { '#', 'S', ' ', ' ', ' ', ' ', '#' },
                { '#', ' ', '#', '#', '#', ' ', '#' },
                { '#', ' ', ' ', ' ', '#', ' ', '#' },
                { '#', '#', '#', '#', '#', 'E', '#' }
            };

            // Create a new MazeGame object.
            MazeGame mazeGame = new MazeGame(mazeGrid);

            // Start the maze game.
            mazeGame.StartGame();
        }
    }
}
