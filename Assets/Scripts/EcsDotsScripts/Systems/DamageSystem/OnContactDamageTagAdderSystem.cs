﻿using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(ContactSystem))]
    [UpdateBefore(typeof(DeathOnContactSystem))]
    [BurstCompile]
    public partial struct OnContactDamageTagAdderSystem : ISystem
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
            var JobHandle = new OnContactDamageTagAdderJob
            {
                ECB = ecb
            }.ScheduleParallel(state.Dependency);
            state.Dependency = JobHandle;            
        }
    }
    
    public partial struct OnContactDamageTagAdderJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
        
        private void Execute(Entity entity, ref ContactData contactData, DamageDealerData damageDealer,in DamageOnContactTag _, [ChunkIndexInQuery] int sortKey)
        {
            ECB.RemoveComponent<DamageOnContactTag>(sortKey, entity);
            ECB.AddComponent(sortKey, contactData.ContactEntity, new DamageData
            {
                Damage = damageDealer.Damage
            });
        }
    }
}