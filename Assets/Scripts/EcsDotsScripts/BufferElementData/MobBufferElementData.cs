using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct MobBufferElementData : IBufferElementData
    {
        public MobSpawnerData MobSpawnerData;
        public int SpawnCount;
    }
}