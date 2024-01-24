using Source.Common;
using UnityEngine;

namespace Source.Sight
{
    public class ConeOfSight : MeshOfSight
    {
        [SerializeField][Min(1)] private int _accuracy = 10;
        [SerializeField] private Transform _origin;
        
        public void Initialize(in float fovAngle, in float viewDistance)
        {
            MeshFilter.mesh = MeshExtensions.ConstructConeOfViewMesh(fovAngle, viewDistance, _accuracy, out OriginalVertices);
            Initialize();
        }
    }
}