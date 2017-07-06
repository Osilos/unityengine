using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace com.flavienm.superhex
{
    [RequireComponent(typeof(TrailRenderer))]
    [AddComponentMenu("Custom/TrailCollider")]
    public class TrailCollider : MonoBehaviour
    {
        [SerializeField]
        private TrailRenderer trail;
        [SerializeField]
        private List<EdgeCollider2D> edges = new List<EdgeCollider2D>();
        [SerializeField]
        private int maxPointsInEdgeCollider;
        [SerializeField]
        private bool edgeColliderIsTrigger;
        private bool edgeColliderRunTime;

        private List<List<Vector2>> pointLists = new List<List<Vector2>>();
        private int lastMaxIndex = 0;

        [Header("/!\\Trail Renderer Physics Material/!\\")]
        [SerializeField]
        private PhysicsMaterial2D trailPhysicsProperty;
        

        private void Awake ()
        {
            TrailCollider[] trailsTemp = GetComponents<TrailCollider>();
            int trailTempLength = trailsTemp.Length;
            if (trailsTemp.Length > 1)
            {
                for (int i = 1; i < trailTempLength; i++)
                {
                    Destroy(trailsTemp[i]);
                }
            }
            trail = GetComponent<TrailRenderer>();
            Reset();
        }

        public void Reset ()
        {
            if(trail == null ) trail = GetComponent<TrailRenderer>();
            
            trail.Clear();
            ResetEdge();
        }

        private void ResetEdge ()
        {
            foreach(EdgeCollider2D edge in edges)
            {
                Destroy(edge);
            }
            edges.Clear();
            pointLists.Clear();
            AddNewPointList();
            lastMaxIndex = 0;
        }

        private void UpdateEdgePoints ()
        {
            Vector3[] positions = GetPositionsTrail();
            int positionsLength = positions.Length;
            for (int i = lastMaxIndex; i < positionsLength; i++)
            {
                AddPosition(positions[i]);
                if (pointLists[pointLists.Count - 1].Count >= maxPointsInEdgeCollider)
                    AddNewPointList();
            }
            lastMaxIndex = positionsLength;
        }

        private void AddPosition(Vector3 position)
        {
            List<Vector2> list = pointLists[pointLists.Count - 1];
            if (list.Count < 2)
            {
                list.Add(position);
                return;
            }

            if (HasSameDirection(list[list.Count - 2], list[list.Count - 1], position))
                list[list.Count - 1] = position;
            else
                list.Add(position);
        }

        private bool HasSameDirection (Vector3 start, Vector3 endA, Vector3 endB)
        {
            return GetDirection(start, endA) == GetDirection(start, endB);
        }

        private Vector3 GetDirection (Vector3 start, Vector3 end)
        {
            return (start - end).normalized;
        }

        private void AddNewPointList ()
        {
            pointLists.Add(new List<Vector2>());
        }

        private Vector3[] GetPositionsTrail ()
        {
            Vector3[] positions = new Vector3[trail.positionCount];
            trail.GetPositions(positions);
            return positions;
        }

        private void AddNewEdgeCollider ()
        {
            EdgeCollider2D edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
            edgeCollider.isTrigger = edgeColliderIsTrigger;

            if (trailPhysicsProperty != null)
            {
                edgeCollider.sharedMaterial = trailPhysicsProperty;
            }

            edgeColliderRunTime = edgeColliderIsTrigger;
            edgeCollider.points = new Vector2[] { Vector2.zero, Vector2.zero };
            edges.Add(edgeCollider);
        }

        private void SetEdgePoints(EdgeCollider2D edge, Vector3[] points)
        {
            int max = points.Length;
            Vector2[] pointsForEdge = new Vector2[max];
            for (int i = 0; i < max; i++)
            {
                pointsForEdge[i] = new Vector2(points[i].x, points[i].y);
            }
            edge.points = pointsForEdge;
        }

        private Vector3[] GetLocalArray(List<Vector2> list)
        {
            int max = list.Count;
            Vector3[] pointsEdge = new Vector3[max];
            for (int i = 0; i < max; i++)
                pointsEdge[i] = transform.InverseTransformPoint(list[i]);

            return pointsEdge;
        }

        private void Update()
        {
            int pointListsLength;
            
            int maxEdge = edges.Count;
            if (edgeColliderRunTime != edgeColliderIsTrigger) 
            {
                ChangeEdgeColliderTrigger();
            }
            UpdateEdgePoints();

            pointListsLength = pointLists.Count;
            for (int i = 0; i < pointListsLength; i++)
            {
                if (i >= maxEdge)
                    AddNewEdgeCollider();
                if (pointLists[i].Count > 1)
                    SetEdgePoints(edges[i], GetLocalArray(pointLists[i]));
            }
        }

        public void ChangePhysicsMaterial2D(PhysicsMaterial2D newMaterial)
        {
            trailPhysicsProperty = newMaterial;
            foreach (EdgeCollider2D edge in edges)
            {
                edge.sharedMaterial = trailPhysicsProperty;
            }
        }

        public string GetCurrentPhysicsMaterial2D()
        {
           return trailPhysicsProperty.name;
        }

        public void ChangeEdgeColliderTrigger()
        {
            edgeColliderRunTime = edgeColliderIsTrigger;
            foreach (EdgeCollider2D edge in edges) edge.isTrigger = edgeColliderIsTrigger;
        }
    }
}