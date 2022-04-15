using Battleship.Sea;

namespace Battleship.Boat;

public abstract class Ship
{
    private int bowRow;         // The row (0 to 19) wgitch contains the bow (front) of the ship.
    private int bowColumn;      // The column which contains the bow (front) of the ship.
    private int length;         // The number of squares occupied by the ship.
                                // An "empty sea" location has length 1.
    private bool horizontal;    // True if the ship occupies a single row, false otherwise.
    // Ships will either be placed vertically or horizontally in the ocean.
    private bool[]? hit;         // This is a boolean array of size 8 that record hits.
    // Only battleship use all the locations. The others will use fewer.

    public int GetBowRow() => bowRow;

    public void SetBowRow(int bowRow) => this.bowRow = bowRow;

    public int GetBowColumn() => bowColumn;

    public void SetBowColumn(int bowColumn) => this.bowColumn = bowColumn;

    public int GetLength() => length;

    public void SetLength(int length)
    {
        bowRow = -1;
        bowColumn = -1;
        this.length = length;
        horizontal = false;
        hit = new bool[length];
        for (int i = 0; i < length; i++)
        {
            hit[i] = false;
        }
    }

    public bool IsHorizontal() => horizontal;

    public void SetHorizontal(bool horizontal) => this.horizontal = horizontal;

    public bool GetHit(int i) => hit![i];

    public void SetHit(int i) => hit![i] = true;

    public abstract string GetShipType();

    /**
     * 
     */
    public bool OkToPlaceShipAt(int row, int column, bool horizontal, Ocean ocean)
    {
        int x1 = row - 1;
        int x2 = row + 2;
        int y1 = column - 1;
        int y2 = column + 2;
        int oceanLength = ocean.GetShipArray().GetLength(0);
        bool okPlace = true;

        if (horizontal)
        {
            y2 = column + length + 1;
            if (y2 > oceanLength + 1)
            {
                okPlace = false;
            }
        }
        else
        {
            x2 = row + length + 1;
            if (x2 > oceanLength + 1)
            {
                okPlace = false;
            }
        }
        x1 = Math.Max(x1, 0);
        x2 = Math.Min(x2, oceanLength);
        y1 = Math.Max(y1, 0);
        y2 = Math.Min(y2, oceanLength);
        int x, y;
        for (x = x1; x < x2; x++)
        {
            for (y = y1; y < y2; y++)
            {
                if (ocean.GetShipArray()[x,y].GetBowRow() != -1)
                {
                    okPlace = false;
                }
            }
        }
        return okPlace;
    }

    public void PlaceShipAt(int row, int column, bool horizontal, Ocean ocean)
    {
        int position;
        bowRow = row;
        bowColumn = column;
        this.horizontal = horizontal;

        if (horizontal)
        {
            for (position = 0; position < length; position++)
            {
                ocean.GetShipArray()[row, column + position].SetBowRow(row);
                ocean.GetShipArray()[row, column + position].SetBowColumn(column);
            }
        }
        else
        {
            for (position = 0; position < length; position++)
            {
                ocean.GetShipArray()[row + position, column].SetBowRow(row);
                ocean.GetShipArray()[row + position, column].SetBowColumn(column);
            }
        }
    }

    public bool ShootAt(int row, int column)
    {
        bool shoot = false;
        int hitPos;
        if (horizontal)
        {
            if (bowRow == row)
            {
                hitPos = column - bowColumn;
                if (hitPos < length)
                {
                    shoot = true;
                    hit![hitPos] = true;
                }
            }
        }
        else if (bowColumn == column)
        {
            hitPos = row - bowRow;
            if (hitPos < length)
            {
                shoot = true;
                hit![hitPos] = true;
            }
        }
        return shoot;
    }

    public bool IsSunk()
    {
        bool sunk = true;
        for (int i = 0; i < hit!.Length; i++)
        {
            if (!hit[i])
            {
                sunk = false;
            }
        }
        return sunk;
    }

    public override string ToString()
    {
        bool sunk = true;
        for (int i = 0; i < hit!.Length; i++)
        {
            if (!hit[i])
            {
                sunk = false;
            }
        }
        return sunk ? "x." : "S.";
    }

}
