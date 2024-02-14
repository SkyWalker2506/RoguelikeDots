using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup), OrderFirst = true)]
    [BurstCompile]
    public partial struct MovementSystem :ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float time = SystemAPI.Time.DeltaTime;

            new PhysicsMovementJob
            {
                DeltaTime = time
            }.ScheduleParallel();
        }
    } 
    
    [BurstCompile]
    public partial struct MovementJob : IJobEntity
    {
        public float DeltaTime;

        private void Execute(ref LocalTransform localTransform, ref MovementData movementData)
        {
            if(movementData.MoveDirection.Equals(float2.zero)) return;
            float3 direction = math.normalize(new float3(movementData.MoveDirection, 0));
            localTransform.Position += direction * movementData.MoveSpeed * DeltaTime;
        }
    }
    
    [BurstCompile]
    public partial struct PhysicsMovementJob : IJobEntity
    {
        public float DeltaTime;

        private void Execute(ref PhysicsVelocity velocity, ref MovementData movementData)
        {
            if (movementData.MoveDirection.Equals(float2.zero))
            {
                velocity.Linear = float3.zero;
                return;
            }
            float3 direction = math.normalize(new float3(movementData.MoveDirection, 0));
            velocity.Linear = direction * movementData.MoveSpeed;
        }
    }
}
