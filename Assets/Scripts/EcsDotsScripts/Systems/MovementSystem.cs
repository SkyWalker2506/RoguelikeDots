using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
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
           new PhysicsMovementJob().ScheduleParallel();
        }
    } 
    
    [BurstCompile]
    public partial struct PhysicsMovementJob : IJobEntity
    {
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
