using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO interactEventChannel;
    [SerializeField] private float checkRate = 0.05f;
    [SerializeField] private float maxCheckDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject currInteractGameObject;
    [SerializeField] private GameObject pannel;
    [SerializeField] private TextMeshProUGUI promptText;
    
    private float _lastCheckTime;
    private IInteractable _currInteractable;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    
    void Update()
    {
        if (Time.time - _lastCheckTime > checkRate)
        {
            _lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != currInteractGameObject)
                {
                    currInteractGameObject = hit.collider.gameObject;
                    _currInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                    interactEventChannel.OnEventRaised += _currInteractable.OnInteract;
                }
            }
            else
            {
                interactEventChannel.Clear();
                currInteractGameObject = null;
                _currInteractable = null;
                pannel.SetActive(false);
            }
        }
    }
    
    private void SetPromptText()
    {
        pannel.SetActive(true);
        promptText.text = _currInteractable.GetInteractPrompt();
    }
}
