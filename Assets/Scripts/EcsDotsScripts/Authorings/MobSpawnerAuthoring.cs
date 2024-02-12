using System;
using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authorings
{
    public class MobSpawnerAuthoring : MonoBehaviour
    {
        public MobListData[] MobList;
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