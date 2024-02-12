using RoguelikeDots.Components;
using RoguelikeDots.Inputs;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RoguelikeDots.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial class PlayerInputSystem : SystemBase
    {
        private GameInputActions input;
        private Vector2 directionWithVector2;
        
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            input = new GameInputActions();
        }

        protected override void OnStartRunning()
        {
            input.Enable();
        }
        
        protected override void OnStopRunning()
        {
            input.Disable();
        }

        protected override void OnUpdate()
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            SetDirection(playerEntity);
        }
        
        private void SetDirection(Entity playerEntity)
        {
            var newDirection =  input.Player.Move.ReadValue<Vector2>();
            if(directionWithVector2 == newDirection) return;
            directionWithVector2 = newDirection;
            float2 direction = new float2(directionWithVector2.x, directionWithVector2.y);
            var movementData =SystemAPI.GetComponent<MovementData>(playerEntity); 
            movementData.MoveDirection = direction;
            SystemAPI.SetComponent(playerEntity, movementData);
            if(newDirection.Equals(Vector2.zero)) return;
            var playerData = SystemAPI.GetComponent<LookData>(playerEntity);
            playerData.LookDirection = direction;
            SystemAPI.SetComponent(playerEntity, playerData);
            
        }
    }
}