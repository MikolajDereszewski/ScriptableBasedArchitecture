using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableBasedArchitecture.Example
{
    public class LoadSceneCommand : MonoBehaviour
    {
        [SerializeField]
        private string _sceneToLoad;

        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}