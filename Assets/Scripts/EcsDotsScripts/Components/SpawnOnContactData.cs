using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct SpawnOnContactData : IComponentData
    {
        public Entity SpawnEntity;
        public float SpawnScale;
    }
}