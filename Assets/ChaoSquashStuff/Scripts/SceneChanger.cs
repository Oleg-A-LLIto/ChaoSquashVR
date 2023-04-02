using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] Slider LoadingSlider;
    [SerializeField] Fading fadingAnimator;

    public void ChangeScene(int _id)
    {
        AsyncOperation _loading = SceneManager.LoadSceneAsync(_id);
        fadingAnimator.Fade(true);
        while (!_loading.isDone)
        {
            LoadingSlider.value = _loading.progress;
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
