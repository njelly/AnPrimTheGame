using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    public class ActorView : MonoBehaviour
    {
        public ulong Entity { get; private set; }

        public void Initialize(ulong entity)
        {
            Entity = entity;
        }
    }
}