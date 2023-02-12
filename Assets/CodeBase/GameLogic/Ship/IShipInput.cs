namespace CodeBase.GameLogic.Ship
{
    public interface IShipInput
    {
        bool IsThrusting { get; }
        float TurnValue { get; }
    }
}