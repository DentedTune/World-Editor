using System;
using System.Collections.Generic;
using UnityEngine;

public class EditWorld : MonoBehaviour
{
    public Environment2D currentEnvironment;

    public Object2DApiClient object2DApiClient;
    public LocalRoomState localRoomState;

    public TilePlacer tilePlacer;

    public async void GetMyFurniture()
    {
        IWebRequestReponse webRequestResponse = await object2DApiClient.ReadObject2Ds(currentEnvironment.id);

        switch (webRequestResponse)
        {
            case WebRequestData<List<Object2D>> dataResponse:
                List<Object2D> object2Ds = dataResponse.Data;
                Debug.Log("List of furniture: ");
                object2Ds.ForEach(object2D => Debug.Log(object2D.id));

                localRoomState.LastSavedState = object2Ds;

                // TODO: Handle succes scenario.
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Read furniture error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }

        tilePlacer.PlaceTiles();
    }
}
