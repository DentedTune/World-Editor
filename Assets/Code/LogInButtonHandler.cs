using System;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class LogInButtonHandler : MonoBehaviour
{
    public TMP_InputField emailField;
    [SerializeField] TextMeshProUGUI emailText;

    public TMP_InputField passwordField;
    [SerializeField] TextMeshProUGUI passwordText;

    public GameObject loginScreen;
    public GameObject worldSelectScreen;
    public GameObject errorMessageObject;
    public TMP_Text errorMessageText;

    public string emailTest = "rubyvergg@haha.com";
    public string passwordTest = "Password00@";

    [Header("Dependencies")]
    public UserApiClient userApiClient;
    public Environment2DApiClient enviroment2DApiClient;
    public Object2DApiClient object2DApiClient;
    public EnvironmentStuff environmentStuff;

    #region Login

    [ContextMenu("User/Register")]
    public async void Register()
    {
        IWebRequestReponse webRequestResponse = await userApiClient.Register(new User {email = emailField.text, password = passwordField.text});

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Register succes!");

                errorMessageText.SetText("Naam en Wachtwoord Geregistreerd");
                errorMessageObject.SetActive(true);

                // TODO: Handle succes scenario;
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Register error: " + errorMessage);

                errorMessageText.SetText("Fout: Registreren Niet Gelukt");
                errorMessageObject.SetActive(true);

                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    [ContextMenu("User/Login")]

    public async void Login()
    {
        IWebRequestReponse webRequestResponse = await userApiClient.Login(new User { email = emailField.text, password = passwordField.text});

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Login succes!");

                loginScreen.SetActive(false);
                worldSelectScreen.SetActive(true);

                environmentStuff.GetMyEnvironments();
                //worldButtons.RevealButtons();

                // TODO: Todo handle succes scenario.
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Login error: " + errorMessage);

                errorMessageText.SetText("Fout: Inloggen Niet Gelukt");
                errorMessageObject.SetActive(true);

                    // TODO: Handle error scenario. Show the errormessage to the user.
                    break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    #endregion
}
