using JDG;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public class TileEnvironmentManager : MonoBehaviour
    {
        private static TileEnvironmentManager _instance;
        [SerializeField] private List<TileEnvironmentSO> _tileEnvironmentSOs;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static TileEnvironmentManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TileEnvironmentManager>();
                }
                return _instance;
            }
        }

        public TileEnvironmentSO GetEnvironmentInfo(EnvironmentType environmentType)
        {
            return _tileEnvironmentSOs.Find(e => e.EnvironmentType == environmentType);
        }

        public EnvironmentType GetRandomEnvironment()
        {
            return _tileEnvironmentSOs[Random.Range(0, _tileEnvironmentSOs.Count)].EnvironmentType;
        }

        public List<EnvironmentType> GetAllEnvironmentTypes()
        {
            List<EnvironmentType> result = new List<EnvironmentType>();

            var values = System.Enum.GetValues(typeof(EnvironmentType));

            foreach (var value in values)
            {
                EnvironmentType type = (EnvironmentType)value;
                result.Add(type);
            }

            return result;
        }
    }
}
