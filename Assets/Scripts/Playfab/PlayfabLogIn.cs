using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayFabLogIn : MonoBehaviour
{
    [SerializeField] private GameObject logInPanel;

    [SerializeField] private GameObject registerPanel;

    [SerializeField] private TextMeshProUGUI errorMessage;

    [SerializeField] private GameObject rememberMeCheckMark;

    [SerializeField] private TMP_InputField emailText;
    [SerializeField] private TMP_InputField passwordText;

    [SerializeField] private GameObject waitingPanel;

    [SerializeField] private AudioSource soundButton;

    public string userEmail;

    public string password;

    #region Unity Methods
    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "FD124";
        }

        waitingPanel.SetActive(false);
        CheckRememberMe();
    }

    void CheckRememberMe()
    {
        rememberMeCheckMark.SetActive(PreloadData.Instance.gameData.user.isRememberMe);

        emailText.text = PreloadData.Instance.gameData.user.cachedEmail;
        passwordText.text = PreloadData.Instance.gameData.user.cachedPassword;

        userEmail = PreloadData.Instance.gameData.user.cachedEmail;
        password = PreloadData.Instance.gameData.user.cachedPassword;
    }

    #endregion
    #region Private Methods
    private bool IsValidUsername()
    {
        bool isValid = false;

        if (userEmail == null || password == null) return false;

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
            Password = password,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
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
        CheckHideError();
    }

    public void SetPassword(string _password)
    {
        password = _password;
        CheckHideError();
    }

    public void Login()
    {
        soundButton.Play();
        if (!IsValidUsername()) return;
        waitingPanel.SetActive(true);

        LoginWithPassword(userEmail, password);

        if (PreloadData.Instance.gameData.user.isRememberMe)
        {
            PreloadData.Instance.gameData.user.cachedEmail = userEmail;
            PreloadData.Instance.gameData.user.cachedPassword = password;
        }
    }

    public void ToRegisterPanel()
    {
        logInPanel.SetActive(false);
        registerPanel.SetActive(true);
        errorMessage.gameObject.SetActive(false);
    }

    public void UpdateRemember()
    {
        PreloadData.Instance.gameData.user.isRememberMe = !PreloadData.Instance.gameData.user.isRememberMe;
        rememberMeCheckMark.SetActive(PreloadData.Instance.gameData.user.isRememberMe);
    }

    void CheckHideError()
    {
        if (errorMessage.gameObject.activeInHierarchy)
        {
            errorMessage.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Playfab Callbacks

    //login email
    private void OnLoginEmailSuccess(LoginResult result)
    {
        waitingPanel.SetActive(false);
        Debug.Log("You have logged into Playfab");
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            PreloadData.Instance.gameData.user.name = name;
            HCDebug.Log("name:"+ name);
        }

        SceneManager.LoadScene((int)SceneIndex.Splash);
      
    }

    private void OnLoginEmailFailure(PlayFabError error)
    {
        //AudioAssistant.Shot(TypeSound.ClickError);
        waitingPanel.SetActive(false);
        Debug.Log($"There was an issue with your request: {error.GenerateErrorReport()}");
        errorMessage.text = "Login failed, make sure your email and password are correct!";
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
