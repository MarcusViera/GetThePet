using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashBehavior : MonoBehaviour {

    public float timeOn = 0.1f;                                 // Object's time turned on during flash.
    public float timeOff = 0.1f;                                // Object's time turned off during flash.
    public float duration = 1f;                                 // Flash effect duration.

    public GameObject flashingComponent;                        // Reference to the object that will flash.

    bool isFlashing;                                            // Check if the object is flashing.
    bool isOn;                                                  // Check if the actual object flash state is on.

    // Timers.
    float flashTimer;                                                  
    float durationTimer;            

    public delegate void OnFlashEnded();
    OnFlashEnded onFlashEnded;

    public bool IsFlashing
    {
        get { return isFlashing; }
    }

	// Use this for initialization
	void Start () {        
        // Turn off the class if it started on, to avoid unnecessary Update checks.
        if (!isFlashing) enabled = false;    
	}
	
	// Update is called once per frame
	void Update () {
        if (isFlashing)
        {
            Flashing();
        }        	
	}

    // Makes the object flash.
    void Flashing ()
    {
        flashTimer += Time.fixedDeltaTime;
        durationTimer += Time.fixedDeltaTime;
        
        // Alternate the object renderer on and off, during the flash duration.        
        if (durationTimer < duration)
        {
            if (flashTimer >= timeOn)
            {                
                isOn = !isOn;
                flashingComponent.SetActive(isOn);
                flashTimer = 0f;
            }            
        }
        else
        {
            // Reset the object to its initial state.
            isFlashing = false;            

            if (onFlashEnded != null)
            {
                onFlashEnded();
            }

            onFlashEnded = null;
            flashingComponent.SetActive(true);
            enabled = false;
        }
    }

    /// <summary>
    /// Starts the object's flash effect.
    /// </summary>
    public void Flash()
    {
        isFlashing = true;
        isOn = true;

        // Reset the timers.
        flashTimer = 0f;
        durationTimer = 0f;
        
        enabled = true;
    }

    /// <summary>
    /// Starts the object's flash effect, with a specific duration.
    /// </summary>
    public void Flash (float effectDuration, OnFlashEnded flashEndCallback)
    {
        duration = effectDuration;
        isFlashing = true;
        isOn = true;

        // Reset the timers.
        flashTimer = 0f;
        durationTimer = 0f;

        onFlashEnded = flashEndCallback;
        enabled = true;
    }

    /// <summary>
    /// Starts the object's flash effect, with a callback at the flash end.
    /// </summary>
    public void Flash (OnFlashEnded flashEndCallback)
    {
        isFlashing = true;
        isOn = true;

        // Reset the timers.
        flashTimer = 0f;
        durationTimer = 0f;

        onFlashEnded = flashEndCallback;
        enabled = true;
    }

    /// <summary>
    /// Stops the flash effect.
    /// </summary>
    public void StopFlash ()
    {
        if (isFlashing)
        {
            isFlashing = false;
            isOn = true;
            enabled = false;
        }
    }
}
