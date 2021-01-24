using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    public struct GridCollider : IGridCollider
    {
        public int layer;
        public Vector2Int coord;
        public Vector2Int size;
        public Vector2Int offset;
        
        public int Layer => layer;
        public Vector2Int Coord => coord;
        public Vector2Int Size => size;
        public Vector2Int Offset => offset;
    }
}