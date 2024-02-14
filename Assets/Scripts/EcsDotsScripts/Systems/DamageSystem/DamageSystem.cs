using EcsDotsScripts.Aspects;
using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Systems
{
   
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(HealthSystem))]
    [BurstCompile]
    public partial struct DamageSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            new DamageJob
            {
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }
    
    
    public partial struct DamageJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(Entity entity, DamageableAspect damageable, [ChunkIndexInQuery] int sortKey)
        {
            damageable.ApplyDamage();
            ECB.RemoveComponent<DamageData>(sortKey,entity);
        }
    }
}