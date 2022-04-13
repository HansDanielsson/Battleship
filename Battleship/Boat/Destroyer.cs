namespace Battleship.Boat;

public class Destroyer : Ship
{
    public Destroyer()
    {
        SetLength(4);
    }

    public override string GetShipType()
    {
        return "destroyer 4";
    }
}
