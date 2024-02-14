using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authoring
{
    public class HealthAuthoring : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        private class HealthAuthoringBaker : Baker<HealthAuthoring>
        {
            
            public override void Bake(HealthAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new HealthData
                {
                    MaxHealth = authoring.maxHealth,
                    CurrentHealth = authoring.maxHealth
                });
            }
        }
    }
}