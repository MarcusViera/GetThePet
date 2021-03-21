using UnityEngine;

/*! \class ObstacleSensor
 *  \brief Manages the obstacle collision.
 */
public class ObstacleSensor : CollisionDetection {
    
    protected override void CollisionEvent(Collider other)
    {        
        if (other.CompareTag(Tags.PLAYER_COLLIDER))
        {
            other.GetComponentInParent<PlayerController>().PlayerStumbled();
        }
        else
        {
            ChildSelector childSelector = GetComponent<ChildSelector>();

            if (childSelector != null)
            {
                childSelector.ResetChild();
            }

            base.CollisionEvent(other);
        }
    }    
}
