using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{


    public int BossType = 0; // 0 - 허수아비, 1 - 양철나무꾼, 2 - 사자;
    public GameObject Panal;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Panal.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.Move(BossType);
            }
        }
            
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Panal.SetActive(false);
        }
    }
}
