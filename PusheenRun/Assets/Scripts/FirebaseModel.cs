using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;

public class FirebaseModel : MonoBehaviour
{
    private string email;
    private int score = 0 ;
    private GameController gameController;
    private bool scoreSent = false;
    private int MaxScores = 10;
    protected bool isFirebaseInitialized = false;
    // Start is called before the first frame update
    void Start()
    {
        email = PlayerPrefs.GetString("LoginUser");
        gameController = GameController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.isGameOver() && !scoreSent) {
            score = gameController.getScore();
            AddScore();
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

        isFirebaseInitialized = true;
    }

    TransactionResult AddScoreTransaction(MutableData mutableData)
    {
        List<object> scores = mutableData.Value as List<object>;
        if (scores == null)
            scores = new List<object>();
        else if (mutableData.ChildrenCount >= MaxScores)
        {
            long minScore = long.MaxValue;
            object minVal = null;
            foreach (var child in scores)
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

            scores.Remove(minVal);

        }
        Dictionary<string, object> newScoreMap = new Dictionary<string, object>();
        newScoreMap["score"] = score;
        newScoreMap["email"] = email;
        newScoreMap["time"] = System.DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy");
        scores.Add(newScoreMap);
        mutableData.Value = scores;
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

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("scores");

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
