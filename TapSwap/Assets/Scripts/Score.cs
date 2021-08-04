using UnityEngine;
using UnityEngine.UI;

namespace TapSwap
{
    public class Score : MonoBehaviour
    {
        public int value;
        public float speed = 5f;
        public Text ScoreTxt;
        public GameObject item1, item2, item3;

        private void Start()
        {
            value = 0;
            ScoreTxt.text = value.ToString();
        }

        public void SpeedCheck()
        {
            ScoreTxt.text = value.ToString();
            if (value >= 5)
            {
                speed = 4f;
                changegravity(0.3f);
            }
            else if (value < 5)
            {
                speed = 5f;
                changegravity(0.2f);
            }

            if (value >= 10)
            {
                speed = 3f;
                changegravity(0.4f);
            }
            else if (value < 10 && value >= 5)
            {
                speed = 4f;
                changegravity(0.3f);
            }

            if (value >= 15)
            {
                speed = 2f;
                changegravity(0.5f);
            }
            else if (value < 15 && value >= 10)
            {
                speed = 3f;
                changegravity(0.4f);
            }

            if (value >= 20)
            {
                speed = 1f;
                changegravity(0.6f);
            }
            else if (value < 20 && value >= 15)
            {
                speed = 2f;
                changegravity(0.5f);
            }

            if (value >= 25)
            {
                speed = 0.8f;
                changegravity(0.7f);
            }
            else if (value < 25 && value >= 20)
            {
                speed = 1f;
                changegravity(0.6f);
            }
        }

        private void changegravity(float value)
        {
            item1.GetComponent<Rigidbody2D>().gravityScale = value;
            item2.GetComponent<Rigidbody2D>().gravityScale = value;
            item3.GetComponent<Rigidbody2D>().gravityScale = value;
        }
    }
}
