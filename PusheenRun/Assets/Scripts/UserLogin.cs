using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserLogin : MonoBehaviour
{
    private FirebaseAuth auth;
    public InputField Email, Password;
    public Button SignUp, LoginButton;
    public Text ErrorText;
    private string email;
    private string password;
    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        //SignUp.onClick.AddListener(() => Signup(Email.text, Password.text));
        //LoginButton.onClick.AddListener(() => Login(Email.text, Password.text));
    }

    // Update is called once per frame
    void Update()
    {
     
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
    public void Login( )
    {
        email = Email.text;
        password = Password.text;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
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

            PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");
            PlayerPrefs.SetString("Uid", user != null ? user.UserId : "Unknown");
            SceneManager.LoadScene("StartMenu");
        });
    }
}
