namespace CodeBase.GameLogic.Asteroid
{
    public interface IAsteroidsSpawner
    {
        void Spawn(int counts);
        void DespawnAll();
    }
}