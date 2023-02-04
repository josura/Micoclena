using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] Transform spawnPoint;

    private WaitForSeconds attackWaitingTime = new WaitForSeconds(1f);
    bool inRange = false;
    int bulletCounter = 0;
    [SerializeField] int damage = 3;
    [SerializeField] GameObject bullet;
    [SerializeField] protected GameObject[] bulletList;
    [SerializeField] protected int bulletLength;

    private void Awake()
    {
        bulletList = new GameObject[bulletLength];
        for (int i = 0; i < bulletLength; i++)
        {
            bulletList[i] = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            //bulletList[i].GetComponent<HitBulletEnemy>().SetBulletType(damage);
            bulletList[i].SetActive(false);
        }
    }

    private void Start()
    {
        target = null;
    }

    private void OnEnable()
    {
        StartCoroutine(aim());
        target = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        print("in range qualcosa");
        if (other.gameObject.layer == 8)
        {
            print("in range enemy");
            target = other.gameObject;
            gameObject.transform.parent.gameObject.GetComponent<EnemyMovement>().setTargetTransform(other.gameObject.transform);

        }
        if (other.gameObject.layer == 12)
        {  //collision with enemies

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            target = null;
        }
    }

    IEnumerator aim()
    {
        while (true)
        {
            if (target != null)
            {
                transform.LookAt(target.transform);
            }
            yield return null;
        }
    }


    private void Update()
    {
        
    }

    void SpawnBullet()
    {
        bool bulletFound = false;
        bool nobullets = false;
        while (!bulletFound && !nobullets)
        {
            int counter = 0;
            for (int i = bulletCounter; i < bulletLength && !bulletFound; i++, counter++)
            {
                //Debug.Log(counter);
                if (!bulletList[i].activeInHierarchy)
                {
                    //bulletList[i].GetComponent<BulletEnemy>().Spawn(spawnPoint);
                    bulletCounter = i + 1;
                    bulletFound = true;
                }
                if (i == bulletLength - 1)
                    bulletCounter = 0;
            }
            if (bulletCounter == bulletLength)
                bulletCounter = 0;
            if (counter >= bulletLength)
            {
                nobullets = true;
            }



        }
    }
}
