using System.Collections;
using TMPro;
using UnityEngine;


public class StageStartScript : MonoBehaviour
{

    private GameManager gameManagerScript;

    public TextMeshProUGUI countdownText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CountdownRoutine()
    {
        gameManagerScript.IsGameStarted = false;
        int count = 3;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            // Use WaitForSecondsRealtime if you ever use Time.timeScale = 0
            yield return new WaitForSecondsRealtime(1.0f);
            count--;
        }

        countdownText.text = "GO!";
        Time.timeScale = 1f;
        gameManagerScript.IsGameStarted = true;
        gameManagerScript.StartTimedEvents();

        gameManagerScript.audioOutStageTrack.Play();

        yield return new WaitForSeconds(1.0f);
        countdownText.gameObject.SetActive(false); // Hide the text

    }
}
