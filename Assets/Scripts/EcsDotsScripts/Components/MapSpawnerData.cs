using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace RoguelikeDots.Components
{
    public struct MapSpawnerData : IComponentData
    {
        public Entity GridPrefab;
        public int GridWidth;
        public int GridHeight;
        public float2 GridBound;
        public int MaxSpawnCount;
        public bool IsInitialized;
    }
}