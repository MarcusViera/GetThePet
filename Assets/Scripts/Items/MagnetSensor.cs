using UnityEngine;

/*! \class MagnetSensor
 *  \brief Gets the collision event of the magnet item.
 */
public class MagnetSensor : CollisionDetection {

    private MoveTowards moveTowards;                            //!< Cached component MoveTowards.

    private void Start()
    {
        moveTowards = GetComponent<MoveTowards>();
    }

    protected override void CollisionEvent(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER_COLLIDER))
        {
            other.GetComponentInParent<PlayerController>().MagnetCollected();
        }

        // Turn off the magnet effect.
        moveTowards.enabled = false;

        base.CollisionEvent(other);
    }
}
