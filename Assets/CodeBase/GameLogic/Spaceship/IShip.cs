using System;

namespace CodeBase.GameLogic.Spaceship
{
    public interface IShip
    {
        event Action Dead;
        void AddThrust(float thrustSpeed);
        void Turn(float direction, float turnSpeed);
    }
}
