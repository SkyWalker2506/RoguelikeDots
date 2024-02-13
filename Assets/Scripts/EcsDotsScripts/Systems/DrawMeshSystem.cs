using MonoScripts;
using RoguelikeDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
namespace RoguelikeDots.Systems
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    [UpdateAfter(typeof(SpriteAnimationSystem))]
    [BurstCompile]
    public partial class DrawMeshSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnUpdate()
        {
            
            if(MaterialManager.Instance == null)
                return;
            
            var mesh = MaterialManager.Instance.QuadMesh;
            //Geçici olarak 0. indexli material kullanılıyor
            var material = MaterialManager.Instance.GetMaterial(0);
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            int shaderPropertyId = Shader.PropertyToID("_MainTex_UV");
            Vector4[] uv = new Vector4[1];
            Camera camera = Camera.main;
            Entities.ForEach(( SpriteAnimationData SpriteAnimationData) =>
            {
                uv[0]= SpriteAnimationData.UV;
                propertyBlock.SetVectorArray(shaderPropertyId, uv);
                Graphics.DrawMesh(mesh, SpriteAnimationData.Matrix, material, 0, camera, 0, propertyBlock);
            }).WithoutBurst().Run();
        }

    }
}