using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : CollisionDetection
{
    private MoveTowards moveTowards;                            //!< Cached component MoveTowards.

    private void Start()
    {
        moveTowards = GetComponent<MoveTowards>();
    }

    protected override void CollisionEvent(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER_COLLIDER))
        {
            //Debug.Log("Está colidindo com o duplicator.");
            GameController.Instance.EnableDuplicator();
        }

        // Turn off the magnet effect.
        moveTowards.enabled = false;

        base.CollisionEvent(other);
    }
}
