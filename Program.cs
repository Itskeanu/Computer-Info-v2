using System;

namespace Gambling_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Player starts with 100 points
            int points = 100;
            bool exit = false;

            while (!exit)
            {
                // Clear console for a fresh start each round
                Console.Clear();

                // Greet the player and display their current points
                Console.WriteLine("Welkom bij het gokspel!");
                Console.WriteLine($"Je huidige punten: {points}");

                // Display the main menu
                Console.WriteLine("\nMaak nu uw keuze:\n");
                Console.WriteLine("1. Speel het gokspel (Raad een nummer tussen 1 en 10)");
                Console.WriteLine("2. Bekijk je punten");
                Console.WriteLine("9. Exit\n");

                // Get user input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char inputChar = keyInfo.KeyChar;

                switch (inputChar)
                {
                    case '1':
                        // Play the gambling game
                        points = PlayGamblingGame(points);
                        break;

                    case '2':
                        // Show current points
                        Console.WriteLine($"\nJe hebt {points} punten.");
                        break;

                    case '9':
                        // Exit the game
                        exit = true;
                        Console.WriteLine("Bedankt voor het spelen! Tot ziens.");
                        break;

                    default:
                        Console.WriteLine("Ongeldige invoer. Probeer opnieuw.");
                        break;
                }

                // Wait before showing the menu again
                if (!exit)
                {
                    Console.WriteLine("\nDruk op een toets om verder te gaan...");
                    Console.ReadKey();
                }
            }
        }

        // Method to play the gambling game
        static int PlayGamblingGame(int currentPoints)
        {
            Console.Clear();
            Console.WriteLine("Welkom bij het gokspel!");
            Console.WriteLine($"Je huidige punten: {currentPoints}");

            // Ask how many points the user wants to bet
            Console.Write("\nHoeveel punten wil je inzetten? ");
            int bet = GetBetAmount(currentPoints);

            // Ask the user to guess a number between 1 and 10
            Console.Write("Raad een nummer tussen 1 en 10: ");
            int userGuess = GetNumberBetween1And10();

            // Generate a random number between 1 and 10
            Random random = new Random();
            int randomNumber = random.Next(1, 11); // 1 to 10

            // Show the randomly generated number
            Console.WriteLine($"Het geluksnummer was: {randomNumber}");

            // Determine if the user wins or loses
            if (userGuess == randomNumber)
            {
                Console.WriteLine("Gefeliciteerd! Je hebt gewonnen!");
                currentPoints += bet; // Add bet to current points
            }
            else
            {
                Console.WriteLine("Helaas, je hebt verloren.");
                currentPoints -= bet; // Subtract bet from current points
            }

            // Return the updated points
            return currentPoints;
        }

        // Helper method to get a valid bet amount from the user
        static int GetBetAmount(int currentPoints)
        {
            int bet;
            while (true)
            {
                string betInput = Console.ReadLine();
                if (int.TryParse(betInput, out bet) && bet > 0 && bet <= currentPoints)
                {
                    return bet; // Valid bet
                }
                Console.Write($"Ongeldig bedrag. Je kunt inzetten tussen 1 en {currentPoints}: ");
            }
        }

        // Helper method to get a valid number between 1 and 10 from the user
        static int GetNumberBetween1And10()
        {
            int guess;
            while (true)
            {
                string guessInput = Console.ReadLine();
                if (int.TryParse(guessInput, out guess) && guess >= 1 && guess <= 10)
                {
                    return guess; // Valid guess
                }
                Console.Write("Ongeldige invoer. Voer een nummer in tussen 1 en 10: ");
            }
        }
    }
}