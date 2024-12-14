using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GachaManager : Singleton<GachaManager>
{
    public int gachaTicketNum;

    //ガチャの各画面
    [SerializeField] GameObject window_Start, window_Pull, window_Result;
    //
    [SerializeField] TextMeshProUGUI[] gachaTicketText;
    [SerializeField] Button gachaButton,gachaButton_TenPull;
    //
    [SerializeField] TextMeshProUGUI pullCharaNameText;
    [SerializeField] Image pullCharaImage;
    //リザルトで表示するキャラ
    [SerializeField] GameObject resultCharaNameObj;
    [SerializeField] GameObject resultCharaImageObj;
    //
    [SerializeField] int totsuMax;

    //リザルトで表示するキャラ
    TextMeshProUGUI[] resultCharaNameText;
    Image[] resultCharaImage;

    //入手したキャラを表示する順番を管理する変数
    int[] getCharaArray;
    int getCharaArrayNum;

    void Start()
    {
        //ウィンドウの表示切り替え
        window_Start.SetActive(true);
        window_Pull.SetActive(false);
        window_Result.SetActive(false);

        //手持ちガチャチケット数をロード
        gachaTicketNum = DataManager.Instance.playerData.gachaTicketNum;
        for (int i = 0; i < gachaTicketText.Length; i++)
        {
            gachaTicketText[i].text = "× " + gachaTicketNum;
        }
        //チケットがなければガチャを引くボタンを非アクティブに
        gachaButton.interactable = (gachaTicketNum >= 1);
        gachaButton_TenPull.interactable = (gachaTicketNum >= 10);

        //リザルト画面用のオブジェクトを格納
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
    /// ガチャを引くボタン
    /// </summary>
    public void PullGacha(int pullValue)
    {
        //ガチャチケットを消費
        gachaTicketNum -= pullValue;
        PlayerPrefs.SetInt("GachaTicket", gachaTicketNum);
        gachaTicketText[0].text = "× " + gachaTicketNum;
        //チケットがなければガチャを引くボタンを非アクティブに
        gachaButton.interactable = (gachaTicketNum >= 1);
        gachaButton_TenPull.interactable = (gachaTicketNum >= 10);

        Array.Resize(ref getCharaArray, pullValue);
        for (int i = 0; i < pullValue; i++)
        {
            //0 ～ キャラ数で抽選
            int randomNum = UnityEngine.Random.Range(0, DataManager.Instance.charaNum);

            //キャラクター入手の情報を保存
            DataManager.Instance.charaData[randomNum].get = true;
            DataManager.Instance.charaData[randomNum].totsu = Math.Min(DataManager.Instance.charaData[randomNum].totsu + 1, totsuMax);
            SaveManager.Instance.CharaDataSave();

            getCharaArray[i] = randomNum;

            //リザルトのキャラを表示
            resultCharaNameText[i].gameObject.SetActive(true);
            resultCharaNameText[i].text = DataManager.Instance.charaData[randomNum].charaName;
            resultCharaImage[i].gameObject.SetActive(true);
            resultCharaImage[i].sprite = DataManager.Instance.charaData[randomNum].sprite;
            resultCharaImage[i].preserveAspect = true;
        }
        //リザルトのキャラを非表示
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
    /// 入手したキャラクターを表示する
    /// </summary>
    void ViewGetChara(int getCharaId)
    {
        pullCharaNameText.text = DataManager.Instance.charaData[getCharaId].charaName;
        pullCharaImage.sprite = DataManager.Instance.charaData[getCharaId].sprite;
        pullCharaImage.preserveAspect = true;
    }
    
    /// <summary>
    /// 引いたキャラをページ送りするボタン
    /// </summary>
    public void PullCharaNext()
    {
        getCharaArrayNum++;
        //最後のキャラまで表示したらリザルトに移る
        if (getCharaArrayNum >= getCharaArray.Length - 1)
        {
            ViewResult();
            return;
        }
        ViewGetChara(getCharaArray[getCharaArrayNum]);
    }

    /// <summary>
    /// 引いたキャラのリザルトを表示する
    /// </summary>
    public void ViewResult()
    {
        window_Result.SetActive(true);
    }

    /// <summary>
    /// リザルトを閉じるボタン
    /// </summary>
    public void ResultEnd()
    {
        window_Pull.SetActive(false);
        window_Result.SetActive(false);
        window_Start.SetActive(true);

        gachaTicketText[1].text = "× " + gachaTicketNum;
    }
}
