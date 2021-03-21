using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \class ChildSelector
 *  \brief Selects a random child gameObject to turn on.
 */
public class ChildSelector : MonoBehaviour {

    private int selectedChildIndex;                                     //!< Holds a reference to the actual child set on.

    [SerializeField] private Transform refTransform;                    //!< Reference Transform to search for child objects.

    public int SelectedChildIndex
    {
        get { return selectedChildIndex; }
    }

    /// <summary>
    /// Returns the last selected child to initial state.
    /// </summary>
    public void ResetChild ()
    {
        refTransform.GetChild(selectedChildIndex).gameObject.SetActive(false);
    }

    /// <summary>
    /// Selects a random child, whith the object mesh, to turn on.
    /// </summary>
	public void SelectChild ()
    {
        int childAmount = refTransform.childCount;
        selectedChildIndex = Random.Range(0, childAmount);
        refTransform.GetChild(selectedChildIndex).gameObject.SetActive(true);        
    }

    /// <summary>
    /// Selects a specific child object to turn on.
    /// </summary>
    public void SelectChild (int index)
    {
        selectedChildIndex = index;
        refTransform.GetChild(index).gameObject.SetActive(true);
    }
}
