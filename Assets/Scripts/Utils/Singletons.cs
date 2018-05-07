using UnityEngine;

public class SingletonAuto<T> : MonoBehaviour where T : new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }
}

public class SingletonSimple<T> : MonoBehaviour where T : class
{
    public static T Instance { get; private set; }

    public SingletonSimple()
    {
        Instance = this as T;
    }
}