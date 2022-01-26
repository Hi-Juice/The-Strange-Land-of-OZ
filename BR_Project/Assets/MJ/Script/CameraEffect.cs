using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayCameraBounce()
    {
        //Debug.Log("PlayerCameraBounce=======================");
        StartCoroutine(CameraBounceEffect());
    }

    public float cameraBounceTime = 0.05f;
    IEnumerator CameraBounceEffect()
    {
        cam.fieldOfView = 59.2f;
        yield return new WaitForSeconds(cameraBounceTime);
        cam.fieldOfView = 60;

    }
}
