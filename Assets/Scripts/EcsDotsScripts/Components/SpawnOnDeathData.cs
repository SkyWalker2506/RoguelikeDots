using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct SpawnOnDeathData : IComponentData
    {
        public Entity SpawnEntity;   
    }
}