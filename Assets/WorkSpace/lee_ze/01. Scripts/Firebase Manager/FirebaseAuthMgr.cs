using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class FirebaseAuthMgr : MonoBehaviour
{
    public static FirebaseAuthMgr Instance { get; private set; }

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button loginButton;

    [SerializeField]
    private Button signUpButton;

    [SerializeField]
    private Button logoutButton;

    private static FirebaseUser user; // 인증된 유저 정보

    private static DatabaseReference dbRef; // DB 추가

    private static bool hasUser;

    #region 프로퍼티

    public static FirebaseUser User
    {
        get
        {
            return user;
        }

        private set
        {
            user = value;
        }
    }

    public static DatabaseReference DBRef
    {
        get => dbRef;

        private set
        {
            dbRef = value;
        }
    }

    public static bool HasUser
    {
        get
        {
            return hasUser;
        }
    }

    #endregion

    public static bool IsFirebaseReady { get; private set; } = false;

    public FirebaseAuth auth; // 인증 진행을 위한 정보

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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            DependencyStatus dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;

                DBRef = FirebaseDatabase.DefaultInstance.RootReference;

                IsFirebaseReady = true;
            }

            else
            {
                Debug.LogError("파이어베이스 오류");
            }
        });

        hasUser = (User == null) ? false : true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        GetUIs();

        RegisterUIEvents();
    }

    private void Start()
    {
        if (startButton != null) startButton.interactable = false;

        if (warningText != null) warningText.text = "";

        if (confirmText != null) confirmText.text = "";
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Account") // Account 씬으로 돌아왔을 때만
        {
            GetUIs();

            RegisterUIEvents();
        }
    }

    private void OnDisable()
    {
        loginButton?.onClick.RemoveListener(Login);

        signUpButton?.onClick.RemoveListener(Register);

        logoutButton?.onClick.RemoveListener(Logout);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Login()
    {
        StartCoroutine(LoginCor(emailField.text + "@unimo.com", passwordField.text));
    }

    public void Register()
    {
        StartCoroutine(RegisterCor(emailField.text + "@unimo.com", passwordField.text, nicknameField.text));
    }

    #region UI
    private void GetUIs()
    {
        // 버튼
        if (startButton == null) startButton = GameObject.Find("Account Canvas").transform.Find("Account/Start Button").GetComponent<Button>();

        if (loginButton == null) loginButton = GameObject.Find("Account Canvas").transform.Find("Account/Login Button").GetComponent<Button>();

        if (signUpButton == null) signUpButton = GameObject.Find("Account Canvas").transform.Find("Account/Sign Up Button").GetComponent<Button>();

        if (logoutButton == null) logoutButton = GameObject.Find("Mode Selection Canvas").transform.Find("Logout Button").GetComponent<Button>();

        // 인풋필드
        if (emailField == null) emailField = GameObject.Find("Account Canvas").transform.Find("Account/Account Input Fields/ID").GetComponent<TMP_InputField>();

        if (passwordField == null) passwordField = GameObject.Find("Account Canvas").transform.Find("Account/Account Input Fields/Password").GetComponent<TMP_InputField>();

        if (nicknameField == null) nicknameField = GameObject.Find("Account Canvas").transform.Find("Account/Account Input Fields/Nickname").GetComponent<TMP_InputField>();

        if (warningText == null) warningText = GameObject.Find("Account Canvas").transform.Find("Account/Account Input Fields/Nickname Text").GetComponent<TextMeshProUGUI>();

        if (confirmText == null) confirmText = GameObject.Find("Account Canvas").transform.Find("Account/Account Input Fields/Nickname Text").GetComponent<TextMeshProUGUI>();
    }

    private void RegisterUIEvents()
    {
        loginButton.onClick.RemoveAllListeners();

        signUpButton.onClick.RemoveAllListeners();

        logoutButton.onClick.RemoveAllListeners();


        loginButton.onClick.AddListener(Login);

        signUpButton.onClick.AddListener(Register);
        
        logoutButton.onClick.AddListener(Logout);
    }

    public void SetButtonInteractable(bool value) // 회원가입 or 로그인 시 << 이 버튼 비활성화 및 start 버튼 활성화
    {
        if (startButton == null || loginButton == null || signUpButton == null)
        {
            GetUIs();
        }

        startButton.interactable = value;

        loginButton.interactable = !value;

        signUpButton.interactable = !value;
    }

    #endregion

    #region 로그인 (코루틴)

    private IEnumerator LoginCor(string email, string password)
    {
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        // 로그인 계정에 문제가 있다면
        if (LoginTask.Exception != null)
        {
            Debug.LogWarning(message: "다음과 같은 이유로 로그인 실패: " + LoginTask.Exception);

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

        // 로그인 계정에 문제가 없다면
        else
        {
            // 로그인
            User = LoginTask.Result.User; // 유저 정보 기억

            hasUser = true;

            warningText.text = "";

            nicknameField.text = User.DisplayName;

            confirmText.text = "nickname: " + User.DisplayName;

            Debug.Log(User.DisplayName + " 로그인");

            SetButtonInteractable(hasUser);

            FirebaseDataBaseMgr.Instance.InitIngameCurrency();
        }
    }

    #endregion

    #region 회원가입 (코루틴)

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

            // 회원가입 조건에 문제가 있다면
            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: "실패 사유" + RegisterTask.Exception);

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

            // 회원가입 조건에 문제가 없다면
            else
            {
                // 바로 로그인
                User = RegisterTask.Result.User;

                hasUser = true;

                if (User != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = username };

                    Task ProfileTask = User.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    // 초기 값 세팅
                    yield return StartCoroutine(InitPlayerCurrency());

                    if (ProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message: "닉네임 설정 실패" + ProfileTask.Exception);

                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;

                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                        warningText.text = "Failed to set nickname";
                    }
                    else
                    {
                        warningText.text = "";

                        confirmText.text = "nickname: " + User.DisplayName;

                        SetButtonInteractable(hasUser);
                    }
                }
            }
        }
    }

    private IEnumerator InitPlayerCurrency() // 회원가입 시 초기값 설정
    {
        // 초기 인게임 재화 생성(크레딧)
        var DBTask = DBRef.Child("users").Child(User.UserId).Child("nickname").SetValueAsync(User.DisplayName);

        // 초기 인게임 재화 생성(크레딧)
        DBTask = DBRef.Child("users").Child(User.UserId).Child("rewardIngameCurrency").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // 초기 메타 재화 생성(프리팹)
        DBTask = DBRef.Child("users").Child(User.UserId).Child("rewardMetaCurrency").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // 초기 메타 재화 생성(설계도)
        DBTask = DBRef.Child("users").Child(User.UserId).Child("rewardBluePrint").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // 개인 최고 점수
        DBTask = DBRef.Child("users").Child(User.UserId).Child("score").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // 승률
        DBTask = DBRef.Child("users").Child(User.UserId).Child("rate").Child("winningRate").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // 매치 진행 횟수
        DBTask = DBRef.Child("users").Child(User.UserId).Child("rate").Child("playCount").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // 매치 승리 횟수
        DBTask = DBRef.Child("users").Child(User.UserId).Child("rate").Child("winCount").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    }
    #endregion

    #region 로그아웃

    public void Logout()
    {
        //GetUIs();

        if (auth != null)
        {
            auth.SignOut();

            User = null;

            hasUser = false;

            FirebaseDataBaseMgr.IsDataBaseReady = false;

            emailField.text = "";

            passwordField.text = "";

            nicknameField.text = "";

            if (warningText != null) warningText.text = "";

            if (confirmText != null) confirmText.text = "";

            //SetButtonInteractable(hasUser);

            //if (startButton != null) startButton.interactable = false;

            //if (loginButton != null) loginButton.interactable = true;

            //if (signUpButton != null) signUpButton.interactable = true;

            Debug.Log(auth.CurrentUser);

            Debug.Log("로그아웃 완료");
        }
    }

    #endregion
}
