using UnityEngine;

using ZL.Unity.Unimo;

public partial class PlayerManager 
{
    private void OnEnable()
    {
        MonsterManager.Instance.Target = transform;
    }

    private void OnDisable()
    {
        if (MonsterManager.Instance != null)
        {
            MonsterManager.Instance.Target = null;
        }
    }

    private void Start()
    {
        ActionStart();

        MoveStart();
        StatusStart();
    }

    private void Update()
    {
        ActionUpdate();

        MoveUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        Debug.Log(LayerMask.NameToLayer("Energy"));
        if (other.gameObject.layer == LayerMask.NameToLayer("Energy"))
        {
            Debug.Log("tri");
            GetEnergy(3);

            Destroy(other.gameObject);
        }
    }
}