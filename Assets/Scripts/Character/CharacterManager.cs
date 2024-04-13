using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UU
{
    public class CharacterManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}