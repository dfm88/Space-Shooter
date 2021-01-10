using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;

    private Player _player;
    //handle to Animator controller onEnemyDeath
    [SerializeField]
    private Animator _anim;


    // Start is called before the first frame update
    void Start()
    {
        //take the current position and assign to start position random
        transform.position = new Vector3(Random.Range(-9.4f, 9.4f), 9f, 0);

        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("Player is null in Enemy.cs");
        }

        _anim = GetComponent<Animator>();//don't have to find it cause already attached to Enmy Object

        if(_anim==null)
        {
            Debug.LogError("Animation is null in Enemy.cs");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directions = new Vector3(0, -1, 0);
        transform.Translate(directions * Time.deltaTime * _enemySpeed);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player")
        {
            //damage player - take the Player instance and damage him
            Player player = other.transform.GetComponent<Player>();
            //
            if(player!=null)
            {
                player.Damage();
            }

            enemyDestroyed();

        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject); //se collido con un priettile distruggi il proiettile e me (enemy)
            
            if(_player!=null)
            {
                _player.scoreFormKillEnemy(10);
            }

            Destroy(GetComponent<Collider2D>());  // distruggo il collider in quanto, siccome il nemico come oggetto lo distruggo
            //dopo 2.8 secondi (vedi metodo sotto) per poter riprodurre l'animazione, se sparo di nuovo essendoci ancora il collider posso ancora 
            //colpire il nemico, invece ditruggendo il collider, anche se il nemico c'è ancora anche se non si vede, non posso colpirlo

            enemyDestroyed();

        }



    }

    private void enemyDestroyed()
    {
        _anim.SetTrigger("onEnemyDeath"); //trigger that calls the destoy animation
        _enemySpeed = 0; //have to stop enemy speed so I don't get hit in the seconds the animationruns
        Destroy(this.gameObject, 2.8f); //se collido con un player, distruggi l'Enemy, imposto 2.8 sec
                                        //prima della distruzione per lasciare correre l'animazione
    }
   

}
