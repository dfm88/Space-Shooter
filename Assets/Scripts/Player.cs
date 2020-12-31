using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager spawnMan;

   

    // Start is called before the first frame update
    void Start()
    {
        //take the current position and assign to start position
        transform.position = new Vector3(0, 0, 0);
        //communicate withe the SpwnManager.cs
        //Find the object, then get the component, withe tag ora name of GameObj
        spawnMan = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        //Nullity check
        if (spawnMan == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        movementsManager();
        movementBound();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            fireLaser();
        }
        

    }

    void fireLaser()
    {
        _canFire = Time.time + _fireRate;
        // Debug.Log("space key pressed"); with 0.8 offset from player x position
        Instantiate(_laserPrefab, transform.position +  new Vector3(0, 0.8f, 0), Quaternion.identity); //crea un laser       
    }

    void movementsManager()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 directions = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(directions * Time.deltaTime * _speed);
    }

    void movementBound()
    {
      
        //limito il movimento verticale fra -3.8 e 0
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        
        //consento il movimento fluido orizzontale
        if (transform.position.x > 11.3f) //viceversa
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f) //non può andare giù
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        _lives--;
        //check ifdead

        if(_lives == 0 )
        {
            //interrupt the enemies spwning
            spawnMan.onPlayerDeath();           
            Destroy(this.gameObject);
        }
    }
}
