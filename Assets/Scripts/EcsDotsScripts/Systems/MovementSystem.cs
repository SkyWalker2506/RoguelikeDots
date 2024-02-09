using EcsDotsScripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsDotsScripts.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct MovementSystem :ISystem
    {

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float time = SystemAPI.Time.DeltaTime;

            new MovementJob
            {
                DeltaTime = time
            }.ScheduleParallel();

        }

    } 
    
    [BurstCompile]
    public partial struct MovementJob : IJobEntity
    {
        public float DeltaTime;

        public void Execute(Entity entity, ref LocalTransform localTransform, in MovementData movementData)
        {
            float3 direction = new float3(movementData.MoveDirection, 0);
            localTransform.Position += direction * movementData.MoveSpeed * DeltaTime;
        }
    }
}
