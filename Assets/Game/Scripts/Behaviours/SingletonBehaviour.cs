// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (!_instance) // means _instance == null
                {
                    _instance = FindObjectOfType(typeof(T)) as T; //GameObject.FindObjectOfType<T>(); can be used too.
                    if (!_instance)
                    {
                        // Debug.LogError("An instance of "+ typeof(T) +
                        //                " is needed in the scene, but there is none.");

                        _instance = new GameObject("Instance of " + typeof(T)).AddComponent<T>();
                    }
                }
                
                return _instance;
            }
            
            set
            {
                _instance=value;
            }
        }
        
        private void Awake()
        {
            if(_instance != null)
            {
                Destroy(this.gameObject); // prevent duplicates
            }
        }
        
    }
}