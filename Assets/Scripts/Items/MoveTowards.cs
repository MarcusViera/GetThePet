using UnityEngine;

/*! \class MoveTowards
 *  \brief Moves the object towards a position.
 */
public class MoveTowards : MonoBehaviour {

    private Transform myTransform;                                  //!< Cached Transform component.

    private void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update () {
        Move();
	}

    /// Moves the object.
    private void Move ()
    {
        Vector3 dir = GameController.Instance.GetPlayerPosition() - myTransform.position;   // Movement direction.

        myTransform.position += dir.normalized * Globals.IT_MAGNETIZED_ITEM_SPEED * Time.deltaTime;
    }
}
