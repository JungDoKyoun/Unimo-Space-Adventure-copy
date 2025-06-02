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

            //유물 추가하는 함수 나오면 추가
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

            Debug.Log($"{PlayerManager.PlayerStatus.maxHP}인 플레이어 맥스 HP가 {maxHp}만큼 변화");
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
            int maxFule = 100; //100자리에 맥스연료 받아올수 있으면 넣으면됨

            maxFule += eventEffect._value;

            int tmep = maxFule; //temp 자리에 맥스연료 받아올수 있으면 넣으면됨
            Debug.Log("맥스 연료 변화");
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

            Debug.Log("현재 연료 변화");
        }
    }
}
