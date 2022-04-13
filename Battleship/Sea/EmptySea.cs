using Battleship.Boat;

namespace Battleship.Sea;

public class EmptySea : Ship
{
    public EmptySea()
    {
        SetLength(1);
    }

    public bool ShootAt(int row, int column)
    {
        return false;
    }

    public bool IsSunk()
    {
        return false;
    }

    public override string ToString()
    {
        return "..";
    }


    public override string GetShipType()
    {
        return "empty 1";
    }
}
