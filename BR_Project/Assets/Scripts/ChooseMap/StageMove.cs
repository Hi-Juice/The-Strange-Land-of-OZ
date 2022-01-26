using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{


    public int BossType = 0; // 0 - ����ƺ�, 1 - ��ö������, 2 - ����;
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
