using SyntheticCardLibrary.Utilities;
using System;
using System.Reflection;
using UnboundLib.Utils;
using UnboundLib;
using UnityEngine;
using HarmonyLib;
using System.Linq;

namespace SyntheticCardLibrary.Core {
    public class CardBuilder
    {
        public static void CreateHiddenCard(string name, CardInfo.Rarity rarity, CardThemeColor.CardThemeColorType theme, string description = "", CardInfoStat[] stats = null, GameObject cardArt = null, Func<SyntheticCard, GameObject> cardArtGenerator = null, GameObject cardBase = null,
            bool allowMultiple = true, CardCategory[] categories = null, CardCategory[] blacklistedCategories = null, Func<SyntheticCard, object[]> instantiationParams = null, bool temparary = false, bool doReassign = true,
             Action<SyntheticCard> buildCallback = null, Action<SyntheticCard, Player> addAction = null, Action<SyntheticCard, Player> removeAction = null, Action<SyntheticCard, Player> readdAction = null, string reinstaionSrting = "", Type baseInfo =  null) {
            if(baseInfo!= null && !baseInfo.IsSubclassOf(typeof(SyntheticCard)))
                throw new SyntheticCardError("Using baseInfo that does not extend SyntheticCard");
            GameObject location = PoolHandler.GetNextHolder(false, temparary);
            SyntheticCard card = (SyntheticCard)location.GetComponent(baseInfo ?? typeof(SyntheticCard));
            card.source = Assembly.GetCallingAssembly().FullName;
            card.source = card.source.Substring(0, card.source.IndexOf(","));
            card.cardName = name;
            card.rarity = rarity;
            card.colorTheme = theme;
            card.cardDestription = description;
            card.cardStats = stats ?? new CardInfoStat[0];
            card.temperary = temparary;
            card.artFunction = cardArtGenerator;
            card.allowMultiple = allowMultiple;
            

            buildCallback?.Invoke(card);
            location.GetComponents<MonoBehaviour>().Where(c => !c.GetType().IsAssignableFrom(typeof(SyntheticCard))).ToList().ForEach(UnityEngine.Object.Destroy);
        }

        public static void CreateCardpoolCard(string name, CardInfo.Rarity rarity, CardThemeColor.CardThemeColorType theme, bool temparary = false, Func<SyntheticCard, GameObject> cardArtGenerator = null, Func<SyntheticCard, object[]> instantiationParams = null,
            Action<SyntheticCard> buildCallback = null, Action<SyntheticCard, Gun, ApplyCardStats, CharacterStatModifiers, Block> setUpAction = null, 
            Action<SyntheticCard, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers> addAction = null, 
            Action<SyntheticCard, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers> removeAction = null, 
            Action<SyntheticCard, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers> readdAction = null, string reinstaionSrting = "", Type baseInfo = null) {
            if(baseInfo != null && !baseInfo.IsSubclassOf(typeof(SyntheticCard)))
                throw new SyntheticCardError("Using baseInfo that does not extend SyntheticCard");
            GameObject location = PoolHandler.GetNextHolder(true, temparary);
            SyntheticCard card = (SyntheticCard)location.GetComponent(baseInfo ?? typeof(SyntheticCard));
            card.source = Assembly.GetCallingAssembly().FullName;
            card.source = card.source.Substring(0, card.source.IndexOf(","));
            card.cardName = name;
            card.rarity = rarity;
            card.colorTheme = theme;
            card.temperary = temparary;
            card.artFunction = cardArtGenerator;
            if(!temparary)
                CardManager.cards.Add(location.name, new Card("Synthetic: " +card.source, Unbound.config.Bind("SyntheticCards: " + card.source, card.name, true) , card));
            else
                CardChoice.instance.cards = (CardInfo[])CardChoice.instance.cards.AddItem(card);

            buildCallback?.Invoke(card);
        }

        public static void RegesterReinstaionCallback(Func<string,string, bool> callback /*CardName ReinstaionSrting Built?*/) {

        }
    }
}
