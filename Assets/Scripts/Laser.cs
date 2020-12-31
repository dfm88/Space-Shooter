﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8f;

    // Update is called once per frame
    void Update()
    {
        //float verticalInput = Input.GetAxis("Vertical");
        Vector3 directions = new Vector3(0, 1, 0);
        transform.Translate(directions * Time.deltaTime * _laserSpeed);

        //if laser y >8 destroy
        if(transform.position.y > 8f)
        {
            Destroy(this.gameObject);
        }
    }
}
