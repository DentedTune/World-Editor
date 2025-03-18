using Unity.VisualScripting;
using UnityEngine;

public class HoverOverTile : MonoBehaviour
{
    public GameObject chair;
    public GameObject tile;

    private void OnMouseEnter()
    {
        Instantiate(chair, tile.transform.position, Quaternion.identity);
    }
}
