using RoguelikeDots.Components;
using Unity.Entities;
using UnityEngine;


namespace RoguelikeDots.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float MoveSpeed;
        
        private class PlayerBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,new PlayerTag());
                AddComponent(entity, new MovementData
                {
                    MoveSpeed = authoring.MoveSpeed,
                });
            }
        }
    }

    
}
