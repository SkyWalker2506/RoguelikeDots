using Unity.Entities;
using Unity.Mathematics;

namespace RoguelikeDots.Components
{
    public struct MapSpawnerData : IComponentData
    {
        public Entity GridPrefab;
        public int GridWidth;
        public int GridHeight;
        public float2 MapSize;
        public int MaxSpawnCount;
        public bool IsInitialized;
    }
}