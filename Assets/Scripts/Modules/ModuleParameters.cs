using UnityEngine;

/*! \class ModuleParameters
 *  \brief Modules repositioning parameters.
 */
public class ModuleParameters : MonoBehaviour {

    [SerializeField] private Transform connectionPoint;                         //!< Transform to position to connect this module on recycle.

    /// <summary>
    /// Check if the module is outside the camera to recycle position its position.
    /// </summary>    
    public bool Recycled (float camZPos)
    {
        if (transform.position.z < camZPos - Globals.MD_RECYCLE_POS_OFFSET)
        {
            transform.position = connectionPoint.position;
            return true;
        }

        return false;
    }
}
