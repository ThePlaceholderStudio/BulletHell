using UnityEngine;

public class ExperienceManager : MonoBehaviour
{   
    public static ExperienceManager Instance { get; private set; }

    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange = (int amount) => { };

    private void Awake()
    {
        Debug.Log("awake exp manager");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddExperience(int amount)
    {
        Debug.Log(amount.ToString());
        OnExperienceChange?.Invoke(amount);
    }
}
