using Unity.VisualScripting;
using UnityEngine;

public class TilePlacer : MonoBehaviour
{
    public Environment2D testEnvironment = new Environment2D { height = 3, width = 5};
    public EditWorld editWorld;
    public GameObject tile;
    public Camera cameraToMove;

    public void PlaceTiles()
    {
        cameraToMove.transform.position = new Vector3(editWorld.currentEnvironment.width / 2 + (float)0.5, editWorld.currentEnvironment.height / 2 + (float)0.5, -10);
        cameraToMove.orthographicSize = editWorld.currentEnvironment.height * 16/9 > editWorld.currentEnvironment.width ? (editWorld.currentEnvironment.height / 2) + (float)0.5 : (editWorld.currentEnvironment.width / 2) + (float)0.5;

        for (int row = 1; row <= editWorld.currentEnvironment.height; row++)
        {
            for(int col = 1; col <= editWorld.currentEnvironment.width; col++)
            {
                Instantiate(tile, new Vector3(col * 1, row * 1, 0), Quaternion.identity);
            }
        }
    }
}
