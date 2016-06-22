using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy = null;
	public float spawnTime = 5.0f;

	float deltaSpawnTime = 0.0f;
	public int maxEnemyNum = 5;
	int currentEnemyNum = 0;

	GameObject[] enemyPool = null;
	int poolSize = 10;

	public void ReduceEnemyCount()
	{
		if (currentEnemyNum > 0)
			currentEnemyNum -= 1;
	}

	void Start()
	{
		enemyPool = new GameObject[poolSize];
		for(int i=0; i < poolSize; ++i)
		{
			enemyPool[i] = Instantiate(enemy) as GameObject;
			enemyPool[i].name = "Enemy_" + i;
			enemyPool[i].SetActive(false);
		}
	}

	void Update()
	{
		deltaSpawnTime += Time.deltaTime;
		if(deltaSpawnTime > spawnTime)
		{
			deltaSpawnTime = 0.0f;
			if(currentEnemyNum < maxEnemyNum)
			{
				for(int i=0; i<poolSize; ++i)
				{
					GameObject enemyObj = enemyPool[i];
					if (enemyObj.activeSelf == true)
						continue;
					enemyObj.SetActive(true);

					float x = Random.Range(-20.0f, 20.0f);
					enemyObj.transform.position = new Vector3(x, 0.1f, 20.0f);
					currentEnemyNum += 1;
					break;
				}

			}
		}
	}
}