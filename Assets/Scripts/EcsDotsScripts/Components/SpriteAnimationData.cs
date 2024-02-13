using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct SpriteAnimationData : IComponentData
    {
        public int CurrentFrame;
        public int FrameCount;
        public float FrameTimer;
        public float FrameTimerMax;
    }
}