using System;
using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authorings
{
    public class MobSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] [Min(10)]private float minDistance=25;
        [SerializeField] [Range(10,100)]private int maxDistance=100;
        [SerializeField] private MobListData[] MobList;
        private class MobSpawnerAuthoringBaker : Baker<MobSpawnerAuthoring>
        {
            public override void Bake(MobSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var mobBuffer = AddBuffer<MobBufferElementData>(entity);
                foreach (var mob in authoring.MobList)
                {
                    mobBuffer.Add(new MobBufferElementData()
                    {
                        MobSpawnerData = new MobSpawnerData
                        {
                            MobPrefab = GetEntity(mob.MobPrefab.gameObject, TransformUsageFlags.Dynamic),
                            MobCount = mob.MobCount
                        }
                    });
                }
                AddSharedComponent(entity, new MobSpawnerSharedData
                {
                    MinDistance = authoring.minDistance,
                    MaxDistance = authoring.maxDistance
                });
            }
        }
        
        [Serializable]
        public struct MobListData
        {
              public MobAuthoring MobPrefab;
              public int MobCount;   
        }
    }
    

}