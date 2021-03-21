using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/*! \class GameController
 *  \brief Controls the general aspects of the game scene.
 */
public class GameController : MonoBehaviour
{

    #region Singleton
    private static GameController instance;

    public static GameController Instance
    {
        get { return instance; }
    }
    #endregion

    [SerializeField] private GameObject[] characters;               //!< Playable characters prefabs. 

    [SerializeField] private Text txtCoinAmount;                    //!< UI text with the amount of collected coins.
    [SerializeField] private Text txtScore;                         //!< UI text for the score.

    private int coinAmount;                                         //!< Actual coin amount.
    private int crystalAmount;                                      //!< Actual crystal amount.
    [SerializeField] private float effectDupliactorDuration = 100f;  //!< Time duration of the magnetic field effect. 

    private bool isGameOver;                                        //!< Check when the game is over.
    private bool isDuplicatorEnable;                                //!< Check when the duplicator is enable.

    private Transform playerPosition;                               //!< Reference to the player position.

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameObject.FindGameObjectWithTag(Tags.FADER).GetComponent<SceneFader>().FadeScene(FadeState.FadeOut);
        CreateCharacter();
        isDuplicatorEnable = false;
    }

    private void Update()
    {
        if (isGameOver)
        {
            CheckReset();
        }
    }

    /// Instantiates the player character.
    private void CreateCharacter ()
    {
        GameObject character = Instantiate(characters[GameData.selectedCharacterIndex], Vector3.zero, Quaternion.identity);
        playerPosition = character.transform;

        // Set the camera to follow the character.
        Camera.main.GetComponent<CameraMovement>().enabled = true;
    }

    /// Saves the amount of coins and crystals collected.
    private void SaveCollectedItems ()
    {
        int totalCoins = GameData.GetSavedAttribute(SaveItems.COIN_AMOUNT) + coinAmount;
        int totalCrystals = GameData.GetSavedAttribute(SaveItems.CRYSTAL_AMOUNT) + crystalAmount;

        GameData.SaveData(SaveItems.COIN_AMOUNT, totalCoins);
        GameData.SaveData(SaveItems.CRYSTAL_AMOUNT, totalCrystals);
    }

    /// <summary>
    /// Gets the player position.
    /// </summary>    
    public Vector3 GetPlayerPosition ()
    {
        return playerPosition.position;
    }

    /// <summary>
    /// Updates the score value.
    /// </summary>
    public void UpdateScore (float dist)
    {
        txtScore.text = dist.ToString();
    }

    /// <summary>
    /// Adds one coin to the actual collected amount.
    /// </summary>
    public void AddCoin ()
    {
        if(isDuplicatorEnable)
        {
            coinAmount = coinAmount + 2;
            txtCoinAmount.text = coinAmount.ToString();
        }
        else
        {
            coinAmount++;
            txtCoinAmount.text = coinAmount.ToString();
        }
    }

    /// <summary>
    /// Adds one crystal to the actual collected amount.
    /// </summary>
    public void AddCrystal ()
    {
        crystalAmount++;
    }

    /// <summary>
    /// Adds one animal sphere to the saved data.
    /// </summary>    
    public void AddAnimalSphere (AnimalSpheres sphereType)
    {
        SaveItems collectedItem = SaveItems.BIRD_SPHERE_AMOUNT;

        switch (sphereType)
        {
            case AnimalSpheres.Bunny:
                collectedItem = SaveItems.BUNNY_SPHERE_AMOUNT;
                break;

            case AnimalSpheres.Cat:
                collectedItem = SaveItems.CAT_SPHERE_AMOUNT;
                break;

            case AnimalSpheres.Dog:
                collectedItem = SaveItems.DOG_SPHERE_AMOUNT;
                break;

            case AnimalSpheres.Ferret:
                collectedItem = SaveItems.FERRET_SPHERE_AMOUNT;
                break;

            case AnimalSpheres.Bird:
            default:
                break;
        }

        int amount = GameData.GetSavedAttribute(collectedItem);
        amount++;
        GameData.SaveData(collectedItem, amount);
    }

    /// <summary>
    /// Start the Coroutine off Duplicator
    /// </summary>
    public void EnableDuplicator()
    {
        StartCoroutine(Duplicator());
    }
    /// Routine to enable duplicator.
    private IEnumerator Duplicator()
    {
        isDuplicatorEnable = true;
        yield return new WaitForSeconds(effectDupliactorDuration);
        isDuplicatorEnable = false;
    }

    /// <summary>
    /// Finishes the level.
    /// </summary>
    public void GameOver ()
    {
        GameObject.FindGameObjectWithTag(Tags.SPAWNER).GetComponent<SpawnController>().StopSpawn();

        // Adjust the camera height to focus on the character.
        Camera.main.GetComponent<CameraMovement>().SetGameOverPos();
        isGameOver = true;

        SaveCollectedItems();
    }

    /// Game over reset trigger.
    private void CheckReset ()
    {               
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);            
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SceneManager.LoadScene(0);
        }
#endif  
    }
}
