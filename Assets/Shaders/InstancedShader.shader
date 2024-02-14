Shader "Custom/InstancedShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
    }
 
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100
        Cull Back
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha
 
        Pass
        {
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
 
            sampler2D _MainTex;

            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, _MainTex_UV)
            UNITY_INSTANCING_BUFFER_END(Props)
 
            v2f vert (appdata v)
            {
                v2f o;
 
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
 
                float3 worldPosition = float3(v.vertex.x, v.vertex.y, -v.vertex.y / 100);
 
                o.vertex = UnityObjectToClipPos(float4(worldPosition, 1.0f));
                o.uv = (v.uv * UNITY_ACCESS_INSTANCED_PROP(Props, _MainTex_UV).xy) + UNITY_ACCESS_INSTANCED_PROP(Props, _MainTex_UV).zw;
 
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
 
                fixed4 col = tex2D(_MainTex, i.uv) * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
                clip(col.a - .25f);
                col.rgb *= col.a;
 
                return col;
            }
            ENDCG
        }
    }
}