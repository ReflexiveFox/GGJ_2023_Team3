using UnityEngine;
using UnityEngine.SceneManagement;

namespace RootBoy
{
    public class GameStateManager : MonoBehaviour
    {
        public delegate void GameStateChangeHandler(GameState newGameState);
        public event GameStateChangeHandler OnGameStateChanged;

        public static GameStateManager Instance;

        public GameState CurrentGameState { get; private set; }


        private void Awake()
        {
            // if the istance exists and it's not us
            if (Instance != null && Instance != this)
            {
                //destroy this instance 
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                // don't destroy the game manager while changing the levels
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += SetGameState;
                PlayerHealth.OnPlayerDead += SetLostGameState;
            }
        }

        private void SetLostGameState()
        {
            SetState(GameState.Lost);
        }

        private void SetGameState(Scene scene, LoadSceneMode loadSceneMode)
        {
            if(scene.buildIndex == 0)   //Main menu
            {
                SetState(GameState.MainMenu);
            }
            else if(scene.buildIndex >= 1)
            {
                SetState(GameState.Gameplay);
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= SetGameState;
        }

        public void SetState(GameState newGameState)
        {
            if (newGameState == CurrentGameState)
                return;

            CurrentGameState = newGameState;
            
            Time.timeScale = CurrentGameState is GameState.Gameplay ? 1f : 0f;

            OnGameStateChanged?.Invoke(newGameState);
        }
    }
}