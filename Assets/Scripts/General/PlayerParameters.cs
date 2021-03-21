using UnityEngine;

/*! \class PlayerParameters
 *  \brief Scriptable object containing the player character variables parameters.
 */
[CreateAssetMenu(fileName = "Player Parameters", menuName = "Get the pet/Player Parameters", order = 1)]
public class PlayerParameters : ScriptableObject {

	[Header("Inputs")]
    [Tooltip("Minimum distance to consider a swipe movement.")]
    public float swipeThreshold = 30f;                              //!< Minimum distance to consider a swipe movement. 

    [Header("Movement")]
    [Tooltip("Character initial movement speed.")]
    public float movementSpeed = 10f;                               //!< Character movement speed.
    [Tooltip("Maximum movement speed.")]
    public float maxSpeed = 15f;                                    //!< Maximum movement speed.
    [Tooltip("Character acceleration in units/seconds.")]
    public float acceleration = 0.5f;                               //!< Character speed acceleration.

    [Header("Jump")]
    [Tooltip("Jump height.")]
    public float jumpHeight = 2f;                                   //!< Jump height.

    [Header("Shield")]
    [Tooltip("Invincibility duration time.")]
    public float invincibilityDuration = 1.5f;                      //!< Invincibility duration time.
}
