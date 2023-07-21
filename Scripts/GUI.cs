using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GUI : MonoBehaviour
{
    public Text obj_text;
    public Image fade;
    public int count = 2;
    int horse_Count;
    public int horse_Num;
    public bool checkFirst;
    float timer = 0f;

    public GameObject btnSelectList;
    public GameObject selectedList;
    public GameObject alramList;

    public float fadeTime;

    public List<int> randomList = new List<int>();
    public List<GameObject> alram = new List<GameObject>();

    void Awake()
    {      
        StartCoroutine(FadeIn());
        Invoke("IsFadeActive", fadeTime);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 || SceneManager.GetActiveScene().buildIndex != 2)
        {
            GameManager.instance.gui = this.gameObject;
            for (int index = 0; index < 10; index++)
            {
                alram.Add(alramList.transform.GetChild(index).gameObject);
            }
        }
    }

    public void Btn_HorseCount_Up()
    {
        obj_text = obj_text.GetComponent<Text>();
        count++;
        count = Mathf.Clamp(count, 2, 10);
        obj_text.text = count.ToString();
    }

    public void Btn_HorseCount_Down()
    {
        obj_text = obj_text.GetComponent<Text>();
        count--;
        count = Mathf.Clamp(count, 2, 10);
        obj_text.text = count.ToString();
    }

    public void Btn_Random_Select()
    {
        if(GameManager.instance.selectedNum.Count != 0) { return; }

        int ran;

        for (int index = 1; index <= count; index++)
        {
            do
            {
                ran = Random.Range(0, 11);
            }
            while (randomList.Contains(ran));

            randomList.Add(ran);
            SelectHorse(ran);
        }
    }

    public void resestBtn()
    {
        randomList.Clear();

        for (int index = 0; index <= 10; index++)
        {
            GameObject btn = btnSelectList.transform.GetChild(index).transform.GetChild(0).gameObject;
            Image btnImage = btn.GetComponent<Image>();
            GameObject select = selectedList.transform.GetChild(index).gameObject;
            GameObject selectedHorse;

            if (GameManager.instance.selectedNum.Contains(index) == true)
            {
                selectedHorse = GameManager.instance.horse[index];

                selectColor(1f, btnImage);

                select.SetActive(false);

                horse_Count = GameManager.instance.horse_Count;
                horse_Count--;
                horse_Count = Mathf.Clamp(horse_Count, 0, 10);
                GameManager.instance.horse_Count = horse_Count;

                GameManager.instance.selectedNum.Remove(index);
            }
        }
    }

    public void SelectHorse(int num)
    {
        GameObject btn = btnSelectList.transform.GetChild(num).transform.GetChild(0).gameObject;
        Image btnImage = btn.GetComponent<Image>();
        GameObject select = selectedList.transform.GetChild(num).gameObject;
        GameObject selectedHorse;

        if (GameManager.instance.selectedNum.Contains(num) == false)
        {
            if(GameManager.instance.horse_Count >= 10) { return; }
            
            selectedHorse = GameManager.instance.horse[num];

            selectColor(0f, btnImage);

            select.SetActive(true);

            horse_Count = GameManager.instance.horse_Count;
            horse_Count++;
            horse_Count = Mathf.Clamp(horse_Count, 0, 10);
            GameManager.instance.horse_Count = horse_Count;

            GameManager.instance.selectedNum.Add(num);
        }
        else if(GameManager.instance.selectedNum.Contains(num) == true)
        {
            selectedHorse = GameManager.instance.horse[num];

            selectColor(1f, btnImage);

            select.SetActive(false);

            horse_Count = GameManager.instance.horse_Count;
            horse_Count--;
            horse_Count = Mathf.Clamp(horse_Count, 0, 10);
            GameManager.instance.horse_Count = horse_Count;

            GameManager.instance.selectedNum.Remove(num);
        }
    }

    public void LoadScene(int num)
    {
        Color alpha = fade.color;

        switch (num)
        {
            case 0:
                StartCoroutine(FadeOut());
                StartCoroutine(GoMain(1.2f));
                break;
            case 1:
                if (GameManager.instance.horse_Count <= 1) { return; }
                Time.timeScale = 1f;
                StartCoroutine(FadeOut());
                StartCoroutine(NextSceneLoad(1.2f));
                break;
        }
    }

    IEnumerator FadeIn()
    {
        Color alpha = fade.color;

        while(alpha.a < 1f)
        {
            timer += Time.deltaTime;
            if (timer <= 1f)
            {
                alpha.a = Mathf.Lerp(1, 0, timer);
                fade.color = alpha;
            }
            else if (timer > 1f)
            {
                timer = 0f;
                break;
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator FadeOut()
    {
        Color alpha = fade.color;
        fade.raycastTarget = true;

        while (alpha.a < 1f)
        {
            timer += Time.deltaTime;
            alpha.a = Mathf.Lerp(0, 1, timer);
            fade.color = alpha;
            yield return null;
        }

        yield return null;
    }

    void IsFadeActive()
    {
        fade.raycastTarget = false;
        //fade.gameObject.SetActive(false);
    }

    IEnumerator NextSceneLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator GoMain(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.Init();
        SceneManager.LoadScene(0);
    }

    void selectColor(float colorF, Image obj)
    {
        Image img = obj.GetComponent<Image>();

        img.color = new Color(colorF, colorF, colorF, 1f);
    }

    public void Alram(int index, string myName, string otherName, int listNum)
    {
            if (alram[listNum-1].activeSelf == false)
            {
                switch (index)
                {
                    case 0:
                        alram[listNum-1].transform.GetChild(0).GetComponent<Text>().text = myName + " 스킬 발동!";
                        alram[listNum-1].SetActive(true);
                        break;
                        
                    case 1:
                        alram[listNum-1].transform.GetChild(0).GetComponent<Text>().text = myName + " 스킬 발동!" + System.Environment.NewLine + "대상 : " + otherName;
                        alram[listNum-1].SetActive(true);
                        break;

                    case 2:
                        alram[listNum-1].transform.GetChild(0).GetComponent<Text>().text = myName + " 스킬 발동!" + System.Environment.NewLine + "속도 : " + otherName;
                        alram[listNum-1].SetActive(true);
                        break;

                    case 3:
                        alram[listNum-1].transform.GetChild(0).GetComponent<Text>().text = myName + " 스킬 발동!" + System.Environment.NewLine + "길이 : " + otherName;
                        alram[listNum-1].SetActive(true);
                        break;

                    case 4:
                        alram[listNum - 1].transform.GetChild(0).GetComponent<Text>().text = myName + " 스킬 면역!" + System.Environment.NewLine + "공격자 : " + otherName;
                        alram[listNum - 1].SetActive(true);
                        break;
            }
            }
        }
    }