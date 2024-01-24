using UnityEngine;

namespace Source.Common
{
    public static class Math
    {
        public static Vector3 GetTranslateToTargetPosition(this in Vector3 fromPos, in Vector3 toPos, in float dt, in float speed, in float threshold = .01f)
        {
            var distance = (toPos - fromPos).magnitude;
            return distance < threshold ? toPos : Vector3.Lerp(fromPos, toPos, speed * dt / distance);
        }
    }
}