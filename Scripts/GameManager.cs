using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
   
    public static GameManager instance;

    [Header("Object")]
    public Rank rank;
    public GameObject rank_obj;
    public GameObject beforeCam;
    public CameraMove cameraMove;
    public GameObject persentUI;
    public GameObject timerObj;
    public GameObject gui;

    [Header("Int")]
    public int horse_Count = 0;
    public int currentHorseNum = 0;
    public float timer = 0f;

    [Header("List")]
    public List<GameObject> horse = new List<GameObject>();
    public List<int> selectedNum = new List<int>();
    public List<int> finishNum = new List<int>();
    public List<float> enterTime = new List<float>();

    [Header("Bool")]
    public bool settingStart = false;
    public bool startGame = false;
    public bool endGame = false;


    bool firstIn = false;
    bool firstSetting = false;

    GameObject countDown;
    GameObject Title;

    

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        if (settingStart == true) 
        {
            CountDown();
        }
        if(startGame == true)
        {
            timer += Time.fixedDeltaTime;
        }
    }

    public void Init()
    {
        Destroy(this.gameObject);
    }

    void TextChange(GameObject obj, string text) 
    {
        Text textString = obj.GetComponent<Text>();
        textString.text = text;
    }

    public void EndGame()
    {
        for (int index = 0; index < horse_Count; index++)
        {
            HorseAnimation horseAnim = rank.horse_clone[index].GetComponent<HorseAnimation>();
            if (horseAnim.horse_Num == 5 && horseAnim.isSkill == true && firstIn == false)
            {
                finishNum.Add(index);
                firstIn = true;
            }
        }
        startGame = false;
        StartCoroutine(NextScene(2f));
    }

    IEnumerator NextScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        gui.GetComponent<GUI>().LoadScene(1);
    }

    void CountDown()
    {
        countDown = GameObject.Find("Text_CountDown");
        Title = GameObject.Find("Text_Title");
        timer += Time.fixedDeltaTime;

        if (timer > 1f && firstSetting == false)
        {
            TextChange(Title, "도시 - 맑음");
        }
        if (timer > 8f && firstSetting == false)
        {
            timer = 0f;
            Title.SetActive(false);
            firstSetting = true;
        }
        if (timer > 1f && firstSetting == true) { TextChange(countDown, "3"); }
        if (timer > 2f && firstSetting == true) { TextChange(countDown, "2"); }
        if (timer > 3f && firstSetting == true) { TextChange(countDown, "1"); }
        if (timer > 4f && firstSetting == true) 
        { 
            TextChange(countDown, "출발!");
            startGame = true;
            rank_obj.SetActive(true);
        }
        if (timer > 5f && firstSetting == true) { countDown.SetActive(false); settingStart = false; }
        if (timer > 5.01f && firstSetting == true) { timer = 0.01f;}
    }

}
