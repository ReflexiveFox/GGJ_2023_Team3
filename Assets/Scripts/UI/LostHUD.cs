using UnityEngine;

namespace RootBoy
{
    public class LostHUD : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            DisableHUD();
            GameStateManager.Instance.OnGameStateChanged += CheckState;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= CheckState;
        }

        private void CheckState(GameState newGameState)
        {
            if(newGameState is GameState.Lost)
            {
                EnableHUD();
            }
        }

        private void EnableHUD()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        private void DisableHUD()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }
    }
}