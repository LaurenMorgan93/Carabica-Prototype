using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitialiseGame : MonoBehaviour
{
    static public bool isInitialised;

    public List<GameObject> gameObjectsToAppear = new List<GameObject>();
    public List<Vector3> scales = new List<Vector3>();

    private Coroutine startRoutine;

    public TextMeshProUGUI titleScreenText;
    public TextMeshProUGUI startDescriptorText;

    float initFontTitle, initFontDescription;

    public float appearanceSpeed;

    public bool titleAppearance;

    private void Awake()
    {
        isInitialised = false;

        gameObjectsToAppear.ForEach(obj => { scales.Add(obj.transform.localScale); });

        foreach(var obj in gameObjectsToAppear)
        {
            obj.transform.localScale = Vector3.zero;
        }

        initFontTitle = titleScreenText.fontSize;
        initFontDescription = startDescriptorText.fontSize;

        StartCoroutine(TitleAppearanceRoutine());
    }

    private IEnumerator TitleAppearanceRoutine()
    {
        float lerp = 0;

        while(lerp < 1)
        {
            titleScreenText.color = Color.Lerp(titleScreenText.color, Color.black, lerp);

            lerp += Time.deltaTime * appearanceSpeed;

            yield return null;
        }

        lerp = 0;

        while (lerp < 1)
        {
            startDescriptorText.color = Color.Lerp(startDescriptorText.color, Color.black, lerp);

            lerp += Time.deltaTime * appearanceSpeed;

            yield return null;
        }

        titleAppearance = true;
    }

    private IEnumerator StartRoutine()
    {
        print("yuh");

        foreach(var obj in gameObjectsToAppear)
        {
            obj.SetActive(true);
        }

        float yuppa=0;

        while(yuppa < 1)
        {
            titleScreenText.fontSize = Mathf.Lerp(titleScreenText.fontSize, 0, yuppa);
            startDescriptorText.fontSize = Mathf.Lerp(startDescriptorText.fontSize, 0, yuppa);

            yuppa += Time.deltaTime * appearanceSpeed * 2;

            yield return null;
        }

        float lerp = 0;

        while(lerp < 1)
        {
            lerp += Time.deltaTime * appearanceSpeed * 2;

            for(int i = 0; i < gameObjectsToAppear.Count; ++i)
            {
                gameObjectsToAppear[i].transform.localScale = Vector3.Lerp(Vector3.zero, scales[i], lerp);
            }

            yield return null;
        }

        print("huh");

        isInitialised = true;
    }

    private void Update()
    {
        if(titleAppearance)
        {
            if (Input.GetButtonDown("Submit")  && !isInitialised)
            {
                if(startRoutine == null)
                {
                    print("yuh");
                    startRoutine = StartCoroutine(StartRoutine());
                }

            }
            else
            {
                if(startRoutine == null)
                {
                    titleScreenText.fontSize = Mathf.Abs(Mathf.Sin(Time.time)) * 5 + initFontTitle - 5;
                    startDescriptorText.fontSize = Mathf.Abs(Mathf.Sin(Time.time)) * 5 + initFontDescription + 5;
                }

            }
        }

    }
}
