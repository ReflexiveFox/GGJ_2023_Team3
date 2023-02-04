using UnityEngine;

namespace RootBoy
{
    public class CursorHandler : MonoBehaviour
    {
        private void Start()
        {
            GameStateManager.Instance.OnGameStateChanged += SetCursorState;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= SetCursorState;
        }

        private void SetCursorState(GameState newGameState)
        {
            Cursor.lockState = newGameState is GameState.Gameplay? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}