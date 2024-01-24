using System;
using UnityEngine;

namespace Source.Gameplay.Triggers
{
    public class RevealTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask _checkObstacleMask;
        [SerializeField] private SphereCollider _sightCollider;
        
        [HideInInspector] public float NearDistance = 1f;
        [HideInInspector] public int FOVAngle = 85;

        public float FarViewDistance { set => _sightCollider.radius = value; }

        public event Action<GameObject> Triggered;

        private void OnTriggerStay(Collider other)
        {
            var triggerPos = transform.position;
            var revealedPos = other.transform.position;
            if (Physics.Linecast(triggerPos, revealedPos, out var hit, _checkObstacleMask))
                return;

            var sightAngle = Vector3.Angle(transform.forward, revealedPos - transform.position);
            
            if(Vector3.Distance(triggerPos, revealedPos) > NearDistance && sightAngle > FOVAngle / 2f)
                return;
            
            Triggered?.Invoke(other.gameObject);
        }

        private void OnDrawGizmosSelected() 
            => Gizmos.DrawWireSphere(transform.position, NearDistance);
    }
}