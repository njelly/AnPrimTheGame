using Tofunaut.TofuUnity;
using UnityEngine;

namespace Tofunaut.AnPrimTheGame.Components
{
    public struct GridTransform : IGridCollider
    {
        public int Layer { get; set; }
        public Vector2Int Coord { get; set; }
        public Vector2Int Size { get; set; }
        public Vector2Int Offset { get; set; }
        public ECardinalDirection4 Facing { get; set; }
    }
}