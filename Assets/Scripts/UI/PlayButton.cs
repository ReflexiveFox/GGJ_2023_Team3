using UnityEngine;
using UnityEngine.SceneManagement;

namespace RootBoy
{
    public class PlayButton : MonoBehaviour
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene(2);
        }
    }
}