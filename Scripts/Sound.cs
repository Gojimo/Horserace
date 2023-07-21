using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource sound;
    public List<AudioClip> bgm = new List<AudioClip>();

    int ranInt;
    bool first=true;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    void Start()
    {
        ranInt = Random.Range(0, bgm.Count);
        sound.clip = bgm[ranInt];
        Invoke("PlaySound", 1f);
    }
    void Update()
    {
        if (first == false)
        {
            if (sound.isPlaying == false)
            {
                ranInt = Random.Range(0, bgm.Count);
                sound.clip = bgm[ranInt];
                PlaySound();
            }
        }
    }
    void PlaySound()
    {
        first = false;
        sound.Play();
    }
}
