using System.Collections;
using UnityEngine;

/*! \class ModulesController
 *  \brief Controls the modules recycling.
 */
public class ModulesController : MonoBehaviour {

    [SerializeField] private ModuleParameters[] moduleParameters;                           //!< All game modules.
    private int actualModuleIndex;                                                          //!< Index to the closest module to recycle.

    private Transform cameraTransform;                                                      //!< Camera Transform component.

	// Use this for initialization
	void Start () {
        cameraTransform = Camera.main.transform;
        StartCoroutine(RecycleUpdate());
	}

    /// <summary>
    /// Parallel loop to check the modules position recycle.
    /// </summary>    
    private IEnumerator RecycleUpdate ()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            // Iterate to the next module when the actual was recycled.
            if (moduleParameters[actualModuleIndex].Recycled(cameraTransform.position.z))
            {
                actualModuleIndex++;

                if (actualModuleIndex >= moduleParameters.Length)
                {
                    actualModuleIndex = 0;
                }
            }
        }
    }
}
