using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \class SpawnController
 *  \brief Controls the level objects creation.
 */
public class SpawnController : MonoBehaviour {

    /*! Spawning grid cell parameters. */
    [System.Serializable]
    private struct GridCell
    {
        public Vector3 cellPosition;                    //!< Cell world position.
        public int cellColumn;                          //!< Cell grid column.
        public int cellRow;                             //!< Cell grid row.  
        public bool isOccupied;                         //!< Check if the grid cell is already occupied.

        /// <summary>
        /// Calculates the world position based on the spawning grid.
        /// </summary>        
        public static Vector3 WorldCoordinates(Vector3 basePos, int column, int row)
        {
            Vector3 worldPoint = new Vector3(basePos.x + (column * SpawnParameters.CELL_WIDTH + SpawnParameters.CELL_WIDTH / 2f), 0f,
                                             basePos.z + (row * SpawnParameters.CELL_HEIGHT + SpawnParameters.CELL_HEIGHT / 2f));
            return worldPoint;
        }
    }

    public Transform gridDebugger;

    [SerializeField] private SpawnParameters parameters;                          //!< Spawn parameters file.

    private int carSpawnTimer;                                                    //!< Counts the car spawn time.  
    private int chameleonSpawnTimer;                                              //!< Counts the chameleon spawn time.
    private int coneSpawnTimer;                                                   //!< Counts the cone spawn time.
    private int lizardSpawnTimer;                                                 //!< Counts the lizard spawn time.
    private int plankSpawnTimer;                                                  //!< Counts the plank spawn time.

    private int crystalSpawnTimer;                                                //!< Counts the crystal spawn time.    
    private int magnetSpawnTimer;                                                 //!< Counts the magnet spawn time.
    private int shieldSpawnTimer;                                                 //!< Counts the shield spawn time.

    //Linha de Teste para o Duplicator
    private int duplicatorSpawnTimer;                                             //!< Counts the duplicator spawn time.

    private int animalSphereSpawnTimer;                                           //!< Counts the animal sphere spawn time.

    private bool isGridReseted;                                                   //!< Check if the grid positions are already reseted.

    private float lastSpawnedZPos;                                                //!< Last object spawned z axis position.
    private GridCell[] gridCells;                                                 //!< Spawning grid cell paramenters.

    private List<int> gridFreeCells;                                              //!< Spawning grid cells that aren't occupied.
    private List<int> gridPathCells;                                              //!< Spawning grid cells that can accept only avoidable obstacles.

	// Use this for initialization
	void Start () {
        InitializeGrid();        
        StartCoroutine(SpawnLoop());

        // Spawn the initial obstacles.
        ResetGrid();
        SpawnCar(); 
	}

    /// Initializes the spawning grid.
    void InitializeGrid ()
    {
        // Initialize the lists with a default size.
        gridFreeCells = new List<int>(SpawnParameters.GRID_ROWS * SpawnParameters.GRID_COLUMNS);
        gridPathCells = new List<int>(SpawnParameters.GRID_COLUMNS);

        gridCells = new GridCell[SpawnParameters.GRID_ROWS * SpawnParameters.GRID_COLUMNS];

        for (int column = 0; column < SpawnParameters.GRID_COLUMNS; column++)
        {
            for (int row = 0; row < SpawnParameters.GRID_ROWS; row++)
            {
                int index = column + row * SpawnParameters.GRID_COLUMNS;

                GridCell newCell = new GridCell();
                newCell.cellColumn = column;
                newCell.cellRow = row;

                gridCells[index] = newCell;

            }
        }
    }

    /// Resets the grid parameters.
    private void ResetGrid ()
    {
        float positionFromPlayer = GameController.Instance.GetPlayerPosition().z + Globals.SP_POSITION_FROM_PLAYER;
        float gridOriginPos = Mathf.Max(positionFromPlayer, lastSpawnedZPos) + SpawnParameters.CELL_HEIGHT;

        gridDebugger.position = new Vector3(gridDebugger.position.x, gridDebugger.position.y, gridOriginPos);

        gridFreeCells.Clear();
        gridPathCells.Clear();

        for (int row = 0; row < SpawnParameters.GRID_ROWS; row++)
        {
            for (int column = 0; column < SpawnParameters.GRID_COLUMNS; column++)
            {
                int index = column + row * SpawnParameters.GRID_COLUMNS;
                gridCells[index].isOccupied = false;
                gridCells[index].cellPosition = GridCell.WorldCoordinates(new Vector3(Globals.LINE01_X_POS - SpawnParameters.CELL_WIDTH / 2f, 0f, gridOriginPos),
                                                                          gridCells[index].cellColumn, gridCells[index].cellRow);

                gridFreeCells.Add(index);
            }
        }

        isGridReseted = true;
    }    

