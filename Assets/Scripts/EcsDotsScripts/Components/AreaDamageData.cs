using Unity.Entities;
using Unity.Physics.Authoring;

namespace RoguelikeDots.Components
{
    public struct AreaDamageData : IComponentData
    {
        public float Radius;
        public float Damage;
        public PhysicsCategoryTags Tags;
    }
}