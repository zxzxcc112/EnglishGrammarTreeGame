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
        //�ǰe��Ƶ���L�}���Ϩ�L�}���i�H��s���
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveGame(ref gameData);
        }

        //Debug.Log("Saved full screen is " + gameData.isFullScreen);
        
        //�ϥ�data handler�x�s���
        dataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        //��data handler�q�ɮ׸��J����w�x�s��data
        gameData = dataHandler.Load();

        //�p�G�S���i���J�ɮסA��l�Ʒs�C��
        if(gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        //�N���J����Ʊ��e��ݭn���}��
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
