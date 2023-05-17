using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayfabLogIn : MonoBehaviour
{
    [SerializeField] private GameObject logInPanel;

    [SerializeField] private GameObject registerPanel;

    [SerializeField] private TextMeshProUGUI errorMessage;

    public string userEmail;

    public string password;

    #region Unity Methods
    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "FD124";
        }
    }
    #endregion
    #region Private Methods
    private bool IsValidUsername()
    {
        bool isValid = false;

        if (userEmail.Length >= 3 && userEmail.Length <= 24)
            isValid = true;

        return isValid;
    }
    private void LoginWithCustomId()
    {
        Debug.Log($"Login to Playfab as {userEmail}");
        var request = new LoginWithCustomIDRequest { CustomId = userEmail, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSuccess, OnFailure);
    }

    public void LoginWithPassword(string usernameOrEmail, string password)
    {
        LoginWithEmailAddressRequest request = new LoginWithEmailAddressRequest
        {
            Email = usernameOrEmail,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginEmailSuccess, OnLoginEmailFailure);
    }

    private void UpdateDisplayName(string displayname)
    {
        Debug.Log($"Updating Playfab account's Display name to: {displayname}");
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayname };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameSuccess, OnFailure);
    }
    #endregion


    #region  Public Methods
    public void SetEmail(string _email)
    {
        userEmail = _email;
    }

    public void SetPassword(string _password)
    {
        password = _password;
    }


    public void Login()
    {
        if (!IsValidUsername()) return;

        LoginWithPassword(userEmail, password);
    }

    public void ToRegisterPanel()
    {
        logInPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    #endregion
    #region Playfab Callbacks

    //login email
    private void OnLoginEmailSuccess(LoginResult result)
    {
        Debug.Log("You have logged into Playfab");

        SceneManager.LoadScene((int)SceneIndex.Splash);
    }

    private void OnLoginEmailFailure(PlayFabError error)
    {
        Debug.Log($"There was an issue with your request: {error.GenerateErrorReport()}");
        errorMessage.text = error.GenerateErrorReport();
        errorMessage.gameObject.SetActive(true);
    }

    //log in custom id
    private void OnLoginCustomIdSuccess(LoginResult result)
    {
        Debug.Log($"You have logged into Playfab using custom id {userEmail}");
        UpdateDisplayName(userEmail);
    }
    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"You have updated the displayname of the playfab account!");
    }
    private void OnFailure(PlayFabError error)
    {
        Debug.Log($"There was an issue with your request: {error.GenerateErrorReport()}");
    }
    #endregion
}
