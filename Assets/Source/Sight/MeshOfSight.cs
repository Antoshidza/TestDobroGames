using Source.Common;
using UnityEngine;

namespace Source.Sight
{
    public class MeshOfSight : MonoBehaviour
    {
        [SerializeField] protected MeshFilter MeshFilter;
        [SerializeField] private LayerMask _layerMask;

        protected Vector3[] OriginalVertices;

        public virtual void Initialize() => OriginalVertices = MeshFilter.mesh.vertices;

        public void UpdateMesh(Transform origin) 
            => MeshFilter.mesh.ModifyByRayObstacle(origin, OriginalVertices, _layerMask);
    }
}