using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [BurstCompile]
    public partial struct HealthSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            new SetDeathJob
            {
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct SetDeathJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(Entity entity, [ReadOnly] in HealthData healthData, [ChunkIndexInQuery] int chunkIndex)
        {
            if (healthData.CurrentHealth <= 0)
            {
                ECB.AddComponent(chunkIndex, entity, new DeathTag());
                ECB.RemoveComponent<HealthData>(chunkIndex, entity);
            }
        }
    }
    
}