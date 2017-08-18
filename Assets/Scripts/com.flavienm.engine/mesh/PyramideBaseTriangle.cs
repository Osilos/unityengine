using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.flavienm.engine.mesh
{
    public class PyramideBaseTriangle : CustomMesh
    {
        public float xSize, ySize, zSize;

        private Color[] color = { Color.black, Color.blue, Color.yellow, Color.red, Color.green };
        
        protected override void Generate()
        {

            triangles = new int[(int)xSize * (int)zSize * 6 + 12];
            GetComponent<MeshFilter>().mesh = mesh = new Mesh();
            mesh.name = "Procedural Pyramide Carre";
            CreateVertices();
            //CreateBase();
            //CreateFaces();
            mesh.triangles = triangles;

            mesh.RecalculateNormals();

        }

        protected override void CreateVertices()
        {
            vertices = new Vector3[4];
            int v = 0;
            vertices[0] = new Vector3(0, 0, 0);
            vertices[v++] = new Vector3(xSize, 0, 0);
            vertices[v++] = new Vector3(xSize / 2, 0, Mathf.Sqrt(xSize));


            //calcul du barycentre
            Vector3 sommet = vertices[0];
            float distance2Sommets;
            Vector3 vector2Sommets = vertices[1] - vertices[2];
            vector2Sommets = vector2Sommets * 0.5f;
            //float distance2Points = Vector3.Distance(sommet, )
            Vector3 barycentre = (vector2Sommets - sommet);

            Debug.DrawLine(vertices[0], vector2Sommets, Color.green, float.PositiveInfinity);
            vertices[v++] = new Vector3(barycentre.x, ySize, barycentre.z);
            Debug.Log(v);
            
            mesh.vertices = vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = vertices[i];
                sphere.gameObject.GetComponent<Renderer>().material.color = color[i];


            }
        }
        private void CreateBase()
        {
            triangles[0] = 0; //a
            triangles[1] = 1; //b
            triangles[2] = 2; // c

            triangles[3] = 2; // b
            triangles[4] = 3; // d
            triangles[5] = 0; // c

        }
        private void CreateFaces()
        {
            triangles[6] = 4;
            triangles[7] = 1;
            triangles[8] = 0;

            triangles[9] = 4;
            triangles[10] = 2;
            triangles[11] = 1;

            triangles[12] = 4;
            triangles[13] = 3;
            triangles[14] = 2;

            triangles[15] = 4;
            triangles[16] = 0;
            triangles[17] = 3;

        }

        private void OnDrawGizmos()
        {
            if (vertices == null)
            {
                return;
            }
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.color = color[i];
                Gizmos.DrawSphere(vertices[i], 0.1f);

            }
        }
    }
}
