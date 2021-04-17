using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YsoCorp
{
    public class SoundWaxime : YCBehaviour
    {
        public static string SOUNDHOME = "44";
        public static string SOUNDPLAY = "41";
        public static string SOUNDVICTORY = "18";
        public static string SOUNDLOOSE = "23";
        public static string SOUNDPARAM = "22";
        public static string SOUNDTILE = "08";
        public static string SOUNDTILESELECT = "03";
        public static string SOUNDBUTTONTYPE = "20";
        public static string SOUNDERROR = "29";

        [SerializeField]
        private float timeGoal = 0.1f;
        private float time = 0.0f;
        private bool canTilesSound = true;

        // Start is called before the first frame update
        void Start()
        {
            this.ycManager.ycConfig.SoundMusic = true;
            this.ycManager.ycConfig.SoundEffect = true;
        }

        void Update()
        {
            if (this.time >= this.timeGoal)
            {
                this.time = 0.0f;
                this.canTilesSound = true;
            }
            if (!this.canTilesSound)
                this.time += Time.deltaTime;
        }

        public void ChangeMusic()
        {
            string tmpSound = "Ambiant" + Random.Range(0, Resources.LoadAll<AudioClip>("Sounds/Musics").Length);
            this.ycManager.soundManager.StopMusic();
            this.ycManager.soundManager.PlayMusic(tmpSound, 0.2f);
        }

        public void PlayEffect(string playNumber, float volume = 1f)
        {
            this.ycManager.soundManager.PlayEffect("DM-CGS-" + playNumber, volume);
            int tmpRand = Random.Range(1, Resources.LoadAll<AudioClip>("Sounds/Effects").Length + 1); //TODO Suppr
            string tmpSound = "DM-CGS-" + ((tmpRand < 10) ? "0" : "") + tmpRand;
            Debug.Log(tmpSound);
            //this.ycManager.soundManager.PlayEffect(tmpSound);
        }

        public void TileSound()
        {
            if (this.canTilesSound)
            {
                this.canTilesSound = false;
                this.PlayEffect(SoundWaxime.SOUNDTILE, 0.4f);
            }
        }
    }
}
