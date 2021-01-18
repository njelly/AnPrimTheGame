using Tofunaut.TofuUnity;

namespace Tofunaut.AnPrimTheGame
{
    public class StartScreenStateModel : IAppStateModel
    {
        
    }
    
    public class StartScreenStateController : AppStateController<StartScreenStateController, StartScreenStateModel>
    {
        private void Start()
        {
            Complete();
        }
        
        public override void SetModel(StartScreenStateModel model)
        {
            base.SetModel(model);

            IsReady = true;
        }
    }
}