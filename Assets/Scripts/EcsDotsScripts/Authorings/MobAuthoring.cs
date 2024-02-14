using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authoring
{
    public class MobAuthoring : MonoBehaviour
    {
        [SerializeField] private float health = 100;
        [SerializeField] private float damage = 10;
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float attackCooldown = 1;
        
        private class MobAuthoringBaker : Baker<MobAuthoring>
        {
            public override void Bake(MobAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MobData
                {
                    Health = authoring.health,
                    Damage = authoring.damage,
                    MoveSpeed = authoring.moveSpeed,
                    AttackCooldown = authoring.attackCooldown,
                    AttackPassedTime = authoring.attackCooldown
                });
                AddComponent(entity,new MovementData()
                {
                    MoveSpeed = authoring.moveSpeed
                });
                AddComponent(entity,new HealthData()
                {
                    CurrentHealth = authoring.health,
                    MaxHealth = authoring.health
                });
            }
        }
    }
}