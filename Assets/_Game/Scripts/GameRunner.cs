using System.Linq;
using System.Threading.Tasks;
using Tofunaut.AnPrimTheGame.Components;
using Tofunaut.TofuECS;
using Tofunaut.TofuUnity;

namespace Tofunaut.AnPrimTheGame
{
    public class GameRunner : SingletonBehaviour<GameRunner>
    {
        public static bool IsRunning => _instance._isRunning;
        public static ECS ECS => HasInstance ? _instance._ecs : null;
        public static GameConfig.Config Config => _instance._ecs.CurrentFrame.Config<GameConfig.Config>();
        
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
            _isRunning = true;
        }

        public void StopGame()
        {
            _isRunning = false;
        }

        public static async Task<ulong> AddActor(GameConfig.Config.Actor actorModel)
        {
            while (!_instance._isRunning)
                await Task.Yield();

            var toReturn = _instance._ecs.CurrentFrame.Create();

            return toReturn;
        }
    }
}