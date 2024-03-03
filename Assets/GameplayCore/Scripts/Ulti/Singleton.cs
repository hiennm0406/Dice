    using UnityEngine;

public class Singleton<ClassName> : MonoBehaviour where ClassName : MonoBehaviour
{
    static ClassName _Instance;
    public static ClassName Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<ClassName>();
            }
            if (_Instance == null)
            {
                GameObject g = new GameObject("singleton");
                g.AddComponent<ClassName>();
                _Instance = g.GetComponent<ClassName>();
            }
            else
            {
                ClassName[] tmts = FindObjectsOfType<ClassName>();
                if (tmts.Length == 1)
                    _Instance = tmts[0];
                if (tmts.Length > 1)
                {
                    _Instance = tmts[0];
                    for (int i = 1; i <= tmts.Length - 1; i++)
                    {
                        Destroy(tmts[i].gameObject);
                    }
                }
            }
            return _Instance;
        }
    }
}
