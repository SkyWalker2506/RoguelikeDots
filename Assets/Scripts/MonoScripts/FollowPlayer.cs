using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    Entity playerEntity;
    EntityManager entityManager;
    private void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private void LateUpdate()
    {
        if (entityManager.Exists(playerEntity))
        {
            Vector3 playerPosition = entityManager.GetComponentData<LocalTransform>(playerEntity).Position;
            transform.position = playerPosition + offset;
        }
        else
        {
            playerEntity = entityManager.CreateEntityQuery(typeof(PlayerTag)).GetSingletonEntity();
        }
    }
}