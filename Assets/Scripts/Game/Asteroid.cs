using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 3f;
    [SerializeField]
    private GameObject _explosionPrefab;

    //prendo un riferim. allo SpwnManager per poterlo avvisare nel momento in cui
    //l'asteroide è sitrutto che può iniziare a spwanare nemici , power up ecc.
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directions = new Vector3(0, 0,1 );
        transform.Rotate(directions * Time.deltaTime * _rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Laser")
        {
            //instanzio l'animazione
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject); //distruggo il laser
            _spawnManager.startSpawning();//avviso spawnm anager che può spawnare
            Destroy(this.gameObject); //distruggo l'ateroide

           


        }

    }


}
