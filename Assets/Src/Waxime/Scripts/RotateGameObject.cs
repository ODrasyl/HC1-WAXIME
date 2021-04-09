using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    public bool _isRotate = true;
    public Vector3 _rotateVector = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRotate)
        {
            Vector3 tmpVector = _rotateVector * Time.deltaTime;
            this.transform.Rotate(tmpVector);
        }
    }
}
