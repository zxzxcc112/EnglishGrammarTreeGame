using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Analytics;
using System;

public class FirebaseManager : MonoBehaviour
{
    //public event UnityAction OnStateChanged;
    [Header("RaisedEvents")]
    [SerializeField] stringChannelSO SignUpEvent;
    [SerializeField] stringChannelSO SignInEvent;
    [SerializeField] stringChannelSO SignOutEvent;
    [SerializeField] stringChannelSO UpdateProfileEvent;

    public bool developer = true;

    FirebaseAuth auth;
    FirebaseUser user;

    DatabaseReference database;

    string notification = default;

    public async void SignUp(string email, string password)
    {
        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(SignUpTaskResult);

        if (SignUpEvent != null)
        {
            SignUpEvent.Raise(MessageHandler.GetTextInParentheses(notification));
            notification = default;
        }
    }
    public async void SignIn(string email, string password)
    {
        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(SignInTaskResult);

        if (SignInEvent != null)
        {
            SignInEvent.Raise(MessageHandler.GetTextInParentheses(notification));
            notification = default;
        }
    }

    public void SignOut()
    {
        auth.SignOut();
    }

    public void UpdateDisplayName(string name)
    {
        if (user == null) return;
        UserProfile profile = new UserProfile();
        profile.DisplayName = name;
        profile.PhotoUrl = null;
        user.UpdateUserProfileAsync(profile).ContinueWith(UpdateProfileTaskResult);

        if (UpdateProfileEvent != null)
        {
            UpdateProfileEvent.Raise(MessageHandler.GetTextInParentheses(notification));
            notification = default;
        }
    }

    void UpdateProfileTaskResult(Task task)
    {
        if (task.IsCanceled || task.IsFaulted)
        {
            notification = task.Exception.InnerException.Message;
            return;
        }
        notification = "Change user name successfully.";
    }

    void SignUpTaskResult(Task<FirebaseUser> task)
    {
        if (task.IsCanceled)
        {
            notification = developer ?
                "CreateUserWithEmailAndPasswordAsync was canceled." :
                "Create user was canceled.";
            return;
        }
        if (task.IsFaulted)
        {
            notification = developer ?
                "CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception :
                task.Exception.InnerException.Message;
            return;
        }
        // Firebase user has been created.
        FirebaseUser newUser = task.Result;
        notification = $"Firebase user created successfully: {newUser.DisplayName} ({newUser.UserId})";
        Debug.Log(notification);
    }

    void SignInTaskResult(Task<FirebaseUser> task)
    {
        if (task.IsCanceled)
        {
            notification = developer ?
                "SignInWithEmailAndPasswordAsync was canceled." :
                "Sign in was canceled.";
            return;
        }
        if (task.IsFaulted)
        {
            notification = developer ? 
                "SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception :
                task.Exception.InnerException.Message;
            return;
        }

        FirebaseUser newUser = task.Result;
        notification = $"User signed in successfully: {newUser.DisplayName} ({newUser.UserId})";
        Debug.Log(notification);
    }


    void DebugError(string message)
    {
        Debug.LogError(message);
    }

    private void Start()
    {
        //------------Auth----------------
        InitializeFirebase();
        if (SignUpEvent != null)
            SignUpEvent.OnEventTriggered += DebugError;

        //-----------database----------------
        database = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId + "\n" +
                          "email : " + user.Email + "\n" +
                          "displayName : " + user.DisplayName + "\n" +
                          "phone : " + user.PhoneNumber + "\n" +
                          "PhotoUrl : " + user.PhotoUrl + "\n" +
                          "ProviderId : " + user.ProviderId);
            }
        }
    }

    // Handle removing subscription and reference to the Auth instance.
    // Automatically called by a Monobehaviour after Destroy is called on it.
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        if (SignUpEvent != null)
            SignUpEvent.OnEventTriggered -= DebugError;
        auth.SignOut();
        auth = null;
    }

    //-----------------------------database---------------------------------------
    public async void SetValueAsync(object value)
    {
        Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
        keyValuePairs["item4"] = 110;
        keyValuePairs["item5"] = 112;
        keyValuePairs["item6"] = 345;

        Dictionary<string, int> keyValuePairs2 = new Dictionary<string, int>();
        keyValuePairs2["item1"] = 110;
        keyValuePairs2["item2"] = 112;
        keyValuePairs2["item3"] = 345;

        Dictionary<string, object> keyValuePairs1 = new Dictionary<string, object>();
        keyValuePairs1["test"] = keyValuePairs;
        keyValuePairs1["test3"] = keyValuePairs2;
        try
        {
            await database.SetValueAsync(value);
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
