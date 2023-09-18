using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class AuthenticationManager : MonoBehaviour
{
    [SerializeField] private GameObject[] signUpErrorList;
    [SerializeField] private GameObject[] loginErrorList;

    private Button loginButton;
    private Button signUpButton;
    private Button returnButton;

    private Coroutine loginCoroutine;
    private Coroutine signOutCoroutine;
    private Coroutine getUsernameCoroutine;
    DatabaseReference database;
    private string username;

    private void Awake()
    {
        DeactivateErrors();

        loginButton = GameObject.Find("Button_Login").GetComponent<Button>();
        signUpButton = GameObject.Find("Button_SignUp").GetComponent<Button>();
        returnButton = GameObject.Find("Button_Return").GetComponent<Button>();
        signUpButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
    }

    void Start()
    {
        loginButton.onClick.AddListener(HandleLoginButtonClicked);
        signUpButton.onClick.AddListener(HandleRegisterButtonClicked);
        database = FirebaseDatabase.DefaultInstance.RootReference;
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            SceneManager.LoadScene((int)AppScene.HOME);
    }

    private void DeactivateErrors()
    {
        foreach (var item in loginErrorList){
            item.gameObject.SetActive(false);
        }
        foreach (var item in signUpErrorList){
            item.gameObject.SetActive(false);
        }
    }

    private void HandleLoginButtonClicked()
    {
        string email = GameObject.Find("InputField_Email").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputField_Password").GetComponent<TMP_InputField>().text;

        loginCoroutine = StartCoroutine(LoginUser(email, password));
    }

    private void GetUsername(FirebaseAuth auth)
    {
        FirebaseDatabase.DefaultInstance
        .GetReference("users/" + auth.CurrentUser.UserId + "/username")
        .GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
                Debug.Log(task.Exception);
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.LogFormat(
                "User signed in successfully: {0} ({1} : {2})",
                snapshot.Value,
                auth.CurrentUser.Email,
                auth.CurrentUser.UserId);
            }
        });
    }
    
    private IEnumerator LoginUser(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => registerTask.IsCompleted);
        if (registerTask.IsCanceled)
            Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
        else if (registerTask.IsFaulted)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + registerTask.Exception.Message);

            if (registerTask.Exception.Message == "One or more errors occurred. (The email address is badly formatted.)")
            {
                DeactivateErrors();
                loginErrorList[1].gameObject.SetActive(true);
            }
            else if (registerTask.Exception.Message == "One or more errors occurred. (An email address must be provided.)")
            {
                DeactivateErrors();
                loginErrorList[2].gameObject.SetActive(true);
            }
            else if (registerTask.Exception.Message == "One or more errors occurred. (A password must be provided.)")
            {
                DeactivateErrors();
                loginErrorList[3].gameObject.SetActive(true);
            }
            else
            {
                DeactivateErrors();
                loginErrorList[0].gameObject.SetActive(true);
            }
        }
        else
        {
            AuthResult result = registerTask.Result;

            GetUsername(auth);
            SceneManagement.Instance.ChangeScene((int)AppScene.HOME);
        }

    }

    private void HandleRegisterButtonClicked()
    {
        string email = GameObject.Find("InputField_Email").GetComponent<TMP_InputField>().text;
        string username = GameObject.Find("InputField_Username").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputField_Password").GetComponent<TMP_InputField>().text;

        signOutCoroutine = StartCoroutine(RegisterUser(email, username, password));
    }

    private IEnumerator RegisterUser(string email, string username, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.IsCanceled)
            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
        else if (registerTask.IsFaulted)
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + registerTask.Exception.Message);

            if (registerTask.Exception.Message == "One or more errors occurred. (The email address is already in use by another account.)")
            {
                DeactivateErrors();
                signUpErrorList[1].gameObject.SetActive(true);
            }
            else if (registerTask.Exception.Message == "One or more errors occurred. (The email address is badly formatted.)")
            {
                DeactivateErrors();
                loginErrorList[2].gameObject.SetActive(true);
            }
            else if (registerTask.Exception.Message == "One or more errors occurred. (An email address must be provided.)")
            {
                DeactivateErrors();
                signUpErrorList[3].gameObject.SetActive(true);
            }
            else if (registerTask.Exception.Message == "One or more errors occurred. (The given password is invalid.)")
            {
                DeactivateErrors();
                signUpErrorList[4].gameObject.SetActive(true);
            }
            else if (registerTask.Exception.Message == "One or more errors occurred. (A password must be provided.)")
            {
                DeactivateErrors();
                signUpErrorList[5].gameObject.SetActive(true);
            }
        }
        else
        {
            AuthResult result = registerTask.Result;

            if (ValidUsername(username))
            {
                Debug.LogFormat(
                        "Firebase user created successfully: {0} ({1} : {2})",
                        username,
                        result.User.Email,
                        result.User.UserId);

                PlayerPrefs.SetString("UserID", result.User.UserId);
                database.Child("users").Child(result.User.UserId).Child("username").SetValueAsync(username);
                database.Child("users").Child(result.User.UserId).Child("level").SetValueAsync(1);
                database.Child("users").Child(result.User.UserId).Child("coins").SetValueAsync(0);

                SceneManagement.Instance.ChangeScene((int)AppScene.REGISTER);
            }
            else
            {
                FirebaseUser user = auth.CurrentUser;
                user.DeleteAsync();
                if (registerTask.IsCanceled)
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                else if (registerTask.IsFaulted)
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + registerTask.Exception.Message);
                else
                    Debug.Log("User deleted successfully.");
            }
        }
    }

    private bool ValidUsername(string username)
    {
        this.username = username;
        if (String.IsNullOrEmpty(username))
        {
            DeactivateErrors();
            signUpErrorList[6].gameObject.SetActive(true);
            return false;
        }
        if (String.IsNullOrWhiteSpace(username))
        {
            DeactivateErrors();
            signUpErrorList[6].gameObject.SetActive(true);
            return false;
        }
        else 
            return true;
    }
}