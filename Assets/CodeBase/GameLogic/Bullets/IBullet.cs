using System;

namespace CodeBase.GameLogic.Bullets
{
    public interface IBullet
    {
        event Action<Bullet> Dead;
    }
}
