﻿using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace RoguelikeDots.Authoring
{
    public class MapSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject gridPrefab;
        [SerializeField] private int gridWidth = 3;
        [SerializeField] private int gridHeight = 3;
        [SerializeField] private Vector2 mapSize = new Vector2(20.4f,20.4f);
        [SerializeField] private float Scale = 1; 
        private int MaxSpawnCount => gridWidth * gridHeight;
        
        private class MapSpawnerAuthoringBaker : Baker<MapSpawnerAuthoring>
        {
            public override void Bake(MapSpawnerAuthoring authoring)
            {   
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new MapSpawnerData
                {   
                    GridPrefab =  GetEntity(authoring.gridPrefab,TransformUsageFlags.Dynamic),
                    GridWidth = authoring.gridWidth,
                    GridHeight = authoring.gridHeight,
                    GridBound = authoring.mapSize,
                    Scale = authoring.Scale,
                    MaxSpawnCount = authoring.MaxSpawnCount 
                }); 
                
            }
        }
    }
    
    
}