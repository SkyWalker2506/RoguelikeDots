using EcsDotsScripts.Components;
using Unity.Entities;
using UnityEngine;


namespace EcsDotsScripts.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float MoveSpeed;
        public Vector2 MoveDirection;
    }

    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new MovementData
            {
                MoveSpeed = authoring.MoveSpeed,
                MoveDirection = authoring.MoveDirection
            });
        }
    }
}
