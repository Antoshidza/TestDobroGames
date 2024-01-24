using System.Threading.Tasks;
using UnityEngine;

namespace Source.Common
{
    public static class Coroutines
    {
        public static async Task SendToTarget(this Transform agent, Transform target, float moveSpeed, float distanceThreshold = .01f)
        {
            while (Vector3.Distance(agent.position, target.position) > distanceThreshold)
            {
                if(agent == null || target == null)
                    return;
                
                agent.position = agent.position.GetTranslateToTargetPosition(target.position, Time.deltaTime, moveSpeed, distanceThreshold);
                await Task.Yield();
            }
        }
    }
}