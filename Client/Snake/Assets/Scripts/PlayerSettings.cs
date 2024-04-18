using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance { get; private set; }
    public string Login { get; private set; }
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetLogin(string login) => Login = login;

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
