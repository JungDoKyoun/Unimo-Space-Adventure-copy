using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;

public class FirebaseAuthMgr : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    public static FirebaseUser user; // ������ ���� ����

    public FirebaseAuth auth; // ���� ������ ���� ����

    [SerializeField]
    private TMP_InputField emailField;

    [SerializeField]
    private TMP_InputField passwordField;

    //[SerializeField]
    //private Text warningText;

    //[SerializeField]
    //private Text confirmText;

    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;

            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

                startButton.interactable = true;
            }

            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("�α��� ����");

                return;
            }

            if (task.IsCanceled)
            {
                Debug.Log("�α��� ���");

                return;
            }

            Debug.Log("�α��� ����");

            FirebaseUser registerdUser = task.Result.User;
           
            Debug.Log(registerdUser);
        });
    }

    public void Register()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("��� ����");

                return;
            }

            if (task.IsCanceled)
            {
                Debug.Log("��� ���");

                return;
            }

            FirebaseUser registerduser = task.Result.User;
        });
    }
}
