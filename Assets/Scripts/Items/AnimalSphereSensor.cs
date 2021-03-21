using UnityEngine;

/*! \class AnimalSphereSensor
 *  \brief Gets the collision event of the animal sphere item.
 */
public class AnimalSphereSensor : CollisionDetection {
        
    private MoveTowards moveTowards;                            //!< Cached component MoveTowards.

    private void Start()
    {
        moveTowards = GetComponent<MoveTowards>();
    }

    protected override void CollisionEvent(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER_COLLIDER))
        {
            AnimalSpheres activeSphere = (AnimalSpheres)GetComponent<ChildSelector>().SelectedChildIndex;
            GameController.Instance.AddAnimalSphere(activeSphere);
        }

        // Turn off the magnet effect.
        moveTowards.enabled = false;

        base.CollisionEvent(other);
    }
}
