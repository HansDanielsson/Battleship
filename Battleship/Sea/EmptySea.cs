using Battleship.Boat;

namespace Battleship.Sea;

public class EmptySea : Ship
{
    public EmptySea()
    {
        SetLength(1);
    }

    public new bool ShootAt(int row, int column) => false;

    public new bool IsSunk() => false;

    public override string ToString()
    {
        return "..";
    }

    public override string GetShipType()
    {
        return "empty 1";
    }
}
