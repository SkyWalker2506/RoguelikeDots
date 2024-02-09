using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RoguelikeDots.Systems
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

        private void Execute(Entity entity, ref LocalTransform localTransform, ref MovementData movementData)
        {
            if(movementData.MoveDirection.Equals(float2.zero)) return;
            float3 direction = math.normalize(new float3(movementData.MoveDirection, 0));
            localTransform.Position += direction * movementData.MoveSpeed * DeltaTime;
        }
    }
}
