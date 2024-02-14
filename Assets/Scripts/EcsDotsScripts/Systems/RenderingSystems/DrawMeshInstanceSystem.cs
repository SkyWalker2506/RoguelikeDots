using MonoScripts;
using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    [UpdateAfter(typeof(SpriteAnimationSystem))]
    [RequireMatchingQueriesForUpdate]
    [BurstCompile]
    public partial class DrawMeshInstanceSystem : SystemBase
    {
        EntityQuery entityQuery;


        protected override void OnStartRunning()
        {
            entityQuery = SystemAPI.QueryBuilder().WithAll<SpriteAnimationData>().WithAll<MaterialData>().WithAll<LocalTransform>().Build();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            for (var index = 0; index < MaterialManager.Instance.MaterialCount; index++)
            {
                var instanceID = MaterialManager.Instance.GetInstanceID(index);
                entityQuery.SetSharedComponentFilter(new MaterialData {MaterialId = instanceID});
                NativeArray<SpriteAnimationData> animationDataArray = entityQuery.ToComponentDataArray<SpriteAnimationData>(Allocator.TempJob);
                NativeArray<LocalTransform> transformArray = entityQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);
                
                DrawMeshInstance(animationDataArray);
            }
        }
        
        private void DrawMeshInstance(NativeArray<SpriteAnimationData> animationDataArray)
        {
            int arrayLength = animationDataArray.Length;
            if(arrayLength == 0) return;
            int materialID = animationDataArray[0].MaterialId;
            var material = MaterialManager.Instance.GetMaterial(materialID);
            Matrix4x4[] matrixArray = new Matrix4x4[arrayLength];
            Vector4[] uvArray = new Vector4[arrayLength];
    
            int sliceCount = 1023;
            
            for (int i = 0; i < arrayLength; i+= sliceCount)
            {
                int sliceSize = Mathf.Min(sliceCount, arrayLength - i);
                
                for (int j = 0; j < sliceSize ; j++)
                {
                    int index = i + j;
                    matrixArray[index] = animationDataArray[index].Matrix;
                    uvArray[index] = animationDataArray[index].UV;
                }

            }
            animationDataArray.Dispose();

            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            var mesh = MaterialManager.Instance.QuadMesh;
            int shaderPropertyId = Shader.PropertyToID("_MainTex_UV");

            
            propertyBlock.SetVectorArray(shaderPropertyId, uvArray);

            Graphics.DrawMeshInstanced(mesh,0,material,matrixArray,arrayLength,propertyBlock);
        }
    }
}