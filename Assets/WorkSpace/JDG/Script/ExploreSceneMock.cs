using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;

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
            yield return new WaitForSeconds(5f);

            SceneLoader.Instance.ClearTile();
            SceneLoader.Instance.ReturnToWorldMap();
        }
    }
}
