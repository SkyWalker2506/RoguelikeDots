using EcsDotsScripts.Aspects;
using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup),OrderFirst = true)]
    [BurstCompile]
    public partial struct MapPlacementSystem : ISystem
    {
        private float passedTime;
        private float checkInterval;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
            passedTime = checkInterval =.5f;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            passedTime += SystemAPI.Time.DeltaTime;
            if(checkInterval > passedTime) return;
            var entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            if (playerEntity != Entity.Null)
            {
                new GridPlacementJob
                {
                    ECB = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                    PlayerPosition = SystemAPI.GetComponent<LocalTransform>(SystemAPI.GetSingletonEntity<PlayerTag>()).Position
                }.ScheduleParallel();
            }
            passedTime = 0;
        }
    }
    
    [BurstCompile]
    public partial struct GridPlacementJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
        public float3 PlayerPosition;
        private void Execute(MapGridAspect mapGridAspect, [ChunkIndexInQuery] int sortKey)
        {
            mapGridAspect.SetGridPositions(ECB, sortKey, PlayerPosition.xy);
        }
    }
}