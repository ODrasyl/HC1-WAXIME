using UnityEngine;

namespace YsoCorp {

    [DefaultExecutionOrder(-1)]
    public class ResourcesManager : AResourcesManager {

        public Map forceMap;

        private Map[] _mapsTuto;
        private Map[] _maps;

        protected override void Awake() {
            base.Awake();
            //this._maps = this.LoadIterator<Map>("Maps/Map");
            this._mapsTuto = this.LoadIterator<Map>("WaximeMaps/MapTuto");
            this._maps = this.LoadIterator<Map>("WaximeMaps/Map");
        }

        public int GetMapNumber() {
            int level = this.dataManager.GetLevel();
            return level;
        }

        public Map GetMap() {
#if UNITY_EDITOR
            if (this.forceMap != null) {
                return this.forceMap;
            }
#endif
            int level = this.dataManager.GetLevel() - 1;
            if (level < this._mapsTuto.Length) {
                return this._mapsTuto[level % this._mapsTuto.Length];
            }
            return this._maps[Random.Range(0, this._maps.Length)];
        }

    }

}