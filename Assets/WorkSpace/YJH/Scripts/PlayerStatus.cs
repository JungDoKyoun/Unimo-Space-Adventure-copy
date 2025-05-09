using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager
{
    [SerializeField] float playerHP;//ü�� �ʿ� ����?
    bool isOnHit = false;//�¾Ҵ���?
    [SerializeField] float onHitTime = 1f;//�����ð�
    [SerializeField] float onHitBlinkTime = 0.5f;// �����ð����� �����̴� ����
    [SerializeField] SkinnedMeshRenderer cartRenderer;
    [SerializeField] SkinnedMeshRenderer bodyRenderer;
    [SerializeField] SkinnedMeshRenderer faceRenderer;

    public float PlayerHP
    {
        get {  return playerHP; }
        set 
        {
            if (value < 0)
            {
                playerHP = 0;
            }
            else
            {
                playerHP = value;
            }
        }
    }
    public void PlayerGetDemage(float dmg)
    {
        if (isOnHit == false)// �ǰ����� �ƴҶ� ������ �Լ� ȣ�� ��Ŀ� ���� �� ���� ������ �� ���� ���� 
        {
            
            playerHP -= dmg;//������ ����

            if (playerHP < 0)
            {
                playerHP = 0;

            }
            else
            {
                StartCoroutine(PlayerBlink());
            }
        }
        else
        {
            return;//�ǰ����� ��
        }
    }
    IEnumerator PlayerBlink()
    {
        BlinkRenderer();
        yield return new WaitForSeconds(onHitBlinkTime);
        BlinkRenderer();
        yield return new WaitForSeconds(onHitBlinkTime);
        BlinkRenderer();
        yield break;
    }
    public void BlinkRenderer()
    {
        if(bodyRenderer.enabled == false)
        {
            bodyRenderer.enabled = true;
        }
        else
        {
            bodyRenderer.enabled = false;
        }

        if (faceRenderer.enabled == false)
        {
            faceRenderer.enabled = true;
        }
        else
        {
            faceRenderer.enabled = false;
        }

        if (cartRenderer.enabled == false)
        {
            cartRenderer.enabled = true;
        }
        else
        {
            cartRenderer.enabled = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        float tempdmg = 1f;
        if (false&&isOnHit==false)//������ �ǰݽ� 
        {
            isOnHit = true;
            PlayerGetDemage(tempdmg);
        }
        else
        {

        }
            
    }



}
