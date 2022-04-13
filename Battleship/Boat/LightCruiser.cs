namespace Battleship.Boat;

public class LightCruiser : Ship
{
    public LightCruiser()
    {
        SetLength(5);
    }

    public override string GetShipType()
    {
        return "light cruiser 5";
    }
}
