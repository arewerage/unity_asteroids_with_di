using System;

namespace CodeBase.GameLogic.Ship
{
    public interface IShipController
    {
        public event Action PlayerDead;
        public void Spawn();
        public void Despawn();
    }
}
