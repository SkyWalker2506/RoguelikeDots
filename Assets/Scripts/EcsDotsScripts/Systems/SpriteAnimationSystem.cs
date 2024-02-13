using EcsDotsScripts.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(MovementSystem))]
    
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
        private void Execute(SpriteRendererAspect spriteRendererAspect)
        {
            spriteRendererAspect.Animate(DeltaTime);
        }
    }
}