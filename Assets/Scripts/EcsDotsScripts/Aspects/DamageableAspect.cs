using RoguelikeDots.Components;
using Unity.Entities;

namespace EcsDotsScripts.Aspects
{
    public readonly partial struct DamageableAspect : IAspect
    {
        private readonly RefRW<HealthData> healthData;
        private readonly RefRO<DamageData> damageData;

        public float Damage => damageData.ValueRO.Damage;
        public float CurrentHealth => healthData.ValueRW.CurrentHealth;
        public void ApplyDamage()
        {
            healthData.ValueRW.CurrentHealth -= damageData.ValueRO.Damage;
            
        }

    }
}