using System.Collections;
using System.Collections.Generic;
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
            PlayerHealth.OnPlayerDead += EnableHUD;
        }

        private void OnDestroy()
        {
            PlayerHealth.OnPlayerDead -= EnableHUD;
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