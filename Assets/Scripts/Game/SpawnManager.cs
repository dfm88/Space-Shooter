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
    private GameObject[] _powerUpArray; //array che contiene tutti i possibili power ups
  
    [SerializeField]
    private bool stopSpawnEnemis = false;


    // Start is called before the first frame update
    void Start()
    {
        //special method to start a Coroutine
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //spawn game object every 5 seconds
    //Create a "COROUTINE" of type "IEnumerator" -- allow to Yield (cedere) events
    IEnumerator SpawnEnemyRoutine()
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
    IEnumerator SpawnPowerUpRoutine()
    {
        while (stopSpawnEnemis == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            int randomPowerUp = Random.Range(0, 3);
         
  
            GameObject newPowerUp = Instantiate(_powerUpArray[randomPowerUp], posToSpawn, Quaternion.identity);
           
            //for the power up we don't need a container because it will 
            //only be active on screen for 5 seconds
            yield return new WaitForSeconds(Random.Range(3f,8f));
        }

    }

    //method called by Playes.cs when he dies, to stop the enemies spawning loop
    public void onPlayerDeath()
    {
        stopSpawnEnemis = true;
    }


}
