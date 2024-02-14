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
    [BurstCompile]
    public partial struct PlayerAttackSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<PlayerTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            new PlayerAttackJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime,
                PlayerDirection = SystemAPI.GetComponent<LookData>(playerEntity).LookDirection,
                PlayerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position.xy,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct PlayerAttackJob : IJobEntity
    {
        public float DeltaTime;
        public float2 PlayerDirection;
        public float2 PlayerPosition;
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(WeaponSpawnerAspect weaponSpawnerAspect, [ChunkIndexInQuery] int sortKey)
        {
            weaponSpawnerAspect.Tick(DeltaTime);
            if (weaponSpawnerAspect.CanFire())
            {
                weaponSpawnerAspect.Fire(PlayerPosition,PlayerDirection,ECB,sortKey);
            }
        }
    }
}