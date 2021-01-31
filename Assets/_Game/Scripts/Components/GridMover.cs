namespace Tofunaut.AnPrimTheGame.Components
{
    public struct GridMover
    {
        public bool CanMove => MoveTimer <= 0f;
        
        public float MoveSpeed;
        public float MoveTimer;
    }
}