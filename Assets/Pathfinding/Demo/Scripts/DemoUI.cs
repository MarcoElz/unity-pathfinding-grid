using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ignita.Pathfinding.Demo
{
    /// <summary>
    /// Controls any communication to the UI
    /// </summary>
    public class DemoUI : MonoBehaviour
    {
        [SerializeField] Text title;

        private void Start()
        {
            DemoManager.Instance.onAlgorithmChange += (a) => title.text = a.ToString();
        }

    }
}