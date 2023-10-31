using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SyntheticCardLibrary.Utilities {
	public class SyntheticCardError : Exception
    {
		internal SyntheticCardError(string name):base(name) { }
    }
}
