using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveManager : Singleton<SaveManager>
{
    //CSV�t�@�C���̃p�X
    string playerCsvPath = "Assets/Resources/PlayerSaveData.csv";
    string charaCsvPath = "Assets/Resources/CharaSaveData.csv";

    void Awake()
    {
        //�V�[����J�ڂ��Ă�SaveManager�͎c��
        if (gameObject.transform.parent != null) gameObject.transform.parent = null;
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// �L�����̃f�[�^��CSV�t�@�C���ɏ�������
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
            string line = string.Join(",", writeDataList[i]); //�z����J���}�ŋ�؂���������ɕϊ�����
            writer.WriteLine(line);
        }
        writer.Flush();
        writer.Close();
    }

    /// <summary>
    /// �L�����̃Z�[�u�f�[�^���Ȃ��ꍇ�̏���������
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
            string line = string.Join(",", writeDataList[i]); //�z����J���}�ŋ�؂���������ɕϊ�����
            writer.WriteLine(line);
        }
        writer.Flush();
        writer.Close();
    }
}
