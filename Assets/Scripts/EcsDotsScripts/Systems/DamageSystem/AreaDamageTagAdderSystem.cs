﻿using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [BurstCompile]
    public partial struct AreaDamageTagAdderSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PhysicsWorldSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            var deltaTime = SystemAPI.Time.DeltaTime;
            var physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            new AreaDamageTagAdderJob
            {
                ECB = ecb,
                DeltaTime = deltaTime,
                PhysicsWorld = physicsWorld
            }.ScheduleParallel();
        }

    }
    
    [BurstCompile]
    public partial struct AreaDamageTagAdderJob : IJobEntity
    {
        [ReadOnly] public PhysicsWorldSingleton PhysicsWorld;
        public EntityCommandBuffer.ParallelWriter ECB;
        public float DeltaTime;
        
        private void Execute(LocalTransform localTransform, ref AreaDamageData areaDamageData, [ChunkIndexInQuery] int sortKey)
        {
            float3 center = localTransform.Position;
            CollisionFilter filter = CollisionFilter.Default;
            filter.CollidesWith = areaDamageData.Tags.Value;
            NativeList<DistanceHit> hits = new NativeList<DistanceHit>(Allocator.Temp);
            PhysicsWorld.OverlapSphere(center, areaDamageData.Radius,ref hits, filter);
            foreach (var hit in hits)
            {
                var entity = PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
                if (entity != Entity.Null)
                {
                    ECB.AddComponent(sortKey, entity, new DamageData()
                    {
                        Damage = areaDamageData.Damage*DeltaTime
                    });
                }
            }          
            hits.Dispose();
        }
    }
}
