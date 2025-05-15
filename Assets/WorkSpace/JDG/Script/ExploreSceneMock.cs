using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{

    public class ExploreSceneMock : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(a());
        }

        private IEnumerator a()
        {
            Debug.Log("Ω√¿€");
            yield return new WaitForSeconds(5f);

            SceneLoader.Instance.ClearTile();
            SceneLoader.Instance.ReturnToWorldMap();
        }
    }
}
