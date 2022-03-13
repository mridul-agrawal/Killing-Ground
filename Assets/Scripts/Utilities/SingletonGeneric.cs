using UnityEngine;

/// <summary>
/// This is a Generic Singleton Class which can be inherited to make that child class a Singleton.
/// </summary>

namespace KillingGround.Utilities
{
    public class SingletonGeneric<T> : MonoBehaviour where T : SingletonGeneric<T>
    {
        private static T instance;
        internal static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}