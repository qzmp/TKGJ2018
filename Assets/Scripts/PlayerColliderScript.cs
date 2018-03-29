using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderScript : MonoBehaviour {

    private PlayerController pc;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Solid Barrier")
        {
            ManageCollision();
        }
        else if (other.tag == "Passable Barrier")
        {
            PassBarrier(other);
        }
    }

    private void ManageCollision()
    {
        pc.hp--;
    }

    private void PassBarrier(Collider other)
    {
        if (other.gameObject.GetComponent<ParticleSystemRenderer>().trailMaterial.color == pc.color)
        {
            pc.score++;
            pc.updateSpeed();
        }
        else
        {
            pc.hp--;
        }
    }
}
