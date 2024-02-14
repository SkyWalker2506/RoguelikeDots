using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial struct SpawnOnContactSystem : ISystem
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
            new SpawnOnContactJob
            {
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }

    }
    
     [BurstCompile]
    public partial struct SpawnOnContactJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(Entity entity, ContactData contactData, SpawnOnContactData spawnOnContact, [ChunkIndexInQuery] int sortKey)
        {
            var spawnedEntity = ECB.Instantiate(sortKey, spawnOnContact.SpawnEntity);
            ECB.SetComponent(sortKey, spawnedEntity, new LocalTransform()
            {
                Position = contactData.ContactPoint,
                Rotation = quaternion.identity,
                Scale = spawnOnContact.SpawnScale
            });
            ECB.RemoveComponent<SpawnOnContactData>(sortKey, entity);
        }
    }
}