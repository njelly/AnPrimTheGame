using System.Linq;
using System.Threading.Tasks;
using Tofunaut.AnPrimTheGame.Components;
using Tofunaut.TofuECS;
using Tofunaut.TofuUnity;
using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    public class GameRunner : SingletonBehaviour<GameRunner>
    {
        public static bool IsRunning => HasInstance && _instance._isRunning;
        public static ECS ECS => HasInstance ? _instance._ecs : null;
        public static GameConfig.Config Config => HasInstance ? _instance._ecs.CurrentFrame.Config<GameConfig.Config>() : null;
        
        private bool _isRunning;
        private ECS _ecs;

        private void Update()
        {
            if (!_isRunning)
                return;
            
            _ecs.Tick();
        }

        public void StartGame(GameConfig.Config config)
        {
            _ecs = new ECS(config);
            _ecs.CurrentFrame.RegisterComponent<Actor>(128);
            _ecs.CurrentFrame.RegisterComponent<GridTransform>(1024);
            _ecs.CurrentFrame.RegisterComponent<GridMover>(1024);
            
            _isRunning = true;
        }

        public void StopGame()
        {
            _isRunning = false;
        }
    }
}