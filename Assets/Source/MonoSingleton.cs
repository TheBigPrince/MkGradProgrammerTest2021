using System;
using System.Linq;
using UnityEngine;

namespace Protodroid.Helper
{
    
    /// <summary>
    /// Inheriting from this will turn your Monobehavior into a Singleton.
    /// You can use all of the Monobehavior Event function Start, Update etc.. as per normal
    /// except for Awake. Instead use the method InitliaseOnAwake in place of Awake.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;
        
        private DateTime creationDateTime;
        
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    T[] instances = FindObjectsOfType<T>();

                    if (instances.Length == 0 || instances == null)
                    {
                        GameObject singletonGO = new GameObject($"{typeof(T).Name} Singleton");
                        instance = singletonGO.AddComponent<T>();
                    }
                    else
                    {
                        instance = instances.OrderBy(i => i.creationDateTime).FirstOrDefault();
                        foreach (T badInstance in instances.Where(i => i != instance))
                            Destroy(badInstance);
                    }
                }

                return instance;
            }
        }


        private void Awake()
        {
            creationDateTime = DateTime.Now;

            if (instance == null) instance = (T) this;
            else
            {
                Destroy(this);
                return;
            }

            InitialiseOnAwake();
        }

        /// <summary>
        /// For use with singletons only - use this in place of Monobehaviors Awake Event.
        /// </summary>
        protected abstract void InitialiseOnAwake();
    }
}