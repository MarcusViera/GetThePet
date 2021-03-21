using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \class ShieldSensor
 *  \brief Gets the collision event of the shield item.
 */
public class ShieldSensor : CollisionDetection {

    private MoveTowards moveTowards;                            //!< Cached component MoveTowards.

    private void Start()
    {
        moveTowards = GetComponent<MoveTowards>();
    }

    protected override void CollisionEvent(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER_COLLIDER))
        {
            other.GetComponentInParent<PlayerController>().ShieldCollected();
        }

        // Turn off the magnet effect.
        moveTowards.enabled = false;

        base.CollisionEvent(other);
    }
}
