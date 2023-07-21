using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Rank : MonoBehaviour
{
    public List<Image> rank = new List<Image>();
    public List<float> rank_float = new List<float>();
    public List<string> rank_string = new List<string>();
    public List<Sprite> rank_Image = new List<Sprite>();
    public List<GameObject> horse_clone = new List<GameObject>();

    public float timer = 2f;
    public bool first = false;
    public GameObject spawn;

    public int index=0;
    public float maxRank;
    public int maxIndex;

    void Awake()
    {
        GameManager.instance.rank = this;
    }

    void Start()
    {
        while(index < GameManager.instance.horse_Count)
        {
            horse_clone.Add(spawn.transform.GetChild(index).gameObject);
            rank[index].gameObject.SetActive(true);
            index++;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1f)
        {
            rank_float.Clear();
            rank_string.Clear();
            for (int i = 0; i < GameManager.instance.horse_Count; i++)
            {
                RankUpdate(i);
            }
            RankChange();
            timer = 0f;
        }
        
    }

    void RankUpdate(int num)
    {
        HorseAnimation horseAnim = horse_clone[num].GetComponent<HorseAnimation>();
        string name = horseAnim.horse_Name;
        float dist = horseAnim.distance;
        Sprite img = horseAnim.horse_Image;

        rank_float.Add(dist);
        rank_string.Add(name);
        rank_Image.Add(img);
    }

    void RankChange()
    {
        for(int i = 0; i < GameManager.instance.horse_Count; i++)
        {
            maxRank = rank_float.Max();
            maxIndex = rank_float.IndexOf(maxRank);
            rank[i].sprite = rank_Image[maxIndex];
            //rank[i].text = (i + 1) + "À§ : " + rank_string[maxIndex];
            rank_float[maxIndex] = 0;
        }
        
    }
}
