using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // this script updates the game over screen 
    public TextMeshProUGUI flavourText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI restartText;


    public float initTime;

    void Start()
    {
   
        UpdateGameOverScreen();


        initTime = Time.time;
    }

    public void UpdateGameOverScreen()
    {
        // when the Game Over Screen loads, pull the distance travelled float from PlayerPrefs and update the distance text.
        // if there's nothing in the PPs, the default is 0
        distanceText.text = "Distance Travelled: " + PlayerPrefs.GetFloat("DistanceTravelled", 0f) + " KMs";
        
        
        // depending on fail conditions this text can be updated with a number of strings
        flavourText.text = "You crashed your car!"; 
    }

    public void Update()
    {
        if (Input.GetButtonDown("Submit") || Time.time >= initTime + 30)
        {
            GetComponent<SceneChanger>().loadSceneOnClick("Test");
        }

        distanceText.fontSize = Mathf.Abs(Mathf.Sin(Time.time)*5)+70;
        restartText.fontSize = Mathf.Abs(Mathf.Sin(Time.time) * 5) + 70;

        distanceText.transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time));
        restartText.transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time));

    }

}
