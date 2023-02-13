using System;

namespace CodeBase.GameLogic.Ship
{
    public interface IShipInput
    {
        event Action Fired;
        
        bool IsThrusting { get; }
        float TurnValue { get; }
    }
}