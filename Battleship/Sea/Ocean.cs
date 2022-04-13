using Battleship.Boat;

namespace Battleship.Sea;

public class Ocean
{
    private static Ship[,] ships = new Ship[20,20]; // Used to quickly determine which ship is in given location.
    private static ushort shotsFired;                  // The total number of shots fired by the user.
    private static ushort hitCount;                    // The number of times a shot hit a ship.
    private static ushort shipsSunk;                   // The number of ships sunk.

    public Ocean()
    {
        for (ushort x = 0; x < ships.GetLength(0); x++)
        {
            for (ushort y = 0; y < ships.GetLength(1); y++)
            {
                ships[x,y] = new EmptySea();
            }
        }
        shotsFired = 0;
        hitCount = 0;
        shipsSunk = 0;
    }

    public void PlaceShipOnOcean(byte forCounter, byte battleType)
    {
        Ship battlePos;
        Random rn = new();
        int row;
        int column;
        bool shiploop;
        bool horizontal;
        for (byte forloop = 0; forloop < forCounter; forloop++)
        {
            shiploop = true;
            horizontal = rn.Next(100) < 50;

            switch (battleType)
            {
                case 0:
                    battlePos = (BattleShips)new();
                    break;
                case 1:
                    battlePos = (BattleCruiser)new();
                    break;
                case 2:
                    battlePos = (Cruiser)new();
                    break;
                case 3:
                    battlePos = (LightCruiser)new();
                    break;
                case 4:
                    battlePos = (Destroyer)new();
                    break;
                default:
                    battlePos = (Submarine)new();
                    break;
            }

            battlePos.SetHorizontal(horizontal);
            while (shiploop)
            {
                row = rn.Next(20);
                column = rn.Next(20);
                if (battlePos.OkToPlaceShipAt(row, column, horizontal, this))
                {
                    ships[row, column] = battlePos;
                    battlePos.PlaceShipAt(row, column, horizontal, this);
                    shiploop = false;
                }
            }
        }
    }

    public void PlaceAllShipsRandomly()
    {
        
        // one 8-Battleship
        PlaceShipOnOcean(1,0);

        // one 7-Battle cruiser
        PlaceShipOnOcean(1, 1);

        // two 6-Cruisers
        PlaceShipOnOcean(2, 2);

        // two 5-Light Cruisers
        PlaceShipOnOcean(2, 3);

        // three 4-Destroyers
        PlaceShipOnOcean(3, 4);

        // four 3-Submarines
        PlaceShipOnOcean(4, 5);
    }


    /**
     *  Returns true if the given location contains a ship, false if it does not.
     */

    public bool IsOccupied(int row, int column) => ships[row, column].GetBowRow() != -1;

    /**
     *  Returns true if the given location contains a real ship, still afloat.
     */

    public bool ShootAt(short row, short column)
    {
        bool shoot = false;
        int bowRow = ships[row,column].GetBowRow();
        if (bowRow != -1)
        {
            int hitPos;
            int bowColumn = ships[row,column].GetBowColumn();
            if (ships[bowRow,bowColumn].IsHorizontal())
            {
                hitPos = column - bowColumn;
            }
            else
            {
                hitPos = row - bowRow;
            }
            ships[bowRow,bowColumn].SetHit(hitPos);
            ships[row,column].SetBowRow(-1);
            ships[row,column].SetBowColumn(-1);
            if (ships[bowRow,bowColumn].IsSunk())
            {
                Console.WriteLine("You just sank a " + ships[bowRow, bowColumn].GetShipType());
                shipsSunk++;
            }
            hitCount++;
            shoot = true;
        }
        shotsFired++;
        return shoot;
    }

    public int GetShotsFired() => shotsFired;

    public int GetHitCount() => hitCount;

    public int GetShipsSunk() => shipsSunk;

    public bool IsGameOver()
    {
        bool gameOver = true;
        for (int i = 0; i < ships.GetLength(0); i++)
        {
            for (int j = 0; j < ships.GetLength(1); j++)
            {
                if (ships[i,j].GetBowRow() != -1)
                {
                    gameOver = false;
                }
            }
        }
        return gameOver;
    }

    public Ship[,] GetShipArray() => ships;

    public void Print()
    {
        int row, col;
        Console.Write("..");
        for (col = 0; col < ships.GetLength(1); col++)
        {
            Console.Write(col / 10);
            Console.Write(col % 10);
        }
        Console.WriteLine("..");
        for (row = 0; row < ships.GetLength(0); row++)
        {
            Console.Write(row / 10);
            Console.Write(row % 10);
            for (col = 0; col < ships.GetLength(1); col++)
            {
                Console.Write(ships[row,col]);
            }
            Console.WriteLine(" ");
        }
    }
}
