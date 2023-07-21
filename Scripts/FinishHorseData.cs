using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishHorseData : MonoBehaviour
{
    public int num;
    public Sprite image;
    public string name;
    public float time;

    public GameObject finishObj;
    public GameObject dummy;
    Finish fin;

    void Awake()
    {
        fin = finishObj.GetComponent<Finish>();
        
    }
    void Start()
    {
        //time = GameManager.instance.; 
        fin.horse.Insert(num, this.gameObject);
    }
}
