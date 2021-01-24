using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    public class ActorSpawner : MonoBehaviour
    {
        public string actorName;

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

            var entity = GameRunner.AddActor(model);
        }
    }
}