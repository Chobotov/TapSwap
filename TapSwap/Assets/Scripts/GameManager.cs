using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TapSwap
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Health = new GameObject[3];
        [SerializeField] private GameObject Background;

        private AudioSource asrc;
        public AudioClip _gameover;

        public GameObject Player,
            Main,
            Spawn,
            Game,
            Heal,
            Pipes,
            Buttons,
            PauseButton,
            ScoreTxt,
            Text,
            RestartButton,
            ResumeButton,
            Lines;

        public Text NowScore, RecordScore, Timer;
        public int heartNum, score;
        private string issound = "on";
        private IEnumerator Items;

        private void Awake()
        {
            if (PlayerPrefs.GetString("Sound").Equals("on"))
            {
                Lines.SetActive(true);
                Player.GetComponent<AudioSource>().volume = 1.0F;
            }
            else
            {
                Lines.SetActive(false);
                Player.GetComponent<AudioSource>().volume = 0.0F;
            }
        }

        private void Start()
        {
            asrc = Player.GetComponent<AudioSource>();
            Time.timeScale = 1f;
            heartNum = 2;
            issound = PlayerPrefs.GetString("Sound");
            Items = ItemSpawn();
        }

        private void Update()
        {
            Player.GetComponent<ControlPipes>().choosepipe();
            if (GetComponent<Score>().value < 0 || heartNum < 0)
                GameOver();
        }

        public void StartGame()
        {
            PauseButton.SetActive(true);
            Main.SetActive(false);
            Pipes.SetActive(true);
            Heal.SetActive(true);
            Game.SetActive(true);
            Background.SetActive(false);
            StartCoroutine(Items);
        }

        private void GameOver()
        {
            asrc.PlayOneShot(_gameover);
            StopCoroutine(Items);
            Background.SetActive(true);
            Main.SetActive(false);
            Pipes.SetActive(false);
            Heal.SetActive(false);
            Game.SetActive(false);
            Text.SetActive(true);
            Buttons.SetActive(true);
            PauseButton.SetActive(false);
            ResumeButton.SetActive(false);
            RestartButton.SetActive(true);
            if (GetComponent<Score>().value < 0)
            {
                GetComponent<Score>().value = 0;
                score = 0;
            }
            else
                score = GetComponent<Score>().value;

            NowScore.GetComponent<Text>().text = GetComponent<Score>().value.ToString();
            GetComponent<Score>().value = 0;
            heartNum = 2;
            RecordScore.GetComponent<Text>().text = PlayerPrefs.GetInt("Record").ToString();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            Heal.SetActive(false);
            Pipes.SetActive(false);
            Game.SetActive(false);
            PauseButton.SetActive(false);
            Background.SetActive(true);
            Text.SetActive(true);
            Buttons.SetActive(true);
            NowScore.GetComponent<Text>().text = GetComponent<Score>().value.ToString();
            RecordScore.GetComponent<Text>().text = PlayerPrefs.GetInt("Record").ToString();
        }

        public void Resume()
        {
            StartCoroutine(ResumeGame());
            Background.SetActive(false);
            Heal.SetActive(true);
            Game.SetActive(true);
            Text.SetActive(false);
            Buttons.SetActive(false);
        }

        public void Restart()
        {
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }

        public void SoundOnOff()
        {
            if (PlayerPrefs.GetString("Sound").Equals("on"))
            {
                Lines.SetActive(false);
                issound = "off";
                PlayerPrefs.SetString("Sound", issound);
                Player.GetComponent<AudioSource>().volume = 0.0F;
            }
            else
            {
                Lines.SetActive(true);
                issound = "on";
                PlayerPrefs.SetString("Sound", issound);
                Player.GetComponent<AudioSource>().volume = 1.0F;
            }
        }

        public void Exit()
        {
            PlayerPrefs.Save();
            Application.Quit();
        }

        public void CheckRecord(int score)
        {
            int record = PlayerPrefs.GetInt("Record");
            if (score > record) PlayerPrefs.SetInt("Record", score);
        }

        IEnumerator ItemSpawn()
        {
            while (GetComponent<Score>().value >= 0)
            {
                Spawn.GetComponent<SpawnItems>().Spawn();
                yield return new WaitForSeconds(GetComponent<Score>().speed);
            }
        }

        IEnumerator ResumeGame()
        {
            int num = 3;
            Timer.gameObject.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                Timer.text = num.ToString();
                num -= 1;
                yield return new WaitForSecondsRealtime(1);
            }

            Timer.gameObject.SetActive(false);
            Pipes.SetActive(true);
            PauseButton.SetActive(true);
            Time.timeScale = 1f;
        }

        public void ShowHealth(int heartNum, bool show)
        {
            for (int i = 0; i <= heartNum; i++) Health[i].SetActive(show);
        }
    }
}
