using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(ContactSystem))]
    [UpdateBefore(typeof(DeathOnContactSystem))]
    [BurstCompile]
    public partial struct OnContactDamageWithIntervalTagHandlerSystem : ISystem
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
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

            new OnContactDamageWithIntervalTagUpdaterJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime
            }.ScheduleParallel();
            
            new OnContactDamageWithIntervalTagAdderJob
            {
                ECB = ecb
            }.ScheduleParallel();
        }
    }

    
    public partial struct OnContactDamageWithIntervalTagUpdaterJob : IJobEntity
    {
        public float DeltaTime;
        private void Execute(ref DamageOnContactWithIntervalData damageOnContactWithIntervalData)
        {
            damageOnContactWithIntervalData.PassedTime += DeltaTime;
        }
    }
    public partial struct OnContactDamageWithIntervalTagAdderJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
        
        private void Execute(Entity entity, ref ContactData contactData, DamageDealerData damageDealer,ref DamageOnContactWithIntervalData damageOnContactWithIntervalData, [ChunkIndexInQuery] int sortKey)
        {
            
            if (damageOnContactWithIntervalData.AvailableToDamage)
            {
                damageOnContactWithIntervalData.PassedTime = 0;
                ECB.AddComponent(sortKey, contactData.ContactEntity, new DamageData
                {
                    Damage = damageDealer.Damage
                });
            }
            ECB.RemoveComponent<ContactData>(sortKey, entity);
        }
    }
}
