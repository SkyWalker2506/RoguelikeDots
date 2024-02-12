using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoguelikeDots.Authorings
{
    public class MapSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject gridPrefab;
        [SerializeField] private int gridWidth = 3;
        [SerializeField] private int gridHeight = 3;
        [SerializeField] private Vector2 mapSize = new Vector2(20.4f,20.4f);
        private int MaxSpawnCount => gridWidth * gridHeight;
        
        private class MapSpawnerAuthoringBaker : Baker<MapSpawnerAuthoring>
        {
            public override void Bake(MapSpawnerAuthoring authoring)
            {   
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent(entity, new MapSpawnerData
                {   
                    GridPrefab =  GetEntity(authoring.gridPrefab,TransformUsageFlags.Dynamic),
                    GridWidth = authoring.gridWidth,
                    GridHeight = authoring.gridHeight,
                    MapSize = authoring.mapSize,
                    MaxSpawnCount = authoring.MaxSpawnCount 
                }); 
            }
        }
    }
    
    
}