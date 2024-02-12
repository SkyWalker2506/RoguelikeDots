using System;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authorings
{
    public class PlayerWeaponSpawnAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int projectileCount = 1;
        [SerializeField] private float cooldown = 1.2f;
        [SerializeField] private float speed = 15;
        [SerializeField] private float damage = 15;
        [SerializeField] private AttackDirection attackDirection;
        private class WeaponAuthoringBaker : Baker<PlayerWeaponSpawnAuthoring>
        {
            public override void Bake(PlayerWeaponSpawnAuthoring spawnAuthoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Components.WeaponSpawnData
                {
                    ProjectilePrefab =  GetEntity(spawnAuthoring.projectilePrefab, TransformUsageFlags.Dynamic),
                    ProjectileCount = spawnAuthoring.projectileCount,
                    Cooldown = spawnAuthoring.cooldown,
                    ProjectileSpeed = spawnAuthoring.speed,
                    Damage = spawnAuthoring.damage,
                    AttackDirection = (int)spawnAuthoring.attackDirection
                });
            }
        }
        

    }
    
    [Serializable]
    public enum AttackDirection
    {
        Attacker,
        Random
    }
}