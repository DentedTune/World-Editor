using UnityEngine;

public class SaveChanges : MonoBehaviour
{
    public LocalRoomState state;

    public EditWorld thisWorld;

    public Object2DApiClient furnitureClient;
    public Environment2DApiClient worldClient;

    public async void SaveTheseChanges()
    {
        for (int i = 0; i < state.ListOfCurrentEdits.Count; i++)
        {
            if (state.ListOfCurrentEdits[i].Place)
            {
                Object2D newFurniture = state.ListOfCurrentEdits[i].Furniture;
                newFurniture.worldId = thisWorld.currentEnvironment.id;

                await furnitureClient.CreateObject2D(newFurniture);

                state.LastSavedState.Add(newFurniture);
            }
            else
            {
                await furnitureClient.DeleteObject2D(state.ListOfCurrentEdits[i].Furniture.id);
                for (int j = 0; j < state.LastSavedState.Count; j++)
                {
                    if (state.LastSavedState[j].positionX == state.ListOfCurrentEdits[i].Furniture.positionX && state.LastSavedState[j].positionY == state.ListOfCurrentEdits[i].Furniture.positionY)
                    {
                        state.LastSavedState.RemoveAt(j);
                        break;
                    }
                }
            }
        }
        state.ListOfCurrentEdits.Clear();

        Debug.Log("Finished saving!");
    }

    public async void DeleteWorld()
    {
        await worldClient.DeleteEnvironment(thisWorld.currentEnvironment.id);
    }
}
