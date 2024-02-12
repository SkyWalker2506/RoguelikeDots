using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup),OrderLast = true)]    
    public partial struct MobSpawnSystem : ISystem
    {
        private bool IsSpawned;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if(IsSpawned) return;
            var ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            new MobSpawnJob
            {
                ECB = ECB.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                Center = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position.xy
            }.ScheduleParallel();
            IsSpawned = true;
        }
        
        
    }
    
    
    [BurstCompile]
    public partial struct MobSpawnJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
        public float2 Center;
            
        private void Execute(Entity entity, DynamicBuffer<MobBufferElementData> mobBuffer, [ChunkIndexInQuery] int sortKey)
        {
            float minDistance = 40;
            float maxDistance = 100;
                
            for (int i = 0; i < mobBuffer.Length; i++)
            {
                for (int j = 0; j < mobBuffer[i].MobSpawnerData.MobCount; j++)
                {
                    var mob = ECB.Instantiate(sortKey, mobBuffer[i].MobSpawnerData.MobPrefab);   
                    Random random = new Random((uint) (i * 1000 + j*10)+1);
                    var radius = random.NextFloat(minDistance,maxDistance);
                    var angle = random.NextFloat(0, math.PI*2);
                    var x = radius * math.cos(angle);
                    var y = radius * math.sin(angle);
                    ECB.SetComponent(sortKey, mob, new LocalTransform()
                        {
                            Position = new float3(Center.x + x , Center.y + y, 0),
                            Rotation = quaternion.identity,
                            Scale = 1
                        }
                    );

                }
            }
        }
        
    }
    
}