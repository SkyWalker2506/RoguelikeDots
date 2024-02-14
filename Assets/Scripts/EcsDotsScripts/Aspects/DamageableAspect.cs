﻿using RoguelikeDots.Components;
using Unity.Entities;

namespace EcsDotsScripts.Aspects
{
    public readonly partial struct DamageableAspect : IAspect
    {
        private readonly RefRW<HealthData> healthData;
        private readonly RefRO<DamageData> damageData;

        public void ApplyDamage()
        {
            healthData.ValueRW.CurrentHealth -= damageData.ValueRO.Damage;
        }

    }
}