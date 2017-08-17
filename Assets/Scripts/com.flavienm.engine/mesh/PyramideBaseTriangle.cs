using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.flavienm.engine.mesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PyramideBaseTriangle : MonoBehaviour
    {
        public float xSize, ySize, zSize;

        private Color[] color = { Color.black, Color.blue, Color.yellow, Color.red, Color.green };
        private Mesh mesh;
        private Vector3[] vertices;
        int[] triangles;

        private void Awake()
        {
            Generate();
        }
        private void Generate()
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

        private void CreateVertices()
        {
            vertices = new Vector3[4];
            int v = 0;
            Debug.Log(v);
            vertices[0] = new Vector3(0, 0, 0);
            Debug.Log(v);
            vertices[v++] = new Vector3(xSize, 0, 0);
            Debug.Log(v);
            vertices[v++] = new Vector3(xSize / 2, 0, Mathf.Sqrt(xSize));
            Debug.Log(v);


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

            /*for (int ti = 0, vi = 0, x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + (int)xSize + 1;
                triangles[ti + 5] = vi + (int)xSize + 2;
            }*/

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
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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
