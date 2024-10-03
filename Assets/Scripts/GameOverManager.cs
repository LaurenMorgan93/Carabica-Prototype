using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    // this script updates the game over screen 
    public TextMeshProUGUI flavourText;
    public TextMeshProUGUI distanceText;
    void Start()
    {
        // when the Game Over Screen loads, pull the distance travelled float from PlayerPrefs and update the distance text.
        // if there's nothing in the PPs, the default is 0
        distanceText.text ="You drove " +  PlayerPrefs.GetFloat("DistanceTravelled", 0f) + " KMs before crashing";
        
        
        // depending on fail conditions this text can be updated with a number of strings
        flavourText.text = "You crashed your car!"; 
        

    }

}
