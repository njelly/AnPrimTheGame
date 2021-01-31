using Tofunaut.AnPrimTheGame.Components;
using Tofunaut.TofuECS;
using Tofunaut.TofuUnity.Interfaces;
using UnityEngine;

namespace Tofunaut.AnPrimTheGame
{
    public class OnGridMoverMoved : IBlackboardEvent
    {
        public Vector2Int PreviousPosition;
        public Vector2Int NewPosition;
    }
    
    public unsafe class GridMoverSystem : ECSSystem
    {
        public override void Update(Frame f)
        {
            var gridMovers = f.Filter<GridMover>();
            while(gridMovers.Next(out var e, out var gridMover))
                UpdateGridMover(e, gridMover);
        }

        private static void UpdateGridMover(ulong e, GridMover* gridMover)
        {
            gridMover->MoveTimer -= Time.deltaTime;
        }
    }
}