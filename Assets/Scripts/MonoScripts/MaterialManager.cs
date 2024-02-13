using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonoScripts
{
    public class MaterialManager : MonoBehaviour
    {
        public Mesh QuadMesh;
        private Material[] materials = Array.Empty<Material>();
        private Dictionary<int, Material> materialDictionary;
        public int MaterialCount => materials.Length;
        public static bool IsInitialized { get; set; }

        public static MaterialManager Instance;
        
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

            CreateDictionary();
            IsInitialized = true;
        }
        
        void CreateDictionary()
        {
            materials = Resources.LoadAll<Material>($"Materials/");
            materialDictionary = new Dictionary<int, Material>();
            foreach (var material in materials)
            {
                materialDictionary.Add(material.GetInstanceID(), material);
            }
        }
        
        
        public int GetInstanceID(int materialOrder)
        {
           return materials[materialOrder].GetInstanceID();
        }
        
        public Material GetMaterial(int id)
        {
            if (materialDictionary.TryGetValue(id, out var material))
            {
                return material;
            }
            Debug.LogError("Material not found");
            return null;
        }
        
    }
}