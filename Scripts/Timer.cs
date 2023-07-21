using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public float timer = 0f;
    float second;
    public Text text;

    void Start()
    {
        GameManager.instance.timerObj = this.gameObject;
    }

    void FixedUpdate()
    {
        if(GameManager.instance.startGame == true)
        {
            timer += Time.fixedDeltaTime;
            second = timer % 60;
            text.text = string.Format("{0:F2}", second);
            
        }
    }
}
