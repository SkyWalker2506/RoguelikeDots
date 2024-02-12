using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct MobData : IComponentData
    {
        public float Health;
        public float Damage;
        public float MoveSpeed;        
        public float AttackCooldown;        
        public float AttackPassedTime;
    }
}