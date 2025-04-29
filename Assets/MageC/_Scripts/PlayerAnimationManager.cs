using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour

{

    public Animator Animator;

    public Vector3 LastPosition;

    void Start()

    {

        Animator = GetComponent<Animator>();
        LastPosition = this.transform.position;

    }

    void Update()

    {

        if (GetComponentInParent<PlayerMovementManager>().IsCrouching == true)

        {

            Animator.SetBool("IsCrouching", true);
            Animator.SetBool("IsFalling", false);
            Animator.SetBool("IsErect", false);

            if (this.transform.position == LastPosition)

            {

                Animator.SetFloat("CrouchSpeed", 0);

            }

            else

            {

                Animator.SetFloat("CrouchSpeed", 1);

            }

            LastPosition = this.transform.position;

        }

        if (GetComponentInParent<PlayerMovementManager>().IsCrouching == false)

        {

            Animator.SetBool("IsCrouching", false);
            Animator.SetBool("IsFalling", false);
            Animator.SetBool("IsErect", true);

            if (this.transform.position == LastPosition)

            {

                Animator.SetFloat("Speed", 0);

            }

            else if (GetComponentInParent<PlayerMovementManager>().IsRunning == true)

            {

                Animator.SetFloat("Speed", 2);

            }

            else

            {

                Animator.SetFloat("Speed", 1);

            }

            LastPosition = this.transform.position;

        }

        if (GetComponentInParent<PlayerMovementManager>().IsFalling == true)

        {

            Animator.SetBool("IsFalling", true);
            Animator.SetBool("IsErect", false);
            Animator.SetBool("IsCrouching", false);

        }

    }



}