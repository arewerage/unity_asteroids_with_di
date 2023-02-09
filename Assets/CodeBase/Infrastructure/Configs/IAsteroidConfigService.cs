namespace CodeBase.Infrastructure.Configs
{
    public interface IAsteroidConfigService
    {
        void LoadAll(string path);

        AsteroidConfig GetBy(AsteroidSize size);
    }
}
