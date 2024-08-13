using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    

    public static SceneTransition Create()
    {
        SceneTransition _transition = Instantiate(Resources.Load<SceneTransition>("Transition"), null);
        _transition.name = "SceneTransition";
        return _transition;
    }

    public static void Load(string _sceneName)
    {
        DOTween.KillAll();
        Time.timeScale = 0;
        Create().LoadClass(_sceneName);
    }

    public static void Reload()
    {
        DOTween.KillAll();
        Time.timeScale = 0;
        Create().LoadClass(SceneManager.GetActiveScene().name);
    }

    public void LoadClass(string _sceneName)
    {
        StartCoroutine(Transition(_sceneName));
    }


    public RectTransform image;
    public float tweenDuration;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator Transition(string _sceneName)
    {
        
        Tween _tween = image.transform.DOScale(new Vector3(20, 20, 20), tweenDuration).SetEase(Ease.Linear).ChangeStartValue(Vector3.zero).SetUpdate(true);
        yield return _tween.WaitForKill();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Time.timeScale = 1;
        yield return new WaitForEndOfFrame();
        _tween = image.transform.DOScale(0, tweenDuration).SetEase(Ease.Linear).SetUpdate(true);
        yield return _tween.WaitForKill();
        Destroy(gameObject);
    }

}
