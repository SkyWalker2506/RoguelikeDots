using Unity.Entities;
using UnityEngine;

namespace RoguelikeDots.Authorings
{
    public class MapGridAuthoring : MonoBehaviour
    {
        
        private class MapGridBaker : Baker<MapGridAuthoring>
        {
            public override void Bake(MapGridAuthoring authoring)
            {
            }
        }
    }
    

    
}