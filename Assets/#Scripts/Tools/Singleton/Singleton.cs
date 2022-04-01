using System;

public abstract class Singleton<T> : BaseSingleton<T> where T : Singleton<T>
{
    #region Fields

    /// <summary>
    /// The instance.
    /// </summary>
    private static T instance;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindInstance();
            }

            return instance;
        }
    }

    protected bool IsInstance => Instance == this;

    #endregion Properties

    #region Methods

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    #endregion Methods
}