using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour

{

    public bool IsDamageCollider;
    public bool IsDeathCollider;
    public bool IsHealthCollider;

    public float Amount;

    public void OnTriggerStay(Collider other)

    {

        if (other.gameObject.tag == "Player")

        {

            if (IsDamageCollider)

            {

                other.GetComponent<PlayerHealthManager>().CurrentHealth -= Amount;

            }

            if (IsHealthCollider)

            {

                other.GetComponent<PlayerHealthManager>().CurrentHealth += Amount;

            }

            if (IsDeathCollider)

            {

                other.GetComponent<PlayerHealthManager>().ForceDeath();

            }

        }

    }

}