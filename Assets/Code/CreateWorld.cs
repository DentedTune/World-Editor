using System;
using TMPro;
using UnityEngine;

public class CreateWorld : MonoBehaviour
{
    public Environment2DApiClient environment2DApiClient;
    public Environment2D environment2D;
    public EnvironmentStuff environmentStuff;

    public GameObject worldCreate;
    public GameObject worldSelect;

    public TMP_InputField worldNameInput;
    public TMP_InputField widthInput;
    public TMP_InputField lengthInput;

    [ContextMenu("Environment2D/Create")]
    public async void CreateEnvironment2D()
    {
        IWebRequestReponse webRequestResponse = await environment2DApiClient.CreateEnvironment(new Environment2D {id = "813b4762-a0eb-4302-a577-d451592190c1", userId = "813b4762-a0eb-4302-a577-d451592190c1", name = worldNameInput.text, maxHeight = int.Parse(lengthInput.text), maxLength = int.Parse(widthInput.text)});

        switch (webRequestResponse)
        {
            case WebRequestData<Environment2D> dataResponse:
                //environment2D.id = dataResponse.Data.id;
                // TODO: Handle succes scenario.

                environmentStuff.GetMyEnvironments();

                worldCreate.SetActive(false);
                worldSelect.SetActive(true);

                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Create environment2D error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }
}
