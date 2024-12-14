using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //メニュー中の各Cavnas
    [SerializeField] GameObject[] canvas;

    void Awake()
    {
        //Cavnasの表示切り替え
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
        canvas[2].SetActive(false);
        canvas[3].SetActive(false);
        canvas[4].SetActive(false);
        canvas[5].SetActive(false);
        //ゲーム起動時にのみタイトル画面を表示
        if (DataManager.Instance.isStartup)
        {
            canvas[1].SetActive(false);
            canvas[0].SetActive(true);
            DataManager.Instance.isStartup = false;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerPrefs.SetInt("GachaTicket", PlayerPrefs.GetInt("GachaTicket") + 100);
            DataManager.Instance.playerData.gachaTicketNum += 100;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.DeleteKey("GachaTicket");
            DataManager.Instance.playerData.gachaTicketNum = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            SaveManager.Instance.CharaDataInitialize();
        }
        //～ デバッグ用
    }

    //メニュー画面へ移行するボタン
    public void Menu()
    {
        canvas[0].SetActive(false);
        canvas[2].SetActive(false);
        canvas[3].SetActive(false);
        canvas[4].SetActive(false);
        canvas[5].SetActive(false);

        canvas[1].SetActive(true);
    }
    //戦闘画面へ移行するボタン
    public void StageSelect(int num)
    {
        //引数numが0ならステージセレクト画面の表示切り替え
        if (num <= 0)
        {
            canvas[2].SetActive(!canvas[2].activeSelf);
            return;
        }
        //numのシーン番号をロード
        FadeManager.Instance.LoadSceneIndex(num, 0.5f);
    }
    //キャラクターのステータス画面へ移行するボタン
    public void CharaStatus()
    {
        canvas[1].SetActive(canvas[3].activeSelf);
        canvas[2].SetActive(canvas[3].activeSelf);
        canvas[4].SetActive(canvas[3].activeSelf);
        canvas[5].SetActive(canvas[3].activeSelf);

        canvas[3].SetActive(!canvas[3].activeSelf);
    }
    //ガチャ画面へ移行するボタン
    public void Gacha()
    {
        canvas[1].SetActive(canvas[4].activeSelf);
        canvas[2].SetActive(canvas[4].activeSelf);
        canvas[3].SetActive(canvas[4].activeSelf);
        canvas[5].SetActive(canvas[4].activeSelf);

        canvas[4].SetActive(!canvas[4].activeSelf);
    }
    //オプション画面へ移行するボタン
    public void Option()
    {
        canvas[5].SetActive(!canvas[5].activeSelf);
    }
}
