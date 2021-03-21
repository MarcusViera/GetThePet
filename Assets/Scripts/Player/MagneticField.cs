using UnityEngine;

/*! \class MagneticField
 *  \brief Controls the player magnetic field activation.
 */
public class MagneticField : MonoBehaviour {

    [SerializeField] private float effectDuration = 10f;                            //!< Time duration of the magnetic field effect.

    private float durationTimer;                                                    //!< Counts the effect duration time.	
	
	// Update is called once per frame
	void Update () {
        CountDuration();
	}

    /// Counts the duration time.
    private void CountDuration ()
    {
        durationTimer += Time.deltaTime;

        if (durationTimer >= effectDuration)
        {
            gameObject.SetActive(false);
        }
    }

    /// Event called when an collectable enters the magnetic field range.
    private void OnTriggerEnter (Collider other)
    {
        other.GetComponent<MoveTowards>().enabled = true;
    }

    /// <summary>
    /// Activates the magnetic field.
    /// </summary>
    public void ActivateMagneticField ()
    {
        durationTimer = 0f;
        gameObject.SetActive(true);
    }
}
