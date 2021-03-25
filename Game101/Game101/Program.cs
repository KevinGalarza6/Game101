using System;

namespace Game101
{
    class Program
    {
        static void Main()
        {
            MenuScreen();
        }

        public static void MenuScreen()
        {
            Console.Clear();
            Console.WriteLine("\n__________________________________________________________________\n");
            Console.WriteLine("                             Select Option!\n");
            Console.WriteLine("   1 - Start Game");
            Console.WriteLine("   2 - Controls Game");
            Console.WriteLine("   3 - Exit");
            Console.WriteLine("\n__________________________________________________________________\n");

            var Key = Console.ReadKey(true);

            if (Key.Key == ConsoleKey.NumPad1 || Key.Key == ConsoleKey.D1)
                GameScreen(NewGame());

            else if (Key.Key == ConsoleKey.NumPad2 || Key.Key == ConsoleKey.D2)
                ControlsScreen();

            else if (Key.Key == ConsoleKey.NumPad3 || Key.Key == ConsoleKey.D3)
                Environment.Exit(1);

            MenuScreen();
        }

        public static void ControlsScreen()
        {
            Console.Clear();
            Console.WriteLine("\n__________________________________________________________________\n");
            Console.WriteLine("   PLAYER ONE:                          PLAYER TWO:                 \n");
            Console.WriteLine("   W - A - S - D                        Arrows Keys");
            Console.WriteLine("\n   To return press \"ESC\"");
            Console.WriteLine("\n__________________________________________________________________\n");
            var Key = Console.ReadKey(true);

            if (Key.Key == ConsoleKey.Escape)
                MenuScreen();

            ControlsScreen();
        }

        public static void GameScreen(int[,] boardGame, int positionPlayerOneX = 99, int positionPlayerOneY = 99, int positionPlayerTwoX = 99, int positionPlayerTwoY = 99, int playerOneScore = 0, int playerTwoScore = 0, bool endGame = false, PlayerTime playerTime = PlayerTime.One)
        {
            if (positionPlayerOneX == 99 || positionPlayerOneY == 99 || positionPlayerTwoX == 99 || positionPlayerTwoY == 99)
            {
                positionPlayerOneX = new Random().Next(1, 10);
                positionPlayerOneY = new Random().Next(1, 10);

                var maxDistance = 2;

                positionPlayerTwoX = SetMaxPosition(positionPlayerOneX, positionPlayerTwoX, maxDistance);
                positionPlayerTwoY = SetMaxPosition(positionPlayerOneY, positionPlayerTwoY, maxDistance);

                while (positionPlayerTwoX == positionPlayerOneX && positionPlayerTwoY == positionPlayerOneY)
                {
                    positionPlayerTwoX = SetMaxPosition(positionPlayerOneX, positionPlayerTwoX, maxDistance);
                    positionPlayerTwoY = SetMaxPosition(positionPlayerOneY, positionPlayerTwoY, maxDistance);
                }
            }

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    if (i == positionPlayerOneY && j == positionPlayerOneX)
                        Console.BackgroundColor = ConsoleColor.Red;

                    if (i == positionPlayerTwoY && j == positionPlayerTwoX)
                        Console.BackgroundColor = ConsoleColor.Blue;

                    Console.Write($" {boardGame[i, j]} ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write("\n\n");
            }

            Console.Write("\n\n");
            Console.Write($"Score Player 1: {playerOneScore}\n");
            Console.Write($"Score Player 2: {playerTwoScore}\n");

            PlayersMove(boardGame, positionPlayerOneX, positionPlayerOneY, positionPlayerTwoX, positionPlayerTwoY, playerOneScore, playerTwoScore, endGame, playerTime);
        }

        private static int SetMaxPosition(int positionPlayerOne, int position, int maxDistance)
        {
            if (positionPlayerOne - maxDistance > 1 && positionPlayerOne + maxDistance < 10)
                position = new Random().Next(positionPlayerOne - maxDistance, positionPlayerOne + maxDistance);
            else if (positionPlayerOne - maxDistance > 1)
                position = new Random().Next(positionPlayerOne - maxDistance, positionPlayerOne);
            else if (positionPlayerOne + maxDistance < 10)
                position = new Random().Next(positionPlayerOne, positionPlayerOne + maxDistance);

            return position;
        }

