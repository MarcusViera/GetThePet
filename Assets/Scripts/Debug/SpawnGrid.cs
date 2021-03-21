using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \class SpawnGrid
 *  \brief Draws the lines of the spawn grid.
 */
public class SpawnGrid : MonoBehaviour {

    private void OnDrawGizmos()
    {
        float totalHeight = transform.position.z + SpawnParameters.CELL_HEIGHT * SpawnParameters.GRID_ROWS;
        float totalWidth = transform.position.x + SpawnParameters.CELL_WIDTH * SpawnParameters.GRID_COLUMNS;

        // Draw columns.
        for (int i = 0; i < SpawnParameters.GRID_COLUMNS + 1; i++)
        {
            float x = transform.position.x + i * SpawnParameters.CELL_WIDTH;            

            Gizmos.DrawLine(new Vector3(x, transform.position.y, transform.position.z), new Vector3(x, transform.position.y, totalHeight));            
        }

        // Draw rows.
        for (int i = 0; i < SpawnParameters.GRID_ROWS + 1; i++)
        {
            float z = transform.position.z + i * SpawnParameters.CELL_HEIGHT;

            Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y, z), new Vector3(totalWidth, transform.position.y, z));
        }
    }
}
