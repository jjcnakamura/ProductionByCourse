using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    [System.NonSerialized] public bool isStartup = true; //ゲーム起動時か

    //CSVからデータを読み込む変数
    TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>();

    //プレイヤーのデータを格納する変数
    public PlayerData playerData;

    //キャラクターのデータを格納する変数
    public CharaData[] charaData;
    [System.NonSerialized] public int charaNum;
    //敵のデータを格納する変数
    public EnemyData[] enemyData;

    StringReader reader;

    void Awake()
    {
        //シーンを遷移してもDataManagerは残る
        if (gameObject.transform.parent != null) gameObject.transform.parent = null;
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        GameDataLoad();
        SaveDataLoad();
    }

    //ゲームのデータをロード
    void GameDataLoad()
    {
        //キャラのデータをCSVから読み出し
        csvFile = Resources.Load("CharaData") as TextAsset;
        reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        charaNum = csvDatas.Count;
        charaData = new CharaData[charaNum];
        //キャラのデータを配列に格納
        for (int i = 0; true; i++)
        {
            if (i >= charaNum) break;
            charaData[i] = new CharaData();
            //ステータス
            charaData[i].id = i;
            charaData[i].charaName = csvDatas[i][0];
            charaData[i].sprite = Resources.Load<Sprite>(csvDatas[i][1]);
            charaData[i].hp = int.Parse(csvDatas[i][2]);
            charaData[i].atk = int.Parse(csvDatas[i][3]);
            charaData[i].def = int.Parse(csvDatas[i][4]);
            charaData[i].spd = int.Parse(csvDatas[i][5]);
            charaData[i].skillId1 = int.Parse(csvDatas[i][6]);
            charaData[i].skillId2 = int.Parse(csvDatas[i][7]);
        }
        //リストのデータをクリア
        csvDatas.Clear();

        //敵のデータをCSVから読み出し
        csvFile = Resources.Load("EnemyData") as TextAsset;
        reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        int enemyNum = csvDatas.Count;
        enemyData = new EnemyData[enemyNum];
        //敵のデータを配列に格納
        for (int i = 0; true; i++)
        {
            if (i >= enemyNum) break;
            enemyData[i] = new EnemyData();
            //ステータス
            enemyData[i].id = i;
            enemyData[i].enemyName = csvDatas[i][0];
            enemyData[i].sprite = Resources.Load<Sprite>(csvDatas[i][1]);
            enemyData[i].hp = int.Parse(csvDatas[i][2]);
            enemyData[i].atk = int.Parse(csvDatas[i][3]);
            enemyData[i].def = int.Parse(csvDatas[i][4]);
            enemyData[i].spd = int.Parse(csvDatas[i][5]);
            enemyData[i].skillId1 = int.Parse(csvDatas[i][6]);
            enemyData[i].skillId2 = int.Parse(csvDatas[i][7]);
        }
        //リストのデータをクリア
        csvDatas.Clear();
    }

    //セーブデータをロード
    void SaveDataLoad()
    {
        //プレイヤーのセーブデータをCSVから読み出し
        csvFile = Resources.Load("PlayerSaveData") as TextAsset;
        reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        //データを配列に格納
        playerData.gachaTicketNum = PlayerPrefs.GetInt("GachaTicket"); //ガチャチケット数
        //リストのデータをクリア
        csvDatas.Clear();

        //キャラのパラメータのセーブデータをCSVから読み出し
        csvFile = Resources.Load("CharaSaveData") as TextAsset;
        reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        //キャラのパラメータのセーブデータを配列に格納
        for (int i = 0; true; i++)
        {
            if (i >= charaNum) break;
            charaData[i].get = (int.Parse(csvDatas[i][0]) <= 1); //入手済みのキャラか
            charaData[i].lv = int.Parse(csvDatas[i][1]);         //レベル
            charaData[i].exp = int.Parse(csvDatas[i][2]);        //入手経験値
            charaData[i].totsu = int.Parse(csvDatas[i][0]);  //凸(同じキャラを何回引いているか)
        }
        //リストのデータをクリア
        csvDatas.Clear();
    }
}

//プレイヤーのデータ
[System.Serializable]
public class PlayerData
{
    public int gachaTicketNum;
}

//キャラクターのデータ
[System.Serializable]
public class CharaData
{
    public int id;
    public string charaName;
    public Sprite sprite;

    public bool get;
    public int totsu;
    public int lv;
    public int exp;

    public int hp;
    public int atk;
    public int def;
    public int spd;

    public int skillId1;
    public int skillId2;
}

//敵のデータ
[System.Serializable]
public class EnemyData
{
    public int id;
    public string enemyName;
    public Sprite sprite;

    public int hp;
    public int atk;
    public int def;
    public int spd;

    public int skillId1;
    public int skillId2;
}