    /// Check the objects spawn by priority level.
    private void CheckSpawn ()
    {
        if (carSpawnTimer >= parameters.carParameters.spawnDelay)
        {
            ResetGrid();
            SpawnCar();
            carSpawnTimer = 0;
        }

        if (coneSpawnTimer >= parameters.coneParameters.spawnDelay)
        {
            if (!isGridReseted)
            {
                ResetGrid();
            }

            SpawnCone();
            coneSpawnTimer = 0;
        }

        if (chameleonSpawnTimer >= parameters.chameleonParameters.spawnDelay)
        {
            if (!isGridReseted)
            {
                ResetGrid();
            }

            SpawnChameleon();
            chameleonSpawnTimer = 0;
        }

        if (lizardSpawnTimer >= parameters.lizardParameters.spawnDelay)
        {
            if (!isGridReseted)
            {
                ResetGrid();
            }

            SpawnLizard();
            lizardSpawnTimer = 0;
        }

        if (plankSpawnTimer >= parameters.plankParameters.spawnDelay)
        {
            if (!isGridReseted)
            {
                ResetGrid();
            }

            SpawnPlank();
            plankSpawnTimer = 0;
        }

        if (magnetSpawnTimer >= parameters.magnetSpawnDelay)
        {
            if (!isGridReseted)
            {
                ResetGrid();
            }

            SpawnMagnet();
            magnetSpawnTimer = 0;
        }

        if (crystalSpawnTimer >= parameters.crystalSpawnDelay)
        {
            if (!isGridReseted)
            {
                ResetGrid();
            }

            SpawnCrystal();
            crystalSpawnTimer = 0;
        }

        if (shieldSpawnTimer >= parameters.shieldSpawnDelay)
        {
            if (!isGridReseted)
            {
                ResetGrid();
            }

            SpawnShield();
            shieldSpawnTimer = 0;
        }

        if (duplicatorSpawnTimer >= parameters.duplicatorSpawnDelay)
        {
            if (!isGridReseted)
            {
                ResetGrid();
            }

            SpawnDuplicator();
            duplicatorSpawnTimer = 0;
        }

        if (animalSphereSpawnTimer >= parameters.animalSphereDelay)
        {
            if (!isGridReseted)
            {
                ResetGrid();
            }

            SpawnAnimalSphere();
            animalSphereSpawnTimer = 0;
        }
    }

    /// Increments the spawn timers.
    private void IncrementSpawnTimers ()
    {
        carSpawnTimer++;
        coneSpawnTimer++;
        chameleonSpawnTimer++;       
        lizardSpawnTimer++;                                                 
        plankSpawnTimer++;                                                  

        crystalSpawnTimer++;                                                
        magnetSpawnTimer++;                                                 
        shieldSpawnTimer++;
        duplicatorSpawnTimer++;

        animalSphereSpawnTimer++;

        CheckSpawn();
    }

    #region Grid search
    /// Finds the first path cell available to spawn.
    private int FindFreePath ()
    {
        int cellIndex = -1;

        for (int i = 0; i < gridPathCells.Count; i++)
        {
            if (!gridCells[gridPathCells[i]].isOccupied)
            {
                cellIndex = gridPathCells[i];
                return cellIndex;
            }
        }

        return cellIndex;
    }

    /// Finds the first row with the three columns cells available to spawn.
    private int FindFreeRow ()
    {
        int rowIndex = -1;

        for (int i = 0; i < SpawnParameters.GRID_ROWS; i++)
        {            
            if (!gridCells[0 + i * SpawnParameters.GRID_COLUMNS].isOccupied && 
                !gridCells[1 + i * SpawnParameters.GRID_COLUMNS].isOccupied && 
                !gridCells[2 + i * SpawnParameters.GRID_COLUMNS].isOccupied)
            {
                rowIndex = i;
                return rowIndex;
            }
        }

        return rowIndex;
    }
    #endregion

