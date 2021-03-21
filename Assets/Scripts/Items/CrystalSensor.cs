using UnityEngine;

/*! \class CrystalSensor
 *  \brief Gets the collision event of the crystal item.
 */
public class CrystalSensor : CollisionDetection {

    private MoveTowards moveTowards;                            //!< Cached component MoveTowards.

    private void Start()
    {
        moveTowards = GetComponent<MoveTowards>();
    }

    protected override void CollisionEvent(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER_COLLIDER))
        {
            GameController.Instance.AddCrystal();
        }

        // Turn off the magnet effect.
        moveTowards.enabled = false;

        base.CollisionEvent(other);
    }
}
