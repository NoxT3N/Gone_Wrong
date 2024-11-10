using UnityEngine;
using UnityEngine.UIElements;

public class DemonScript : MonoBehaviour
{

    public GameObject demon;
    public int aggro;

    public MeshRenderer demonhider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        demonhider.enabled = false;
    }

    // FixedUpdate is like Update but not tied to framerate
    void FixedUpdate()
    {
        
    }
}