    #region Spawn methods
    /// Spawns the selected object.
    private void SpawnObject (Vector3 spawnPos, string objTag, bool selectChild)
    {
        GameObject spawned = ObjectPooler.Instance.GetPooledObject(objTag);        
        spawned.transform.position = spawnPos;

        if (selectChild)
        {
            spawned.GetComponent<ChildSelector>().SelectChild();
        }

        spawned.SetActive(true);
    }

    /// Spawns the selected object, and returns a reference of it.
    private GameObject SpawnObject (Vector3 spawnPos, string objTag)
    {
        GameObject spawned = ObjectPooler.Instance.GetPooledObject(objTag);
        spawned.transform.position = spawnPos;       
        spawned.SetActive(true);

        return spawned;
    }

    /// Spawns a single coin.
    private void SpawnSingleCoin (Vector3 coinPosition, string coinTag)
    {
        SpawnObject(coinPosition, coinTag, false);
    }

    /// Coin spawn routine, starting in the first grid row and ascending in the column.
    private void SpawnCoinAscending (int firstColumn, int firstRow, ref int lastColumn, ref int lastRow)
    {
        int selectedAmount = Random.Range(parameters.minCoinSpawnAmount, parameters.maxCoinSpawnAmount);
        
        for (int i = 0; i < selectedAmount; i++)
        {
            int index = firstRow + firstColumn + i * 3;

            // Do not spawn on occupied positions.
            if (gridCells[index].isOccupied) continue;

            Vector3 selectedPos = gridCells[index].cellPosition;

            SpawnObject(selectedPos, Tags.IT_COIN, false);

            // Set the grid cell as occupied.            
            gridCells[index].isOccupied = true;
            gridFreeCells.Remove(index);

            if (i == selectedAmount - 1)
            {
                lastColumn = gridCells[index].cellColumn;
                lastRow = gridCells[index].cellRow;
            }
        }        
    }

    /// Coin spawn routine, descending in the spawn grid.
    private void SpawnCoinDescending (int firstColumn, int firstRow)
    {
        int selectedAmount = Random.Range(parameters.minCoinSpawnAmount, parameters.maxCoinSpawnAmount);
        Vector3 selectedPos = Vector3.zero;

        for (int i = 0; i < selectedAmount; i++)
        {
            int index = firstColumn + firstRow * SpawnParameters.GRID_COLUMNS - i * 3;

            if (index < 0) break; // Stop the spawn when the grid space ends.

            // Do not spawn on occupied positions.
            if (gridCells[index].isOccupied) continue;

            selectedPos = gridCells[index].cellPosition;
            SpawnObject(selectedPos, Tags.IT_COIN, false);

            // Set the grid cell as occupied.            
            gridCells[index].isOccupied = true;
            gridFreeCells.Remove(index);            
        }
    }

    /// Magnet spawn routine.
    private void SpawnMagnet ()
    {
        int selectedIndex = gridFreeCells[Random.Range(0, gridFreeCells.Count)];

        Vector3 selectedPos = gridCells[selectedIndex].cellPosition;
        SpawnObject(selectedPos, Tags.IT_MAGNET, false);

        // Set the grid cell occupied by the magnet.                
        gridFreeCells.Remove(selectedIndex);
        gridCells[selectedIndex].isOccupied = true;

        lastSpawnedZPos = selectedPos.z;        
    }

    /// Crystal spawn routine.
    private void SpawnCrystal ()
    {
        int selectedIndex = gridFreeCells[Random.Range(0, gridFreeCells.Count)];

        Vector3 selectedPos = gridCells[selectedIndex].cellPosition;
        SpawnObject(selectedPos, Tags.IT_CRYSTAL, false);

        // Set the grid cell occupied by the magnet.                
        gridFreeCells.Remove(selectedIndex);
        gridCells[selectedIndex].isOccupied = true;

        lastSpawnedZPos = selectedPos.z;
    }

