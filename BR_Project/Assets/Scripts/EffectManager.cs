using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject prefab_VFX_playerAttack;
    GameObject[] effectPool;
    public int effectCount;
    void Start()
    {
        GameObject effectParent = new GameObject("EffectParent");
        effectPool = new GameObject[effectCount];
        for(int i=0; i<effectCount; i++)
        {
            effectPool[i] = (GameObject)Instantiate(prefab_VFX_playerAttack);
            effectPool[i].transform.SetParent(effectParent.transform);
            effectPool[i].SetActive(false);
        }
    }


    GameObject eff;
    public GameObject GetEffect()
    {
        foreach(GameObject obj in effectPool)
        {
            if(obj.activeSelf == false)
            {
                eff = obj;
                break;
            }
        }

        eff.SetActive(true);
        StartCoroutine(BackToPool(eff));
        //Debug.Log("EffectManager : " + eff.name);
        return eff;
    }

    IEnumerator BackToPool(GameObject eff)
    {
        yield return new WaitForSeconds(2.0f);
        eff.SetActive(false);
    }
}
