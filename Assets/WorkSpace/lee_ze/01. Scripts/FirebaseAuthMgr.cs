using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Threading.Tasks;
using ZL.Unity;

public class FirebaseAuthMgr : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    public static FirebaseUser user; // ������ ���� ����

    public static DatabaseReference dbRef; // DB �߰�

    public static bool IsFirebaseReady { get; private set; } = false;

    public FirebaseAuth auth; // ���� ������ ���� ����

    [SerializeField]
    private TMP_InputField emailField;

    [SerializeField]
    private TMP_InputField passwordField;

    [SerializeField]
    private TMP_InputField nicknameField;

    [SerializeField]
    private TextMeshProUGUI warningText;

    [SerializeField]
    private TextMeshProUGUI confirmText;

    [SerializeField]
    private GameObject laser;

    private void Awake()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            DependencyStatus dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;

                dbRef = FirebaseDatabase.DefaultInstance.RootReference;

                IsFirebaseReady = true;
            }

            else
            {
                //UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                Debug.LogError("���̾�̽� ����");
            }
        });
    }

    private void Start()
    {
        startButton.interactable = false;

        warningText.text = "";

        confirmText.text = "";
    }

    public void Login()
    {
        StartCoroutine(LoginCor(emailField.text, passwordField.text));
    }

    public void Register()
    {
        StartCoroutine(RegisterCor(emailField.text, passwordField.text, nicknameField.text));
    }

    #region �α��� �ڷ�ƾ

    private IEnumerator LoginCor(string email, string password)
    {
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        // �α��ο� ������ �ִٸ�
        if (LoginTask.Exception != null)
        {
            Debug.LogWarning(message: "������ ���� ������ �α��� ����: " + LoginTask.Exception);

            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;

            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "";

            switch (errorCode)
            {
                case AuthError.MissingEmail:

                    message = "Missing email";

                    break;

                case AuthError.MissingPassword:
                    
                    message = "Missing password";
                    
                    break;

                case AuthError.WrongPassword:
                    
                    message = "Wrong password";
                    
                    break;

                case AuthError.InvalidEmail:
                    
                    message = "Invalid email format";
                    
                    break;

                case AuthError.UserNotFound:
                    
                    message = "User not found";
                    
                    break;

                default:
                    
                    message = "Please contact the administrator";
                    
                    break;
            }
            warningText.text = message;
        }

        // �α��ο� ������ ���ٸ�
        else
        {
            user = LoginTask.Result.User; // ���� ���� ���

            warningText.text = "";

            nicknameField.text = user.DisplayName;

            confirmText.text = "nickname: " + user.DisplayName;

            startButton.interactable = true;

            laser.Destroy();
        }
    }

    #endregion

    #region ȸ������ �ڷ�ƾ

    private IEnumerator RegisterCor(string email, string password, string username)
    {
        if (username == "")
        {
            warningText.text = "Nickname is missing";
        }
        else
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            // ȸ������ ������ �ִٸ�
            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: "���� ����" + RegisterTask.Exception);

                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;

                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Registration failed";

                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        
                        message = "Missing email";
                        
                        break;

                    case AuthError.MissingPassword:
                        
                        message = "Missing password";
                        
                        break;

                    case AuthError.WeakPassword:
                        
                        message = "Weak password";
                        
                        break;

                    case AuthError.EmailAlreadyInUse:
                        
                        message = "Email already in use";
                        
                        break;

                    default:
                        
                        message = "Other reason. Please contact the administrator";
                        
                        break;
                }

                warningText.text = message;
            }

            // ȸ������ ������ ���ٸ�
            else
            {
                user = RegisterTask.Result.User;

                if (user != null)
                {
                    yield return StartCoroutine(InitPlayerCurrency());

                    UserProfile profile = new UserProfile { DisplayName = username };

                    Task ProfileTask = user.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message: "�г��� ���� ����" + ProfileTask.Exception);

                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;

                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                        warningText.text = "Failed to set nickname";
                    }
                    else
                    {
                        warningText.text = "";

                        confirmText.text = "nickname: " + user.DisplayName;

                        startButton.interactable = true;

                        laser.Destroy();
                    }
                }
            }
        }
    }

    #endregion

    private IEnumerator InitPlayerCurrency() // ȸ������ �� ��ȭ �ʱⰪ ����
    {
        // �ʱ� �ΰ��� ��ȭ ����
        var DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardIngameCurrency").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // �ʱ� ��Ÿ ��ȭ ����
        DBTask = dbRef.Child("users").Child(user.UserId).Child("rewardMetaCurrency").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    }
}
