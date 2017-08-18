using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.flavienm.engine.mesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class CustomMesh : MonoBehaviour
    {

        private void Awake()
        {
            Generate();
        }
        protected Mesh mesh;
        protected Vector3[] vertices;
        protected int[] triangles;

        protected virtual void Generate() { }
        protected virtual void CreateVertices() { }
        protected virtual void CreateTriangles() { }
    }
}
