using UnityEngine;

public class PlacingSettings : MonoBehaviour
{
    public Object2D furnitureLook;


    void Start()
    {
        furnitureLook.objectType = "chair";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TurnCounterlockwise();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TurnClockwise();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ScrollUpThroughFurniture();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ScrollDownThroughFurniture();
        }
    }

    private void TurnClockwise()
    {
        Debug.Log("Turning Clockwise!");

        if (furnitureLook.direction < 3)
        {
            furnitureLook.direction++;
        }
        else
        {
            furnitureLook.direction = 0;
        }
    }
    private void TurnCounterlockwise()
    {
        Debug.Log("Turning Counterclockwise!");

        if (furnitureLook.direction > 0 && furnitureLook.direction < 4)
        {
            furnitureLook.direction--;
        }
        else
        {
            furnitureLook.direction = 3;
        }
    }

    private void ScrollUpThroughFurniture()
    {
        switch (furnitureLook.objectType)
        {
            case "chair":
                furnitureLook.objectType = "table";
                break;
            case "table":
                furnitureLook.objectType = "vase";
                break;
            case "vase":
                furnitureLook.objectType = "chair";
                break;
            default:
                furnitureLook.objectType = "chair";
                break;
        }
    }
    private void ScrollDownThroughFurniture()
    {
        switch (furnitureLook.objectType)
        {
            case "chair":
                furnitureLook.objectType = "vase";
                break;
            case "table":
                furnitureLook.objectType = "chair";
                break;
            case "vase":
                furnitureLook.objectType = "table";
                break;
            default:
                furnitureLook.objectType = "chair";
                break;
        }
    }
}
