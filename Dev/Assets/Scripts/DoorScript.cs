using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoorScript : MonoBehaviour
{
    public float speed;
    public Text text, text2;
    public string op;
    public MeshRenderer mr;
    public Material red, green;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        text.text = "Speed" + "\n" + op + speed;
        text2.text = "Speed" + "\n" + op + speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            mr.material = green;
        }
        else
        {
            mr.material = red;
        }
    }
}
