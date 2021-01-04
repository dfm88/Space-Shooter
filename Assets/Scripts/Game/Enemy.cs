using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;

    private Player _player;


    // Start is called before the first frame update
    void Start()
    {
        //take the current position and assign to start position random
        transform.position = new Vector3(Random.Range(-9.4f, 9.4f), 9f, 0);

        _player = GameObject.Find("Player").GetComponent<Player>();


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directions = new Vector3(0, -1, 0);
        transform.Translate(directions * Time.deltaTime * _enemySpeed);

        if (transform.position.y < -6f)
        {
            Start();
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
           
            Destroy(this.gameObject); //se collido con un player, distruggi l'Enemy
           
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject); //se collido con un priettile distruggi il proiettile e me (enemy)
            
            if(_player!=null)
            {
                _player.scoreFormKillEnemy(10);
            }
            
            Destroy(this.gameObject);

        }



    }
   

}
