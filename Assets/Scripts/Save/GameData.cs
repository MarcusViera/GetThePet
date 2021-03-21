using UnityEngine;

/*! \class GameData
 *  \brief Saves and get the data from the game scene.
 */
public class GameData {

    public static int selectedCharacterIndex = 0;                               //!< Index of the character to spawn.

    /// <summary>
    /// Gets the saved amount of a specific attribute.
    /// </summary>    
    public static int GetSavedAttribute (SaveItems itemName)
    {
        return PlayerPrefs.GetInt(itemName.ToString());
    }

    /// <summary>
    /// Saves a specific game data.
    /// </summary>
    public static void SaveData (SaveItems keyName, int keyValue)
    {
        PlayerPrefs.SetInt(keyName.ToString(), keyValue);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Clears all the game saved data.
    /// </summary>
    public static void ClearSavedData ()
    {
        PlayerPrefs.DeleteAll();        
    }
}
