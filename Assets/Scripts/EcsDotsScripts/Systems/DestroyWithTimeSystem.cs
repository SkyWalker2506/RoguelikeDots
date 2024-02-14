using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [BurstCompile]
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
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            new DestroyWithTimeJob
            {
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
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