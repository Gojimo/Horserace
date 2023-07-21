using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject line;
    public GameObject rank;
    public GameObject mainCamera;

    public GameObject handle;

    CameraMove camMove;

    public bool endLine;
    public bool camLine;
    public bool rankOffLine;

    public bool ranHorse;
    int ran;

    public bool firstIn = false;


    public int num;

    void Awake()
    {
        camMove = mainCamera.GetComponent<CameraMove>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(endLine == true)
        {
            if (other.gameObject.tag == "Horse")
            {
                HorseAnimation horseAnim = other.GetComponent<HorseAnimation>();
                GameManager.instance.finishNum.Add(horseAnim.horse_Num);
                horseAnim.inEndLine = true;
                horseAnim.enterTime = GameManager.instance.timerObj.GetComponent<Timer>().timer;
                GameManager.instance.enterTime.Add(GameManager.instance.timerObj.GetComponent<Timer>().timer);
                GameManager.instance.currentHorseNum++;
                if (GameManager.instance.currentHorseNum >= GameManager.instance.horse_Count)
                {
                    GameManager.instance.EndGame();
                }
            }
        }

        if(rankOffLine == true)
        {
            if(other.gameObject.tag == "Horse")
            {
                GameManager.instance.rank_obj.SetActive(false);
                handle.SetActive(false);
            }
        }

        else if(camLine == true)
        {
            if (other.gameObject.tag == "Horse")
            {
                if(firstIn == false) 
                {
                    if(ranHorse == true)
                    {
                        ran = Random.Range(0, GameManager.instance.horse_Count);
                        camMove.inHorse = GameManager.instance.rank.horse_clone[ran];
                    }
                    else
                    {
                        camMove.inHorse = other.gameObject;
                    }
                    
                    camMove.ChangeCam(num);
                    

                    firstIn = true;
                }
                
            }
        }
    }
}
