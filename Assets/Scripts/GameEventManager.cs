using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameEventManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _assembledObjects;
        [SerializeField] private GameObject _animatedEngine;
        //[SerializeField] private float waitTimer = 10.0f;
        public bool isAssemblyComplete;
        private bool _isObjectDestroyedBeforeAnimation = false;
        //private  float waitTime;
        

        private void Awake()
        {
            _animatedEngine.SetActive(false);
            //waitTime = 0.0f;
        }

        private void LateUpdate()
        {
            if (!isAssemblyComplete) return;
            
            /*
            if (waitTime < waitTimer)
            {
                waitTime += Time.deltaTime;
            }
            */
            
            if (_isObjectDestroyedBeforeAnimation) return;
            foreach (var part in _assembledObjects)
            {
                part.SetActive(false);
                Destroy(part);
            }
            _isObjectDestroyedBeforeAnimation = true;
            _animatedEngine.SetActive(true);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}