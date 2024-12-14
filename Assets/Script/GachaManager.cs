using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GachaManager : Singleton<GachaManager>
{
    public int gachaTicketNum;

    //�K�`���̊e���
    [SerializeField] GameObject window_Start, window_Pull, window_Result;
    //
    [SerializeField] TextMeshProUGUI[] gachaTicketText;
    [SerializeField] Button gachaButton,gachaButton_TenPull;
    //
    [SerializeField] TextMeshProUGUI pullCharaNameText;
    [SerializeField] Image pullCharaImage;
    //���U���g�ŕ\������L����
    [SerializeField] GameObject resultCharaNameObj;
    [SerializeField] GameObject resultCharaImageObj;
    //
    [SerializeField] int totsuMax;

    //���U���g�ŕ\������L����
    TextMeshProUGUI[] resultCharaNameText;
    Image[] resultCharaImage;

    //���肵���L������\�����鏇�Ԃ��Ǘ�����ϐ�
    int[] getCharaArray;
    int getCharaArrayNum;

    void Start()
    {
        //�E�B���h�E�̕\���؂�ւ�
        window_Start.SetActive(true);
        window_Pull.SetActive(false);
        window_Result.SetActive(false);

        //�莝���K�`���`�P�b�g�������[�h
        gachaTicketNum = DataManager.Instance.playerData.gachaTicketNum;
        for (int i = 0; i < gachaTicketText.Length; i++)
        {
            gachaTicketText[i].text = "�~ " + gachaTicketNum;
        }
        //�`�P�b�g���Ȃ���΃K�`���������{�^�����A�N�e�B�u��
        gachaButton.interactable = (gachaTicketNum >= 1);
        gachaButton_TenPull.interactable = (gachaTicketNum >= 10);

        //���U���g��ʗp�̃I�u�W�F�N�g���i�[
        resultCharaImage = new Image[resultCharaImageObj.transform.childCount];
        resultCharaNameText = new TextMeshProUGUI[resultCharaImage.Length];
        for (int i = 0; i < resultCharaImage.Length; i++)
        {
            resultCharaImage[i] = resultCharaImageObj.transform.GetChild(i).GetComponent<Image>();
            resultCharaImage[i].gameObject.SetActive(false);

            resultCharaNameText[i] = resultCharaNameObj.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            resultCharaNameText[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// �K�`���������{�^��
    /// </summary>
    public void PullGacha(int pullValue)
    {
        //�K�`���`�P�b�g������
        gachaTicketNum -= pullValue;
        PlayerPrefs.SetInt("GachaTicket", gachaTicketNum);
        gachaTicketText[0].text = "�~ " + gachaTicketNum;
        //�`�P�b�g���Ȃ���΃K�`���������{�^�����A�N�e�B�u��
        gachaButton.interactable = (gachaTicketNum >= 1);
        gachaButton_TenPull.interactable = (gachaTicketNum >= 10);

        Array.Resize(ref getCharaArray, pullValue);
        for (int i = 0; i < pullValue; i++)
        {
            //0 �` �L�������Œ��I
            int randomNum = UnityEngine.Random.Range(0, DataManager.Instance.charaNum);

            //�L�����N�^�[����̏���ۑ�
            DataManager.Instance.charaData[randomNum].get = true;
            DataManager.Instance.charaData[randomNum].totsu = Math.Min(DataManager.Instance.charaData[randomNum].totsu + 1, totsuMax);
            SaveManager.Instance.CharaDataSave();

            getCharaArray[i] = randomNum;

            //���U���g�̃L������\��
            resultCharaNameText[i].gameObject.SetActive(true);
            resultCharaNameText[i].text = DataManager.Instance.charaData[randomNum].charaName;
            resultCharaImage[i].gameObject.SetActive(true);
            resultCharaImage[i].sprite = DataManager.Instance.charaData[randomNum].sprite;
            resultCharaImage[i].preserveAspect = true;
        }
        //���U���g�̃L�������\��
        for (int i = pullValue; i < resultCharaImage.Length; i++)
        {
            resultCharaImage[i].gameObject.SetActive(false);
            resultCharaNameText[i].gameObject.SetActive(false);
        }

        getCharaArrayNum = 0;
        ViewGetChara(getCharaArray[getCharaArrayNum]);

        window_Start.SetActive(false);
        window_Pull.SetActive(true);
    }

    /// <summary>
    /// ���肵���L�����N�^�[��\������
    /// </summary>
    void ViewGetChara(int getCharaId)
    {
        pullCharaNameText.text = DataManager.Instance.charaData[getCharaId].charaName;
        pullCharaImage.sprite = DataManager.Instance.charaData[getCharaId].sprite;
        pullCharaImage.preserveAspect = true;
    }
    
    /// <summary>
    /// �������L�������y�[�W���肷��{�^��
    /// </summary>
    public void PullCharaNext()
    {
        getCharaArrayNum++;
        //�Ō�̃L�����܂ŕ\�������烊�U���g�Ɉڂ�
        if (getCharaArrayNum >= getCharaArray.Length - 1)
        {
            ViewResult();
            return;
        }
        ViewGetChara(getCharaArray[getCharaArrayNum]);
    }

    /// <summary>
    /// �������L�����̃��U���g��\������
    /// </summary>
    public void ViewResult()
    {
        window_Result.SetActive(true);
    }

    /// <summary>
    /// ���U���g�����{�^��
    /// </summary>
    public void ResultEnd()
    {
        window_Pull.SetActive(false);
        window_Result.SetActive(false);
        window_Start.SetActive(true);

        gachaTicketText[1].text = "�~ " + gachaTicketNum;
    }
}
