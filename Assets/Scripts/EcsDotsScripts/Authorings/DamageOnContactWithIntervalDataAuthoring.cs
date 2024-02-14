using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authoring
{
    public class DamageOnContactWithIntervalDataAuthoring : MonoBehaviour
    {
        public float Interval;

        public class DamageOnContactWithIntervalDataBaker : Baker<DamageOnContactWithIntervalDataAuthoring>
        {
            public override void Bake(DamageOnContactWithIntervalDataAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new DamageOnContactWithIntervalData
                {
                    Interval = authoring.Interval,
                    PassedTime = authoring.Interval,
                });
            }
        }
    }
}