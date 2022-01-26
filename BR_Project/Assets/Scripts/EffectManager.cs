using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject prefab_VFX_playerAttack;
    public GameObject prefab_VFX_staffEffect;
    GameObject[] effectPool_bullet;
    GameObject[] effectPool_staff;
    public int effectCount;

    void Start()
    {
        GameObject effectParent = new GameObject("EffectParent");
        effectPool_bullet = new GameObject[effectCount];
        for(int i=0; i<effectCount; i++)
        {
            effectPool_bullet[i] = (GameObject)Instantiate(prefab_VFX_playerAttack);
            effectPool_bullet[i].transform.SetParent(effectParent.transform);
            effectPool_bullet[i].SetActive(false);
        }

        effectPool_staff = new GameObject[effectCount];
        for (int i = 0; i < effectCount; i++)
        {
            effectPool_staff[i] = (GameObject)Instantiate(prefab_VFX_staffEffect);
            effectPool_staff[i].transform.SetParent(effectParent.transform);
            effectPool_staff[i].SetActive(false);
        }

    }


    GameObject eff_bullet;
    public GameObject GetBulletEffect()
    {
        foreach(GameObject obj in effectPool_bullet)
        {
            if(obj.activeSelf == false)
            {
                eff_bullet = obj;
                break;
            }
        }

        eff_bullet.SetActive(true);
        StartCoroutine(BackToPool(eff_bullet));
        //Debug.Log("EffectManager : " + eff.name);
        return eff_bullet;
    }

    GameObject eff_staff;
    public GameObject GetStaffEffect()
    {
        foreach (GameObject obj in effectPool_staff)
        {
            if (obj.activeSelf == false)
            {
                eff_staff = obj;
                break;
            }
        }

        eff_staff.SetActive(true);
        StartCoroutine(BackToPool(eff_staff));
        //Debug.Log("EffectManager : " + eff.name);
        return eff_staff;
    }

    IEnumerator BackToPool(GameObject eff)
    {
        yield return new WaitForSeconds(2.0f);
        eff.SetActive(false);
    }
}
