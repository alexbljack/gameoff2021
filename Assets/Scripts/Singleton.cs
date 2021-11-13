using UnityEngine;
using UnityEngine.Assertions;

namespace Lib
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }

        public void Awake()
        {
            _instance = this as T;
            Assert.IsNotNull(_instance, $"{typeof(T)} can be instantiated only once.");
            DontDestroyOnLoad(gameObject);
        }
    }
}