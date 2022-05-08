using System;
using UnityEngine;

namespace Title.Core
{
    public class BGMManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}