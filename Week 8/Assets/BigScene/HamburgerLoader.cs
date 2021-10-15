using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HamburgerLoader : MonoBehaviour
{
    [SerializeField]
    private Button start;
    [SerializeField]
    private TMPro.TextMeshProUGUI text;
    [SerializeField]
    private Button open;

    private AsyncOperation asyncLoad;
    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(StartLoading);
        open.onClick.AddListener(OpenScene);
        open.interactable = false;
    }

    private void OpenScene()
    {
        asyncLoad.allowSceneActivation = true;
    }

    private void StartLoading()
    {
        StartCoroutine(Load());
    }

    //https://docs.unity3d.com/ScriptReference/AsyncOperation-allowSceneActivation.html
    private IEnumerator Load()
    {
        asyncLoad = SceneManager.LoadSceneAsync("Hamburger", LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        while(!asyncLoad.isDone)
        {
            text.text = $"{asyncLoad.progress}%";
            yield return null;
            if (asyncLoad.progress >= 0.09f)
            {
                open.interactable = true;
            }
        }
        text.text = "100%";
    }
}
