using RoguelikeDots.Authoring;
using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsDotsScripts.Aspects
{
    public readonly partial struct WeaponSpawnerAspect : IAspect
    {
        private readonly RefRW<WeaponSpawnData> weaponSpawnData;
        
        public void Tick(float deltaTime)
        {
            weaponSpawnData.ValueRW.PassedTime += deltaTime;
        }

        public bool CanFire()
        {
            return weaponSpawnData.ValueRW.PassedTime >= weaponSpawnData.ValueRW.Cooldown;
        }

        public void Fire(float2 spawnPosition, float2 movementDirection, EntityCommandBuffer.ParallelWriter ecb,
            int sortKey)
        {
            for (int i = 0; i < weaponSpawnData.ValueRW.ProjectileCount; i++)
            {
                var projectile = ecb.Instantiate(sortKey, weaponSpawnData.ValueRO.ProjectilePrefab);
                var moveDirection = float2.zero;
                switch (weaponSpawnData.ValueRO.AttackDirection)
                {
                    case (int)AttackDirection.Attacker:
                        moveDirection = movementDirection;
                        break;
                    case (int)AttackDirection.Random:
                        moveDirection = Random.CreateFromIndex((uint)((i+weaponSpawnData.ValueRW.PassedTime)*10000)).NextFloat2Direction();
                        break;
                }
                var direction = math.atan2(moveDirection.y, moveDirection.x);
                quaternion rotation = quaternion.RotateZ(direction);
                ecb.SetComponent(sortKey, projectile, new LocalTransform()
                {
                    Position = new float3(spawnPosition, 0),
                    Rotation = rotation,
                    Scale = 1
                });
                ecb.AddComponent(sortKey, projectile, new MovementData()
                {
                    MoveDirection = moveDirection,
                    MoveSpeed = weaponSpawnData.ValueRO.ProjectileSpeed
                });
                ecb.AddComponent(sortKey, projectile, new DamageData()
                {
                    Damage = weaponSpawnData.ValueRW.Damage
                });
            }
            weaponSpawnData.ValueRW.PassedTime = 0;

        }
    }
}