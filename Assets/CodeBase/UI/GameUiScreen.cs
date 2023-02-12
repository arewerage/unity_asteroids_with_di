using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class GameUiScreen : MonoBehaviour, IGameUiScreen
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _inputHintText;
        
        public void ShowWithTitle(string title, string inputHint)
        {
            gameObject.SetActive(true);
            _titleText.text = title;
            _inputHintText.text = inputHint;
        }

        public void Hide() =>
            gameObject.SetActive(false);
    }
}
