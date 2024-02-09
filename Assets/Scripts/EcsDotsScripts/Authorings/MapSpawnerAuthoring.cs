using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authorings
{
    public class MapSpawnerAuthoring : MonoBehaviour
    {
        public GameObject GridPrefab;
        
        private class MapSpawnerAuthoringBaker : Baker<MapSpawnerAuthoring>
        {
            public override void Bake(MapSpawnerAuthoring authoring)
            {   
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MapSpawnerData
                {
                    GridPrefab = GetEntity(authoring.GridPrefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }

}