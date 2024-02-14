using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]

    [UpdateBefore(typeof(SpawnOnContactSystem))]
    [BurstCompile]
    public partial struct ContactSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<PhysicsWorldSingleton>();
            state.RequireForUpdate<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            var ecbSingleton =  SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            var JobHandle = new AddContactDataJob
            {
                PhysicsWorld = physicsWorld,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(),state.Dependency);
            state.Dependency = JobHandle;
        }

    }

    [BurstCompile]
    public struct AddContactDataJob : ITriggerEventsJob
    {
        [ReadOnly] public PhysicsWorldSingleton PhysicsWorld;
        public EntityCommandBuffer ECB;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            var entity1 = triggerEvent.EntityA;
            var entity2 = triggerEvent.EntityB;
            var body1=PhysicsWorld.Bodies[triggerEvent.BodyIndexA];
            var body2=PhysicsWorld.Bodies[triggerEvent.BodyIndexB];
            ECB.AddComponent(entity1, new ContactData
            {
                ContactEntity = entity2,
                ContactPoint = body1.WorldFromBody.pos,
            });
            ECB.AddComponent(entity2, new ContactData
            {
                ContactEntity = entity1,
                ContactPoint = body2.WorldFromBody.pos,
            });
        }

    }
}