using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public bool poured;
    private void Update()
    {
        if (!GetComponent<Rigidbody>())
        {
            poured = false;
        }
        else
        {
            poured = true;
        }
    }
}
