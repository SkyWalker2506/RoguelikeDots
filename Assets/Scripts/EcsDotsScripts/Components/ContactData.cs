using Unity.Entities;
using Unity.Mathematics;

namespace RoguelikeDots.Components
{
    public struct ContactData : IComponentData
    {
        public Entity ContactEntity;
        public float3 ContactPoint;
    }
}