using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authoring
{
    public class DamageOnContactAuthoring : MonoBehaviour
    {
        private class DamageOnceAuthoringBaker : Baker<DamageOnContactAuthoring>
        {
            public override void Bake(DamageOnContactAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new DamageOnContactOnceTag());
            }
        }
    }
}