using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T mInstance;
    public static T instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = Resources.Load<T>("Data/" + typeof(T).ToString());
            }

            return mInstance;
        }
    }
}
