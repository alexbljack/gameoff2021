using UnityEngine;

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

                    if (_instance == null)
                    {
                        var singleton = new GameObject(typeof(T).Name);
                        _instance = singleton.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}