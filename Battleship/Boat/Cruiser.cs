namespace Battleship.Boat;

public class Cruiser : Ship
{
    public Cruiser()
    {
        SetLength(6);
    }

    public override string GetShipType()
    {
        return "cruiser 6";
    }
}
