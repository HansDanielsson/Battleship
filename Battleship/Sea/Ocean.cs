using Battleship.Boat;

namespace Battleship.Sea;

public class Ocean
{
    private static Ship[,] ships = new Ship[20,20]; // Used to quickly determine which ship is in given location.
    private static int shotsFired;                  // The total number of shots fired by the user.
    private static int hitCount;                    // The number of times a shot hit a ship.
    private static int shipsSunk;                   // The number of ships sunk.

    public Ocean()
    {
        for (int x = 0; x < ships.Length; x++)
        {
            for (int y = 0; y < ships.Length; y++)
            {
                ships[x,y] = new EmptySea();
            }
        }
        shotsFired = 0;
        hitCount = 0;
        shipsSunk = 0;
    }

    public void PlaceAllShipsRandomly()
    {
        bool shiploop = true;
        int forloop;
        Random rn = new Random();
        int row;
        int column;
        bool horizontal = rn.Next(100) < 50;

        // one 8-Battleship
        BattleShips battleShipPos = new();
        battleShipPos.SetHorizontal(horizontal);
        while (shiploop)
        {
            row = rn.Next(20);
            column = rn.Next(20);
            if (battleShipPos.OkToPlaceShipAt(row,column,horizontal, this)) {
                ships[row,column] = battleShipPos;
                battleShipPos.PlaceShipAt(row, column, horizontal, this);
                shiploop = false;
            }
        }

        // one 7-Battle cruiser
        shiploop = true;
        horizontal = rn.Next(100) < 50;
        BattleCruiser battleCruiserPos = new();
        battleCruiserPos.SetHorizontal(horizontal);
        while (shiploop)
        {
            row = rn.Next(20);
            column = rn.Next(20);
            if (battleCruiserPos.OkToPlaceShipAt(row,column, horizontal,this))
            {
                ships[row, column] = battleCruiserPos;
                battleCruiserPos.PlaceShipAt(row, column, horizontal, this);
                shiploop=false;
            }
        }

        // two 6-Cruisers
        for (forloop = 0; forloop < 2; forloop++)
        {
            shiploop = true;
            horizontal= rn.Next(100) < 50;
            Cruiser cruiserPos = new();
            cruiserPos.SetHorizontal(horizontal);
            while (shiploop)
            {
                row = rn.Next(20);
                column = rn.Next(20);
                if (cruiserPos.OkToPlaceShipAt(row, column, horizontal, this))
                {
                    ships[row, column] = cruiserPos;
                    cruiserPos.PlaceShipAt(row, column, horizontal, this);
                    shiploop = false;
                }
            }
        }

        // two 5-Light Cruisers
        for (forloop = 0; forloop < 2; forloop++)
        {
            shiploop = true;
            horizontal = rn.Next(100) < 50;
            LightCruiser lightcruiserPos = new();
            lightcruiserPos.SetHorizontal(horizontal);
            while (shiploop)
            {
                row = rn.Next(20);
                column = rn.Next(20);
                if (lightcruiserPos.OkToPlaceShipAt(row, column, horizontal, this))
                {
                    ships[row, column] = lightcruiserPos;
                    lightcruiserPos.PlaceShipAt(row, column, horizontal, this);
                    shiploop = false;
                }
            }
        }

        // three 4-Destroyers
        for (forloop = 0; forloop < 3; forloop++)
        {
            shiploop = true;
            horizontal = rn.Next(100) < 50;
            Destroyer destroyerPos = new();
            destroyerPos.SetHorizontal(horizontal);
            while (shiploop)
            {
                row = rn.Next(20);
                column = rn.Next(20);
                if (destroyerPos.OkToPlaceShipAt(row, column, horizontal, this))
                {
                    ships[row, column] = destroyerPos;
                    destroyerPos.PlaceShipAt(row, column, horizontal, this);
                    shiploop = false;
                }
            }
        }

        // four 3-Submarines
        for (forloop = 0; forloop < 4; forloop++)
        {
            shiploop = true;
            horizontal = rn.Next(100) < 50;
            Submarine submarinePos = new();
            submarinePos.SetHorizontal(horizontal);
            while (shiploop)
            {
                row = rn.Next(20);
                column = rn.Next(20);
                if (submarinePos.OkToPlaceShipAt(row, column, horizontal, this))
                {
                    ships[row, column] = submarinePos;
                    submarinePos.PlaceShipAt(row, column, horizontal, this);
                    shiploop = false;
                }
            }
        }
    }


    /**
     *  Returns true if the given location contains a ship, false if it does not.
     */

    public bool IsOccupied(int row, int column)
    {
        return ships[row,column].GetBowRow() != -1;
    }

    /**
     *  Returns true if the given location contains a real ship, still afloat.
     */

    public bool ShootAt(int row, int column)
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

    public int GetShotsFired()
    {
        return shotsFired;
    }

    public int GetHitCount()
    {
        return hitCount;
    }

    public int GetShipsSunk()
    {
        return shipsSunk;
    }

    public bool IsGameOver()
    {
        bool gameOver = true;
        for (int i = 0; i < ships.Length; i++)
        {
            for (int j = 0; j < ships.Length; j++)
            {
                if (ships[i,j].GetBowRow() != -1)
                {
                    gameOver = false;
                }
            }
        }
        return gameOver;
    }

    public Ship[,] GetShipArray()
    {
        return ships;
    }

    public void Print()
    {
        int row, col;
        Console.WriteLine(".....");
        for (col = 0; col < ships.Length; col++)
        {
            Console.WriteLine(col / 10);
            Console.WriteLine(col % 10);
        }
        Console.WriteLine("...");
        for (row = 0; row < ships.Length; row++)
        {
            Console.WriteLine(row / 10);
            Console.WriteLine(row % 10);
            for (col = 0; col < ships.Length; col++)
            {
                Console.WriteLine(ships[row,col]);
            }
            Console.WriteLine(" ");
        }
    }
}
