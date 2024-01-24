using System.Threading.Tasks;
using Source.Common;
using Source.Sight;
using UnityEngine;

namespace Source.Bandit
{
    public class BanditView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField][Min(0f)] private float _shootSpeed = 1f;
        [SerializeField] private ConeOfSight _coneOfSight;
        [SerializeField] private Transform _nearSightTransform;
        
        public float NearViewDistance { set => _nearSightTransform.localScale = Vector3.one * value; }

        public void Initialize(in float fovAngle, in float farViewDistance) => _coneOfSight.Initialize(fovAngle, farViewDistance);

        private void Update() 
            => _coneOfSight.UpdateMesh(transform);

        public async Task ShootTo(Vector3 fromPos, Transform target)
        {
            var bullet = Instantiate(_bulletPrefab, fromPos, Quaternion.identity);
            bullet.transform.LookAt(target);
            await bullet.transform.SendToTarget(target, _shootSpeed);
            Destroy(bullet);
        }
    }
}