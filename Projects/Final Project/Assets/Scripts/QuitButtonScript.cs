using UnityEngine;
using UnityEngine.UI;

public class QuitButtonScript : MonoBehaviour
{
    private StateManager stateManager;
    private Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(clickQuitButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clickQuitButton()
    {
        stateManager.quitGame();
    }
}
