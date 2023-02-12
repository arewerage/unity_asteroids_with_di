using System;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public class SceneRequest
    {
        public string Name { get; set; }
        public LoadSceneMode Mode { get; set; }
        public Action OnLoaded { get; set; }
    }
}
