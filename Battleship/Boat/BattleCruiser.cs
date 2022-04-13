namespace Battleship.Boat;

public class BattleCruiser : Ship
{
    public BattleCruiser()
    {
        SetLength(7);
    }

    public override string GetShipType()
    {
        return "battlecruiser 7";
    }
}
