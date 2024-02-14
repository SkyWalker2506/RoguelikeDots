using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsDotsScripts.Aspects
{
    public readonly partial struct MobAspect : IAspect
    {
        private readonly RefRO<MobTag> mobData;
        private readonly RefRW<MovementData> movementData;
        private readonly RefRO<LocalTransform> transformData;
        
        public void SetMovementDirection(float2 playerPosition)
        {
            movementData.ValueRW.MoveDirection = math.normalize(playerPosition - transformData.ValueRO.Position.xy);
        }
    }
}