using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveManager : Singleton<SaveManager>
{
    //CSVファイルのパス
    string playerCsvPath = "Assets/Resources/PlayerSaveData.csv";
    string charaCsvPath = "Assets/Resources/CharaSaveData.csv";

    void Awake()
    {
        //シーンを遷移してもSaveManagerは残る
        if (gameObject.transform.parent != null) gameObject.transform.parent = null;
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// キャラのデータをCSVファイルに書き込む
    /// </summary>
    public void CharaDataSave()
    {
        List<int[]> writeDataList = new List<int[]>();
        for (int i = 0; i < DataManager.Instance.charaNum; i++)
        {
            int[] writeData = new int[3];

            writeData[0] = DataManager.Instance.charaData[i].totsu;
            writeData[1] = DataManager.Instance.charaData[i].lv;
            writeData[2] = DataManager.Instance.charaData[i].exp;

            writeDataList.Add(writeData);
        }

        StreamWriter writer = new StreamWriter(charaCsvPath, false);
        for (int i = 0; i < DataManager.Instance.charaNum; i++)
        {
            string line = string.Join(",", writeDataList[i]); //配列をカンマで区切った文字列に変換する
            writer.WriteLine(line);
        }
        writer.Flush();
        writer.Close();
    }

    /// <summary>
    /// キャラのセーブデータがない場合の初期化処理
    /// </summary>
    public void CharaDataInitialize()
    {
        List<int[]> writeDataList = new List<int[]>();
        for (int i = 0; i < DataManager.Instance.charaNum; i++)
        {
            int[] writeData = new int[3];

            writeData[0] = 0;
            writeData[1] = 0;
            writeData[2] = 0;

            writeDataList.Add(writeData);
        }

        StreamWriter writer = new StreamWriter(charaCsvPath, false);
        for (int i = 0; i < DataManager.Instance.charaNum; i++)
        {
            string line = string.Join(",", writeDataList[i]); //配列をカンマで区切った文字列に変換する
            writer.WriteLine(line);
        }
        writer.Flush();
        writer.Close();
    }
}
