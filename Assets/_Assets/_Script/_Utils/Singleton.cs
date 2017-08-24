using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T s_instance;

    public static T s_Instance
    {
        get
        {
            return s_instance;
        }
        protected set
        {
            s_instance = value;
        }
    }

    public static bool s_InstanceExists { get { return s_instance != null; } }

    protected virtual void Awake()
    {
        if (s_instance != null){
            Destroy(gameObject);
        }else{
            s_instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (s_instance == this)
        {
            s_instance = null;
        }
    }
	
}
