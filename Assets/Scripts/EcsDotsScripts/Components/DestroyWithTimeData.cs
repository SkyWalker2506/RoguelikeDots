using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct DestroyWithTimeData : IComponentData
    {
        public float TimeToDestroy;
        public float PassedTime;
    }
}