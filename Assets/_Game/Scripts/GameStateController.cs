using Tofunaut.TofuECS;
using Tofunaut.TofuUnity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Tofunaut.AnPrimTheGame
{
    public class GameStateModel : IAppStateModel
    {
        public string gameConfigReference;
    }
    
    public class GameStateController : AppStateController<GameStateController, GameStateModel>
    {
        private GameConfig _gameConfig;
        
        public override async void SetModel(GameStateModel model)
        {
            base.SetModel(model);

            _gameConfig = await Addressables.LoadAssetAsync<GameConfig>(new AssetReference(model.gameConfigReference)).Task;

            IsReady = true;
        }
        
    }
}