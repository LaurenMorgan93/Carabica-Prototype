using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // this script is used to change scenes 
    

    public void loadSceneOnClick(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
