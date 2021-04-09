using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YsoCorp
{
    public class TileGame : YCBehaviour
    {
        [SerializeField]
        public int _nbPlayer = 2;

        public TileStates _stateUse = TileStates.None;
        public int[] _stateChangeNb = new int[3] { 0, 0, 0 };

        public float _targetTime = 1.0f;

        public bool _gameStarted = false;

        public GameButton _gameButton;

        // Start is called before the first frame update
        void Start()
        {
            if (Resources.FindObjectsOfTypeAll<GameButton>().Length > 0)
                this._gameButton = Resources.FindObjectsOfTypeAll<GameButton>()[0];
            this.ShowStateChangeNb();
        }

        // Update is called once per frame
        void Update()
        {
            this.CheckWinLose();
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Touch touch = Input.GetTouch(0);
                Camera cam = GameObject.FindObjectOfType<Camera>();
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(touch.position);
                if (CheckStateChangeNb(_stateUse) && Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.GetComponent<Tile>() != null)
                        {
                            this._gameStarted = true;
                            Tile tile = hit.collider.GetComponent<Tile>();
                            tile.SelectedTile();
                        }
                    }
                }
            }
        }

        public void ChangeAdjTiles(Tile tileOrigin)
        {
            Vector3 posOrigin = tileOrigin.gameObject.transform.position;
            posOrigin.y = 0.0f;
            Tile[] tiles = GameObject.FindObjectsOfType<Tile>();
            foreach (Tile tile in tiles)
            {
                Vector3 pos = tile.gameObject.transform.position;
                pos.y = 0.0f;
                if (tile == tileOrigin ||
                    Vector3.Distance(posOrigin, pos) > 11 * this.gameObject.transform.localScale[0])
                {
                    continue;
                }
                if (this.CheckRockPaperScissors(tileOrigin._tileState, tile._tileState))
                {
                    tile.ChangeTileState(true, tileOrigin._tileState, tileOrigin._player);
                }
            }
        }

        private bool CheckRockPaperScissors(TileStates player, TileStates enemy)
        {
            if (enemy == TileStates.None)
                return true;
            switch (player)
            {
                case TileStates.Triangle:
                    if (enemy == TileStates.Circle)
                        return true;
                    break;
                case TileStates.Square:
                    if (enemy == TileStates.Triangle)
                        return true;
                    break;
                case TileStates.Circle:
                    if (enemy == TileStates.Square)
                        return true;
                    break;
                default:
                    break;
            }
            return false;
        }

        private void CheckWinLose()
        {
            int nbOwnedTile = 0;
            int nbNeutralTile = 0;
            int nbIsChange = 0;
            Tile[] tiles = GameObject.FindObjectsOfType<Tile>();
            foreach (Tile tile in tiles)
            {
                if (tile._player == 0)
                    nbOwnedTile += 1;
                if (tile._player < 0 || tile._player >= this._nbPlayer)
                    nbOwnedTile += 1;
                if (tile._isChange)
                    nbIsChange += 1;
            }
            if (nbOwnedTile == tiles.Length)
                this.game.Win();
            else if ((this._gameStarted && nbOwnedTile == 0 && nbNeutralTile == 0) ||
                (nbIsChange == 0 && !this.CheckStateChangeAllNb()))
                this.game.Lose();
        }

        public bool CheckStateChangeAllNb()
        {
            if (this.CheckStateChangeNb(TileStates.Triangle) ||
                this.CheckStateChangeNb(TileStates.Square) ||
                this.CheckStateChangeNb(TileStates.Circle))
                return true;
            return false;
        }

        public bool CheckStateChangeNb(TileStates state)
        {
            if (state == TileStates.Triangle && this._stateChangeNb.Length >= 1 && this._stateChangeNb[0] > 0)
                return true;
            if (state == TileStates.Square && this._stateChangeNb.Length >= 2 && this._stateChangeNb[1] > 0)
                return true;
            if (state == TileStates.Circle && this._stateChangeNb.Length >= 3 && this._stateChangeNb[2] > 0)
                return true;
            return false;
        }

        public int GetStateChangeNb(TileStates state)
        {
            if (state == TileStates.Triangle && this._stateChangeNb.Length >= 1)
                return this._stateChangeNb[0];
            if (state == TileStates.Square && this._stateChangeNb.Length >= 2)
                return this._stateChangeNb[1];
            if (state == TileStates.Circle && this._stateChangeNb.Length >= 3)
                return this._stateChangeNb[2];
            return 0;
        }

        public void DecreaseStateChangeNb()
        {
            switch (this._stateUse)
            {
                case TileStates.Triangle:
                    if (this._stateChangeNb.Length >= 1)
                        this._stateChangeNb[0] -= 1;
                    break;
                case TileStates.Square:
                    if (this._stateChangeNb.Length >= 2)
                        this._stateChangeNb[1] -= 1;
                    break;
                case TileStates.Circle:
                    if (this._stateChangeNb.Length >= 3)
                        this._stateChangeNb[2] -= 1;
                    break;
                default:
                    break;
            }
            this.ShowStateChangeNb();
        }

        public void ChangeStateUse(TileStates newState)
        {
            this._stateUse = newState;
        }

        public void ShowStateChangeNb()
        {
            this._gameButton.ChangeStateTriangleNb(this.GetStateChangeNb(TileStates.Triangle));
            this._gameButton.ChangeStateSquareNb(this.GetStateChangeNb(TileStates.Square));
            this._gameButton.ChangeStateCircleNb(this.GetStateChangeNb(TileStates.Circle));
        }
    }
}
