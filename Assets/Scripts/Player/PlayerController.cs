using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/*! \class PlayerController
 *  \brief Controls the actions for player inputs.
 */
public class PlayerController : MonoBehaviour {

    [SerializeField] private PlayerParameters playerParameters;                 //!< Scriptable class with the player parameters.       

    private CharacterActions actualAction = CharacterActions.None;              //!< Actual character action.
    private MoveDirection dodgeDir = MoveDirection.None;                        //!< Dodge movement direction.
    private float actionTimer;                                                  //!< Counts the character actions duration.
   
    private Movement movement;                                                  //!< Class that controls the movement.
    private float actualSpeed;                                                  //!< Actual character speed.
    private Dodge dodge;                                                        //!< Class that controls the dodge movement.
    
    [SerializeField] private Transform jumpTransform;                           //!< Object that will receive the jump position variation.
    private Jump jump;                                                          //!< Class that controls the jump.

    private float[] linesXCoord;                                                //!< X coordinates of the running lines.
    private int actualLine = 1;                                                 //!< Actual character running line.
    
    [SerializeField] private GameObject shieldFX;                               //!< Shield effect.    
    private bool hasShield;                                                     //!< Check if the character has the shield activated.
    private bool isInvincible;                                                  //!< Check if the character is invincible.

#if UNITY_EDITOR || UNITY_WEBGL
    float startXTouch;                                                          //!< Initial touch position X.
    float startYTouch;                                                          //!< Initial touch position Y.
#endif    

    private Animator anim;                                                      //!< Cached Animator component.
    private Transform myTransform;                                              //!< Cached Transform component.    
    private CapsuleCollider playerCollider;                                     //!< Cached Collider component.

    // Use this for initialization
    void Start () {
        movement = new Movement();
        jump = new Jump();
        dodge = new Dodge();

        myTransform = transform;
        playerCollider = GetComponentInChildren<CapsuleCollider>();
        anim = GetComponentInChildren<Animator>();

        linesXCoord = new float[] { Globals.LINE01_X_POS, Globals.LINE02_X_POS, Globals.LINE03_X_POS };
        actualSpeed = playerParameters.movementSpeed;

        // Starts the run animation.
        anim.SetBool(AnimationParameters.RUN_PARAM, true);
        StartCoroutine(DistanceCalculation());
	}
	
	// Update is called once per frame
	void Update () {
        SetAction();
        ApplyAction();
	}

    /// Sets the actual character action.
    private void SetAction ()
    {
        // Block new inputs during the execution of an action.
        if (actualAction != CharacterActions.None) return;

        actualAction = GetInputs();
        
        // Set the instructions triggered once, at the input beginning.
        switch (actualAction)
        {
            case CharacterActions.Jump:                
                anim.SetTrigger(AnimationParameters.JUMP_PARAM);
                break;

            case CharacterActions.Slide:
                playerCollider.center = new Vector3(playerCollider.center.x, Globals.CH_COLLIDER_SLIDING_Y_POS, playerCollider.center.z);
                playerCollider.height = Globals.CH_COLLIDER_SLIDING_HEIGHT;
                anim.SetTrigger(AnimationParameters.SLIDE_PARAM);
                break;

            case CharacterActions.Dodge:
                // Check if the character can dodge on the selected direction.
                if ((dodgeDir == MoveDirection.Left && actualLine == 0) || (dodgeDir == MoveDirection.Right && actualLine == 2))
                {
                    dodgeDir = MoveDirection.None;
                    actualAction = CharacterActions.None;
                    return;
                }

                actualLine = dodgeDir == MoveDirection.Left ? actualLine - 1 : actualLine + 1;                               

                dodge.SetDodgeParameters(myTransform.position.x, linesXCoord[actualLine]);

                break;

            default:
                break;
        }
    }

    /// Applies an action to the character.
    private void ApplyAction ()
    {        
        // Apply the movement affected by the player input.
        switch (actualAction)
        {
            case CharacterActions.Jump:
                jump.DoJump(jumpTransform, playerParameters.jumpHeight);

                if (IsActionComplete(Globals.CH_JUMP_DURATION))
                {                    
                    jump.ResetJump(jumpTransform);
                    actualAction = CharacterActions.None;
                }
                break;

            case CharacterActions.Slide:
                if (IsActionComplete(Globals.CH_SLIDE_DURATION))
                {
                    playerCollider.center = new Vector3(playerCollider.center.x, Globals.CH_COLLIDER_NORMAL_Y_POS, playerCollider.center.z);
                    playerCollider.height = Globals.CH_COLLIDER_NORMAL_HEIGHT;
                    actualAction = CharacterActions.None;
                }
                break;

            case CharacterActions.Dodge:
                dodge.MoveToDodge(myTransform);

                // Check when the dodge movement ends.
                if (dodge.IsDodgeComplete())
                {
                    actualAction = CharacterActions.None;
                }

                break;

            default:
                break;
        }
        
        movement.Move(myTransform, ref actualSpeed, playerParameters.acceleration, actualSpeed < playerParameters.maxSpeed);
    }

