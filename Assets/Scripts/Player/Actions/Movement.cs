using UnityEngine;

/*! \class Movement
 *  \brief Controls the player character running movement.
 */
public class Movement {    

    private Vector3 tempPos;                                                //!< Container used to calculate the player position.

    /// <summary>
    /// Moves the character forward.
    /// </summary>
	public void Move (Transform playerTransform, ref float speed, float acceleration, bool accelerate)
    {
        tempPos = playerTransform.position;

        // Acceleration.
        if (accelerate)
        {
            speed += acceleration * Time.deltaTime;
        }

        tempPos += playerTransform.forward * speed * Time.deltaTime;
        playerTransform.position = tempPos;
    }    
}
