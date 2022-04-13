using Battleship.Sea;

namespace Battleship;

public class BattleshipGame
{

    public static Ocean board = new();

    private static string? MessageInput(string msg)
    {
        Console.Write(msg);
        string? printInput = Console.ReadLine();
        return printInput;
    }

    private static void PrintShots()
    {
        Console.WriteLine("Number of shots: " + board.GetShotsFired());
        Console.WriteLine("Number of hits: " + board.GetHitCount());
        Console.WriteLine("Number of ships: " + board.GetShipsSunk());
    }

    private static void InputShots()
    {
        short row, column;
        string? inShot = MessageInput("Shoot 5 shots: ");
        string[] arrInShot = inShot!.Split(';');
        string[] sShot;
        foreach (var Point in arrInShot)
        {
            sShot = Point.Split(',');
            try
            {
                row = short.Parse(sShot[0]);
                column = short.Parse(sShot[1]);
            }
            catch (Exception)
            {
                row = 0;
                column = 0;
            }
            if (board.ShootAt(row, column))
            {
                Console.WriteLine("Hitt ship row:" + row + ", col:" + column);
            }
        }
    }



    public static void Main(string[] args)
    {
        board.PlaceAllShipsRandomly();
        string? textIn;
        board.Print();
        bool PlayAgain = true;
        while (PlayAgain)
        {
            InputShots();
            if (board.IsGameOver())
            {
                PlayAgain = false;
            }
            else
            {
                textIn = MessageInput("Play again (Y/N) ? ");
                if (textIn!.Equals("P"))
                {
                    board.Print();
                }
                else
                {
                    PlayAgain = textIn.Equals("Y");
                }
            }
        }
    }
}