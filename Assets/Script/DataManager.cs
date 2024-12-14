using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    [System.NonSerialized] public bool isStartup = true; //�Q�[���N������

    //CSV����f�[�^��ǂݍ��ޕϐ�
    TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>();

    //�v���C���[�̃f�[�^���i�[����ϐ�
    public PlayerData playerData;

    //�L�����N�^�[�̃f�[�^���i�[����ϐ�
    public CharaData[] charaData;
    [System.NonSerialized] public int charaNum;
    //�G�̃f�[�^���i�[����ϐ�
    public EnemyData[] enemyData;

    StringReader reader;

    void Awake()
    {
        //�V�[����J�ڂ��Ă�DataManager�͎c��
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

    //�Q�[���̃f�[�^�����[�h
    void GameDataLoad()
    {
        //�L�����̃f�[�^��CSV����ǂݏo��
        csvFile = Resources.Load("CharaData") as TextAsset;
        reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        charaNum = csvDatas.Count;
        charaData = new CharaData[charaNum];
        //�L�����̃f�[�^��z��Ɋi�[
        for (int i = 0; true; i++)
        {
            if (i >= charaNum) break;
            charaData[i] = new CharaData();
            //�X�e�[�^�X
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
        //���X�g�̃f�[�^���N���A
        csvDatas.Clear();

        //�G�̃f�[�^��CSV����ǂݏo��
        csvFile = Resources.Load("EnemyData") as TextAsset;
        reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        int enemyNum = csvDatas.Count;
        enemyData = new EnemyData[enemyNum];
        //�G�̃f�[�^��z��Ɋi�[
        for (int i = 0; true; i++)
        {
            if (i >= enemyNum) break;
            enemyData[i] = new EnemyData();
            //�X�e�[�^�X
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
        //���X�g�̃f�[�^���N���A
        csvDatas.Clear();
    }

    //�Z�[�u�f�[�^�����[�h
    void SaveDataLoad()
    {
        //�v���C���[�̃Z�[�u�f�[�^��CSV����ǂݏo��
        csvFile = Resources.Load("PlayerSaveData") as TextAsset;
        reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        //�f�[�^��z��Ɋi�[
        playerData.gachaTicketNum = PlayerPrefs.GetInt("GachaTicket"); //�K�`���`�P�b�g��
        //���X�g�̃f�[�^���N���A
        csvDatas.Clear();

        //�L�����̃p�����[�^�̃Z�[�u�f�[�^��CSV����ǂݏo��
        csvFile = Resources.Load("CharaSaveData") as TextAsset;
        reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        //�L�����̃p�����[�^�̃Z�[�u�f�[�^��z��Ɋi�[
        for (int i = 0; true; i++)
        {
            if (i >= charaNum) break;
            charaData[i].get = (int.Parse(csvDatas[i][0]) <= 1); //����ς݂̃L������
            charaData[i].lv = int.Parse(csvDatas[i][1]);         //���x��
            charaData[i].exp = int.Parse(csvDatas[i][2]);        //����o���l
            charaData[i].totsu = int.Parse(csvDatas[i][0]);  //��(�����L��������������Ă��邩)
        }
        //���X�g�̃f�[�^���N���A
        csvDatas.Clear();
    }
}

//�v���C���[�̃f�[�^
[System.Serializable]
public class PlayerData
{
    public int gachaTicketNum;
}

//�L�����N�^�[�̃f�[�^
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

//�G�̃f�[�^
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
