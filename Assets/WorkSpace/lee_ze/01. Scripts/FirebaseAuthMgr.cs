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

    public static FirebaseUser user; // 인증된 유저 정보

    public FirebaseAuth auth; // 인증 진행을 위한 정보

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
                Debug.Log("로그인 오류");

                return;
            }

            if (task.IsCanceled)
            {
                Debug.Log("로그인 취소");

                return;
            }

            Debug.Log("로그인 성공");

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
                Debug.Log("등록 오류");

                return;
            }

            if (task.IsCanceled)
            {
                Debug.Log("등록 취소");

                return;
            }

            FirebaseUser registerduser = task.Result.User;
        });
    }
}
