using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    private CanvasGroup cG;
    private void Awake()
    {
        cG = GetComponent<CanvasGroup>();
    }

        private void Start()
    {
        DisableHUD();
    }

        public void EnableHUD()
        {
            cG.alpha = 1f;
            cG.blocksRaycasts = true;
        }

        public void DisableHUD()
        {
            cG.alpha = 0f;
            cG.blocksRaycasts = false;
        }
}
