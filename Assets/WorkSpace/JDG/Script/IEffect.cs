using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZL.Unity.Unimo;

namespace JDG
{
    public interface IEffect
    {
        public void Execute(EventEffect eventEffect);
    }

    public class ChangeResource : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            if(eventEffect._target == TargetType.IngameCurrency)
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(eventEffect._value);
                Debug.Log($"인게임 {eventEffect._value} 만큼 변화");
            }
            else if (eventEffect._target == TargetType.MetaCurrency)
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(eventEffect._value);
                Debug.Log($"메타 {eventEffect._value} 만큼 변화");
            }
            else if(eventEffect._target == TargetType.Blueprint)
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardBluePrint(eventEffect._value);
                Debug.Log($"설계도 {eventEffect._value} 만큼 변화");
            }
        }
    }

    public class ChangeRelic : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            if (eventEffect._relicData == null)
                return;

            if(eventEffect._choiceEffectType == ChoiceEffectType.Useful)
            {
                PlayerInventoryManager.AddRelic(eventEffect._relicData);
            }
            else if(eventEffect._choiceEffectType == ChoiceEffectType.Harmful)
            {
                PlayerInventoryManager.RemoveRelic(eventEffect._relicData);
            }
            
            Debug.Log("유물 추가");
        }
    }

    public class ChangeMaxHP : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            if (PlayerManager.PlayerStatus == null)
                return;

            float maxHp = PlayerManager.PlayerStatus.maxHP;

            maxHp += eventEffect._value;

            PlayerManager.PlayerStatus.maxHP = maxHp;
            Debug.Log(PlayerManager.PlayerStatus.maxHP);
        }
    }

    public class ChangeCurrentHP : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            if (PlayerManager.PlayerStatus == null)
                return;

            float maxHp = PlayerManager.PlayerStatus.maxHP;

            float currentHP = PlayerManager.PlayerStatus.currentHealth;

            currentHP += eventEffect._value;

            if(currentHP >= maxHp)
            {
                currentHP = maxHp;
            }

            Debug.Log($"{PlayerManager.PlayerStatus.currentHealth}인 플레이어 현재 HP가 {currentHP}만큼 변화");
            PlayerManager.PlayerStatus.currentHealth = currentHP;
            Debug.Log(PlayerManager.PlayerStatus.currentHealth);
        }
    }

    public class ChangeMaxFuel : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            float maxFule = PlayerFuelManager.FuelMax;

            maxFule += eventEffect._value;

            PlayerFuelManager.FuelMax = maxFule;
        }
    }

    public class ChangeCurrentFuel : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            float maxFule = PlayerFuelManager.FuelMax; 

            float currentFuel = PlayerFuelManager.Fuel;

            currentFuel += eventEffect._value;

            if(currentFuel >= maxFule)
            {
                currentFuel = maxFule;
            }

            PlayerFuelManager.Fuel = currentFuel;
        }
    }
}
