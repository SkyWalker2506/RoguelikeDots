using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authorings
{
    public class MapSpawnerAuthoring : MonoBehaviour
    {
        public GameObject GridPrefab;
        public int GridWidth = 3;
        public int GridHeight = 3;
        public Vector2 MapSize = new Vector2(20.5f,20.5f);
        public int MaxSpawnCount => GridWidth * GridHeight;
    }
    
    public class MapSpawnerAuthoringBaker : Baker<MapSpawnerAuthoring>
    {
        public override void Bake(MapSpawnerAuthoring authoring)
        {   
            var entity = GetEntity(TransformUsageFlags.Dynamic);
                
            AddComponent(entity, new MapSpawnerData
            {   
                GridPrefab =  GetEntity(authoring.GridPrefab,TransformUsageFlags.Dynamic),
                GridWidth = authoring.GridWidth,
                GridHeight = authoring.GridHeight,
                MapSize = authoring.MapSize,
                MaxSpawnCount = authoring.MaxSpawnCount 
            }); 
        }
    }
}