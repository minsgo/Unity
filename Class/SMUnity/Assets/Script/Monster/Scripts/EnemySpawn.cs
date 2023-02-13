using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{   
    [SerializeField]
    public Transform            canvasTransform;
    [SerializeField]
    public GameObject           enemyHpBarPrefab;   
    [SerializeField]
    public Transform[]          point;
    public Wave                 currentWave;
    public List<Enemy>          EnemyList;
    void Awake()
    {
        point = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
        EnemyList = new List<Enemy>();
    }
    
    public void StartWave(Wave wave) {
        currentWave = wave;
        StartCoroutine("Spawn");
    }
    private IEnumerator Spawn(){
        int spawnEnemyCount = 0;
        while(spawnEnemyCount < currentWave.maxEnemyCount){
            int randPosition    =     Random.Range(0,point.Length);
            int randEnemy       =     Random.Range(0,currentWave.enemyPrefab.Length);
            GameObject clone    =     Instantiate(currentWave.enemyPrefab[randEnemy],point[randPosition]);
            Enemy enemy         =     clone.GetComponent<Enemy>();

            enemy.Setup(this);
            EnemyList.Add(enemy);

            SpawnEnemyHpSlider(clone);
            spawnEnemyCount++;

            yield return new WaitForSeconds(currentWave.spawnTime);
        }
    }
    public void DestroyEnemy(Enemy enemy){
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
    private void SpawnEnemyHpSlider(GameObject enemy){
        GameObject  sliderClone   = Instantiate(enemyHpBarPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHpViewer>().Setup(enemy.GetComponent<EnemyHp>());

    }
 
}
