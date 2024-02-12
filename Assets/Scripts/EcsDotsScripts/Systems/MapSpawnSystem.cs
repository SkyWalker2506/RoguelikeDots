using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Scenes;


namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(SceneSystemGroup))]
    [BurstCompile]
    public partial struct MapSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            new GridSpawnJob
            {
                ECB = entityCommandBuffer.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
                
            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct GridSpawnJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
        
        private void Execute(Entity entity, ref MapSpawnerData mapSpawnerData, [ChunkIndexInQuery] int sortKey)
        {
            if (!mapSpawnerData.IsInitialized)
            {
                var buffer = ECB.AddBuffer<GridBufferElementData>(sortKey,entity);

                for (int i = 0; i < mapSpawnerData.MaxSpawnCount; i++)
                {
                    var gridEntity = ECB.Instantiate(sortKey, mapSpawnerData.GridPrefab);

                    buffer.Add(new GridBufferElementData
                    {
                        Entity = gridEntity
                    });
                }

                mapSpawnerData.IsInitialized = true;
            }
        }
        
    }
   
}