using System;
using UnityEngine;

namespace YsoCorp {
    public class Game : YCBehaviour {

        public enum States {
            None,
            Home,
            Playing,
            Lose,
            Win,
        }

        private States _previousState = States.None;
        private States _state = States.None;
        public States state {
            get {
                return this._state;
            }
            set {
                if (this._state != value)
                {
                    this._previousState = this._state;
                    this._state = value;
                    this.soundWaxime.ChangeMusic();
                    if (this.onStateChanged != null) {
                        this.onStateChanged(value);
                    }
                    if (value == States.Home)
                    {
                        if (this._previousState != States.None)
                            this.soundWaxime.PlayEffect(SoundWaxime.SOUNDHOME);
                        this.HideAllMenus();
                        this.menuHome.Display();
                        this.Reset();
                    } else if (value == States.Playing)
                    {
                        this.soundWaxime.PlayEffect(SoundWaxime.SOUNDPLAY);
                        this.ycManager.OnGameStarted(this.dataManager.GetLevel());
                        this.HideAllMenus();
                        this.menuGame.Display();
                        this.gameButton.CleanButton();
                    } else if (value == States.Lose)
                    {
                        this.soundWaxime.PlayEffect(SoundWaxime.SOUNDLOOSE);
                        this.ycManager.OnGameFinished(false);
                        this.HideAllMenus();
                        this.menuLose.Display();
                    } else if (value == States.Win)
                    {
                        this.soundWaxime.PlayEffect(SoundWaxime.SOUNDVICTORY);
                        this.ycManager.OnGameFinished(true);
                        this.dataManager.NextLevel();
                        this.HideAllMenus();
                        this.menuWin.Display();
                    }
                }
            }
        }

        public MenuHome menuHome;
        public MenuGame menuGame;
        public MenuLose menuLose;
        public MenuWin menuWin;

        public GameButton gameButton;

        public Transform trash;

        public event Action<States> onStateChanged;

        public Map map { get; set; } = null;
        public Map previousMap;
        public GameObject playButton;

        [SerializeField]
        private CameraController _camera;

        private void Start() {

            this.game.state = States.Home;
            this.playButton = Instantiate(this.playButton, this.transform);
            this.playButton.SetActive(false);
        }

        void Update()
        {
            //if (this._state != States.Home && this._previousState != this._state)
                //this._previousState = this._state;
            if (this.game.state == States.Home && !this.playButton.activeSelf)
            {
                this.playButton.SetActive(true);
                this.map.gameObject.SetActive(false);
                this._camera.ChangePosition(false);
            }
            else if(this.game.state != States.Home && this.playButton.activeSelf)
            {
                this.playButton.SetActive(false);
                this.map.gameObject.SetActive(true);
                this._camera.ChangePosition(true);
            }
        }

        public void Win() {
            this.game.state = States.Win;
        }

        public void Lose() {
            this.game.state = States.Lose;
        }

        private void ResetTrash() {
            if (this.trash != null) {
                DestroyImmediate(this.trash.gameObject);
                DestroyImmediate(this.map?.gameObject);
            }
            this.trash = new GameObject().transform;
            this.trash.name = "Trash";
        }

        public void Reset() {
            this.ResetTrash();

            this.CheckKeepMap();
            this.map = Instantiate(previousMap, this.transform).GetComponent<Map>();
            if (this.player != null)
                this.player.Reset();
            this.GetComponent<PanController>().Reset();
        }

        public void CheckKeepMap()
        {
            if (_previousState == States.None || _previousState == States.Win)
            {
                Map tmpMap;
                int i = 0;
                while ((tmpMap = this.resourcesManager.GetMap()) == this.previousMap)
                {
                    if (i > 10)
                        break;
                    i += 1;
                }
                this.previousMap = tmpMap;
            }
        }

        void HideAllMenus() {
            this.menuHome.Hide();
            this.menuGame.Hide();
            this.menuLose.Hide();
            this.menuWin.Hide();
        }

    }
}