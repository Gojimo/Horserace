using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cam = new List<CinemachineVirtualCamera>();

    public GameObject inHorse;

    int beforeCam=0;

    void Start()
    {
        GameManager.instance.cameraMove = this;
    }

    public void ChangeCam(int num)
    {
        switch (num)
        {
            case 0:
                cam[beforeCam].gameObject.SetActive(false);
                cam[1].transform.position = inHorse.transform.GetChild(2).transform.position;
                cam[1].transform.rotation = inHorse.transform.GetChild(2).transform.rotation;
                cam[1].Follow = inHorse.transform.GetChild(2).transform;
                cam[1].LookAt = inHorse.transform.GetChild(2).transform;
                cam[1].gameObject.SetActive(true);
                beforeCam = 1;
                break;

            case 1:
                cam[beforeCam].gameObject.SetActive(false);
                cam[2].LookAt = inHorse.transform;
                cam[2].gameObject.SetActive(true);
                beforeCam = 2;
                break;

            case 2:
                cam[beforeCam].gameObject.SetActive(false);
                cam[3].LookAt = inHorse.transform;
                cam[3].gameObject.SetActive(true);
                beforeCam = 3;
                break;

            case 3:
                cam[beforeCam].gameObject.SetActive(false);
                cam[4].LookAt = inHorse.transform;
                cam[4].gameObject.SetActive(true);
                beforeCam = 4;
                break;

            case 4:
                cam[beforeCam].gameObject.SetActive(false);
                cam[5].LookAt = inHorse.transform;
                cam[5].gameObject.SetActive(true);
                beforeCam = 5;
                break;

            case 5:
                cam[beforeCam].gameObject.SetActive(false);
                cam[6].gameObject.SetActive(true);
                beforeCam = 6;
                break;

            case 6:
                cam[beforeCam].gameObject.SetActive(false);
                cam[7].gameObject.SetActive(true);
                beforeCam = 7;
                break;
        }
    }
}
