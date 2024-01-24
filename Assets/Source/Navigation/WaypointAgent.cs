using System;
using UnityEngine;
using Math = Source.Common.Math;

namespace Source.Guardians
{
    public class WaypointAgent : MonoBehaviour
    {
        [Serializable]
        private struct Waypoint
        {
            public Vector3 Pos;
            public float StayDuration;
        }

        [SerializeField] private Transform _agentTransform;
        [SerializeField] private Waypoint[] _waypoints;
        [SerializeField][Min(0f)] private float _moveSpeed = 1f;
        [SerializeField] private float _stayTimer;

        private int _waypointIndex;
        private int _waypointDirection = 1;

        private void Start() => _agentTransform.LookAt(_waypoints[0].Pos);

        private void Update()
        {
            var dt = Time.deltaTime;
            var waypoint = _waypoints[_waypointIndex];

            _agentTransform.position = Math.GetTranslateToTargetPosition(_agentTransform.position, waypoint.Pos, dt, _moveSpeed);

            if (_agentTransform.position != waypoint.Pos) 
                return;
            
            if (_stayTimer > 0f)
                _stayTimer -= dt;
            else
            {
                SetNextWaypoint();
                var nextWaypoint = _waypoints[_waypointIndex];
                _agentTransform.LookAt(nextWaypoint.Pos);
                _stayTimer = nextWaypoint.StayDuration;
            }
        }

        private void SetNextWaypoint()
        {
            var nextIndex = _waypointIndex + _waypointDirection;

            if (nextIndex < 0 || nextIndex > _waypoints.Length - 1)
                _waypointDirection *= -1;
            
            _waypointIndex += _waypointDirection;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_waypoints == null || _waypoints.Length == 0)
                return;

            var prevPos = _waypoints[0].Pos;

            foreach (var waypoint in _waypoints)
            {
                Gizmos.DrawSphere(waypoint.Pos, .1f);
                Gizmos.DrawLine(prevPos, waypoint.Pos);
                prevPos = waypoint.Pos;
            }
        }
#endif
    }
}