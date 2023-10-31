using System.Collections;
using System.Collections.Generic;
using UnboundLib.GameModes;
using UnityEngine;
using System.Linq;

namespace SyntheticCardLibrary.Utilities {
    internal class PoolHandler:MonoBehaviour {
        internal static PoolHandler instance;
        public Transform PermenentCardPool;
        internal int PermenentCardCount=0;
        public Transform FleatingCardPool;
        internal int FleatingCardCount=0;
        public Transform HiddenPool;

        internal static GameObject GetNextHolder(bool inPool, bool temparary) {
            if(!inPool) return instance.HiddenPool.gameObject;
            if(temparary) return instance.FleatingCardPool.GetChild(instance.FleatingCardCount++).gameObject;
            return instance.PermenentCardPool.GetChild(instance.PermenentCardCount++).gameObject;
        }

        internal static IEnumerator HookInitStart(IGameModeHandler _) {
            for(int i = 0; i < instance.FleatingCardCount; i++) {
                instance.FleatingCardPool.GetChild(i).gameObject.GetComponents<MonoBehaviour>()
                    .ToList().ForEach(Destroy);
            }
            instance.FleatingCardCount = 0;
            yield break;
        }

    }
}
