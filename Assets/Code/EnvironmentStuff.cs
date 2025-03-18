using System;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentStuff : MonoBehaviour
{
    public Environment2DApiClient enviroment2DApiClient;
    public WorldButtons worldButtons;

    public List<Environment2D> UsersEnvironments = new List<Environment2D>();

    public async void GetMyEnvironments()
    {
        IWebRequestReponse webRequestResponse = await enviroment2DApiClient.ReadEnvironment2Ds();

        switch (webRequestResponse)
        {
            case WebRequestData<List<Environment2D>> dataResponse:
                List<Environment2D> environment2Ds = dataResponse.Data;
                Debug.Log("List of environment2Ds: ");
                environment2Ds.ForEach(environment2D => Debug.Log(environment2D.id));
                
                UsersEnvironments = environment2Ds;

                worldButtons.RevealButtons();
                // TODO: Handle succes scenario.
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Read environment2Ds error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }
}
