using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    public partial struct DestroyWithTimeSystem : ISystem
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
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            new DestroyWithTimeJob
            {
                ECB = ecb,
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }
    
    
    [BurstCompile]
    public partial struct DestroyWithTimeJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
        public float DeltaTime;
            
        private void Execute(Entity entity, ref DestroyWithTimeData destroyWithTime, [ChunkIndexInQuery] int sortKey)
        {
            destroyWithTime.PassedTime += DeltaTime;
            if (destroyWithTime.PassedTime >= destroyWithTime.TimeToDestroy)
            {
                ECB.DestroyEntity(sortKey, entity);
            }
        }
        
    }
}