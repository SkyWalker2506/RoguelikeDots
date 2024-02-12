using EcsDotsScripts.Aspects;
using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct MobPlayerFollow : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }


        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            new MobPlayerFollowJob
            {
                PlayerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position.xy
            }.ScheduleParallel();
        }

    }
    
    [BurstCompile]
    public partial struct MobPlayerFollowJob : IJobEntity
    {
        public float2 PlayerPosition;
            
        private void Execute(MobAspect mobAspect)
        {
            mobAspect.SetMovementDirection(PlayerPosition);
        }
        
    }
    
}