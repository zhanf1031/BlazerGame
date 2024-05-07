using System;

class Program
{
    static Random rng = new Random();

    static void Main(string[] args)
    {
        bool playing = true;

        while (playing)
        {
            
            List<string> deck = CreateDeck();
            Shuffle(deck: deck);

            List<string> playerHand = DrawCards(deck, 3);
            List<string> computerHand = DrawCards(deck, 3);

            
            Console.WriteLine("Your Cards:");
            DisplayHand(playerHand);
            Console.WriteLine("Computer's Cards:");
            DisplayHiddenHand(computerHand);

           
            Console.WriteLine("Would you like to switch one of your cards with the computer's? (y/n):");
            char switchChoice = Console.ReadKey(true).KeyChar;
            if (switchChoice == 'y' || switchChoice == 'Y')
            {
                ExchangeCard(playerHand, computerHand);
            }

            
            Console.WriteLine("Your Final Cards:");
            DisplayHand(playerHand);
            Console.WriteLine("Computer's Final Cards:");
            DisplayHand(computerHand);

            
            int playerSum = CalculateSum(playerHand);
            int computerSum = CalculateSum(computerHand);

            Console.WriteLine($"Your Sum: {playerSum}");
            Console.WriteLine($"Computer's Sum: {computerSum}");

            if (playerSum > computerSum)
            {
                Console.WriteLine("You win!");
            }
            else if (playerSum < computerSum)
            {
                Console.WriteLine("You Lose!");
            }
            else
            {
                Console.WriteLine("Draw! Restarting...");
                continue;
            }

            
            Console.WriteLine("Play again? (y/n):");
            char playAgain = Console.ReadKey(true).KeyChar;
            playing = playAgain == 'y' || playAgain == 'Y';
        }

        Console.WriteLine("Game Over!");

        List<string> CreateDeck()
        {
            string[] suits = {
                "♠",
                "♥",
                "♦",
                "♣"};
            string[] values = { "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
                "J",
                "Q",
                "K",
                "A" };
            List<string> deck = new List<string>();

            foreach (string suit in suits)
            {
                foreach (string value in values)
                {
                    deck.Add($"{value} of {suit}");
                }
            }

            return deck;
        }

        void Shuffle(List<string> deck)
        {
            for (int i = deck.Count - 1; i > 0; i--)
            {

                Random rng = new Random();
                int j = rng.Next(i + 1);
                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }

        List<string> DrawCards(List<string> deck, int count)
        {
            List<string> hand = new List<string>();
            for (int i = 0; i < count; i++)
            {
                hand.Add(deck[0]);
                deck.RemoveAt(0);
            }
            return hand;
        }

        void DisplayHand(List<string> hand)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {hand[i]}");
            }
        }

        void DisplayHiddenHand(List<string> hand)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                Console.WriteLine($"{i + 1}: [Hidden]");
            }
        }

        void ExchangeCard(List<string> playerHand, List<string> computerHand)
        {
            Console.WriteLine("\nChoose a card to give to the computer (1-3):");
            int playerCardPool = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("\nChoose a computer card to take (1-3):");
            DisplayHiddenHand(computerHand);
            int computerCardPool = int.Parse(Console.ReadLine()) - 1;

            string playerCard = playerHand[playerCardPool];
            string computerCard = computerHand[computerCardPool];

            playerHand[playerCardPool] = computerCard;
            computerHand[computerCardPool] = playerCard;

            Console.WriteLine("Cards switched successfully!");
        }

        int CalculateSum(List<string> hand)
        {
            int sum = 0;
            foreach (string card in hand)
            {
                sum += CardValue(card);
            }
            return sum;
        }

        int CardValue(string card)
        {
            string value = card.Split(' ')[0];
            switch (value)
            {
                case "2": return 2;
                case "3": return 3;
                case "4": return 4;
                case "5": return 5;
                case "6": return 6;
                case "7": return 7;
                case "8": return 8;
                case "9": return 9;
                case "10": return 10;
                case "J": return 11;
                case "Q": return 12;
                case "K": return 13;
                case "A": return 1;
                default: return 0;
            }
        }
    }
}