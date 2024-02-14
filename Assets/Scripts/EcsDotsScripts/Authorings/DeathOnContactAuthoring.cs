using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authoring
{
    public class DeathOnContactAuthoring : MonoBehaviour
    {
        private class DeathOnContactAuthoringBaker : Baker<DeathOnContactAuthoring>
        {
            public override void Bake(DeathOnContactAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new DeathOnContactTag());
            }
        }
    }
}