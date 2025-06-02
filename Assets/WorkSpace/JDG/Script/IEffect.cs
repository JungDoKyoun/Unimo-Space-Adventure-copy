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

            //���� �߰��ϴ� �Լ� ������ �߰�
            Debug.Log("���� �߰�");
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

            Debug.Log($"{PlayerManager.PlayerStatus.maxHP}�� �÷��̾� �ƽ� HP�� {maxHp}��ŭ ��ȭ");
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

            Debug.Log($"{PlayerManager.PlayerStatus.currentHealth}�� �÷��̾� ���� HP�� {currentHP}��ŭ ��ȭ");
            PlayerManager.PlayerStatus.currentHealth = currentHP;
            Debug.Log(PlayerManager.PlayerStatus.currentHealth);
        }
    }

    public class ChangeMaxFuel : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            int maxFule = 100; //100�ڸ��� �ƽ����� �޾ƿü� ������ �������

            maxFule += eventEffect._value;

            int tmep = maxFule; //temp �ڸ��� �ƽ����� �޾ƿü� ������ �������
            Debug.Log("�ƽ� ���� ��ȭ");
        }
    }

    public class ChangeCurrentFuel : IEffect
    {
        public void Execute(EventEffect eventEffect)
        {
            int maxFule = 100; //100�ڸ��� �ƽ����� �޾ƿü� ������ �������

            int currentFuel = 20; //20�ڸ��� ���翬�� �޾ƿ� �־�ߵ�;

            currentFuel += eventEffect._value;

            int tmep = currentFuel;

            if(currentFuel >= maxFule)
            {
                currentFuel = maxFule;
            }

            Debug.Log("���� ���� ��ȭ");
        }
    }
}
