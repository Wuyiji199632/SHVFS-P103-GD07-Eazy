using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Class3
{
    public class SingleTon : MonoBehaviour
    {

    }

    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        // our private field of type T;
        private static T instance = null;
        //our public property of type T;
        //This is what our other components will access;
        //This is a pattern you see a lot in c:private fields with public properties 
        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
               // DontDestroyOnLoad(instance.gameObject);
                return instance;
            }
        }
        public virtual void Awake()
        {
            if (instance != null) Destroy(gameObject);
        }

    }
}


