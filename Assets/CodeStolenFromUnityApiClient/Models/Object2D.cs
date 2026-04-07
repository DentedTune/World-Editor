using System;
using UnityEngine;

/**
 * Bijzonderheden wegens beperkingen van JsonUtility:
 * - Models hebben variabelen met kleine letters omdat JsonUtility anders de velden uit de JSON niet correct overzet naar het C# object.
 * - De id is een string in plaats van een Guid omdat JsonUtility Guid niet ondersteunt. Gelukkig geeft dit geen probleem indien we gewoon een string gebruiken in Unity en een Guid in onze backend API.
*/
[Serializable]
public class Object2D
{
    public string id = "";

    public string environmentId = "";

    public string objectType = "chair";

    public int positionX = 1;

    public int positionY = 1;

    public int width = 1;

    public int length = 1;

    public int direction = 0;

    public float rotationZ = 0;
}