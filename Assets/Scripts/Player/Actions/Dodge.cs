using UnityEngine;

/*! \class Dodge
 *  \brief Controls the character dodge side movement.
 */
public class Dodge {

    private float dodgeStartedTime;                                         //!< Game time when the dodge started.
    private float dodgeStartPos;                                            //!< Position where the character comes from.
    private float dodgeEndPos;                                              //!< Position to move the character when the dodge ends.

    private float dodgeDurationRate;                                        //!< Calculates the executed dodge movement percentage.

    private Vector3 tempPos;                                                //!< Container used to calculate the player position.

    /// <summary>
    /// Checks if the dodge movement is complete.
    /// </summary>    
    public bool IsDodgeComplete ()
    {
        return dodgeDurationRate >= 1f;
    }

    /// <summary>
    /// Initializes the dodge parameters.
    /// </summary>
    public void SetDodgeParameters (float startPos, float endPos)
    {
        dodgeStartPos = startPos;
        dodgeEndPos = endPos;
        dodgeStartedTime = Time.time;
        dodgeDurationRate = 0f;
    }

    /// <summary>
    /// Moves the character sideways.
    /// </summary>
    public void MoveToDodge (Transform playerTransform)
    {
        tempPos = playerTransform.position;

        float timeSinceStarted = Time.time - dodgeStartedTime;
        dodgeDurationRate = timeSinceStarted / Globals.CH_DODGE_DURATION;
        tempPos.x = Mathf.Lerp(dodgeStartPos, dodgeEndPos, dodgeDurationRate);
        playerTransform.position = tempPos;
    }
}
