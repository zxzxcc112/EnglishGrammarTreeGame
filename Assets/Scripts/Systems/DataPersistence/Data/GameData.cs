using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //GameSettings
    public bool isFullScreen;
    public int screenWidth;
    public int screenHeight;


    //新遊戲開始時沒有檔案可以載入時，要有一個預設值
    public GameData()
    {
        isFullScreen = true;
        screenWidth = 0;
        screenHeight = 0;
    }
}
