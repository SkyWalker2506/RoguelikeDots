using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Physics.Authoring;
using UnityEngine;

namespace RoguelikeDots.Authoring
{
    public class AreaDamageDataAuthoring : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private float damage;
        [SerializeField] private PhysicsCategoryTags damageToTags;
        private class AreaDamageDataAuthoringBaker : Baker<AreaDamageDataAuthoring>
        {
            public override void Bake(AreaDamageDataAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new AreaDamageData
                {
                    Tags = authoring.damageToTags,
                    Radius = authoring.radius,
                    Damage = authoring.damage
                    
                });
            }
        }
    }
}