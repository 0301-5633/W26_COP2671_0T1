using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private StateManager stateManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void togglePause()
    {
        stateManager.PauseStateToggle();
    }
}
