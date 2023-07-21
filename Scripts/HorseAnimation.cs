using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HorseAnimation : MonoBehaviour
{

    Animator anim;
    AudioSource se;

    float firstZPos;
    float currentZPos;
    

    int ranInt;
    float ranFloat;
    GameObject ranHorse;
    GUI gui;


    [Header("Instance")]
    public int horse_Num;
    public string horse_Name;
    public int speedInt;
    public string skill_Name;
    public string skill_Detail;
    public float skill_Persent;
    public int Weather;
    public Sprite horse_Image;

    [Header("Default Setting")]
    public GameObject animDummy;
    public GameObject soundEffect;

    [Header("Index")]
    public float speed;
    public float buffSpeed = 1f;
    public float weatherSpeed = 1f;
    public float distance;
    public float timer;
    public bool isSkill = false;
    public bool inEndLine = false;
    public float enterTime = 0f;

    void Awake()
    {
        anim = animDummy.GetComponent<Animator>();
        se = soundEffect.GetComponent<AudioSource>();
        gui = GameManager.instance.gui.GetComponent<GUI>();
    }

    void Start()
    {
        firstZPos = transform.position.z;
        ResetSpeed();
        ranInt = Random.Range(0, 1);
    }

    void Update()
    {
        timer += Time.deltaTime;
        Dist();
        if (GameManager.instance.startGame == true)
        {
            anim.speed = 1f;
            Move();
            //se.mute = false;
        }
        if (GameManager.instance.startGame == false)
        {
            anim.speed = 0.3f;
            se.mute = true;
        }
    }

    void Move()
    {
        anim.SetBool("isRun", true);

        transform.Translate(Vector3.forward * speed * buffSpeed * weatherSpeed * Time.deltaTime);
    }

    void Dist()
    {
        currentZPos = transform.position.z;
        distance = currentZPos - firstZPos;
    }

    public void ResetSpeed()
    {
        switch (speedInt)
        {
            case 0:
                speed = Random.Range(40f, 50f);
                break;

            case 1:
                speed = Random.Range(45f, 55f);
                break;

            case 2:
                speed = Random.Range(50f, 60f);
                break;

            case 3:
                speed = Random.Range(55f, 65f);
                break;

            case 4:
                speed = Random.Range(60f, 70f);
                break;

            case 5:
                speed = Random.Range(40f, 70f);
                break;

            case 6:
                speed = 0;
                break;
        }
    }

    public void ResetBuff()
    {
        buffSpeed = 1f;
    }

    public void Skill(float ranSkill)
    {
        
        switch (horse_Num)
        {
            case 0:
                if(ranSkill <= skill_Persent)
                {
                    isSkill = true;
                    ranFloat = Random.Range(1f, 3f);
                    ranHorse = GameManager.instance.rank.horse_clone[Random.Range(0, GameManager.instance.horse_Count)];
                    HorseAnimation horseAnim = ranHorse.GetComponent<HorseAnimation>();
                    if(horseAnim.horse_Num == 7 && horseAnim.isSkill == true) 
                    {
                        gui.Alram(4, horseAnim.horse_Name, horse_Name, horseAnim.horse_Num);
                    }
                    else
                    {
                        horseAnim.speed = 0;
                        horseAnim.Invoke("ResetSpeed", ranFloat);
                        Debug.Log(horse_Name + " 스킬 발동! 대상 : " + horseAnim.horse_Name);
                        gui.Alram(1, horse_Name, horseAnim.horse_Name, horse_Num);
                    }
                }
                break;

            case 1:
                if(ranSkill <= skill_Persent)
                {
                    isSkill = true;
                    ranFloat = Random.Range(3f, 7f);
                    speed = Random.Range(90f, 100f);
                    Invoke("ResetSpeed", ranFloat);
                    Debug.Log(horse_Name + " 스킬 발동!");
                    gui.Alram(0, horse_Name, "", horse_Num);
                }
                break;

            case 2:
                if(ranSkill <= skill_Persent)
                {
                    isSkill = true;
                    Debug.Log(horse_Name + " 스킬 발동!");
                    gui.Alram(0, horse_Name, "", horse_Num);
                    for (int index = 0; index < GameManager.instance.horse_Count; index++)
                    {
                        ranFloat = Random.Range(5f, 10f);
                        HorseAnimation horseAnim = GameManager.instance.rank.horse_clone[index].GetComponent<HorseAnimation>();
                        if (horseAnim.horse_Num != 2)
                        {
                            if (horseAnim.horse_Num == 7 && horseAnim.isSkill == true)
                            {
                                gui.Alram(4, horseAnim.horse_Name, horse_Name, horseAnim.horse_Num);
                            }
                            else
                            {
                                horseAnim.buffSpeed = 0.88f;
                                horseAnim.Invoke("ResetBuff", ranFloat);
                            }
                        }
                        
                    }
                }
                break;

            case 3:
                if (ranSkill <= skill_Persent)
                {
                    isSkill = true;
                    ranHorse = GameManager.instance.rank.horse_clone[Random.Range(0, GameManager.instance.horse_Count-1)];
                    HorseAnimation horseAnim = ranHorse.GetComponent<HorseAnimation>();
                    Transform horsePos = ranHorse.GetComponent<Transform>();
                    float otherPos = horsePos.transform.position.z;
                    float myPos = transform.position.z;

                    if (horseAnim.horse_Num == 7 && horseAnim.isSkill == true)
                    {
                        gui.Alram(4, horseAnim.horse_Name, horse_Name, horseAnim.horse_Num);
                    }
                    else
                    {
                        horsePos.position = new Vector3(horsePos.position.x, horsePos.position.y, myPos);
                        transform.position = new Vector3(transform.position.x, transform.position.y, otherPos);
                        Debug.Log(horse_Name + " 스킬 발동! 대상 : " + horseAnim.horse_Name);
                        gui.Alram(1, horse_Name, horseAnim.horse_Name, horse_Num);
                    }
                }
                break;

            case 4:
                if (ranSkill <= skill_Persent)
                {
                    isSkill = true;
                    Debug.Log(horse_Name + " 스킬 발동!");
                    gui.Alram(0, horse_Name, "", horse_Num);
                    for (int index = 0; index < GameManager.instance.horse_Count; index++)
                    {
                        ranFloat = Random.Range(1f, 2f);
                        HorseAnimation horseAnim = GameManager.instance.rank.horse_clone[index].GetComponent<HorseAnimation>();
                        if (horseAnim.horse_Num != 4)
                        {
                            if (horseAnim.horse_Num == 7 && horseAnim.isSkill == true)
                            {
                                gui.Alram(4, horseAnim.horse_Name, horse_Name, horseAnim.horse_Num);
                            }
                            else
                            {
                                horseAnim.speed = 0f;
                                horseAnim.Invoke("ResetSpeed", ranFloat);
                            }
                        }
                    }
                }
                break;

            case 5:
                if (ranSkill <= skill_Persent)
                {
                    isSkill = true;
                    Debug.Log(horse_Name + " 스킬 발동!");
                    gui.Alram(0, horse_Name, "", horse_Num);
                    speed = 0f;
                    speedInt = 6;
                    GameManager.instance.currentHorseNum++;
                }
                break;

            case 6:

                if (ranSkill <= skill_Persent)
                {
                    isSkill = true;
                    bool first = false;
                    if (ranInt == 0)
                    {
                        for (int index = 0; index < GameManager.instance.horse_Count; index++)
                        {
                            if (index % 2 == 0)
                            {
                                ranInt = Random.Range(0, 1);
                                HorseAnimation horseAnim = GameManager.instance.rank.horse_clone[index].GetComponent<HorseAnimation>();
                                if (horseAnim.horse_Num == 7 && horseAnim.isSkill == true)
                                {
                                    gui.Alram(4, horseAnim.horse_Name, horse_Name, horseAnim.horse_Num);
                                }
                                else
                                {
                                    if (ranInt == 0)
                                    {
                                        horseAnim.buffSpeed = 1.1f;
                                        if (first == false)
                                        {
                                            Debug.Log(horse_Name + " 스킬 발동! 대상 : 짝수 버프");
                                            gui.Alram(1, horse_Name, "짝수 버프", horse_Num);
                                            first = true;
                                        }

                                    }
                                    if (ranInt == 1)
                                    {
                                        horseAnim.buffSpeed = 0.9f;
                                        if (first == false)
                                        {
                                            Debug.Log(horse_Name + " 스킬 발동! 대상 : 짝수 너프");
                                            gui.Alram(1, horse_Name, "짝수 너프", horse_Num);
                                            first = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (ranInt == 1)
                    {
                        for (int index = 0; index < GameManager.instance.horse_Count; index++)
                        {
                            if (index % 2 == 1)
                            {
                                ranInt = Random.Range(0, 1);
                                HorseAnimation horseAnim = GameManager.instance.rank.horse_clone[index].GetComponent<HorseAnimation>();
                                if (horseAnim.horse_Num == 7 && horseAnim.isSkill == true)
                                {
                                    gui.Alram(4, horseAnim.horse_Name, horse_Name, horseAnim.horse_Num);
                                }
                                else
                                {
                                    if (ranInt == 0)
                                    {
                                        horseAnim.buffSpeed = 1.1f;
                                        if (first == false)
                                        {
                                            Debug.Log(horse_Name + " 스킬 발동! 대상 : 홀수 버프");
                                            gui.Alram(1, horse_Name, "홀수 버프", horse_Num);
                                            first = true;
                                        }
                                    }
                                    if (ranInt == 1)
                                    {
                                        horseAnim.buffSpeed = 0.9f;
                                        if (first == false)
                                        {
                                            Debug.Log(horse_Name + " 스킬 발동! 대상 : 홀수 너프");
                                            gui.Alram(1, horse_Name, "홀수 너프", horse_Num);
                                            first = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                break;

            case 7:
                if (ranSkill <= skill_Persent)
                {
                    isSkill = true;
                }
                break;

            case 8:
                if (ranSkill <= skill_Persent)
                {
                    isSkill = true;
                    Debug.Log(horse_Name + " 스킬 발동! 속도 : " + speed);
                    gui.Alram(2, horse_Name, speed.ToString(), horse_Num);
                    speed = Random.Range(40f, 70f);
                }
                break;

            case 9:
                if (ranSkill <= skill_Persent)
                {
                    isSkill = true;
                    if (ranInt == 0) { ranFloat = Random.Range(50f, 200f); }
                    if (ranInt == 1) { ranFloat = Random.Range(-200f, -50f); }
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + ranFloat);
                    Debug.Log(horse_Name + " 스킬 발동! 길이 : " + ranFloat);
                    gui.Alram(2, horse_Name, ranFloat.ToString(), horse_Num);
                }
                break;

            case 10:
                if (ranSkill <= skill_Persent)
                {
                    isSkill = true;
                    ranInt = Random.Range(0, GameManager.instance.horse_Count-1);
                    HorseAnimation horseObj = GameManager.instance.rank.horse_clone[ranInt].GetComponent<HorseAnimation>();
                    if (horseObj.horse_Num == 7 && horseObj.isSkill == true)
                    {
                        gui.Alram(4, horseObj.horse_Name, horse_Name, horseObj.horse_Num);
                    }
                    else
                    {
                        horseObj.skill_Persent = 0;
                        Debug.Log(horse_Name + " 스킬 발동! 대상 : " + horseObj.horse_Name);
                        gui.Alram(1, horse_Name, horseObj.horse_Name, horse_Num);
                    }  
                }
                break;

        }
    }
}
