﻿using Unity.Entities;
using Unity.Mathematics;

namespace RoguelikeDots.Components
{
    public struct MovementData : IComponentData
    {
        public float MoveSpeed; 
        public float2 MoveDirection;
    }
}
