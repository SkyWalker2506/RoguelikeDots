using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RoguelikeDots.Components
{
    public struct SpriteAnimationData : IComponentData
    {
        public int MaterialId;
        public int CurrentFrame;
        public int FrameCount;
        public float FrameTimer;
        public float FrameTimerMax;
        public float4 UV;
        public Matrix4x4 Matrix;
    }
    
    public struct MaterialData : ISharedComponentData
    {
        public int MaterialId;
    }
}