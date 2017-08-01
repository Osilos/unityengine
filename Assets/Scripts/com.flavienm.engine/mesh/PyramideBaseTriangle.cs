using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace com.flavienm.engine.mesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PyramideBaseTriangle : Editor
    {
        public int xSize, ySize, zSize;


        private Mesh mesh;
        private Vector3[] vertices;

        private void Generate()
        {

        }
        [MenuItem("GameObject/Create Other/Tetrahedron")]
        static void Create()
        {
            //GameObject gameObject = new GameObject("Tetrahedron");
            //PyramideBaseTriangle s = gameObject.AddComponent(PyramideBaseTriangle);
            //MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            //meshFilter.mesh = new Mesh();
            //s.Rebuild();
        }

        private void CreateBase()
        {

        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}