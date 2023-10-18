using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoveScript : MonoBehaviour
{
    Vector3 startPos;
    public Vector3 newPos;
    public bool movement;
    float counter;
    int way = 1;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (movement)
        {
            transform.position = Vector3.Lerp(startPos, startPos + newPos ,counter);
            counter += way * Time.deltaTime;
            if (counter >= 1 && way == 1)
            {
                way = -1;
            }
            if (counter <= 0 && way == -1)
            {
                way = 1;
            }
        }
    }
}
