using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class PlayFabRegister : MonoBehaviour
{
    [SerializeField]
    private GameObject loginPanel;

    [SerializeField]
    private GameObject registerPanel;

    [SerializeField]
    private TextMeshProUGUI errorMessage;

    [SerializeField]
    private GameObject waitingPnl;

    [SerializeField]
    private GameObject createSucceedPnl;

    public string userName;

    public string password;

    public string confirmPassword;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool IsValidEmail()
    {
        bool isValid = false;

        if (userName.Length >= 6 && userName.Length <= 24 && userName.Contains("@"))
            isValid = true;

        return isValid;
    }

    public void SetRegisterUserName(string _username)
    {
        userName = _username;
        CheckHideError();
    }

    public void SetRegisterPassword(string _password)
    {
        password = _password;
        CheckHideError();
    }

    public void SetConfirmPassword(string _confirmPW)
    {
        confirmPassword = _confirmPW;
        CheckHideError();
    }

    public void RegisterAccount()
    {
        if (!IsValidEmail())
        {
            errorMessage.text = "Email invalid";
            errorMessage.gameObject.SetActive(true);
            return;
        }

        if(password == "")
        {
            errorMessage.text = "Missing password";
            errorMessage.gameObject.SetActive(true);
            return;
        }

        if(password != confirmPassword)
        {
            errorMessage.text = "Password doesn't match";
            errorMessage.gameObject.SetActive(true);
            return;
        }

        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
        {
            Email = userName,
            Password = password,
            RequireBothUsernameAndEmail = false // Set to true if you want to require both username and email
        };

        waitingPnl.SetActive(true);
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        HCDebug.Log("Account registered successfully!, user: " + result.Username, HcColor.Green);
        waitingPnl.SetActive(false);
        createSucceedPnl.SetActive(true);
        StartCoroutine(DelayToLoginPanel());
        // Proceed with further logic or actions after successful registration
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        HCDebug.Log("Failed to register account: " + error.ErrorMessage, HcColor.Red);
        waitingPnl.SetActive(false);
        errorMessage.text = "Email is not valid or available";
        errorMessage.gameObject.SetActive(true);
        // Handle or display the registration failure error message
    }

    public void ToLogInPanel()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        Reset();
    }

    IEnumerator DelayToLoginPanel()
    {
        yield return new WaitForSeconds(2f);
        ToLogInPanel();
    }

    void CheckHideError()
    {
        if (errorMessage.gameObject.activeInHierarchy)
        {
            errorMessage.gameObject.SetActive(false);
        }
    }

    private void Reset()
    {
        waitingPnl.SetActive(false);
        createSucceedPnl.SetActive(false);
        errorMessage.gameObject.SetActive(false);
    }
}
