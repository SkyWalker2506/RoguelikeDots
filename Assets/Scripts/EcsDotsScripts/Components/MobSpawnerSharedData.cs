using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct MobSpawnerSharedData : ISharedComponentData
    {
        public float MinDistance;
        public float MaxDistance;
    }
}