using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \class CameraMovement
 *  \brief Controls the camera movement.
 */
public class CameraMovement : MonoBehaviour {

    [SerializeField] private float distanceFromPlayer = 2f;                         //!< Camera distance position from the player.  
    [SerializeField] private float heightFromPlayer = 4f;                           //!< Camera height position from the player.
    [SerializeField] private float movementSmoothTime = 1f;                         //!< Camera movement smooth time.

    private Vector3 tempPos;                                                        //!< Container used to calculate the camera position.
    private Vector3 currentDampSpeed;                                               //!< Current movement damp speed.
    private Transform targetTransform;                                              //!< Cached Transform component of the target object.
    private Transform myTransform;                                                  //!< Cached Transform component.

    private const float GAME_OVER_DISTANCE = 6f;                                    //!< Distance from the player on game over.
    private const float GAME_OVER_HEIGHT = 3f;                                      //!< Height from the player on game over.

	// Use this for initialization
	void Start () {
        myTransform = transform;
        targetTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER_COLLIDER).transform;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCameraPosition();
	}    

    /// Updates the camera position to follow the player character.
    private void UpdateCameraPosition ()
    {
        tempPos = targetTransform.position - Vector3.forward * distanceFromPlayer + Vector3.up * heightFromPlayer;
        myTransform.position = Vector3.SmoothDamp(myTransform.position, tempPos, ref currentDampSpeed, movementSmoothTime);        
    }

    /// <summary>
    /// Sets new values to the camera distance position.
    /// </summary>
    public void SetGameOverPos ()
    {
        distanceFromPlayer = GAME_OVER_DISTANCE;
        heightFromPlayer = GAME_OVER_HEIGHT;
    }
}