    /// Shield spawn routine.
    private void SpawnShield ()
    {
        int selectedIndex = gridFreeCells[Random.Range(0, gridFreeCells.Count)];

        Vector3 selectedPos = gridCells[selectedIndex].cellPosition;
        SpawnObject(selectedPos, Tags.IT_SHIELD, false);

        // Set the grid cell occupied by the magnet.                
        gridFreeCells.Remove(selectedIndex);
        gridCells[selectedIndex].isOccupied = true;

        lastSpawnedZPos = selectedPos.z;
    }

    /// Duplicator spawn routine.
    private void SpawnDuplicator()
    {
        int selectedIndex = gridFreeCells[Random.Range(0, gridFreeCells.Count)];

        Vector3 selectedPos = gridCells[selectedIndex].cellPosition;
        SpawnObject(selectedPos, Tags.IT_DUPLICATOR, false);

        // Set the grid cell occupied by the magnet.                
        gridFreeCells.Remove(selectedIndex);
        gridCells[selectedIndex].isOccupied = true;

        lastSpawnedZPos = selectedPos.z;
    }

    /// Animal sphere spawn routine.
    private void SpawnAnimalSphere ()
    {
        int selectedIndex = gridFreeCells[Random.Range(0, gridFreeCells.Count)];

        Vector3 selectedPos = gridCells[selectedIndex].cellPosition;
        SpawnObject(selectedPos, Tags.IT_ANIMAL_SPHERE, true);

        // Set the grid cell occupied by the magnet.                
        gridFreeCells.Remove(selectedIndex);
        gridCells[selectedIndex].isOccupied = true;

        lastSpawnedZPos = selectedPos.z;
    }

    /// Car spawn routine.
    private void SpawnCar ()
    {       
        // Roll a chance to spawn two cars aligned.
        int amount = Random.value < parameters.carParameters.doubleSpawnChance ? 2 : 1;
        List<int> availableLines = new List<int>{ 0, 1, 2 };

        // Last spawned coin grid coordinates.
        int lastCoinColumn = 0;
        int lastCoinRow = 0;

        for (int i = 0; i < amount; i++)
        {
            int selectedLineIndex = Random.Range(0, availableLines.Count);
            int nextRowIncrement = 3;   // Index increment, from the last coin, to positioning the cars.

            // Spawn the coins before the first spawned car.
            if (i == 0)
            {
                // The car spawn always starts at the first grid row, so the coins are spawned first.
                SpawnCoinAscending(availableLines[selectedLineIndex], 0, ref lastCoinColumn, ref lastCoinRow);                
            }
            else
            {
                nextRowIncrement += availableLines[selectedLineIndex] - lastCoinColumn;                
            }

            // Spawn the car at the next row from the last spawned coin.
            int actualCarGridIndex = lastCoinColumn + lastCoinRow * SpawnParameters.GRID_COLUMNS + nextRowIncrement;
            Vector3 selectedPos = gridCells[actualCarGridIndex].cellPosition;

            SpawnObject(selectedPos, Tags.OBS_CAR, true);

            // Set the grid cells occupied by the car.
            for (int cell = 0; cell < SpawnParameters.GRID_CAR_CELLS; cell++)
            {
                gridCells[actualCarGridIndex + SpawnParameters.GRID_COLUMNS * cell].isOccupied = true;
                gridFreeCells.Remove(actualCarGridIndex + SpawnParameters.GRID_COLUMNS * cell);

                // Block the spawn on the next cells, behind the car, when it is spawned doubled, to avoid an unreachable object.
                if (amount > 1)
                {
                    gridFreeCells.Remove(actualCarGridIndex + SpawnParameters.GRID_COLUMNS * SpawnParameters.GRID_CAR_CELLS);
                }
            }           

            lastSpawnedZPos = selectedPos.z;
            availableLines.RemoveAt(selectedLineIndex);

            // Set the cells between two cars as 'path', to avoid the spawn of unavoidable obstacles.
            if (amount > 1 && i == 1)
            {
                for (int cell = 0; cell <= SpawnParameters.GRID_CAR_CELLS + 1; cell++)
                {
                    int pathIndex = availableLines[0] + lastCoinRow * SpawnParameters.GRID_COLUMNS + SpawnParameters.GRID_COLUMNS * cell;

                    gridCells[pathIndex].isOccupied = true;
                    gridFreeCells.Remove(pathIndex);
                    gridPathCells.Add(pathIndex);                    
                }
            }
        }
    }

