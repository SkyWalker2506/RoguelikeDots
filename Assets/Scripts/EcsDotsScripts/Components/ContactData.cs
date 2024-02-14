using Unity.Entities;
using Unity.Mathematics;

namespace RoguelikeDots.Components
{
    public struct ContactData : IComponentData
    {
        public float3 ContactPoint;
    }
}