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
    void Start()
    {
   
        UpdateGameOverScreen();

    }

    public void UpdateGameOverScreen()
    {
        // when the Game Over Screen loads, pull the distance travelled float from PlayerPrefs and update the distance text.
        // if there's nothing in the PPs, the default is 0
        distanceText.text = "Distance Travelled: " + PlayerPrefs.GetFloat("DistanceTravelled", 0f) + " KMs";
        
        
        // depending on fail conditions this text can be updated with a number of strings
        flavourText.text = "You crashed your car!"; 
    }


    
    
 

}
