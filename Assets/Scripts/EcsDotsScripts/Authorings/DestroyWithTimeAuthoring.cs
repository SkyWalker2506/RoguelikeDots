using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authoring
{
    public class DestroyWithTimeAuthoring : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy = 5;

        private class DestroyAfterTimeAuthoringBaker : Baker<DestroyWithTimeAuthoring>
        {
            public override void Bake(DestroyWithTimeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new DestroyWithTimeData()
                {
                    TimeToDestroy = authoring.timeToDestroy,
                    PassedTime = 0
                });
            }
        }
    }
}