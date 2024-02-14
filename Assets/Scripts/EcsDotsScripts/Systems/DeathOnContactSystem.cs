using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;


namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup),OrderLast = true)]

    public partial struct DeathOnContactSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton =  SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

            state.Dependency = new DeathOnContactJob
            {
                ECB = ecb
            }.ScheduleParallel(state.Dependency);
            
        }

    }
    
    [BurstCompile]
    public partial struct DeathOnContactJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
        
        private void Execute(Entity entity, ref ContactData contactData, in DeathOnContactTag deathOnContact, [ChunkIndexInQuery] int sortKey)
        {
            ECB.RemoveComponent<DeathOnContactTag>(sortKey, entity);
            ECB.AddComponent<DeathTag>(sortKey, entity);
        }
    }
}