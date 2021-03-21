using System.Collections;
using UnityEngine;

/*! \class LizardController
 *  \brief Controls the lizard activation.
 */
public class LizardController : MonoBehaviour {

    [SerializeField] private Transform raycastPoint;            //!< Point to fire the raycast, checking for jump obstruction.

    private int spawnedLine = 1;                                //!< Running line where the lizard was spawned.
    private bool isJumpTriggered;                               //!< Check if the jump action was triggered once.
    private bool isJumping;                                     //!< Check if the lizard is jumping.
    private bool isJumpImpulse;                                 //!< Check if the lizard animation is in impulse phase.
    private bool changeLine;                                    //!< Check if the jump will be to another line.

    private float timeMoveStarted;                              //!< Game time when the movement to another line started.
    private float impulseTimer;                                 //!< Counts the animation impulse timer.
    private float initialPosX;                                  //!< X position where the object comes from.
    private float finalPosX;                                    //!< X position where the object will move to.

    private Vector3 movePos;                                    //!< Cached container to calculate the jump X movement.
    private Transform myTransform;                              //!< Cached Transform component.

    public int SpawnedLine
    {
        set { spawnedLine = value; }
    }

    /// Method called every time the object is turned on.
    private void OnEnable()
    {
        impulseTimer = 0f;        
        isJumpTriggered = false;
        isJumping = false;
        changeLine = false;
    }

    // Use this for initialization
    void Start () {
        myTransform = transform;
	}    

    // Update is called once per frame
    void Update () {
		if (!isJumpTriggered)
        {
            TriggerJump();
        }
        else if (isJumping)
        {
            if (isJumpImpulse)
            {
                WaitJumpImpulse();
                return;
            }

            MoveSideway();
        }
	}

    /// Waits for the animation impulse end.
    private void WaitJumpImpulse ()
    {   
        impulseTimer += Time.deltaTime;

        if (isJumpImpulse && impulseTimer >= Globals.OBS_LIZARD_JUMP_IMPULSE_DURATION)
        {
            isJumpImpulse = false;
            timeMoveStarted = Time.time;            
        }        
    }

    /// Moves the object to another line.
    private void MoveSideway ()
    {
        float timeSinceMoveStarted = Time.time - timeMoveStarted;
        float rate = timeSinceMoveStarted / Globals.OBS_LIZARD_JUMP_DURATION;

        movePos = myTransform.position;
        movePos.x = Mathf.Lerp(initialPosX, finalPosX, rate);
        myTransform.position = movePos;

        if (rate >= 1f)
        {
            isJumping = false;            
        }
    }

    /// Checks if the player is close to activate the jump.
    private bool CanJump ()
    {
        float sqrDist = (myTransform.position - GameController.Instance.GetPlayerPosition()).sqrMagnitude;        
        return sqrDist <= Globals.OBS_LIZARD_JUMP_TRIGGER_DIST * Globals.OBS_LIZARD_JUMP_TRIGGER_DIST;
    }

    /// Sets the jump direciton.
    private void SetJumpDirection (bool hasObstacleLeft, bool hasObstacleRight)
    {        
        initialPosX = myTransform.position.x;
        isJumping = true;

        // Both border lines have a jump to the middle line.
        if (spawnedLine == 0 || spawnedLine == 2)
        {
            finalPosX = Globals.LINE02_X_POS;
        }
        else if (spawnedLine == 1)
        {
            // Choose a random direction.
            if (Random.value >= 0.5f)
            {
                // Change the direction if the randomized one has an obstacle.
                if (hasObstacleRight)
                {
                    finalPosX = Globals.LINE01_X_POS;
                }
                else
                {
                    finalPosX = Globals.LINE03_X_POS;
                }
            }
            else
            {                
                // Change the direction if the randomized one has an obstacle.
                if (hasObstacleLeft)
                {
                    finalPosX = Globals.LINE03_X_POS;
                }
                else
                {
                    finalPosX = Globals.LINE01_X_POS;
                }
            }
        }        
    }

    /// Check if the jump to another line is blocked by another obstacle.
    private void CheckJumpObstruction ()
    {        
        const float RAY_DIST = Globals.LINE03_X_POS - Globals.LINE02_X_POS; // Distance between lines.
        bool hasObstacleLeft = Physics.Raycast(raycastPoint.position, -raycastPoint.right, RAY_DIST);
        bool hasObstacleRight = Physics.Raycast(raycastPoint.position, raycastPoint.right, RAY_DIST);

        // The leftmost line allow only a jump to the right.
        // The middle line allow a jump in both directions.     
        // The rightmost line allow only a jump to the left.    
        if ((spawnedLine == 0 && hasObstacleRight) ||
            (spawnedLine == 1 && hasObstacleLeft && hasObstacleRight) ||
            (spawnedLine == 2 && hasObstacleLeft))
        {            
            changeLine = false;
            return;
        }

        SetJumpDirection(hasObstacleLeft, hasObstacleRight);
    }

    /// Executes the jump.
    private void TriggerJump ()
    {
        if (CanJump())
        {
            isJumpTriggered = true;
            isJumpImpulse = true;
            GetComponentInChildren<Animator>().SetTrigger(AnimationParameters.JUMP_PARAM);

            // Randomize the chance to jump to another line.
            changeLine = Random.value >= 0.5f;            

            // Jump without changing the line
            if (!changeLine)
            {                
                return;
            }

            CheckJumpObstruction();
        }
    }
}
