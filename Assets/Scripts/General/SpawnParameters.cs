using UnityEngine;

/*! \class SpanwParameters
 *  \brief Scriptable object containing the spawn variables parameters.
 */
 [CreateAssetMenu(fileName = "Spawn Parameters", menuName = "Get the pet/Spawn Parameters", order = 0)]
public class SpawnParameters : ScriptableObject {

    public const float CELL_WIDTH = 2.75f;                      //!< Grid cell widtf.
    public const float CELL_HEIGHT = 4f;                        //!< Grid cell height.
    public const int GRID_ROWS = 15;                            //!< Amount of grid rows.
    public const int GRID_COLUMNS = 3;                          //!< Amount of grid columns.

    public const int GRID_CAR_CELLS = 3;                        //!< Amount of cells, in the spawn grid, the car occupy.
    public const int GRID_CHAMELEON_CELLS = 1;                  //!< Amount of cells, in the spawn grid, the chameleon occupy.
    public const int GRID_CONE_CELLS = 1;                       //!< Amount of cells, in the spawn grid, the cone occupy.
    public const int GRID_LIZARD_CELLS = 1;                     //!< Amount of cells, in the spawn grid, the lizard occupy.
    public const int GRID_PLANK_CELLS = 1;                      //!< Amount of cells, in the spawn grid, the plank occupy.    

    /*! Obstacles base parameters. */
    [System.Serializable]
    public struct ObstacleStruct
    {
        [Tooltip("Delay time to spawn the obstacle.")]
        public float spawnDelay;                                //!< Delay time to spawn the obstacle.
        [Range(0f, 1f)]
        [Tooltip("Percentage chance to spawn two obstacles of this type, at the same time.")]
        public float doubleSpawnChance;                         //!< Percentage chance to spawn two obstacles of this type, at the same time.  
        [Tooltip("Check if this obstacle can be avoided by the slide or jump.")]
        public bool isAvoidable;                                //!< Check if this obstacle can be avoided by the slide or jump.
    }

    #region Obstacles
    [Header("Obstacles Parameters")]
    [Space(5f)]
    public ObstacleStruct carParameters;                        //!< Car obstacle spawn parameters.    

    [Space(5f)]
    public ObstacleStruct chameleonParameters;                  //!< Chameleon obstacle spawn parameters.

    [Space(5f)]
    public ObstacleStruct coneParameters;                       //!< Cone obstacle spawn parameters.

    [Space(5f)]
    public ObstacleStruct lizardParameters;                     //!< Lizard obstacle spawn parameters.

    [Space(5f)]
    public ObstacleStruct plankParameters;                      //!< Plank obstacle spawn parameters.
    #endregion

    #region Items
    [Header("Items Parameters")]
    [Space(5f)]

    [Tooltip("Minimum amount of coins to spawn.")]
    public int minCoinSpawnAmount;                              //!< Minimum amount of coins to spawn.
    [Tooltip("Maximum amount of coins to spawn.")]
    public int maxCoinSpawnAmount;                              //!< Maximum amount of coins to spawn.
    [Tooltip("Minimum distance between the last coin and the obstacle.")]
    public float minDistCoinToObstacle;                         //!< Minimum distance between the last coin and the obstacle.

    [Space(5f)]
    [Tooltip("Delay time to spawn the magnet item.")]
    public float magnetSpawnDelay;                              //!< Delay time to spawn the magnet item.

    [Space(5f)]
    [Tooltip("Delay time to spawn the crystal item.")]
    public float crystalSpawnDelay;                             //!< Delay time to spawn the crystal item.

    [Space(5f)]
    [Tooltip("Delay time to spawn the shield item.")]
    public float shieldSpawnDelay;                              //!< Delay time to spawn the shield item.

    [Space(5f)]
    [Tooltip("Delay time to spawn an animal sphere.")]
    public float animalSphereDelay;                             //!< Delay time to spawn an animal sphere.

    //Linha de Teste para o Duplicator
    [Space(5f)]
    [Tooltip("Delay time to spawn the duplicator item.")]
    public float duplicatorSpawnDelay;                          //!< Delay time to spawn the duplicator item.
    #endregion
}
