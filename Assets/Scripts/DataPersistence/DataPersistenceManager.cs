using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;

    public static DataPersistenceManager instance { get; private set; }

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Data persistence manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void SaveGame()
    {
        //傳送資料給其他腳本使其他腳本可以更新資料
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveGame(ref gameData);
        }

        //Debug.Log("Saved full screen is " + gameData.isFullScreen);
        
        //使用data handler儲存資料
        dataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        //用data handler從檔案載入任何已儲存的data
        gameData = dataHandler.Load();

        //如果沒有可載入檔案，初始化新遊戲
        if(gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        //將載入的資料推送到需要的腳本
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadGame(gameData);
        }

        //Debug.Log("Loaded full screen is " + gameData.isFullScreen);
    }


    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
