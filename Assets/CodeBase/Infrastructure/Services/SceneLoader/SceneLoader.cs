using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(SceneRequest sceneRequest)
        {
            await SceneManager.LoadSceneAsync(sceneRequest.Name, sceneRequest.Mode).ToUniTask();
            
            sceneRequest.OnLoaded?.Invoke();
        }
    }
}
