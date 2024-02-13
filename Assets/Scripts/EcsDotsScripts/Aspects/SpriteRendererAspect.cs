using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace EcsDotsScripts.Aspects
{
    public readonly partial struct SpriteRendererAspect : IAspect
    {
        private readonly RefRO<LocalTransform> LocalTransform;
        public  readonly RefRW<SpriteAnimationData> SpriteAnimationData;
        
        public void Animate(float deltaTime)
        {
            SpriteAnimationData spriteAnimationData = SpriteAnimationData.ValueRW;
            spriteAnimationData.FrameTimer += deltaTime;
            if (spriteAnimationData.FrameTimer >= spriteAnimationData.FrameTimerMax)
            {
                spriteAnimationData.FrameTimer = 0;
                spriteAnimationData.CurrentFrame++;
                if (spriteAnimationData.CurrentFrame >= spriteAnimationData.FrameCount)
                {
                    spriteAnimationData.CurrentFrame = 0;
                }
            }
            float4 uv = new float4(1,1,0,0);

            uv[0]=1f/spriteAnimationData.FrameCount;
            uv[2]=uv[0]*spriteAnimationData.CurrentFrame;
            
            spriteAnimationData.UV = uv;
            
            
            float3 position = LocalTransform.ValueRO.Position;
            position.z = position.y * 0.1f;
            spriteAnimationData.Matrix = Matrix4x4.TRS(position, LocalTransform.ValueRO.Rotation, LocalTransform.ValueRO.Scale*Vector3.one);
            SpriteAnimationData.ValueRW = spriteAnimationData;
        }
    }
}