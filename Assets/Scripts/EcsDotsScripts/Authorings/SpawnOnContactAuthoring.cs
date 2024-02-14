using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authoring
{
    public class SpawnOnContactAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject spawnPrefab;
        [SerializeField] private TransformUsageFlags transformUsageFlag;
        private class SpawnOnDeathAuthoringBaker : Baker<SpawnOnContactAuthoring>
        {
            public override void Bake(SpawnOnContactAuthoring authoring)
            {
                
                var entity = GetEntity(TransformUsageFlags.None);
                var scale = authoring.spawnPrefab.transform.localScale;
                AddComponent(entity ,new SpawnOnContactData
                {
                    SpawnEntity = GetEntity(authoring.spawnPrefab,authoring.transformUsageFlag),
                    SpawnScale = (scale.x+scale.y+scale.z)/3
                });
            }
        }
    }
    
    
}