using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class LocalRoomState : MonoBehaviour
{
    public List<Edit> ListOfCurrentEdits = new List<Edit>();

    public List<Object2D> LastSavedState = new List<Object2D>();

    public void SetDeleteEdit(int xPosition, int yPosition)
    {
        for (int j = 0; j < LastSavedState.Count; j++)
        {
            if (LastSavedState[j].positionX == xPosition && LastSavedState[j].positionY == yPosition)
            {
                for (int i = 0; i < ListOfCurrentEdits.Count; i++)
                {
                    if (ListOfCurrentEdits[i].Furniture.positionX == xPosition && ListOfCurrentEdits[i].Furniture.positionY == yPosition)
                    {
                        ListOfCurrentEdits[i].Place = false;
                    }
                }
                ListOfCurrentEdits.Add(new Edit { Place = false, Furniture = new Object2D { positionX = xPosition, positionY = yPosition } });
                ListStatusInConsole();
                return;
            }
        }
        for (int i = 0; i < ListOfCurrentEdits.Count; i++)
        {
            if (ListOfCurrentEdits[i].Furniture.positionX == xPosition && ListOfCurrentEdits[i].Furniture.positionY == yPosition)
            {
                ListOfCurrentEdits.Remove(ListOfCurrentEdits[i]);
            }
        }
        ListStatusInConsole();
    }

    public void SetPlaceEdit(Object2D newFurniture)
    {
        for (int j = 0; j < LastSavedState.Count; j++)
        {
            if (SamePosition(newFurniture, LastSavedState[j]))
            {
                if (FurnitureLooksTheSame(newFurniture, LastSavedState[j]))
                {
                    int counter = 0;

                    for (int i = 0; i < ListOfCurrentEdits.Count; i++)
                    {

                        if (SamePosition(ListOfCurrentEdits[i].Furniture, newFurniture))
                        {
                            ListOfCurrentEdits.Remove(ListOfCurrentEdits[i]);
                            counter++;
                        }

                    }
                    Debug.Log($"Furniture now matches the most recent save again. Removed {counter} edits that would have affected it.");
                    ListStatusInConsole();
                    return;
                }
            }
        }
        for (int i = 0; i < ListOfCurrentEdits.Count; i++)
        {
            if (SamePosition(ListOfCurrentEdits[i].Furniture, newFurniture))
            {
                if (!FurnitureLooksTheSame(ListOfCurrentEdits[i].Furniture, newFurniture))
                {
                    ListOfCurrentEdits[i].Furniture = newFurniture;
                }

                ListOfCurrentEdits[i].Place = true;
                Debug.Log($"Existing edit altered.");
                ListStatusInConsole();
                return;
            }
        }
        ListOfCurrentEdits.Add(new Edit { Place = true, Furniture = newFurniture });
        Debug.Log($"Added a new edit.");
        ListStatusInConsole();
    }

    public bool SamePosition(Object2D furniture1, Object2D furniture2)
    {
        return furniture1.positionX == furniture2.positionX &&
                furniture1.positionY == furniture2.positionY;
    }

    public bool FurnitureLooksTheSame(Object2D furniture1, Object2D furniture2)
    {
        // Check if everything matches, except ID and Room ID
        return furniture1.objectType == furniture2.objectType &&
                furniture1.width == furniture2.width &&
                furniture1.length == furniture2.length &&
                furniture1.direction == furniture2.direction &&
                furniture1.rotationZ == furniture2.rotationZ;
    }

    public void ListStatusInConsole()
    {
        string changes = "";

        for (int i = 0; i < ListOfCurrentEdits.Count; i++)
        {
            if (ListOfCurrentEdits[i].Place)
            {
                changes += $"Place a {ListOfCurrentEdits[i].Furniture.objectType} at ({ListOfCurrentEdits[i].Furniture.positionX},{ListOfCurrentEdits[i].Furniture.positionY})\n";
            }
            else
            {
                changes += $"Remove the object at ({ListOfCurrentEdits[i].Furniture.positionX},{ListOfCurrentEdits[i].Furniture.positionY})\n";
            }
        }

        Debug.Log($"The following changes will be made if you save now:\n {changes}");
    }
}
