using UnityEngine;
using UnityEngine.SceneManagement;

namespace RootBoy
{

    public class MainMenuButton : MonoBehaviour
    {
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}