using System.Threading.Tasks;
using Tofunaut.TofuUnity;
using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    public class AppStateMachine : MonoBehaviour
    {
        private const int StartScreenSceneIndex = 1;
        private const int GameSceneIndex = 2;

        private AppState<StartScreenStateController, StartScreenStateModel> _startScreenState;
        private AppState<GameStateController, GameStateModel> _gameState;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            _startScreenState = new AppState<StartScreenStateController, StartScreenStateModel>(StartScreenSceneIndex);
            _gameState = new AppState<GameStateController, GameStateModel>(GameSceneIndex);
        }

        private async void Start()
        {
            var startScreenStateModel = new StartScreenStateModel();
            var gameStateModel = new GameStateModel();

            while (true)
            {
                await _startScreenState.Enter(startScreenStateModel);
                while (!_startScreenState.IsComplete)
                    await Task.Yield();

                await _gameState.Enter(gameStateModel);
                while(!_gameState.IsComplete)
                    await Task.Yield();
            }
        }
    }
}