using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] Slider LoadingSlider;
    [SerializeField] Fading fadingAnimator;
    int id;

    public void ChangeScene(int _id)
    {
        id = _id;
        StartCoroutine(LoadTheSceneAsync());
    }

    IEnumerator LoadTheSceneAsync()
    {
        AsyncOperation _loading = SceneManager.LoadSceneAsync(id);
        fadingAnimator.Fade(true);
        while (!_loading.isDone)
        {
            LoadingSlider.value = _loading.progress;
            yield return 0;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
