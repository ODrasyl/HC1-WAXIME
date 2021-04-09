using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YsoCorp
{
    public class CameraController : MonoBehaviour
    {
        public float _timeChanging = 1;

        [SerializeField]
        private Transform _camPosMenuObject;
        [SerializeField]
        private Vector3 _camPosMenu;
        [SerializeField]
        private Vector3 _camPosPlaying;

        public bool _isChanging = false;
        private float _timer = 0.0f;
        private Vector3 _camPosOrigin;
        private Vector3 _camPosGoal;

        // Start is called before the first frame update
        void Start()
        {
            this._timer = this._timeChanging;
            this._camPosMenu = this._camPosMenuObject.position;
            this._camPosPlaying = this.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (this._timer > this._timeChanging)
            {
                this._isChanging = false;
                this.transform.position = this._camPosGoal;
            }
            if (this._isChanging)
            {
                this._timer += Time.deltaTime;
                this.transform.Translate((_camPosGoal - _camPosOrigin) * (Time.deltaTime / _timeChanging), Space.World);
            }
        }

        public void ChangePosition(bool state)
        {
            this._timer = this._timeChanging - this._timer;
            this._isChanging = true;
            if (state)
            {
                this._camPosOrigin = this._camPosMenu;
                this._camPosGoal = this._camPosPlaying;
            }
            else
            {
                this._camPosOrigin = this._camPosPlaying;
                this._camPosGoal = this._camPosMenu;
            }
        }
    }
}