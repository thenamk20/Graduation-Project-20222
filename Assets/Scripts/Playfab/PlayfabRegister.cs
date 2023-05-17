using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayfabRegister : MonoBehaviour
{
    [SerializeField]
    private GameObject loginPanel;

    [SerializeField]
    private GameObject registerPanel;

    
    public string userName;


    public string password;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsValidUsername()
    {
        bool isValid = false;

        if (userName.Length >= 3 && userName.Length <= 24)
            isValid = true;

        return isValid;
    }

    public void SetRegisterUserName(string _username)
    {
        userName = _username;
    }

    public void SetRegisterPassword(string _password)
    {
        password = _password;
    }

    public void RegisterAccount()
    {
        if (!IsValidUsername()) return;

        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
        {
            Email = userName,
            Password = password,
            RequireBothUsernameAndEmail = false // Set to true if you want to require both username and email
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Account registered successfully!, user: " + result.Username);
        // Proceed with further logic or actions after successful registration
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError("Failed to register account: " + error.ErrorMessage);
        // Handle or display the registration failure error message
    }

    public void ToLogInPanel()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }
}
