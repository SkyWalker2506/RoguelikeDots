using UnityEngine;

namespace MonoScripts
{
    public class SpriteAnimationManager : MonoBehaviour
    {
        public Mesh QuadMesh;
        public Material Material;

        public static SpriteAnimationManager Instance;
        
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    }
}