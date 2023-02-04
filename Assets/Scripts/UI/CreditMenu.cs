using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreditMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        DisableHUD();

    }
    public void EnableHUD()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisableHUD()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }
}
