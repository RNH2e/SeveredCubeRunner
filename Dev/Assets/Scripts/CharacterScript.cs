using UnityEngine;
using System.Collections.Generic;

public class CharacterScript : MonoBehaviour
{
    public static CharacterScript Instance;
    public Dictionary<string, List<GameObject>> colorObjectDictionary = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, List<GameObject>> pieceObjDic = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, List<GameObject>> addCubeDic = new Dictionary<string, List<GameObject>>();
    public bool onTriggerActive;
    public List<GameObject> pieceList, pieceList1, pieceList2, pieceList3, pieceList4;

    bool pick, pick1, pick2, pick3, pick4;
    string[] colorNames = new string[] { "Purple", "Orange", "Blue", "Red", "Green" };
    public List<GameObject>[] pieceLists; 
    public bool[] picks; 

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pieceLists = new List<GameObject>[] { pieceList, pieceList1, pieceList2, pieceList3, pieceList4 };
        picks = new bool[pieceLists.Length];

        colorObjectDictionary.Add("Purple", GameManager.Instance.purpleList);
        colorObjectDictionary.Add("Orange", GameManager.Instance.orangeList);
        colorObjectDictionary.Add("Green", GameManager.Instance.greenList);
        colorObjectDictionary.Add("Red", GameManager.Instance.redList);
        colorObjectDictionary.Add("Blue", GameManager.Instance.blueList);

        addCubeDic.Add("PurpleAdd", GameManager.Instance.purpleList);
        addCubeDic.Add("OrangeAdd", GameManager.Instance.orangeList);
        addCubeDic.Add("GreenAdd", GameManager.Instance.greenList);
        addCubeDic.Add("RedAdd", GameManager.Instance.redList);
        addCubeDic.Add("BlueAdd", GameManager.Instance.blueList);


    }
    private void Update()
    {
        for (int i = 0; i < pieceLists.Length; i++)
        {
            if (picks[i])
            {
                Material material = GetMaterialByIndex(i);
                AddCube(pieceLists[i], material, ref picks[i]);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        if (GameManager.Instance.orangeList != null || GameManager.Instance.greenList != null || GameManager.Instance.redList != null || GameManager.Instance.purpleList != null || GameManager.Instance.blueList != null)
        {
            onTriggerActive = true;
            string colorTag = other.gameObject.tag;

            if (colorObjectDictionary.TryGetValue(colorTag, out List<GameObject> colorList))
            {
                PourPieces(colorList);
            }

            for (int i = 0; i < pieceLists.Length; i++)
            {

                if (other.CompareTag(colorNames[i]))
                {
                    PourPieces(pieceLists[i]);
                }
            }


            if (addCubeDic.TryGetValue(colorTag, out List<GameObject> addList))
            {

                GetComponent<Animator>().enabled = true;

                GetComponent<Animator>().SetTrigger("blob");
                if (GameManager.Instance.orangeList == null || GameManager.Instance.greenList == null || GameManager.Instance.redList == null || GameManager.Instance.purpleList == null || GameManager.Instance.blueList == null)
                {
                    return;
                }

                foreach (var vec in addList)
                {
                    vec.transform.parent = gameObject.transform;
                    if (vec.GetComponent<Rigidbody>())
                    {
                        Destroy(vec.GetComponent<Rigidbody>());
                    }
                    GameManager.Instance.ResetToStartPositions();
                }

                foreach (var item in GameManager.Instance.allList)
                {
                    item.isPoured = false;
                }
            }

            if (other.GetComponent<PieceScript>())
            {
                var ps = other.GetComponent<PieceScript>();
                Destroy(ps.gameObject);

                if (ps.ID >= 0 && ps.ID < picks.Length)
                {
                    picks[ps.ID] = true;
                }
            }
        }
    }
    void PourPieces(List<GameObject> pieceList)
    {
        foreach (GameObject obj in pieceList)
        {
            GetComponent<Animator>().enabled = false;
            if (obj != null)
            {
                if (!obj.GetComponent<Rigidbody>())
                {
                    obj.AddComponent<Rigidbody>();
                }
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up * .005f, ForceMode.Impulse);
                obj.GetComponentInChildren<ParticleSystem>().Play();
                obj.transform.parent = null;
            }
        }
    }

    Material GetMaterialByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return GameManager.Instance.purple;
            case 1:
                return GameManager.Instance.orange;
            case 2:
                return GameManager.Instance.blue;
            case 3:
                return GameManager.Instance.red;
            case 4:
                return GameManager.Instance.green;
            default:
                return null;
        }
    }
    void AddCube(List<GameObject> pieceList, Material material, ref bool pickFlag)
    {
        for (int i = 0; i < pieceList.Count; i++)
        {
            pieceList[i].gameObject.SetActive(true);
            pieceList[i].gameObject.GetComponent<MeshRenderer>().sharedMaterial = material;
            if (i + 1 == pieceList.Count)
            {
                pickFlag = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerActive = false;
    }

}
