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
    private GameObject _tripleLaserPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnMan;
    [SerializeField]
    private bool _isTripleShotActive;
    [SerializeField]
    private bool isShieldActive = false;
    [SerializeField]
    private GameObject _shieldAureaVisualizer;

    [SerializeField]
    private int _score;
    private UIManager _uiManag;

    

   

    // Start is called before the first frame update
    void Start()
    {
        //take the current position and assign to start position
        transform.position = new Vector3(0, 0, 0);
        //communicate withe the SpwnManager.cs
        //Find the object, then get the component, withe tag ora name of GameObj
        _spawnMan = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _uiManag = GameObject.Find("Canvas").GetComponent<UIManager>();

        //Nullity check
        if (_spawnMan == null || _uiManag == null)
        {
            Debug.LogError("The Spawn Manager or Ui Manager is null in Player.cs");
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
               

        //check if triple shot power up has been taken
        if(_isTripleShotActive)
        {
            Instantiate(_tripleLaserPrefab, transform.position + new Vector3(0.4f, 0.3f, -2.9f), Quaternion.identity); //crea un triplo laser
        }
        else
        {
            // Debug.Log("space key pressed"); with 1.05 offset from player x position
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity); //crea un laser
        }
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
        if(isShieldActive)
        {
            isShieldActive = false; //se avevo lo scudo, non mi danneggio, ma disabilito lo scudo
            _shieldAureaVisualizer.SetActive(false); //nascondo l'aurea
            return;
        }

        else
        {
            _lives--;

            //update lives images
            _uiManag.UpdateLives(_lives);

            //check ifdead
            if (_lives == 0)
            {
                //interrupt the enemies spawning
                _spawnMan.onPlayerDeath();
                Destroy(this.gameObject);
            }
        }

        
    }

    public void takePowerUpTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(tripleShotPowerDownRoutine());
    }

    //coroutine to stop the power up effects after 5 seconds
    IEnumerator tripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void takePowerUpSpeedUp()
    {
        _speed = _speed + 3.5f;
        StartCoroutine(speedPowerDownRoutine());
    }

    //coroutine to stop the power up effects after 5 seconds
    IEnumerator speedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = _speed - 3.5f;
    }

    public void takePowerUpShield()
    {
        isShieldActive = true;
        //no coroutine for shield, it gets disactivated when player is hit
        _shieldAureaVisualizer.SetActive(true);
    }

    public void scoreFormKillEnemy(int points)
    {
        _score += points;
        _uiManag.updateScoreOnKill(_score);       
    }

}
