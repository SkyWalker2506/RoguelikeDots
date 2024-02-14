using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace EcsDotsScripts.Aspects
{
    public readonly partial struct SpriteRendererAspect : IAspect
    {
        private readonly RefRO<LocalTransform> localTransform;
        private readonly RefRW<SpriteAnimationData> spriteAnimationData;
        
        public void Animate(float deltaTime)
        {
            SpriteAnimationData animationData = spriteAnimationData.ValueRW;
            animationData.FrameTimer += deltaTime;
            if (animationData.FrameTimer >= animationData.FrameTimerMax)
            {
                    animationData.FrameTimer = 0;
                    animationData.CurrentFrame++;
                    if (animationData.CurrentFrame >= animationData.FrameCount)
                    {
                        animationData.CurrentFrame = animationData.Loop ? 0 : animationData.FrameCount-1;
                    }

            }
            float4 uv = new float4(1,1,0,0);

            uv[0]=1f/animationData.FrameCount;
            uv[2]=uv[0]*animationData.CurrentFrame;
            
            animationData.UV = uv;
            
            
            float3 position = localTransform.ValueRO.Position;
            position.z = position.y * 0.1f;
            animationData.Matrix = Matrix4x4.TRS(position, localTransform.ValueRO.Rotation, localTransform.ValueRO.Scale*Vector3.one);
            spriteAnimationData.ValueRW = animationData;
        }
    }
}