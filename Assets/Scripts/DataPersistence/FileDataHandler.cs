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
                //從檔案中載入序列化資料
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //可選的功能: 是否使用加密(解密)
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }


                //將資料反序列化為C#物件
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
            //如果檔案不存在，創建目標路徑檔案
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            //序列化遊戲資料到json
            string dataToStore = JsonUtility.ToJson(data, true);

            //可選的功能: 是否使用加密(解密)
            if(useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //將序列化資料寫入檔案
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

    //簡單使用XOR進行加密解密
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
