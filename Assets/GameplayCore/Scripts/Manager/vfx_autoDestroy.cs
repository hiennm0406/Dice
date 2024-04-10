using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfx_autoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject, 1f);
    }
}
