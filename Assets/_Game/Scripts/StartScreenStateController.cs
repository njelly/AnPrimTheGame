using System.IO;
using System.Net;
using Tofunaut.TofuUnity;
using UnityEditor;
using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    public class StartScreenStateModel : IAppStateModel
    {
        
    }
    
    public class StartScreenStateController : AppStateController<StartScreenStateController, StartScreenStateModel>
    {
        private async void Start()
        {
            Complete();
        }
        
        public override async void SetModel(StartScreenStateModel model)
        {
            IsReady = true;
        }
    }
}