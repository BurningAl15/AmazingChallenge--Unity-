using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Enemy spawning variables
    [SerializeField]
    GameObject[] enemies = new GameObject[3];

    [SerializeField]
    Transform minPosition, maxPosition;

    [SerializeField]
    float  startDelay;
    float delay;

    [SerializeField]
    int minEnemyQuantity, maxEnemyQuantity;

    [HideInInspector]
    public int currentEnemyQuantity;
    #endregion

    #region Waves
    public int maxWaves;

    [HideInInspector]
    public int waveNumber;
    #endregion

    void Start () {
        waveNumber = 1;
        delay = startDelay;
        Spawn_Wave();
    }

    private void Update()
    {
        if(waveNumber <= maxWaves)
        {
            delay -=Time.deltaTime;

            currentEnemyQuantity = FindObjectsOfType<EnemyBehaviour>().Length;

            if (delay <= 0 && currentEnemyQuantity == 0)
            {
                Spawn_Wave();
                waveNumber++;
                currentEnemyQuantity = Random.Range(minEnemyQuantity, maxEnemyQuantity);
                delay = startDelay;
            }
        }
    }

    void Spawn_Wave()
    {
        if(currentEnemyQuantity==0)
            currentEnemyQuantity = Random.Range(minEnemyQuantity, maxEnemyQuantity);

        for (int i=0;i<currentEnemyQuantity;i++)
        {
            Spawn_Enemy(Random.Range(0, 3));
        }
    }
	
    void Spawn_Enemy(int _enemyID)
    {
        Instantiate(enemies[_enemyID], new Vector3(Random.Range(minPosition.position.x,maxPosition.position.x), 
            Random.Range(minPosition.position.y, maxPosition.position.y), Random.Range(minPosition.position.z, maxPosition.position.z)),Quaternion.Euler(0f,Random.Range(0f,360f),0f));
    }
}
