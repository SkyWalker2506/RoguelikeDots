using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct MapSpawnerData : IComponentData
    {
        public Entity GridPrefab;
    }
}