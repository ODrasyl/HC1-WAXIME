using UnityEngine.UI;

namespace YsoCorp {

    public class MenuGame : AMenu {

        public Button bBack;
        public Button bReload;
        public Joystick joystick;

        void Start() {
            this.bBack.onClick.AddListener(() => {
                this.ycManager.adsManager.ShowInterstitial(() => {
                    this.game.state = Game.States.Home;
                });
            });
            this.bReload.onClick.AddListener(() => {
                this.ycManager.adsManager.ShowInterstitial(() => {
                    this.game.state = Game.States.Home;
                    this.game.state = Game.States.Playing;
                });
            });
        }

    }

}
