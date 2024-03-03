using Sirenix.OdinInspector;
using UnityEngine;

public class LocalSerializedSingleton<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
{

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    return _instance;
                }

                if (_instance == null)
                {
                    Debug.LogError("NO SINGLETON FOUND");
                    return null;
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        T[] admods = GameObject.FindObjectsOfType<T>();
        if (admods.Length > 1)
        {
            Destroy(admods[1].gameObject);
        }
    }
}

