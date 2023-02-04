using UnityEngine;

namespace RootBoy
{
    public class ResumeButton : MonoBehaviour
    {
        public void ResumeGame()
        {
            GameStateManager.Instance.SetState(GameState.Gameplay);
        }
    }
}