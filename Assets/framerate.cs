using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class framerate : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}
