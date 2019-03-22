using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class UserLogin : MonoBehaviour
{
    private FirebaseAuth auth;
    public InputField Email, Password;
    public Button SignUp, LoginButton;
    public Text ErrorText;
    private string email;
    private string password;
    private FirebaseUser user;
    private ThreadDispatcher MyDispatcher;
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    void Awake()
    {
        // Create the ThreadDispatcher on a call that is guaranteed to run on the main Unity thread.
        MyDispatcher = new ThreadDispatcher();
    }

    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("initialize");
                auth = FirebaseAuth.DefaultInstance;
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        MyDispatcher.PollJobs();
    }
    public TResult RunOnMainThread<TResult>(System.Func<TResult> f)
    {
        return MyDispatcher.Run(f);
    }

    private void UpdateErrorMessage(string message)
    {
        ErrorText.text = message;
        Invoke("ClearErrorMessage", 3);
    }

    void ClearErrorMessage()
    {
        ErrorText.text = "";
    }

    void HandleSignInWithUser(Task<Firebase.Auth.FirebaseUser> task) {
        if (task.IsCanceled)
        {
            Debug.Log("SignInWithEmailAndPasswordAsync canceled.");
            return;
        }
        if (task.IsFaulted)
        {
            Debug.Log("SignInWithEmailAndPasswordAsync error: " + task.Exception);
            if (task.Exception.InnerExceptions.Count > 0)
                UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
            Password.text = "";

            return;
        }


        FirebaseUser user = task.Result;
        Debug.LogFormat("User signed in successfully: {0} ({1})",
            user.DisplayName, user.UserId);

        PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");
        PlayerPrefs.SetString("Uid", user != null ? user.UserId : "Unknown");
        SceneManager.LoadScene("StartMenu");
    }
    public void Login( )
    {

        email = Email.text;
        password = Password.text;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith((task) =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("SignInWithEmailAndPasswordAsync canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("SignInWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
                Password.text = "";

                return;
            }


            FirebaseUser user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);
            RunOnMainThread(() =>
            {
                PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");
                PlayerPrefs.SetString("Uid", user != null ? user.UserId : "Unknown");
                SceneManager.LoadScene("StartMenu");
                return 0;
            });
        });
    }
}
