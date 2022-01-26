using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    public float grayscaleLerpVal = 0.0f;
    private Material img_effect;
    Camera cam;

    public float shakeAmount;
    float shakeTime;
    Vector3 initialPosition;

    private void Awake()
    {
        img_effect = new Material(Shader.Find("Shader/Grayscale"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(grayscaleLerpVal == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }
        img_effect.SetFloat("_LerpVal", grayscaleLerpVal);
        Graphics.Blit(source, destination, img_effect);
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKey(KeyCode.A)) /* Test code */
        //{
        //    VibrateForTime(0.7f);
        //}
        if(shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * shakeAmount + initialPosition;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0.0f;
            transform.position = initialPosition;
        }
    }

    bool isPlaying = false;

    public void SetGrayScaleEffect()
    {
        if(isPlaying)
        {
            return;
        }

        Debug.Log("SetGrayScaleEffect");
        StartCoroutine(LerpGrayScaleEffect());

    }

    public float lerpTime = 0.1f;
    public float lerpEffectVal = 0.05f;

    float time = 0f;
    public float PlayGrayscaleTime = 3f;

    IEnumerator LerpGrayScaleEffect()
    {
        isPlaying = true;
        time = 0f;
        while (grayscaleLerpVal < 1)
        {
            time += Time.deltaTime / PlayGrayscaleTime;
            grayscaleLerpVal += lerpEffectVal;
            yield return null;
        }

        isPlaying = false;
        //for(float i= grayscaleLerpVal; i<=1; i += lerpEffectVal)
        //{
        //    yield return new WaitForSeconds(lerpTime);
        //}
    }

    // ÃâÃ³ : https://m.blog.naver.com/sj_artist/221851963414

    bool isBounce = false;
    public void PlayCameraBounce()
    {
        if (!isBounce)
        {
            isBounce = true;
            //Debug.Log("PlayerCameraBounce=======================");
            StartCoroutine(CameraBounceEffect());
            
        }
    }

    public float cameraBounceTime = 0.05f;
    public float cameraShakeTime = 0.5f;
    public float lerpCameraVal = 1f;
    public float maxOrthSize = 5;
    public float minOrthSize = 4.5f;
    float c_time = 0f;
    IEnumerator CameraBounceEffect()
    {
        c_time = 0f;
        while(GetComponent<Camera>().orthographicSize > minOrthSize)
        {
            c_time += Time.deltaTime / cameraBounceTime;
            GetComponent<Camera>().orthographicSize -= lerpCameraVal*Time.deltaTime;
            yield return null;
        }
        
        c_time = 0f;
        while (GetComponent<Camera>().orthographicSize < maxOrthSize)
        {
            c_time += Time.deltaTime / cameraBounceTime;
            GetComponent<Camera>().orthographicSize += lerpCameraVal * Time.deltaTime;
            yield return null;
        }
        //while(transform.position)
        //transform.position = new Vector3(Mathf.Lerp(0, 0.3f, Time.time), 0, transform.position.z);
        //yield return new WaitForSeconds(cameraBounceTime);
        //transform.position = new Vector3(Mathf.Lerp(0.3f, 0, Time.time), 0, transform.position.z);
        //GetComponent<Camera>().orthographicSize = Mathf.Lerp(4.5f, 5, Time.time);
        isBounce = false;
    }

    public void VibrateForTime(float time)

    {

        shakeTime = time;

        //canvas.renderMode = RenderMode.ScreenSpaceCamera;

        //canvas.renderMode = RenderMode.WorldSpace;

    }

}
