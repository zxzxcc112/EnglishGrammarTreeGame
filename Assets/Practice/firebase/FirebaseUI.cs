using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirebaseUI : MonoBehaviour
{
    [SerializeField] TMP_InputField account;
    [SerializeField] TMP_InputField password;
    [SerializeField] TMP_InputField userName;
    [SerializeField] TMP_InputField inputValue;
    [SerializeField] MessageBox box;

    [SerializeField] FirebaseManager firebaseManager;

    [Header("Subscription")]
    [SerializeField] stringChannelSO OnErrorTriggered;

    public void SignUp()
    {
        firebaseManager.SignUp(account.text, password.text);
    }

    public void SignIn()
    {
        firebaseManager.SignIn(account.text, password.text);
    }

    public void SignOut()
    {
        firebaseManager.SignOut();
    }

    public void UpdateProfile()
    {
        firebaseManager.UpdateDisplayName(userName.text);
    }

    public void SetValue()
    {
        firebaseManager.SetValueAsync(inputValue.text);
    }

    private void OnEnable()
    {
        if(OnErrorTriggered != null)
            OnErrorTriggered.OnEventTriggered += SetMessage;
    }

    private void OnDisable()
    {
        if (OnErrorTriggered != null)
            OnErrorTriggered.OnEventTriggered -= SetMessage;
    }

    private void SetMessage(string message)
    {
        box.gameObject.SetActive(true);
        box.SetMessage(message);
    }
}
