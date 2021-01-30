using System.Threading.Tasks;
using Tofunaut.TofuECS;
using Tofunaut.TofuUnity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Tofunaut.AnPrimTheGame
{
    public class GameStateModel : IAppStateModel
    {
        public string GameConfigReference;
    }
    
    public class GameStateController : AppStateController<GameStateController, GameStateModel>
    {
        public GameRunner gameRunner;
        private GameConfig _gameConfig;

        public async void Start()
        {
            while (!IsReady)
                await Task.Yield();
            
            gameRunner.StartGame(_gameConfig.config);
        }
        
        public override async void SetModel(GameStateModel model)
        {
            base.SetModel(model);

            _gameConfig = await Addressables.LoadAssetAsync<GameConfig>(new AssetReference(model.GameConfigReference)).Task;

            IsReady = true;
        }
        
    }
}