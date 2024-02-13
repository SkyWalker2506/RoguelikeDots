using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Transforms;

namespace EcsDotsScripts.Aspects
{
    public readonly partial struct SpriteRendererAspect : IAspect
    {
        public readonly RefRO<LocalTransform> LocalTransform;
        public  readonly RefRO<SpriteAnimationData> SpriteAnimationData;


    }
}