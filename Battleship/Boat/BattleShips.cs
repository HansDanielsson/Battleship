namespace Battleship.Boat;

public class BattleShips : Ship
{
    public BattleShips()
    {
        SetLength(8);
    }

    public override string GetShipType()
    {
        return "battleship 8";
    }
}
