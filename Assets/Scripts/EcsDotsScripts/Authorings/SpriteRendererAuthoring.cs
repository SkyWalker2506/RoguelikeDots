using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authorings
{
    public class SpriteRendererAuthoring : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private int frameCount = 4;
        [SerializeField] private float frameTimerMax = 0.1f;
        private class SpriteRendererAuthoringBaker : Baker<SpriteRendererAuthoring>
        {
            public override void Bake(SpriteRendererAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpriteAnimationData
                {
                    MaterialId = authoring.material.GetInstanceID(),
                    FrameCount = authoring.frameCount,
                    FrameTimerMax = authoring.frameTimerMax,
                } );
                AddSharedComponent(entity, new MaterialData {MaterialId = authoring.material.GetInstanceID()} );
            }
        }

    }
}