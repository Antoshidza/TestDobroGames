using UnityEngine;

public class TransformFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _followerTransform;
    [SerializeField] private Transform _targetTransform;

    private Vector3 _initialOffset;
    
    private void Start() 
        => _initialOffset = _followerTransform.position - _targetTransform.position;

    private void Update() 
        => _followerTransform.position = _targetTransform.position + _initialOffset;
}
