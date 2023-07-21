using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public GameObject horseParent;
    public GameObject spawnManager;
    public int horseCount = 0;
    public List<GameObject> spawnPoint = new List<GameObject>();

    public GameObject rank_obj;

    void Start()
    {
        GameManager.instance.endGame = false;
        for (int index = 0; index < GameManager.instance.horse_Count; index++)
        {
            Instantiate(GameManager.instance.horse[GameManager.instance.selectedNum[index]], spawnPoint[index].transform.position, Quaternion.identity, horseParent.transform);
            if(index == GameManager.instance.horse_Count-1) { GameManager.instance.settingStart = true; }
        }
        GameManager.instance.rank_obj = rank_obj;
    }
}
