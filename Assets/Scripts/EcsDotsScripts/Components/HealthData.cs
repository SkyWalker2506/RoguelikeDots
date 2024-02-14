using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct HealthData : IComponentData
    {
        public float CurrentHealth;
        public float MaxHealth;
    }
}