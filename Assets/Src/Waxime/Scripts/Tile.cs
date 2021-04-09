using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YsoCorp
{
    public enum TileStates
    {
        None,
        Triangle,
        Square,
        Circle,
    }

    public class Tile : MonoBehaviour
    {
        public TileStates _tileState = TileStates.None;
        public int _player = -1;
        public bool _isChange = false;

        [SerializeField]
        private Renderer _tileColor;

        [SerializeField]
        private GameObject _triangle;
        [SerializeField]
        private GameObject _square;
        [SerializeField]
        private GameObject _circle;
        [SerializeField]
        private Material _neutralColor;
        [SerializeField]
        private Material _playerColor;
        [SerializeField]
        private Material[] _enemyColor;

        [SerializeField]
        private Material _normalInfoColor;
        [SerializeField]
        private Material _changeInfoColor;

        private TileGame _tileGame;
        [SerializeField]
        private float _timeTile = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            this._tileGame = GameObject.FindObjectOfType<TileGame>();
            this.RefreshTileInfo();
        }

        // Update is called once per frame
        void Update()
        {
            if (this._timeTile > this._tileGame._targetTime)
            {
                this._isChange = false;
                this._timeTile = 0.0f;
                this._tileGame.ChangeAdjTiles(this);
                this.RefreshTileInfo();
            }
            if (this._tileGame._gameStarted && this._isChange)
            {
                this._timeTile += Time.deltaTime;
            }
        }

        private void RefreshTileInfo()
        {
            this.HideAllMenus();
            this.ChangeInfoColor();
            switch (this._tileState)
            {
                case TileStates.Triangle:
                    this._triangle.SetActive(true);
                    break;
                case TileStates.Square:
                    this._square.SetActive(true);
                    break;
                case TileStates.Circle:
                    this._circle.SetActive(true);
                    break;
                default:
                    break;
            }
            if (this._player == 0)
                this._tileColor.material = this._playerColor;
            else if (this._player > 0 && this._player < this._tileGame._nbPlayer)
            {
                this._tileColor.material = this._enemyColor[(this._player - 1) % this._enemyColor.Length];
            }
            else
                this._tileColor.material = this._neutralColor;
        }

        private void ChangeInfoColor()
        {
            if (this._isChange)
            {
                this._triangle.GetComponent<Renderer>().material = this._changeInfoColor;
                this._square.GetComponent<Renderer>().material = this._changeInfoColor;
                this._circle.GetComponent<Renderer>().material = this._changeInfoColor;
            }
            else
            {
                this._triangle.GetComponent<Renderer>().material = this._normalInfoColor;
                this._square.GetComponent<Renderer>().material = this._normalInfoColor;
                this._circle.GetComponent<Renderer>().material = this._normalInfoColor;
            }
        }

        private void HideAllMenus()
        {
            this._triangle.SetActive(false);
            this._square.SetActive(false);
            this._circle.SetActive(false);
        }

        public void SelectedTile()
        {
            if (this._tileGame._stateUse == TileStates.None || this._tileGame._stateUse == this._tileState)
                return;
            if ((this._tileState == TileStates.None && this._player == -1) ||
                this._player == 0)
            {
                this._tileGame.DecreaseStateChangeNb();
                this.ChangeTileState(true, this._tileGame._stateUse, 0);
                this._isChange = true;
            }
        }

        public void ChangeTileState(bool isChange, TileStates newState, int newPlayer)
        {
            this._isChange = isChange;
            this._tileState = newState;
            this._player = newPlayer;
            this.RefreshTileInfo();
        }
    }
}