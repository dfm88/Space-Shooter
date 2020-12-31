using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer; //object that will collect all enemies spawned object 
    [SerializeField]
    private bool stopSpawnEnemis = false;


    // Start is called before the first frame update
    void Start()
    {
        //special method to start a Coroutine
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //spawn game object every 5 seconds
    //Create a "COROUTINE" of type "IEnumerator" -- allow to Yield (cedere) events
    IEnumerator SpawnRoutine()
    {
        while(stopSpawnEnemis == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            //this way all spwned enemies will be wrapped inside a Game Object "SpwnManager -> EnemyContainer -> Enemy"
            //and won't occupy all the game hierarchy space
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(4.0f);
        }
        
    }
    //method called by Playes.cs when he dies, to stop the enemies spawning loop
    public void onPlayerDeath()
    {
        stopSpawnEnemis = true;
    }


}
