using RoguelikeDots.BufferElementData;
using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace EcsDotsScripts.Aspects
{
    public readonly partial struct MapGridAspect : IAspect
    {
        private readonly RefRO<MapSpawnerData> mapSpawnerData;
        private readonly DynamicBuffer<GridBufferElementData> gridBufferElementData;

        public void SetGridPositions(EntityCommandBuffer.ParallelWriter ECB, int sortKey, float2 playerPosition)
        {
            int width = mapSpawnerData.ValueRO.GridWidth;
            int height = mapSpawnerData.ValueRO.GridHeight;
            float2 mapSize = mapSpawnerData.ValueRO.GridBound;
            float2 gridSize = mapSpawnerData.ValueRO.GridBound;
            float2 center = new float2((int)(playerPosition.x / gridSize.x) * gridSize.x,
                (int)(playerPosition.y / gridSize.y) * gridSize.y);
            int initialWidth = -((width - 1) / 2);
            int initialHeight = -((height - 1) / 2);
  
            for (int i = 0; i < gridBufferElementData.Length; i++)
            {
                var widthIndex = i % width;
                var heightIndex = (i - widthIndex) / width;

                ECB.SetComponent(sortKey, gridBufferElementData[i].Entity, new LocalTransform
                {
                    Position = new float3(mapSize.x * (initialWidth + widthIndex) + center.x,
                        mapSize.y * (initialHeight + heightIndex) + center.y, -1),
                    Rotation = quaternion.identity,
                    Scale = mapSpawnerData.ValueRO.GridBound.x
                });
            }
        }
    }
}