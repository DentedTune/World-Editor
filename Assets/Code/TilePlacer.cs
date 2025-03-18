using Unity.VisualScripting;
using UnityEngine;

public class TilePlacer : MonoBehaviour
{
    public Environment2D testEnvironment = new Environment2D { maxHeight = 3, maxLength = 5};
    public EditWorld editWorld;
    public GameObject tile;
    public Camera cameraToMove;

    public void PlaceTiles()
    {
        cameraToMove.transform.position = new Vector3(editWorld.currentEnvironment.maxLength / 2 + (float)0.5, editWorld.currentEnvironment.maxHeight / 2 + (float)0.5, -10);
        cameraToMove.orthographicSize = editWorld.currentEnvironment.maxHeight * 16/9 > editWorld.currentEnvironment.maxLength ? (editWorld.currentEnvironment.maxHeight / 2) + (float)0.5 : (editWorld.currentEnvironment.maxLength / 2) + (float)0.5;

        for (int row = 1; row <= editWorld.currentEnvironment.maxHeight; row++)
        {
            for(int col = 1; col <= editWorld.currentEnvironment.maxLength; col++)
            {
                Instantiate(tile, new Vector3(col * 1, row * 1, 0), Quaternion.identity);
            }
        }
    }
}
