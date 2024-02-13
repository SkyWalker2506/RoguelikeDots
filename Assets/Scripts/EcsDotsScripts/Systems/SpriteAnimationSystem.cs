using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    [UpdateBefore(typeof(SpriteDrawSystem))]
    
    [BurstCompile]
    public partial struct SpriteAnimationSystem : ISystem
    {

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new SpriteAnimationJob
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }

    }
    
    [BurstCompile]
    public partial struct SpriteAnimationJob : IJobEntity
    {
        public float DeltaTime;
        private void Execute(ref SpriteAnimationData spriteAnimationData)
        {
            spriteAnimationData.FrameTimer += DeltaTime;
            if (spriteAnimationData.FrameTimer >= spriteAnimationData.FrameTimerMax)
            {
                spriteAnimationData.FrameTimer = 0;
                spriteAnimationData.CurrentFrame++;
                if (spriteAnimationData.CurrentFrame >= spriteAnimationData.FrameCount)
                {
                    spriteAnimationData.CurrentFrame = 0;
                }
            }
        }
    }
}