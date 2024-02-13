using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authorings
{
    public class SpriteRendererAuthoring : MonoBehaviour
    {
        [SerializeField] private int frameCount;
        [SerializeField] private float frameTimerMax;
        private class SpriteRendererAuthoringBaker : Baker<SpriteRendererAuthoring>
        {
            public override void Bake(SpriteRendererAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpriteAnimationData
                {
                    FrameCount = authoring.frameCount,
                    FrameTimerMax = authoring.frameTimerMax,
                } );
            }
        }

    }
}