using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour

{

    public Text HealthText;

    public float CurrentHealth;
    public float MaximumHealth;
    public float MinimumHealth;

    public GameObject RagdollPlayer;

    public float HealthPickupHealFactor;
    PlayerCameraManager _camera;
    public void Start()

    {

        HealthText = GameObject.Find("HealthText").GetComponent<Text>();

        _camera = GameObject.Find("Main Camera").GetComponent<PlayerCameraManager>();

    }

    void Update()

    {

        HealthText.text = CurrentHealth.ToString("0");

        if (CurrentHealth >= MaximumHealth)

        {

            CurrentHealth = MaximumHealth;

        }

        if (CurrentHealth <= MinimumHealth)

        {
            if (Camera.main != null)
            {
                _camera.PlayerBody = Camera.main.transform;
                Camera.main.transform.SetParent(null);
                
            }

            CurrentHealth = MinimumHealth;
            Instantiate(RagdollPlayer, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);

        }

    }

    public void PickupHeart()

    {

        CurrentHealth += HealthPickupHealFactor;

    }

    public void ForceDeath()

    {

        CurrentHealth -= CurrentHealth;

    }

}