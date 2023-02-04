using System;
using UnityEngine;

namespace RootBoy
{
    public class FinalGoalTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                GameStateManager.Instance.SetState(GameState.Win);
            }
        }
    }
}