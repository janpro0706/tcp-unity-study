using UnityEngine;
using System.Collections;

namespace Assets
{
    class RankFileManager
    {
        private static RankFileManager instance;

        public static RankFileManager GetInstance()
        {
            if (instance == null)
            {
                instance = new RankFileManager();
            }
            return instance;
        }


        public void SetScore(int score)
        {
            System.IO.File.WriteAllText("rank.txt", "" + score);
        }

        public int GetMaxScore()
        {
            if (!System.IO.File.Exists("rank.txt"))
            {
                System.IO.File.WriteAllText("rank.txt", "0");
            }
            string input = System.IO.File.ReadAllText("rank.txt");
            int max = int.Parse(input);

            return max;
        }
    }
}