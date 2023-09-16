using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthenticationManager : MonoBehaviour
{

    private Button loginButton;
    private Button signUpButton;

    private Coroutine loginCoroutine;
    private Coroutine signOutCoroutine;
    private Coroutine getUsernameCoroutine;
    DatabaseReference database;
    private string username;

    private void Awake()
    {
        loginButton = GameObject.Find("Button_Login").GetComponent<Button>();
        signUpButton = GameObject.Find("Button_SignUp").GetComponent<Button>();
    }

    void Start()
    {

        loginButton.onClick.AddListener(HandleLoginButtonClicked);
        signUpButton.onClick.AddListener(HandleRegisterButtonClicked);
        database = FirebaseDatabase.DefaultInstance.RootReference;
    }
    private void HandleLoginButtonClicked()
    {
        string email = GameObject.Find("InputField_Email").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputField_Password").GetComponent<TMP_InputField>().text;
        var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;

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

            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + registerTask.Exception);
        }
        else
        {
            AuthResult result = registerTask.Result;

            PlayerPrefs.SetString("UserID", result.User.UserId);
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

            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + registerTask.Exception);
        }
        else
        {
            Firebase.Auth.AuthResult result = registerTask.Result;
            Debug.LogFormat(
                "Firebase user created successfully: {0} ({1}:{2})",
                username,
                result.User.Email,
                result.User.UserId);

            PlayerPrefs.SetString("UserID", result.User.UserId);
            SceneManagement.Instance.ChangeScene((int)AppScene.REGISTER);
            database.Child("users").Child(result.User.UserId).Child("username").SetValueAsync(username);
        }
    }
}