    /// Gets the player inputs to apply an action accordingly.
    private CharacterActions GetInputs ()
    {
#if UNITY_EDITOR || UNITY_WEBGL
        #region Editor touch simulation
        // Check the starting position of the touch.
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            startXTouch = Input.mousePosition.x;
            startYTouch = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            float swipeX = Input.mousePosition.x - startXTouch; // Difference on X movement.
            float swipeY = Input.mousePosition.y - startYTouch; // Difference on Y movement.
            float absSwipeDeltaX = Mathf.Abs(swipeX);
            float absSwipeDeltaY = Mathf.Abs(swipeY);

            // Return if the movement do not trespass the threshold value.
            if (absSwipeDeltaX < playerParameters.swipeThreshold && absSwipeDeltaY < playerParameters.swipeThreshold) return CharacterActions.None;            

            // Determine the swipe direction.
            if (absSwipeDeltaX > absSwipeDeltaY)
            {
                dodgeDir = swipeX < 0 ? MoveDirection.Left : MoveDirection.Right;
                return CharacterActions.Dodge;
            }
            else
            {
                if (swipeY > 0f) return CharacterActions.Jump;
                else return CharacterActions.Slide;
            }            
        }
        #endregion
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.GetTouch(0);
            
            float swipeX = touch.deltaPosition.x;
            float swipeY = touch.deltaPosition.y;
            float absSwipeDeltaX = Mathf.Abs(swipeX);
            float absSwipeDeltaY = Mathf.Abs(swipeY);
            
            // Return if the movement do not trespass the threshold value.
            if (absSwipeDeltaX < playerParameters.swipeThreshold && absSwipeDeltaY < playerParameters.swipeThreshold) return CharacterActions.None;            

            // Determine the swipe direction.
            if (absSwipeDeltaX > absSwipeDeltaY)
            {
                dodgeDir = swipeX < 0 ? MoveDirection.Left : MoveDirection.Right;
                return CharacterActions.Dodge;
            }
            else
            { 
                if (swipeY > 0f) return CharacterActions.Jump;
                else return CharacterActions.Slide;
            }            
        }
#endif

        return CharacterActions.None;
    }
    
    /// <summary>
    /// Check if the duration of an action (jump or slide) ended.
    /// </summary>    
    private bool IsActionComplete (float duration)
    {
        actionTimer += Time.deltaTime;

        if (actionTimer >= duration)
        {
            actionTimer = 0f;
            return true;
        }

        return false;
    }

    /// Routine to calculate the distance traveled by the character.
    private IEnumerator DistanceCalculation ()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            GameController.Instance.UpdateScore(Mathf.Round(myTransform.position.z * 10) / 10);
        }        
    }

    /// <summary>
    /// Triggers the magnetic field activation.
    /// </summary>
    public void MagnetCollected ()
    {
        GetComponentInChildren<MagneticField>(true).ActivateMagneticField();
    }

    /// <summary>
    /// Activates the shield.
    /// </summary>
    public void ShieldCollected()
    {
        hasShield = true;
        shieldFX.SetActive(true);
    }

    /// <summary>
    /// Turn off the character invincibility.
    /// </summary>
    public void InvincibilityEnds ()
    {
        isInvincible = false;
    }

    /// <summary>
    /// Stops the player movement.
    /// </summary>
    public void PlayerStumbled ()
    {
        if (hasShield)
        {
            hasShield = false;
            shieldFX.SetActive(false);
            isInvincible = true;
            GetComponent<FlashBehavior>().Flash(playerParameters.invincibilityDuration, InvincibilityEnds);
        }
        else if (!isInvincible)
        {
            if (actualAction == CharacterActions.Jump)
            {
                jump.ResetJump(jumpTransform);
            }

            anim.SetTrigger(AnimationParameters.STUMBLE_PARAM);
            gameObject.GetComponentInChildren<Collider>().enabled = false;  // Disable the collider to avoid another call to this method

            // Set the shadow to follow the character movement.        
            myTransform.GetChild(1).GetComponent<Animator>().enabled = true;

            GameController.Instance.GameOver();
            StopAllCoroutines();
            enabled = false;
        }
    }
}
