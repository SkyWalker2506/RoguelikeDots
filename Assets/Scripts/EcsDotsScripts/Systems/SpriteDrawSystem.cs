using EcsDotsScripts.Aspects;
using MonoScripts;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    [UpdateAfter(typeof(SpriteAnimationSystem))]

    [BurstCompile]
    public partial class SpriteDrawSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            
            if(SpriteAnimationManager.Instance == null)
                return;
            
            var mesh = SpriteAnimationManager.Instance.QuadMesh;
            var material = SpriteAnimationManager.Instance.Material;
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            Vector4 uv = new Vector4(1,1,0,0);
            int shaderPropertyId = Shader.PropertyToID("_MainTex_UV");
            Entities.ForEach((SpriteRendererAspect spriteRendererAspect) =>
            {
                uv[0]=1f/spriteRendererAspect.SpriteAnimationData.ValueRO.FrameCount;
                uv[2]=uv[0]*spriteRendererAspect.SpriteAnimationData.ValueRO.CurrentFrame;
                propertyBlock.SetVectorArray(shaderPropertyId, new Vector4[]{uv});

                //uv[0] spriteRendererAspect
                Graphics.DrawMesh(mesh, spriteRendererAspect.LocalTransform.ValueRO.Position, Quaternion.identity, material, 0, Camera.main, 0, propertyBlock, false, false, false);
            }).WithoutBurst().Run();
            
            // material.SetTexture("MainTexture",spriteAnimationData.ValueRO.Texture);

        }

    }
}