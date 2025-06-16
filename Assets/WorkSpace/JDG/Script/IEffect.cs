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
                Debug.Log($"�ΰ��� {eventEffect._value} ��ŭ ��ȭ");
            }
            else if (eventEffect._target == TargetType.MetaCurrency)
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(eventEffect._value);
                Debug.Log($"��Ÿ {eventEffect._value} ��ŭ ��ȭ");
            }
            else if(eventEffect._target == TargetType.Blueprint)
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardBluePrint(eventEffect._value);
                Debug.Log($"���赵 {eventEffect._value} ��ŭ ��ȭ");
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
            
            Debug.Log("���� �߰�");
        }
    }

    public class ChangeMaxHP : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            if (PlayerManager.PlayerStatus == null)
                return;

            float maxHp = PlayerManager.PlayerStatus.maxHealth;
            float currentHP = PlayerManager.PlayerStatus.currentHealth;

            maxHp += eventEffect._value;

            PlayerManager.PlayerStatus.maxHealth = maxHp;
            PlayerEvents.ChangeHP(maxHp, currentHP);
        }
    }

    public class ChangeCurrentHP : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            if (PlayerManager.PlayerStatus == null)
                return;

            float maxHp = PlayerManager.PlayerStatus.maxHealth;

            float currentHP = PlayerManager.PlayerStatus.currentHealth;

            currentHP += eventEffect._value;

            if(currentHP >= maxHp)
            {
                currentHP = maxHp;
            }

            PlayerManager.PlayerStatus.currentHealth = currentHP;
            PlayerEvents.ChangeHP(maxHp, currentHP);
        }
    }

    public class ChangeMaxFuel : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            float maxFule = PlayerFuelManager.MaxFuel;
            float currentFuel = PlayerFuelManager.Fuel;

            maxFule += eventEffect._value;

            PlayerFuelManager.MaxFuel = maxFule;
            PlayerEvents.ChangeFuel(maxFule, currentFuel);
        }
    }

    public class ChangeCurrentFuel : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            float maxFule = PlayerFuelManager.MaxFuel; 
            float currentFuel = PlayerFuelManager.Fuel;

            currentFuel += eventEffect._value;

            if(currentFuel >= maxFule)
            {
                currentFuel = maxFule;
            }

            PlayerFuelManager.Fuel = currentFuel;
            PlayerEvents.ChangeFuel(maxFule, currentFuel);
        }
    }
}
