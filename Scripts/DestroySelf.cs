using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    float timer;
    public float destroyTime;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= destroyTime)
        {
            timer = 0f;
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
