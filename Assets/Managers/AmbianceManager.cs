using UnityEngine;

public class AmbianceManager : MonoBehaviour
{

    [Header("Ambiance Settings")]
    [SerializeField] private string defaultAmbiance = "StartingAmbaince"; 
    private string currentAmbiance = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayAmbiance(defaultAmbiance);
    }

    public void PlayAmbiance(string ambianceName)
    {
        if (currentAmbiance == ambianceName) 
            return;

        if (!string.IsNullOrEmpty(currentAmbiance))
        {
            AudioManager.Instance.Stop(currentAmbiance);
        }

        AudioManager.Instance.Play(ambianceName);
        currentAmbiance = ambianceName;
    }

    public void StopAmbiance()
    {
        if (!string.IsNullOrEmpty(currentAmbiance))
        {
            AudioManager.Instance.Stop(currentAmbiance);
            currentAmbiance = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
