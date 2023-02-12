using System;

namespace CodeBase.GameLogic.Ship
{
    public interface IShip
    {
        event Action Dead;
        void AddThrust(float thrustSpeed);
        void Turn(float direction, float turnSpeed);
    }
}