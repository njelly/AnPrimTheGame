using System;
using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    public class Actor : MonoBehaviour
    {
        [Serializable]
        public class Model
        {
            public string Name;
            public string DisplayName;
            public Vector2Int ColliderSize;
            public float MoveSpeed;
            public Vector2Int Position;
        }

        public string actorName;
    }
}