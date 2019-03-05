﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;
using Firebase.Auth;

public class FirebaseModel : MonoBehaviour
{
    private FirebaseAuth auth;
    private string email;
    private long score = 0 ;
    private GameController gameController;
    private bool scoreSent = false;
    private int MaxScores = 10;
    protected bool isFirebaseInitialized = false;
    private string uid;
    private Firebase.Auth.FirebaseUser user;
    private FirebaseDatabase mDatabase;
    private DataSnapshot snapshot;
    // Start is called before the first frame update
    void Start()
    {

        InitializeFirebase();
        uid = PlayerPrefs.GetString("Uid");
        Debug.Log(uid);
        email = PlayerPrefs.GetString("LoginUser");
        gameController = GameController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.isGameOver() && !scoreSent) {
            score = (long) gameController.getScore();
            Debug.Log(mDatabase);
            AddScore();
            AddNewScoreToUser();
            scoreSent = true;
        }
        
    }

    // Initialize the Firebase database:
    protected virtual void InitializeFirebase()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        // NOTE: You'll need to replace this url with your Firebase App's database
        // path in order for the database connection to work correctly in editor.
        app.SetEditorDatabaseUrl("https://pusheenrun.firebaseio.com/");
        if (app.Options.DatabaseUrl != null)
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
        mDatabase = FirebaseDatabase.GetInstance(app);
        isFirebaseInitialized = true;
    }
    private void AddNewScoreToUser() {
        DatabaseReference mDatabaseRef = mDatabase.GetReference("user_Maxscore");
        Dictionary<string, object> entryValues = new Dictionary<string, object>();
        entryValues["score"] = score;
        entryValues["email"] = email;
        entryValues["time"] = System.DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy");
        Dictionary<string, object> childUpdates = new Dictionary<string, object>();

        Debug.Log(mDatabaseRef.Child(uid));
        /*if (mDatabaseRef.Child(uid) == null)
        {
            childUpdates[uid] = entryValues;
            mDatabaseRef.UpdateChildrenAsync(childUpdates);
        }
        else
        {*/
            mDatabaseRef.Child(uid).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("error get child at user_Maxscore");

                }
                else if (task.IsCompleted)
                {
                    snapshot = task.Result;

                }
                List<object> result = snapshot.Children as List<object>;
                if (result == null)
                {
                    childUpdates[uid] = entryValues;
                    mDatabaseRef.UpdateChildrenAsync(childUpdates);
                }
                else 
                {
                    long childScore = (long)((Dictionary<string, object>)result[0])["score"];
                    if(childScore < score)
                        mDatabaseRef.Child(uid).Child("score").SetValueAsync(score);
                }
            });


        //}
    }

    TransactionResult AddScoreTransaction(MutableData mutableData)
    {
        List<object> user_scores = mutableData.Value as List<object>;
        if (user_scores == null)
            user_scores = new List<object>();
        else if (mutableData.ChildrenCount >= MaxScores)
        {
            long minScore = long.MaxValue;
            object minVal = null;
            
            foreach (var child in user_scores)
            {
                if (!(child is Dictionary<string, object>))
                    continue;
                long childScore = (long)((Dictionary<string, object>)child)["score"];
                if (childScore < minScore)
                {
                    minScore = childScore;
                    minVal = child;
                }
            }

            if (minScore > score)
            {
                return TransactionResult.Abort();
            }

            user_scores.Remove(minVal);

        }
        Dictionary<string, object> newScoreMap = new Dictionary<string, object>();
        
        newScoreMap["score"] = score;
        newScoreMap["email"] = email;
        newScoreMap["time"] = System.DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy");
        user_scores.Add(newScoreMap);
        mutableData.Value = user_scores;
        return TransactionResult.Success(mutableData);
    }

    public void AddScore()
    {
        if (score == 0 || string.IsNullOrEmpty(email))
        {
            Debug.Log("invalid score or email.");
            return;
        }
        Debug.LogFormat("Attempting to add score {0} {1}",
          email, score.ToString());

        //DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("scores");
        DatabaseReference reference = mDatabase.GetReference("leaders");
        Debug.Log("Running Transaction...");
        // Use a transaction to ensure that we do not encounter issues with
        // simultaneous updates that otherwise might create more than MaxScores top scores.
        reference.RunTransaction(AddScoreTransaction)
          .ContinueWith(task => {
              if (task.Exception != null)
              {
                  Debug.Log(task.Exception.ToString());
              }
              else if (task.IsCompleted)
              {
                  Debug.Log("Transaction complete.");
              }
          });
    }

}
