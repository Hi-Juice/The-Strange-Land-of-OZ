using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : MonoBehaviour
{
    public GameObject Player;
    public GameObject Player_Hand;
    public GameObject Attack_Display;

    public ScareCrow Scarecrow;
    public CircleCollider2D AttackCollider;

    public float Rush_Speed;

    public bool isStop = false;
    public bool isAttack = false;
    public bool isChasing = false;
    Vector3 dir;

    void Start()
    {
        AttackCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        
        dir = Player.transform.position - transform.position;
        if (isChasing == true)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if(isAttack == true)
            {

            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 110), 10 * Time.deltaTime);
        }
    }


    public void Start_Attack()
    {
        StartCoroutine(Attack());
    }
    public void Stop_Attack()
    {
        Debug.Log("Stop_Attack");
        AttackCollider.enabled = false;
        isAttack = false;
        isChasing = false;
        isStop = true;
        Scarecrow.isPattern = false;
        Scarecrow.isFork = false;
        
    }


    IEnumerator Attack()
    {
        int stack = 0;

        while (stack < 3)
        {
            if(isStop == true)
            {
                break;
            }
            Debug.Log("isChasing True");
            float time = 0;
            //isAttack = false;
            isChasing = true;
            yield return new WaitForSeconds(1f);
            
            while (time < 0.1f)
            {
                transform.Translate(Vector2.left * Rush_Speed * 1.5f * Time.deltaTime);
                yield return null;
                time += Time.deltaTime;
            }

            time = 0;
            isAttack = true;
            
            while (time < 0.3f)
            {
                yield return null;
                time += Time.deltaTime;
                Attack_Display.SetActive(true);
                
            }

            time = 0;
            SoundManager.Instance.Play_ScareCrowPatternForkSound();
            while (time < 1f)
            {
                
                AttackCollider.enabled = true;
                transform.Translate(Vector2.right * Rush_Speed * 4 * Time.deltaTime);
                yield return null;
                time += Time.deltaTime;
            }

            AttackCollider.enabled = false;
            transform.position = Player_Hand.transform.position;
            isAttack = false;
            Attack_Display.SetActive(false);
            stack++;
            yield return new WaitForSeconds(3f);
            
            
        }

        isChasing = false;
        yield return new WaitForSeconds(1f);
        Scarecrow.isPattern = false;
        Scarecrow.isFork = false;
        Scarecrow.isMove = false;
        //yield return new WaitForSeconds(2f);
        
    }
}
