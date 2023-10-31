using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SyntheticCardLibrary.Core {
    public class SyntheticCard : CardInfo
    {
        internal string source;
        internal bool temperary;
        internal string reinstantaionSrting;
        internal Func<SyntheticCard, GameObject> artFunction;
    }
}
