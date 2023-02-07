namespace CodeBase.GameLogic.Spaceship
{
    public interface IShipInput
    {
        bool IsThrusting { get; }
        float TurnValue { get; }
    }
}
