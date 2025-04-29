using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour

{

    public GameObject Player;
    public Vector3 Spawnpoint;

    public bool Spawn_At_Death;
    public bool Spawn_At_Spawnpoint;

    public void Update()

    {

        if (Spawn_At_Death)

        {

            if (Input.GetKey(KeyCode.R))

            {

                Instantiate(Player, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);

            }

        }

        else if (Spawn_At_Spawnpoint)

        {

            if (Input.GetKey(KeyCode.R))

            {

                Instantiate(Player, Spawnpoint, Quaternion.identity);
                Destroy(this.gameObject);

            }

        }

    }

}