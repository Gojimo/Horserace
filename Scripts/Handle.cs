using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handle : MonoBehaviour
{
    public int num;
    public float dist=0f;

    HorseAnimation horseAnim;

    RectTransform rect;
    GameObject dummy;
    public GameObject goalText;
    public GameObject startText;
    public GameObject back;

    public bool first = false;
    Image backImage;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        dummy = transform.GetChild(0).gameObject;
        back = GameObject.Find("Image_Handleback");
        startText = back.transform.GetChild(1).gameObject;
        goalText = back.transform.GetChild(0).gameObject;
    }


    void Update()
    {
        if (GameManager.instance.startGame == true)
        {
            if (GameManager.instance.selectedNum.Contains(num) == true)
            {
                if (first == false)
                {
                    backImage = back.GetComponent<Image>();
                    backImage.color = new Color(1f, 1f, 1f, 1f);
                    startText.SetActive(true);
                    goalText.SetActive(true);
                    horseAnim = GameManager.instance.rank.horse_clone[GameManager.instance.selectedNum.IndexOf(num)].GetComponent<HorseAnimation>();
                    first = true;
                    dummy.SetActive(true);
                }

                dist = horseAnim.distance;
                rect.anchoredPosition = new Vector3(dist, 40f, 0);

                Mathf.Clamp(dist, 0f, 1200f);
            }
            else
            {
                this.gameObject.SetActive(false);
            }

        }
    }
}
