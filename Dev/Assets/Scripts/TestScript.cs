using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public List<GameObject> cubeList;
    Vector3[] startPositions;

    void Start()
    {
        int shortEdge = Mathf.CeilToInt(Mathf.Sqrt(cubeList.Count));
        float dst = 1f;
        startPositions = new Vector3[cubeList.Count];

        for (int i = 0; i < cubeList.Count; i++)
        {
            cubeList[i].transform.position = new Vector3((i % shortEdge) * dst, 0, (i / shortEdge) * dst);
            cubeList[i].name = "r" + i;
            startPositions[i] = cubeList[i].transform.position;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            foreach (var item in cubeList)
            {
                item.transform.position += Vector3.forward * Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetToStartPositions();
        }
    }

    void ResetToStartPositions()
    {
        for (int i = 0; i < cubeList.Count; i++)
        {
            cubeList[i].transform.position = startPositions[i];
        }
    }
}
