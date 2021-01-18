using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    public interface IGridCollider
    {
        int Layer { get; }
        Vector2Int Coord { get; }
        Vector2Int Size { get; }
        Vector2Int Offset { get; }
    }
}