using Unity.Entities;
using Unity.Mathematics;

namespace RoguelikeDots.Components
{
    public struct LookData : IComponentData
    {
        public float2 LookDirection;
    }
}