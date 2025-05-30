using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            }
            else if (eventEffect._target == TargetType.MetaCurrency)
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(eventEffect._value);
            }
            else if(eventEffect._target == TargetType.Blueprint)
            {
                FirebaseDataBaseMgr.Instance.UpdateRewardBluePrint(eventEffect._value);
            }
        }
    }

    public class ChangeRelic : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            if (eventEffect._relicData == null)
                return;

            //유물 추가하는 함수 나오면 추가
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

            PlayerManager.PlayerStatus.currentHealth = currentHP;
        }
    }

    public class ChangeMaxFuel : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            int maxFule = 100; //100자리에 맥스연료 받아올수 있으면 넣으면됨

            maxFule += eventEffect._value;

            int tmep = maxFule; //temp 자리에 맥스연료 받아올수 있으면 넣으면됨
        }
    }

    public class ChangeCurrentFuel : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            int maxFule = 100; //100자리에 맥스연료 받아올수 있으면 넣으면됨

            int currentFuel = 20; //20자리에 현재연료 받아와 넣어야됨;

            currentFuel += eventEffect._value;

            int tmep = currentFuel;

            if(currentFuel >= maxFule)
            {
                currentFuel = maxFule;
            }
        }
    }
}
