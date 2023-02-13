using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    Wave[] waves;
    [SerializeField]
    EnemySpawn enemySpawn;
    public int currentWaveIndex = -1;
    
    void Start(){
        StartWave();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)){
            StartWave();
        }
    }
    void StartWave(){
        if(enemySpawn.EnemyList.Count == 0  && currentWaveIndex < waves.Length){
            currentWaveIndex++;
            enemySpawn.StartWave(waves[currentWaveIndex]);
        } 
    }   
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefab;
}
