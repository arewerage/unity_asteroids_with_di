namespace CodeBase.Infrastructure.Configs
{
    public interface IAsteroidConfigService
    {
        void LoadAll();

        AsteroidConfig GetBy(AsteroidSize size);
    }
}
