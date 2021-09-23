using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Button : MonoBehaviour
    {
        [SerializeField] private Transform _buttonMesh;

        public UnityEvent onButtonPress;
        public UnityEvent onButtonReleased;
        private bool _isButtonPressed = false;//Course object at the start makes it true
        private void OnTriggerEnter(Collider other)
        {


            if (other.gameObject.GetComponent<XRBaseControllerInteractor>()) return;
            if (!_isButtonPressed)
            {
            
                _buttonMesh.localScale -= new Vector3(0f, 0.75f, 0.0f);
                _buttonMesh.localPosition -= new Vector3(0f, 0.01f, 0.0f);
                _isButtonPressed = true;    
                if (gameObject.TryGetComponent<CapsuleCollider>(out var buttonCollider))
                {
                    buttonCollider.center -= new Vector3(0f, 0.02f, 0.0f);
                }
                
                onButtonPress.Invoke();

            }
            else
            {
                _buttonMesh.localScale += new Vector3(0f, 0.75f, 0.0f);
                _buttonMesh.localPosition += new Vector3(0f, 0.01f, 0.0f);
                _isButtonPressed = false;   
                if (gameObject.TryGetComponent<CapsuleCollider>(out var buttonCollider))
                {
                    buttonCollider.center += new Vector3(0f, 0.02f, 0.0f);
                }
                
                onButtonReleased.Invoke();
            }
            
        }
    }