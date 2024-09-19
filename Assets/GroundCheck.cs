using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    void OnTrigerEnter(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundState(true);
    }

    void OnTrigerExit(Collider other)
    {
        if(other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundState(false);

    }

    void OnTrigerStay(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundState(true);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundState(true);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundState(false);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == playerController.gameObject)
        {
            return;
        }
        playerController.SetGroundState(true);
    }
}

