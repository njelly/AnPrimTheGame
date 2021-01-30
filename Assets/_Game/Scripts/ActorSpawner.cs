using System.Linq;
using System.Threading.Tasks;
using Tofunaut.AnPrimTheGame.Components;
using Tofunaut.TofuECS;
using Tofunaut.TofuUnity;
using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    public class ActorSpawner : MonoBehaviour
    {
        public class Info
        {
            public Vector2Int InitialPosition;
            public ECardinalDirection4 InitialFacing;
            public int Layer;
        }
        
        public string actorName;
        public ActorView actorViewPrefab;

        private async void Start()
        {
            while (!GameRunner.IsRunning)
                await Task.Yield();

            var model = GameRunner.Config.Actors.FirstOrDefault(x => x.Name == actorName);
            if (model == default(GameConfig.Config.Actor))
            {
                Debug.LogError($"no actor found with name {actorName}");
                return;
            }

            SpawnActor(model, actorViewPrefab, new Info
            {
                InitialPosition = (Vector2Int) transform.position.RoundToVector3Int(),
                InitialFacing = ECardinalDirection4.East,
                Layer = gameObject.layer,
            });
            
            Destroy(gameObject);
        }

        private static unsafe void SpawnActor(GameConfig.Config.Actor model, ActorView viewPrefab, Info spawnInfo)
        {
            // create the entity
            var f = GameRunner.ECS.CurrentFrame;
            var e = f.Create();
            
            // add components
            f.Add<Actor>(e);
            f.Add<GridTransform>(e);
            f.Add<GridMover>(e);
            
            // initialize Actor
            var actor = f.Get<Actor>(e);
            actor->ModelHashCode = model.Name.GetHashCode();

            // initialize GridTransform
            var gridTransform = f.Get<GridTransform>(e);
            gridTransform->Coord = spawnInfo.InitialPosition;
            gridTransform->Facing = spawnInfo.InitialFacing;
            gridTransform->Layer = spawnInfo.Layer;
            gridTransform->Size = model.ColliderSize;
            gridTransform->Offset = model.ColliderOffset;
            
            // initialize GridMover
            var gridMover = f.Get<GridMover>(e);
            gridMover->MoveSpeed = model.MoveSpeed;
            
            // create the ActorView
            var actorView = Instantiate(viewPrefab);
            actorView.Initialize(e);
        }
    }
}