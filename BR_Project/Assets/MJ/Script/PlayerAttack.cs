using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator playerAnim;
    public GameObject bullet_prefab;

    public Transform shotPos;
    public float cooltime;
    private float curtime;

    public SubMob_Tree[] mobs;
    GameObject enemy;
    public string tagName;
    public float shortDis;

    [SerializeField] public int shot = 12;
    public float deg;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(curtime <= 0)
        {
            if (Input.GetKey(KeyCode.X))
            {
                playerAnim.SetTrigger("attack");
                SoundManager.Instance.Play_PlayerAttackSound();
                //GameObject go = Instantiate(bullet);
                //go.transform.position = turret.transform.position;
                Shot();
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
    }


    public void Shot()
    {
        if(FindObjectsOfType<SubMob_Tree>() != null)
        {
            SearchNearestMob();
        }
        else
        {
            enemy = GameObject.Find("AttackPoint").gameObject;
        }

        StartCoroutine(CreateMissile());
    }

    IEnumerator CreateMissile()
    {
        int _shot = shot;
        while (_shot > 0)
        {
            _shot--;

            GameObject bullet = Instantiate(bullet_prefab);
            bullet.transform.position = shotPos.position;
            bullet.GetComponent<BezierBullet>().master = shotPos.gameObject;
            bullet.GetComponent<BezierBullet>().enemy = enemy;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }



   private void SearchNearestMob()
    {
        mobs = FindObjectsOfType(typeof(SubMob_Tree)) as SubMob_Tree[];
        if(mobs.Length != 0)
        {
            shortDis = Vector3.Distance(gameObject.transform.position, mobs[0].transform.position); // 첫번째를 기준으로 잡아주기 

            enemy = mobs[0].gameObject; // 첫번째를 먼저 

            foreach (SubMob_Tree found in mobs)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    enemy = found.gameObject;
                    shortDis = Distance;
                }
            }
        }
    }
}