    /// Cone spawn routine.
    private void SpawnCone()
    {
        // Roll a chance to spawn three cones aligned.
        int amount = Random.value < parameters.coneParameters.doubleSpawnChance ? 3 : 1;

        // One cone.
        if (amount == 1)
        {
            // Prioritize the spawn in a path position, to create a challenge.
            int selectedIndex = FindFreePath(); 
            
            // When the path search returns a negative value it means that there is no available path cell to spawn, selected any other cell then.
            if (selectedIndex < 0)            
            {
                selectedIndex = gridFreeCells[Random.Range(0, gridFreeCells.Count)];                
            }

            Vector3 selectedPos = gridCells[selectedIndex].cellPosition;
            SpawnObject(selectedPos, Tags.OBS_CONE, false);

            // Set the grid cell occupied by the cone.                
            gridFreeCells.Remove(selectedIndex);
            gridCells[selectedIndex].isOccupied = true;

            lastSpawnedZPos = selectedPos.z;

            // Spawn the coins.
            int selectedColumn = gridCells[selectedIndex].cellColumn;
            int selectedRow = gridCells[selectedIndex].cellRow;            

            // Start by the row behind the cone (next one), where possible.
            if (selectedRow + 1 < SpawnParameters.GRID_ROWS)
            {
                selectedRow++;
                lastSpawnedZPos = gridCells[selectedColumn + selectedRow * SpawnParameters.GRID_COLUMNS].cellPosition.z; // The coin behind the cone will be the fartest object spawned.
            }

            SpawnCoinDescending(selectedColumn, selectedRow);

            // Spawn one coin above the cone.   
            SpawnSingleCoin(selectedPos, Tags.IT_COIN_UPPER);                                
        }
        // Three cones.
        else
        {
            int spawnRow = FindFreeRow();
            if (spawnRow < 0) return;   // Return if there isn't free space to spawn the cones.            

            Vector3 selectedPos = Vector3.zero;            

            for (int i = 0; i < amount; i++)
            {        
                // Spawn the cones in the selected row.
                int actualObjGridIndex = i + spawnRow * SpawnParameters.GRID_COLUMNS;
                selectedPos = gridCells[actualObjGridIndex].cellPosition;

                SpawnObject(selectedPos, Tags.OBS_CONE, false);                

                // Set the grid cells occupied by the cone.                
                gridFreeCells.Remove(actualObjGridIndex);
                gridCells[actualObjGridIndex].isOccupied = true;

                // Also block the cells behind the cones.
                gridFreeCells.Remove(actualObjGridIndex + SpawnParameters.GRID_COLUMNS);
                gridCells[actualObjGridIndex + SpawnParameters.GRID_COLUMNS].isOccupied = true;

                lastSpawnedZPos = selectedPos.z;                                
            }

            // Spawn one coin above one cone.
            float[] availableLines = new float[] { Globals.LINE01_X_POS, Globals.LINE02_X_POS, Globals.LINE03_X_POS };
            selectedPos.x = availableLines[Random.Range(0, availableLines.Length)];
            SpawnSingleCoin(selectedPos, Tags.IT_COIN_UPPER);
        }
    }

    /// Chameleon spawn routine.
    private void SpawnChameleon ()
    {            
        int selectedIndex = gridFreeCells[Random.Range(0, gridFreeCells.Count)];        

        Vector3 selectedPos = gridCells[selectedIndex].cellPosition;
        SpawnObject(selectedPos, Tags.OBS_CHAMELEON, false);

        // Set the grid cell occupied by the chameleon.                
        gridFreeCells.Remove(selectedIndex);
        gridCells[selectedIndex].isOccupied = true;

        lastSpawnedZPos = selectedPos.z;

        // Spawn the coins.
        if (Random.value > Globals.SP_NO_COINS_CHANCE)
        {
            int selectedColumn = gridCells[selectedIndex].cellColumn;
            int selectedRow = gridCells[selectedIndex].cellRow;

            SpawnCoinDescending(selectedColumn, selectedRow);
        }        
    }

