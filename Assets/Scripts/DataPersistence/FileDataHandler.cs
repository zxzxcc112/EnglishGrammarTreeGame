using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "word";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                //�q�ɮפ����J�ǦC�Ƹ��
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //�i�諸�\��: �O�_�ϥΥ[�K(�ѱK)
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }


                //�N��ƤϧǦC�Ƭ�C#����
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data) 
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            //�p�G�ɮפ��s�b�A�Ыإؼи��|�ɮ�
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            //�ǦC�ƹC����ƨ�json
            string dataToStore = JsonUtility.ToJson(data, true);

            //�i�諸�\��: �O�_�ϥΥ[�K(�ѱK)
            if(useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //�N�ǦC�Ƹ�Ƽg�J�ɮ�
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
        
    }

    //²��ϥ�XOR�i��[�K�ѱK
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for(int i = 0;i < data.Length;i++)
        {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
