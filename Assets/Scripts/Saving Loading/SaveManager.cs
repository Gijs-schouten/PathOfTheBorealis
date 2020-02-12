using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField] private List<Checkpoint> CheckpointList;

    private string SaveFile = "Save";
    [SerializeField] private SaveData Data;

    [SerializeField] private GameObject Player;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ReloadCheckpoint();
        }
    }

    private void Awake()
    {
        Instance = this;

        if (ES3.KeyExists(SaveFile))
            Data = ES3.Load<SaveData>(SaveFile);
        else
            Data = new SaveData();

        ReloadCheckpoint();
    }

    public void ReachedCheckpoint(Checkpoint checkpoint)
    {

        for (int i = 0; i < CheckpointList.Count; i++)
        {
            if(checkpoint == CheckpointList[i])
            {
                Data.LevelIndex = SceneManager.GetActiveScene().buildIndex;
                Data.Checkpoint = i;
                break;
            }
        }

        ES3.Save<SaveData>(SaveFile, Data);
        ES3.Save<GameObject>("Player", Player);
    }

    private void ReloadCheckpoint()
    {
        if (ES3.KeyExists(SaveFile))
            Data = ES3.Load<SaveData>(SaveFile);
        else
        {
            Data = new SaveData();
            return;
        }
            

        if (SceneManager.GetActiveScene().buildIndex != Data.LevelIndex)
        {
            SceneManager.LoadScene(Data.LevelIndex);
        }

        ES3.LoadInto<GameObject>("Player", Player);

        //Destroy(Player);
        //GameObject player = Instantiate(ES3.Load<GameObject>("Player"));

        //Player = player;

        Player.transform.position = CheckpointList[Data.Checkpoint].transform.position;

        //player.transform.SetPositionAndRotation(CheckpointList[Data.Checkpoint].transform.position, CheckpointList[Data.Checkpoint].transform.rotation);
    }

}

public class SaveData
{
    public int LevelIndex, Checkpoint = 0;
}