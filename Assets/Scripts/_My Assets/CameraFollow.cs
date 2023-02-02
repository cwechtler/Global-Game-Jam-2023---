using UnityEngine;

namespace RPG.CameraUI {
    public class CameraFollow : MonoBehaviour {

        private GameObject player;

        void Start() {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void LateUpdate() {
            if (player != null) {
                this.transform.position = new Vector3(player.transform.position.x, 3, -10);
            }
        }
    }
}
