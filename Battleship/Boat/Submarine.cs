namespace Battleship.Boat;

public class Submarine : Ship
{
    public Submarine()
    {
        SetLength(3);
    }

    public override string GetShipType()
    {
        return "submarine 3";
    }
}
