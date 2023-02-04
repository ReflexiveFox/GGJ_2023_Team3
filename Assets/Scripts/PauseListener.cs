using UnityEngine;

namespace EndlessWarfare
{
    public class PauseListener : MonoBehaviour
    {
        private bool canListenInput;

        private void Start()
        {
            canListenInput = true;
            GameStateManager.Instance.SetState(GameState.Gameplay);
            GameStateManager.Instance.OnGameStateChanged += DontListenPauseKeys;
        }

        private void Update()
        {
            if(canListenInput && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
            {
                GameStateManager.Instance.SetState(GameStateManager.Instance.CurrentGameState is GameState.Gameplay
                                                    ? GameState.Paused 
                                                    : GameState.Gameplay);
            }
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= DontListenPauseKeys;
        }

        private void DontListenPauseKeys(GameState newGameState)
        {
            canListenInput = newGameState is GameState.Gameplay;
        }
    }
}