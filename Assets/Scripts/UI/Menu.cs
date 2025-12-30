using UnityEngine;
using UnityEngine.SceneManagement;

namespace OculusionLabyrinth.Menu
{
    public class Menu : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
