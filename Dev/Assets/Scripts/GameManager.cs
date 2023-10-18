using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Material> availableColors;
    public Material purple, orange, blue, red, green;
    public List<GameObject> cubeList;
    public List<GameObject> purpleList, orangeList, greenList, blueList, redList;
    MeshRenderer meshRenderer;
    public List<List<GameObject>> colorLists = new List<List<GameObject>>();
    private List<Vector3[]> startPositions = new List<Vector3[]>();
    public CubeInfo cI;
    public int listCount;
    public bool addBlock;

    [System.Serializable]
    public class CubeInfo
    {
        public bool isPoured;
    }

    public List<CubeInfo> allList;

    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {
        listCount = purpleList.Count;
        for (int i = 0; i < cubeList.Count; i++)
        {

            meshRenderer = cubeList[i].GetComponent<MeshRenderer>();
            int randomIndex = Random.Range(0, availableColors.Count);
            meshRenderer.sharedMaterial = availableColors[randomIndex];

            if (meshRenderer.sharedMaterial == orange)
            {
                orangeList.Add(cubeList[i]);
            }
            else if (meshRenderer.sharedMaterial == green)
            {
                greenList.Add(cubeList[i]);
            }
            else if (meshRenderer.sharedMaterial == red)
            {
                redList.Add(cubeList[i]);
            }
            else if (meshRenderer.sharedMaterial == blue)
            {
                blueList.Add(cubeList[i]);
            }
            else if (meshRenderer.sharedMaterial == purple)
            {
                purpleList.Add(cubeList[i]);
            }

        }

        colorLists.Add(orangeList);
        colorLists.Add(blueList);
        colorLists.Add(redList);
        colorLists.Add(greenList);
        colorLists.Add(purpleList);
        colorLists.Add(CharacterScript.Instance.pieceList);
        colorLists.Add(CharacterScript.Instance.pieceList1);
        colorLists.Add(CharacterScript.Instance.pieceList2);
        colorLists.Add(CharacterScript.Instance.pieceList3);
        colorLists.Add(CharacterScript.Instance.pieceList4);

        addBlock = true;
        for (int i = 0; i < colorLists.Count; i++)
        {
            startPositions.Add(new Vector3[colorLists[i].Count]);

            for (int j = 0; j < colorLists[i].Count; j++)
            {
                startPositions[i][j] = colorLists[i][j].transform.localPosition;
            }
        }

        for (int i = 0; i < cubeList.Count; i++)
        {
            CubeInfo c = new CubeInfo();
            allList.Add(c);
        }

    }


    public void ResetToStartPositions()
    {

        for (int i = 0; i < colorLists.Count; i++)
        {
            List<GameObject> currentList = colorLists[i];
            Vector3[] currentStartPositions = startPositions[i];

            for (int j = 0; j < currentList.Count; j++)
            {
                currentList[j].transform.localPosition = currentStartPositions[j];
            }
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Break();
        }

    }
}
