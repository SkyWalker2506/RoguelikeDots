using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RoguelikeDots.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10;
        
        private class PlayerBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,new PlayerTag());
                AddComponent(entity,new LookData()
                {
                    LookDirection = new float2(1,0)
                });
                AddComponent(entity, new MovementData
                {
                    MoveSpeed = authoring.moveSpeed,
                });
            }
        }
        
    }


}
