using UnityEngine;

namespace Source.Common
{
    public abstract class AMonoSingleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        protected virtual void Awake() 
            => DI.RegisterSingleton(typeof(T), this);

        protected virtual void OnDestroy() 
            => DI.RemoveSingleton(typeof(T));
    }
}