    /// Lizard spawn routine.
    private void SpawnLizard()
    {
        int selectedIndex = gridFreeCells[Random.Range(0, gridFreeCells.Count)];

        Vector3 selectedPos = gridCells[selectedIndex].cellPosition;   
        GameObject spawned = SpawnObject(selectedPos, Tags.OBS_LIZARD);
        spawned.GetComponent<LizardController>().SpawnedLine = gridCells[selectedIndex].cellColumn;  

        // Set the grid cell occupied by the chameleon.                
        gridFreeCells.Remove(selectedIndex);
        gridCells[selectedIndex].isOccupied = true;

        lastSpawnedZPos = selectedPos.z;

        // Spawn the coins.
        if (Random.value > Globals.SP_NO_COINS_CHANCE)
        {
            int selectedColumn = gridCells[selectedIndex].cellColumn;
            int selectedRow = gridCells[selectedIndex].cellRow;

            SpawnCoinDescending(selectedColumn, selectedRow);
        }       
    }

    /// Plank spawn routine.
    private void SpawnPlank()
    {
        // Roll a chance to spawn three planks aligned.
        int amount = Random.value < parameters.plankParameters.doubleSpawnChance ? 3 : 1;

        // One plank.
        if (amount == 1)
        {            
            int selectedIndex = gridFreeCells[Random.Range(0, gridFreeCells.Count)];            

            Vector3 selectedPos = gridCells[selectedIndex].cellPosition;
            SpawnObject(selectedPos, Tags.OBS_PLANK, true);

            // Set the grid cell occupied by the plank.                
            gridFreeCells.Remove(selectedIndex);
            gridCells[selectedIndex].isOccupied = true;

            // Also block the cell behind the cones.
            gridFreeCells.Remove(selectedIndex + SpawnParameters.GRID_COLUMNS);
            gridCells[selectedIndex + SpawnParameters.GRID_COLUMNS].isOccupied = true;

            lastSpawnedZPos = selectedPos.z;

            // Spawn the coins.
            int selectedColumn = gridCells[selectedIndex].cellColumn;
            int selectedRow = gridCells[selectedIndex].cellRow;            

            SpawnCoinDescending(selectedColumn, selectedRow);            
        }
        // Three planks.
        else
        {
            int spawnRow = FindFreeRow();
            if (spawnRow < 0) return;   // Return if there isn't free space to spawn the planks.            

            Vector3 selectedPos = Vector3.zero;
            int avoidableColumn = Random.Range(0, amount);  // Force at least one of the spawned obstacles to be avoidable.

            for (int i = 0; i < amount; i++)
            {
                // Spawn the planks in the selected row.
                int actualObjGridIndex = i + spawnRow * SpawnParameters.GRID_COLUMNS;
                selectedPos = gridCells[actualObjGridIndex].cellPosition;

                if (i != avoidableColumn)
                {
                    SpawnObject(selectedPos, Tags.OBS_PLANK, true);
                }
                else
                {
                    GameObject spawnedPlank = SpawnObject(selectedPos, Tags.OBS_PLANK);
                    spawnedPlank.GetComponent<ChildSelector>().SelectChild(0);  // The avoidable version of obstacles are always at the first index.                    

                    if (gridCells[actualObjGridIndex].cellRow + 1 < SpawnParameters.GRID_ROWS)
                    {
                        // Spawn one coin behind the selected avoidable plank.                    
                        selectedPos = gridCells[actualObjGridIndex + SpawnParameters.GRID_COLUMNS].cellPosition;
                        SpawnSingleCoin(selectedPos, Tags.IT_COIN);                    
                    }

                    lastSpawnedZPos = selectedPos.z;
                }

                // Set the grid cells occupied by the planks.                
                gridFreeCells.Remove(actualObjGridIndex);
                gridCells[actualObjGridIndex].isOccupied = true;

                // Also block the cells behind the cones.
                gridFreeCells.Remove(actualObjGridIndex + SpawnParameters.GRID_COLUMNS);
                gridCells[actualObjGridIndex + SpawnParameters.GRID_COLUMNS].isOccupied = true;
            }            
        }
    }
    #endregion

    /// Counts the objects spawn time.
    private IEnumerator SpawnLoop ()
    {
        while (true)
        {
            isGridReseted = false;
            IncrementSpawnTimers();
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Stops the spawn routine.
    /// </summary>
    public void StopSpawn ()
    {
        StopAllCoroutines();
        enabled = false;
    }
}
