using UnityEngine;

namespace RootBoy
{
    public class GameStateManager
    {
        private static GameStateManager _instance;
        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameStateManager();

                return _instance;
            }
        }

        public GameState CurrentGameState { get; private set; }

        public delegate void GameStateChangeHandler(GameState newGameState);
        public event GameStateChangeHandler OnGameStateChanged;

        private GameStateManager()
        {

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