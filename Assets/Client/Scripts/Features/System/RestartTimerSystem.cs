using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client
{
    sealed class RestartTimerSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameState> _gameState;

        private float _currentTime = 0;
        private float _timeToRestart;

        private bool _isFirstWork = true;

        public void Run (IEcsSystems systems)
        {
            if (_isFirstWork)
            {
                _timeToRestart = _gameState.Value.GameSettings.TimeToRestart;

                _isFirstWork = false;
            }

            _currentTime += Time.unscaledDeltaTime;

            if (_currentTime >= _timeToRestart)
            {
                RestartLevel();
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}