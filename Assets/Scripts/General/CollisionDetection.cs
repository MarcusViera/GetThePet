using UnityEngine;

/*! \class CollisionDetection
 *  \brief Triggers the object collision method.
 */
public class CollisionDetection : MonoBehaviour {    

    private void OnTriggerEnter(Collider other)
    {
        CollisionEvent(other);        
    }
   
    /// Executes the collision event. 
    protected virtual void CollisionEvent (Collider other)
    {
        if (!other.CompareTag(Tags.MAGNETIC_FIELD))
        {
            gameObject.SetActive(false);            
        }
    }
}
