using UnityEngine;

/*! \class CoinSensor
 *  \brief Gets the collision event of the coin item.
 */
public class CoinSensor : CollisionDetection {

    private MoveTowards moveTowards;                            //!< Cached component MoveTowards.

    private void Start()
    {
        moveTowards = GetComponent<MoveTowards>();
    }

    protected override void CollisionEvent(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER_COLLIDER))
        {
            GameController.Instance.AddCoin();
        }

        // Turn off the magnet effect.
        moveTowards.enabled = false;

        base.CollisionEvent(other);        
    }
}
