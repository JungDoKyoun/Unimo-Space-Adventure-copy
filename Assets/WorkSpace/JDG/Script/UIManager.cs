using JDG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        [SerializeField] TopBarUI _topBarUI;
        [SerializeField] TileSelectionUI _tileSelectionUI;
        [SerializeField] ShopUI _shopUI;
        private bool _isUIOpen = false;

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
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UIManager>();
                }
                return _instance;
            }
        }
        public TopBarUI TopBarUI => _topBarUI;
        public TileSelectionUI TileSelectionUI => _tileSelectionUI;
        public ShopUI ShopUI => _shopUI;
        public bool IsUIOpen { get { return _isUIOpen; } set { _isUIOpen = value; } }
    }
}
