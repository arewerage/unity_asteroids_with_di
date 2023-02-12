namespace CodeBase.UI
{
    public interface IGameUiScreen
    {
        void ShowWithTitle(string title, string inputHint);
        void Hide();
    }
}
