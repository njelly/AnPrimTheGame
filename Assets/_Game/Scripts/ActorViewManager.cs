using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tofunaut.AnPrimTheGame.Components;
using Tofunaut.TofuECS;
using Tofunaut.TofuUnity;

namespace Tofunaut.AnPrimTheGame
{
    public class ActorViewManager : SingletonBehaviour<ActorViewManager>
    {
        private Dictionary<ulong, ActorView> _entityToView;

        protected override void Awake()
        {
            base.Awake();

            _entityToView = new Dictionary<ulong, ActorView>();
        }

        private async void Start()
        {
            while (!GameRunner.IsRunning)
                await Task.Yield();
            
            GameRunner.ECS.Subscribe<OnComponentAddedEvent<Actor>>(OnActorAdded);
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            GameRunner.ECS?.Unsubscribe<OnComponentAddedEvent<Actor>>(OnActorAdded);
        }

        private unsafe void OnActorAdded(OnComponentAddedEvent<Actor> evt)
        {
            var actor = GameRunner.ECS.CurrentFrame.Get<Actor>(evt.entity);
            var actorModel = GameRunner.Config.Actors.FirstOrDefault(x => x.Name.GetHashCode() == actor->modelHashCode);
        }
    }
}