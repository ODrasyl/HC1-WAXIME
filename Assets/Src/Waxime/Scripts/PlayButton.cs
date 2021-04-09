using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YsoCorp
{
    public class PlayButton : YCBehaviour
    {
        // Start is called before the first frame update
        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Touch touch = Input.GetTouch(0);
                Camera cam = GameObject.FindObjectOfType<Camera>();
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.GetComponent<PlayButton>() == this)
                    {
                        this.Play();
                    }
                }
            }
        }

        // Update is called once per frame
        public void Play()
        {
            this.game.state = Game.States.Playing;
        }
    }
}
