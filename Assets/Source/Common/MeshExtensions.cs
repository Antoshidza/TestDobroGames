using UnityEngine;

namespace Source.Common
{
    public static class MeshExtensions
    {
        public static Mesh ConstructConeOfViewMesh(in float fovAngle, in float viewDistance, in int accuracy, out Vector3[] vertices)
        {
            var mesh = new Mesh();
            mesh.name = $"ConeOfView_fov:{fovAngle}_dist:{viewDistance}_freq:{accuracy}";
            vertices = new Vector3[accuracy + 1];
            var triangles = new int[ToTriangleIndex(accuracy)];

            // set triangles
            for (var vi = 1; vi < vertices.Length - 1; vi++)
            {
                var ti = ToTriangleIndex(vi);
                triangles[ti] = 0;
                triangles[ti + 1] = vi;
                triangles[ti + 2] = vi + 1;
            }
            
            // set vertices
            vertices[0] = Vector3.zero;
            var angle = -fovAngle / 2f;
            var angleBetweenTwoRays = fovAngle / (accuracy - 1);

            for (var vi = 1; vi < vertices.Length; vi++)
            {
                vertices[vi] = ToVector(angle) * viewDistance;
                angle += angleBetweenTwoRays;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();

            return mesh;

            static int ToTriangleIndex(in int vertexIndex) => (vertexIndex - 1) * 3;

            static Vector3 ToVector(in float angleInDeg)
            {
                var angleInRad = angleInDeg * Mathf.Deg2Rad;
                return new Vector3(Mathf.Sin(angleInRad), 0f, Mathf.Cos(angleInRad));
            }
        }
        
        public static void ModifyByRayObstacle(this Mesh mesh, Transform origin, Vector3[] originalVertices, in LayerMask layerMask)
        {
            var vertices = mesh.vertices;

            for (var vi = 0; vi < originalVertices.Length; vi++)
            {
                var vertex = originalVertices[vi];
                var lineEnd = origin.TransformPoint(vertex);
                var lineStart = origin.position;
                vertices[vi] = Physics.Linecast(lineStart, lineEnd, out var hitInfo, layerMask) 
                    ? origin.InverseTransformPoint(hitInfo.point)
                    : vertex;
            }

            mesh.vertices = vertices;
        }
    }
}