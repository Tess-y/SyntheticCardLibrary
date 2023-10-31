using System.Collections;
using System.Collections.Generic;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using System;

namespace SyntheticCardLibrary.Core {
	public class UnboundSyntheticCard : CustomCard
	{
        Action<SyntheticCard, Gun, ApplyCardStats, CharacterStatModifiers, Block> setUpAction;
        Action<SyntheticCard, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers> addAction;
        Action<SyntheticCard, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers> reassigAction;
        Action<SyntheticCard, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers> removeAction;
        private void Awake() {
            cardInfo = gameObject.GetOrAddComponent<CardInfo>();
            gun = gameObject.GetOrAddComponent<Gun>();
            cardStats = gameObject.GetOrAddComponent<ApplyCardStats>();
            statModifiers = gameObject.GetOrAddComponent<CharacterStatModifiers>();
            block = gameObject.GetOrAddComponent<Block>();
            SetupCard(cardInfo, gun, cardStats, statModifiers, block);
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block) {
            SyntheticCard synthCardInfo = (SyntheticCard)cardInfo;
            synthCardInfo.cardArt = synthCardInfo.artFunction == null ? synthCardInfo.cardArt : synthCardInfo.artFunction.Invoke(synthCardInfo);
            setUpAction?.Invoke(synthCardInfo, gun, cardStats, statModifiers, block);
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats) {
            addAction?.Invoke(GetComponent<SyntheticCard>(), player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }
        public override void OnReassignCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats) {
            reassigAction?.Invoke(GetComponent<SyntheticCard>(), player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats) {
            removeAction?.Invoke(GetComponent<SyntheticCard>(), player, gun, gunAmmo, data, health, gravity, block, characterStats);
        }

        protected override GameObject GetCardArt() {
            throw new System.NotImplementedException();
        }

        protected override string GetDescription() {
            throw new System.NotImplementedException();
        }

        protected override CardInfo.Rarity GetRarity() {
            throw new System.NotImplementedException();
        }

        protected override CardInfoStat[] GetStats() {
            throw new System.NotImplementedException();
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            throw new System.NotImplementedException();
        }

        protected override string GetTitle() {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
	    {
	        
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	}
}
