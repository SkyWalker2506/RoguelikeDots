using Unity.Entities;

namespace RoguelikeDots.Components
{
    public struct WeaponSpawnData : IComponentData
    {
        public Entity ProjectilePrefab;
        public int ProjectileCount;
        public float Cooldown;
        public float PassedTime;
        public float ProjectileSpeed;
        public float Damage;
        public int AttackDirection;
    }
}