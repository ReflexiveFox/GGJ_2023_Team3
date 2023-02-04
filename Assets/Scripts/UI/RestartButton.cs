using UnityEngine;
using UnityEngine.SceneManagement;

namespace RootBoy
{
    public class RestartButton : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}