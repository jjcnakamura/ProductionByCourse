using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //���j���[���̊eCavnas
    [SerializeField] GameObject[] canvas;

    void Awake()
    {
        //Cavnas�̕\���؂�ւ�
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
        canvas[2].SetActive(false);
        canvas[3].SetActive(false);
        canvas[4].SetActive(false);
        canvas[5].SetActive(false);
        //�Q�[���N�����ɂ̂݃^�C�g����ʂ�\��
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
        //�f�o�b�O�p
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
        //�` �f�o�b�O�p
    }

    //���j���[��ʂֈڍs����{�^��
    public void Menu()
    {
        canvas[0].SetActive(false);
        canvas[2].SetActive(false);
        canvas[3].SetActive(false);
        canvas[4].SetActive(false);
        canvas[5].SetActive(false);

        canvas[1].SetActive(true);
    }
    //�퓬��ʂֈڍs����{�^��
    public void StageSelect(int num)
    {
        //����num��0�Ȃ�X�e�[�W�Z���N�g��ʂ̕\���؂�ւ�
        if (num <= 0)
        {
            canvas[2].SetActive(!canvas[2].activeSelf);
            return;
        }
        //num�̃V�[���ԍ������[�h
        FadeManager.Instance.LoadSceneIndex(num, 0.5f);
    }
    //�L�����N�^�[�̃X�e�[�^�X��ʂֈڍs����{�^��
    public void CharaStatus()
    {
        canvas[1].SetActive(canvas[3].activeSelf);
        canvas[2].SetActive(canvas[3].activeSelf);
        canvas[4].SetActive(canvas[3].activeSelf);
        canvas[5].SetActive(canvas[3].activeSelf);

        canvas[3].SetActive(!canvas[3].activeSelf);
    }
    //�K�`����ʂֈڍs����{�^��
    public void Gacha()
    {
        canvas[1].SetActive(canvas[4].activeSelf);
        canvas[2].SetActive(canvas[4].activeSelf);
        canvas[3].SetActive(canvas[4].activeSelf);
        canvas[5].SetActive(canvas[4].activeSelf);

        canvas[4].SetActive(!canvas[4].activeSelf);
    }
    //�I�v�V������ʂֈڍs����{�^��
    public void Option()
    {
        canvas[5].SetActive(!canvas[5].activeSelf);
    }
}
