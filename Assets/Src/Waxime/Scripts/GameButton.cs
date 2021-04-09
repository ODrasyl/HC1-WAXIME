using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YsoCorp
{
    public class GameButton : YCBehaviour
    {
        public Button bTriangle;
        public Button bSquare;
        public Button bCircle;
        public Button bBack;

        [SerializeField]
        private Sprite _cleanTriangle;
        [SerializeField]
        private Sprite _grungeTriangle;
        [SerializeField]
        private Sprite _cleanSquare;
        [SerializeField]
        private Sprite _grungeSquare;
        [SerializeField]
        private Sprite _cleanCircle;
        [SerializeField]
        private Sprite _grungeCircle;

        void Start()
        {
            this.CleanButton();
            this.bTriangle.onClick.AddListener(() =>
            {
                this.CleanButton();
                SetButtonScale(bTriangle);
                bTriangle.GetComponent<Image>().sprite = this._grungeTriangle;
                if (GameObject.FindObjectOfType<TileGame>() != null)
                    GameObject.FindObjectOfType<TileGame>().ChangeStateUse(TileStates.Triangle);
            });
            this.bSquare.onClick.AddListener(() =>
            {
                this.CleanButton();
                SetButtonScale(bSquare);
                bSquare.GetComponent<Image>().sprite = this._grungeSquare;
                if (GameObject.FindObjectOfType<TileGame>() != null)
                    GameObject.FindObjectOfType<TileGame>().ChangeStateUse(TileStates.Square);
            });
            this.bCircle.onClick.AddListener(() =>
            {
                this.CleanButton();
                SetButtonScale(bCircle);
                bCircle.GetComponent<Image>().sprite = this._grungeCircle;
                if (GameObject.FindObjectOfType<TileGame>() != null)
                    GameObject.FindObjectOfType<TileGame>().ChangeStateUse(TileStates.Circle);
            });
        }

        public void CleanButton()
        {
            Vector3 baseScale = new Vector3(1, 1, 1);
            this.bTriangle.gameObject.transform.localScale = baseScale;
            this.bTriangle.GetComponent<Image>().sprite = this._cleanTriangle;
            this.bSquare.gameObject.transform.localScale = baseScale;
            this.bSquare.GetComponent<Image>().sprite = this._cleanSquare;
            this.bCircle.gameObject.transform.localScale = baseScale;
            this.bCircle.GetComponent<Image>().sprite = this._cleanCircle;
        }

        public void SetButtonScale(Button bButton)
        {
            bButton.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

        public void ChangeStateTriangleNb(int nb)
        {
            Text text = this.bTriangle.gameObject.transform.Find("TNb").GetComponent<Text>();
            text.text = nb.ToString();
        }
        public void ChangeStateSquareNb(int nb)
        {
            Text text = this.bSquare.gameObject.transform.Find("TNb").GetComponent<Text>();
            text.text = nb.ToString();
        }
        public void ChangeStateCircleNb(int nb)
        {
            Text text = this.bCircle.gameObject.transform.Find("TNb").GetComponent<Text>();
            text.text = nb.ToString();
        }
    }
}