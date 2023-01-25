using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    public GameObject refe;
    private void Start()
    {
        float screenRatio =(float)Screen.width /(float) Screen.height;
        float targetRatio = refe.transform.localScale.x / refe.transform.localScale.y;
        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = refe.transform.localScale.y/2;
        }
        else
        {
            float differenceInsize  = targetRatio / screenRatio;
            Camera.main.orthographicSize = refe.transform.localScale.y / 2 * differenceInsize;
        }
    }
}