        public static void PlayersMove(int[,] boardGame, int positionPlayerOneX = 0, int positionPlayerOneY = 0, int positionPlayerTwoX = 9, int positionPlayerTwoY = 9, int playerOneScore = 0, int playerTwoScore = 0, bool endGame = false, PlayerTime playerTime = PlayerTime.One)
        {
            while (endGame == false)
            {
                var Key = Console.ReadKey(true);

                if (playerTime == PlayerTime.One)
                {
                    if (Key.Key == ConsoleKey.A)
                    {
                        boardGame[positionPlayerOneY, positionPlayerOneX] = 0;
                        positionPlayerOneX = PlayerMoveLeftOrUp(positionPlayerOneX);
                        playerOneScore = PlayerScore(boardGame, positionPlayerOneX, positionPlayerOneY, playerOneScore);
                        playerTime = PlayerTime.Two;
                    }

                    if (Key.Key == ConsoleKey.D)
                    {
                        boardGame[positionPlayerOneY, positionPlayerOneX] = 0;
                        positionPlayerOneX = PlayerMoveRightOrDown(positionPlayerOneX);
                        playerOneScore = PlayerScore(boardGame, positionPlayerOneX, positionPlayerOneY, playerOneScore);
                        playerTime = PlayerTime.Two;
                    }

                    if (Key.Key == ConsoleKey.W)
                    {
                        boardGame[positionPlayerOneY, positionPlayerOneX] = 0;
                        positionPlayerOneY = PlayerMoveLeftOrUp(positionPlayerOneY);
                        playerOneScore = PlayerScore(boardGame, positionPlayerOneX, positionPlayerOneY, playerOneScore);
                        playerTime = PlayerTime.Two;
                    }

                    if (Key.Key == ConsoleKey.S)
                    {
                        boardGame[positionPlayerOneY, positionPlayerOneX] = 0;
                        positionPlayerOneY = PlayerMoveRightOrDown(positionPlayerOneY);
                        playerOneScore = PlayerScore(boardGame, positionPlayerOneX, positionPlayerOneY, playerOneScore);
                        playerTime = PlayerTime.Two;
                    }
                }
                else if (playerTime == PlayerTime.Two)
                {
                    if (Key.Key == ConsoleKey.LeftArrow)
                    {
                        boardGame[positionPlayerTwoY, positionPlayerTwoX] = 0;
                        positionPlayerTwoX = PlayerMoveLeftOrUp(positionPlayerTwoX);
                        playerTwoScore = PlayerScore(boardGame, positionPlayerTwoX, positionPlayerTwoY, playerTwoScore);
                        playerTime = PlayerTime.One;
                    }

                    if (Key.Key == ConsoleKey.RightArrow)
                    {
                        boardGame[positionPlayerTwoY, positionPlayerTwoX] = 0;
                        positionPlayerTwoX = PlayerMoveRightOrDown(positionPlayerTwoX);
                        playerTwoScore = PlayerScore(boardGame, positionPlayerTwoX, positionPlayerTwoY, playerTwoScore);
                        playerTime = PlayerTime.One;
                    }

                    if (Key.Key == ConsoleKey.UpArrow)
                    {
                        boardGame[positionPlayerTwoY, positionPlayerTwoX] = 0;
                        positionPlayerTwoY = PlayerMoveLeftOrUp(positionPlayerTwoY);
                        playerTwoScore = PlayerScore(boardGame, positionPlayerTwoX, positionPlayerTwoY, playerTwoScore);
                        playerTime = PlayerTime.One;
                    }

                    if (Key.Key == ConsoleKey.DownArrow)
                    {
                        boardGame[positionPlayerTwoY, positionPlayerTwoX] = 0;
                        positionPlayerTwoY = PlayerMoveRightOrDown(positionPlayerTwoY);
                        playerTwoScore = PlayerScore(boardGame, positionPlayerTwoX, positionPlayerTwoY, playerTwoScore);
                        playerTime = PlayerTime.One;
                    }
                }

                if (Key.Key == ConsoleKey.H)
                    GameScreen(NewGame());

                if (playerOneScore >= 101 || playerTwoScore >= 101)
                {
                    endGame = true;
                    PlayerWin(playerOneScore, playerTwoScore);
                }

                Console.Clear();
                GameScreen(boardGame, positionPlayerOneX, positionPlayerOneY, positionPlayerTwoX, positionPlayerTwoY, playerOneScore, playerTwoScore, endGame, playerTime);
            }
        }

        public static void PlayerWin(int playerOneScore = 0, int playerTwoScore = 0)
        {
            Console.Clear();

            if (playerOneScore == 101 || playerTwoScore > 101)
            {
                Console.WriteLine($"\n__________________________________________________________________\n");
                Console.Write($"                  ");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"Player One Victory!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("\n               Press \"ESC\" to go menu!");
                Console.WriteLine($"\n__________________________________________________________________");
            }
            else if (playerTwoScore == 101 || playerOneScore > 101)
            {
                Console.WriteLine($"\n__________________________________________________________________\n");
                Console.Write($"                  ");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Player Two Victory!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("\n                Press \"ESC\" to go menu!");
                Console.WriteLine($"\n__________________________________________________________________");
            }

            var Key = Console.ReadKey(true);

            if (Key.Key == ConsoleKey.Escape)
                MenuScreen();

            PlayerWin(playerOneScore, playerTwoScore);
        }

        private static int PlayerScore(int[,] boardGame, int positionPlayerX, int positionPlayerY, int score)
        {
            var tempScore = score;
            tempScore += boardGame[positionPlayerY, positionPlayerX];

            return tempScore;
        }

        private static int PlayerMoveLeftOrUp(int postition)
        {
            --postition;

            if (postition == -1)
                postition = 9;

            if (postition == 10)
                postition = 0;

            return postition;
        }

        private static int PlayerMoveRightOrDown(int postition)
        {
            ++postition;

            if (postition == -1)
                postition = 9;

            if (postition == 10)
                postition = 0;

            return postition;
        }

        public static int[,] NewGame()
        {
            var boardGame = new int[10, 10];

            var rand = new Random();

            Console.Clear();
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    boardGame[i, j] = rand.Next(1, 10);
                }
            }

            return boardGame;
        }

        public enum PlayerTime
        {
            One,
            Two
        }
    }
}
