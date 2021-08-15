using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    public float grayscaleLerpVal = 0.0f;
    private Material img_effect;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
