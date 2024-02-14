using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup), OrderFirst = true)]
    
    [BurstCompile]
    public partial struct DeathSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
   
            state.Dependency= new DestroyWithDeathJob
            {
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel(state.Dependency);
        }

    }
    
    [BurstCompile]
    public partial struct DestroyWithDeathJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
            
        private void Execute(Entity entity, in DeathTag deathTag, [ChunkIndexInQuery] int sortKey)
        { 
           ECB.RemoveComponent<DeathTag>(sortKey,entity);
           ECB.DestroyEntity(sortKey, entity);
        }
        
    }
}