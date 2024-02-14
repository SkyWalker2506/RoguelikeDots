using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authoring
{
    public class SpawnOnDeathAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject spawnPrefab;
        [SerializeField] private TransformUsageFlags transformUsageFlag;
        private class SpawnOnDeathAuthoringBaker : Baker<SpawnOnDeathAuthoring>
        {
            public override void Bake(SpawnOnDeathAuthoring authoring)
            {
                
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity ,new SpawnOnDeathData
                {
                    SpawnEntity = GetEntity(authoring.spawnPrefab,authoring.transformUsageFlag)
                });
            }
        }
    }
}