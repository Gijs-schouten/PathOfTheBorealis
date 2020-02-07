using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    [SerializeField] private string LoadingSceneName;

    private int TargetScene;
    private bool CanLoadLevel;

    private GameObject LoadingPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (CanLoadLevel)
        {
            if(LoadingPlayer == null)
            {
                LoadingPlayer = GameObject.Find("Player");
            }
            else
            {
                LoadingPlayer.transform.position = new Vector3(0, 0, 0);
                LoadingPlayer.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (TargetScene == SceneManager.GetActiveScene().buildIndex)
                StartCoroutine(EndLoad());
        }
    }

    public void LoadNewScene(int _targetScene)
    {
        TargetScene = _targetScene;
        StartCoroutine(LoadUnloadScene());
    }

    private IEnumerator EndLoad()
    {
        PlayerCanvasScript.Instance.StopFlash();

        yield return new WaitForSeconds(0.04f);

        CanLoadLevel = false;
        LoadingPlayer = null;
    }

    private IEnumerator LoadUnloadScene()
    {
        PlayerCanvasScript.Instance.ActivateFlash();

        yield return new WaitForSeconds(0.08f);

        AsyncOperation _loadingOperation = SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Single);

        while (!_loadingOperation.isDone)
        {
            yield return null;
        }

        CanLoadLevel = true;

        PlayerCanvasScript.Instance.StopFlash();

        yield return new WaitForSeconds(1f);

        AsyncOperation _secondLoadingOperation = SceneManager.LoadSceneAsync(TargetScene, LoadSceneMode.Single);

        while (!_loadingOperation.isDone)
        {
            yield return null;
        }

        Debug.Log("KLAAR MET LADEN");
        PlayerCanvasScript.Instance.ActivateFlash();

        yield return null;
    }
}
