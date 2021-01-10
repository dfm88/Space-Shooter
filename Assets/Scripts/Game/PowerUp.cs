using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _powerUpSpeed = 3f;
    [SerializeField]
    private int powerupID; // ID for powerups: 0=TripleShot / 1=SpeedUp / 3=Shield


    // Start is called before the first frame update
    void Start()
    {
        //take the current position and assign to start position random
        transform.position = new Vector3(Random.Range(-9.4f, 9.4f), 9f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directions = new Vector3(0, -1, 0);
        transform.Translate(directions * Time.deltaTime * _powerUpSpeed);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            //damage player - take the Player instance and damage him
            Player player = other.transform.GetComponent<Player>();
            //
            if (player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.takePowerUpTripleShot();
                        break;
                    case 1:
                        player.takePowerUpSpeedUp();
                        break;

                    case 2:
                        player.takePowerUpShield();
                        break;

                    default:
                        Debug.Log("Caso non gestito nel Power Up");
                        break;
                }
            }

            Destroy(this.gameObject); //se collido con un player, distruggi l'Enemy

        }




    }


}
