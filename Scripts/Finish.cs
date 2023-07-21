using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public List<GameObject> rank = new List<GameObject>();
    public List<GameObject> horse = new List<GameObject>();

    Image img;
    Text text_Name;
    Text text_Time;

    void Start()
    {
        for(int index = 0; index < GameManager.instance.horse_Count; index++)
        {

        }
    }
}
