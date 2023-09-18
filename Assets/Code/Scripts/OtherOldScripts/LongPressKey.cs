//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Scripts {
    public class LongPressKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        private bool pointerDown;
        private float pointerDownTimer;

        [SerializeField]
        private float requiredHoldTime;

        public UnityEvent onLongClick;

        [SerializeField] private Image fillImage;

        [SerializeField] private GameObject puzzlePanel;

        //[SerializeField] PuzzleManager puzzleManager;



        private void OnEnable()
        {

        }
        public void OnPointerDown(PointerEventData eventData)
        {
            pointerDown = true;
            Debug.Log("OnPointerDown");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Reset();
            Debug.Log("OnPointerUp");
        }
        // Comment:
        private void KeyCheck()
        {
            bool isKeyDown = Input.GetKey(KeyCode.F);
            if (isKeyDown)
            {
                pointerDownTimer += Time.deltaTime;
                if(pointerDownTimer >= requiredHoldTime)
                {
                    if(onLongClick != null)
                    {
                        onLongClick.Invoke();
                    }
                }
            }
        }

        private void Update()
        {
            ClickControl();
        }

        private void ClickControl()
        {
            if(pointerDown)
            {
                pointerDownTimer += Time.deltaTime;
                if(pointerDownTimer >= requiredHoldTime)
                {
                    if(onLongClick != null)
                        onLongClick.Invoke();

                    DisableButton();
                    ShowPanelUp();
                    //puzzleManager.EventAssignation();
                    //Reset();

                }
                fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
            }
        }

        private void ShowPanelUp()
        {
            puzzlePanel.SetActive(true);
            //puzzleManager.PuzzlePanelShowUp();
            //PuzzleManager.instance.ShowPuzzleUp();
        }

        private void DisableButton()
        {
            gameObject.SetActive(false);
        }

        private void Reset()
        {
            pointerDown = false;
            pointerDownTimer = 0;
            fillImage.fillAmount = pointerDownTimer;
        }
    }
}
