using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickupsManager : MonoBehaviour

{

    public Text InfoText;

    public Keyboard Keycodes;

    [System.Serializable]

    public class Keyboard

    {

        public KeyCode Pickup = KeyCode.E;

    }

    public void Start()

    {

        InfoText = GameObject.Find("InfoText").GetComponent<Text>();

    }

    public void OnTriggerStay(Collider other)

    {

        InfoText.text = "Press " + Keycodes.Pickup + " To Pick Up";

        if (other.gameObject.tag == "Pickup")

        {

            if (other.gameObject.name == "HeartPickup")

            {

                if (Input.GetKey(Keycodes.Pickup))

                {

                    if (GetComponent<PlayerHealthManager>().CurrentHealth < GetComponent<PlayerHealthManager>().MaximumHealth)

                    {

                        this.transform.SendMessage("PickupHeart");
                        Destroy(other.gameObject);
                        InfoText.text = "Test Level";

                    }

                }

            }

            if (other.gameObject.name == "SpeedPickup")

            {

                if (Input.GetKey(Keycodes.Pickup))

                {

                    this.transform.SendMessage("PickupSpeed");
                    Destroy(other.gameObject);
                    InfoText.text = "Test Level";

                }

            }

            if (other.gameObject.name == "JumpPickup")

            {

                if (Input.GetKey(Keycodes.Pickup))

                {

                    this.transform.SendMessage("PickupJump");
                    Destroy(other.gameObject);
                    InfoText.text = "Test Level";

                }

            }

        }

        else

        {

            InfoText.text = "Test Level";

        }

    }

    public void OnTriggerExit(Collider other)

    {

        InfoText.text = "Test Level";

    }

}