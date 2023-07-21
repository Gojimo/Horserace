using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillZone : MonoBehaviour
{
    public float minPos;
    public float maxPos;
    float ran;
    float skillRan;

    public GameObject effetAlways;
    public GameObject effetEnter;

    Vector3 currentPos;
    HorseAnimation horseAnim;

    void Awake()
    {
        horseAnim = transform.parent.GetComponent<HorseAnimation>();
    }

    void Start()
    {
        effetAlways.SetActive(true);
        ran = Random.Range(minPos, maxPos);
        transform.localPosition = new Vector3(0, 1, ran);
        currentPos = transform.position;
        skillRan = Random.Range(0f, 100f);
    }

    void Update()
    {
        transform.position = currentPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if(skillRan <= horseAnim.skill_Persent)
        {
            effetEnter.SetActive(true);
        }
        horseAnim.Skill(skillRan);            
    }
}
