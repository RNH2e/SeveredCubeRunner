using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public bool pick;
    public int ID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PieceInfo(int ID, bool pick)
    {
        ID = this.ID;
        pick = this.pick;
    }
}
