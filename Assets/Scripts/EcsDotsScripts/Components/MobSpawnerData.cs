using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct MobSpawnerData : IComponentData
    {
        public Entity MobPrefab;
        public int MobCount;
    }
}