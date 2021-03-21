using UnityEngine;

/*! \class Jump
 *  \brief Controls the character jump movement.
 */
public class Jump {

    private float jumpTime;                                                 //!< Actual jump duration time.
    private bool fall;                                                      //!< Check if it is time to move the character downwards.
    private Vector3 tempPos;                                                //!< Container used to calculate the player position.

    /// Changes the acceleration direction to make the character fall.
    private int AccelDirection ()
    {
        if (! fall && jumpTime <= Globals.CH_JUMP_DURATION / 2)
        {
            return 1;
        }
        else
        {
            if (!fall)
            {
                fall = true;
                ResetJumpTime();
            }

            return -1;
        }
    }

    /// <summary>
    /// Sets the character jump position.
    /// </summary>
	public void DoJump (Transform playerTransform, float accel)
    {
        jumpTime += Time.deltaTime;
        tempPos = playerTransform.position;
        tempPos.y += AccelDirection() * accel * jumpTime * jumpTime / 2;
        playerTransform.position = tempPos;
    }
        
    /// Resets the jumpTime to zero.    
    private void ResetJumpTime ()
    {
        jumpTime = 0f;
    }

    /// <summary>
    /// Resets the jump parameters.
    /// </summary>
    public void ResetJump (Transform playerTransform)
    {
        tempPos = playerTransform.position;
        tempPos.y = 0.495f; // Default y position.
        playerTransform.position = tempPos;

        ResetJumpTime();
        fall = false;
    }
}
