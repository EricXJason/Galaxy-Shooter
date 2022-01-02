using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]GameObject EnemyPrefab;
    [SerializeField]GameObject EnemyContainer;//抓取自身來作為生成敵人的母體容器
    public bool StopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRoutine()//產生敵人
    { 
        /*wait 1 frame
        yield return null;
        //then this line called.
        
        yield return new WaitForSeconds(5.0f);
        then this line called.*/

        while(StopSpawning == false)
        {
            Vector3 PositionToSpaqn = new Vector3(Random.Range(-8f, 8f), 8f ,0);
            GameObject NewEnemy = Instantiate(EnemyPrefab, PositionToSpaqn, Quaternion.identity);
            //將生成的敵人的母體定為EnemyContainer
            NewEnemy.transform.parent = EnemyContainer.transform;
            //每5秒循環執行
            yield return new WaitForSeconds(2.5f);
        }
    }

    public void OnPlayerDeath()
    {
        StopSpawning = true;
    }
}
