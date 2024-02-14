using RoguelikeDots.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    Entity playerEntity;
    EntityManager entityManager;
    private EntityQuery playerTagQuery;
    
    private void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        playerTagQuery = entityManager.CreateEntityQuery(typeof(PlayerTag));
    }

    private void LateUpdate()
    {
        if (playerTagQuery.IsEmptyIgnoreFilter) return;

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