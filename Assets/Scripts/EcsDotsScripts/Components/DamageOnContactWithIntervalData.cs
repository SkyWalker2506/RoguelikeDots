using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct DamageOnContactWithIntervalData : IComponentData
    {
        public float Interval;
        public float PassedTime;
        public bool AvailableToDamage=>PassedTime>=Interval;
    }
}