using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    public virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    protected static T _instance;

    /**
       Returns the instance of this singleton.
    */
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Finding a new instance of " + typeof(T) + " for the scene");
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
                else
                {
                    Debug.Log("Instance found with name " + _instance.name + " of type " + _instance.GetType().Name);
                }
            }
            return _instance;
        }
    }
}