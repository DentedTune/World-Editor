using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CurrentEdits
{
    public List<Edit> ListOfCurrentEdits;

    public void SetDeleteEdit(int xPosition, int yPosition)
    {
        foreach (Edit edit in ListOfCurrentEdits)
        {
            if (edit.Furniture.positionX == xPosition && edit.Furniture.positionY == yPosition)
            {
                if (!edit.Place)
                {
                    return;
                }
                else
                {
                    ListOfCurrentEdits.Remove(edit);
                    return;
                }
            }
        }
        ListOfCurrentEdits.Add(new Edit { Place = false });
    }

    public void SetPlaceEdit(int xPosition, int yPosition)
    {
        if (true)
        {

        }
    }
}
