using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public GameObject DialogObject;
    public bool IsDialogOpen;

    private GameObject _currentDialog;
    public Dialog CurrentDialog;

    private GameObject _canvas;
    public int CurrentSentence = -1;
    
    public float MovementSpeed;
    private Vector3 _positionOriginal;
    private Vector3 _positionIncrease;
    private Vector3 _velocity;
    private int _moveUp;

    private List<GameObject> _children;

    private GameObject _dialogPoint;

    public float LeaveCounter;
    public bool LeftEarly;

    private void Start() {
        _canvas = GameObject.Find("Canvas");
        _dialogPoint = GameObject.Find("DialogPoint");
    }

    private void Update() {
        _positionOriginal = _dialogPoint.transform.position;
        _positionIncrease = _positionOriginal + Vector3.up * 16f;
        
        if (IsDialogOpen) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (CurrentSentence >= -1) {
                    NextLine();
                }
            }
            
            if (LeftEarly) {
                if (LeaveCounter > 0) {
                    LeaveCounter -= Time.deltaTime;
                }
                else {
                    _moveUp = 2;
                }
            }

            if (Math.Abs(_currentDialog.transform.position.y - _positionOriginal.y) < 2f && _moveUp == 2) {
                IsDialogOpen = false;
                Destroy(_currentDialog);
            }
        }
    }

    private void FixedUpdate() {
        if (IsDialogOpen) {
            if (_moveUp == 1) {
                _currentDialog.transform.position = Vector3.SmoothDamp(_currentDialog.transform.position,
                    _positionIncrease,
                    ref _velocity,
                    MovementSpeed * Time.deltaTime);
            }
            else if (_moveUp == 2) {
                _currentDialog.transform.position = Vector3.SmoothDamp(_currentDialog.transform.position,
                    _positionOriginal,
                    ref _velocity,
                    MovementSpeed * Time.deltaTime);
            }
        }
    }

    public void OpenDialog(Dialog dialog) {
        LeaveCounter = 0.6F;
        IsDialogOpen = true;
        LeftEarly = false;
        CurrentDialog = dialog;
        _moveUp = 1;
        CurrentSentence = -1;

        _currentDialog = Instantiate(DialogObject, _canvas.transform);

        
        // 0 = Name, 1 = Content, 2 = ResponseButtons
        _children = new List<GameObject>();

        foreach (Transform child in _currentDialog.transform) {
            _children.Add(child.gameObject);
        }
        
        _children[0].GetComponent<Text>().text = string.Join(", ", CurrentDialog.Names);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public void NextLine() {
        _children[1].GetComponent<Text>().text = "";
        
        if (CurrentSentence + 1 < CurrentDialog.Sentences.Length) {
            CurrentSentence++;
        
            // TODO: Delay this by a given amount
            foreach (var character in CurrentDialog.Sentences[CurrentSentence]) {
                _children[1].GetComponent<Text>().text += character;
            }
        }
        else {
            _children[1].GetComponent<Text>().text = CurrentDialog.Sentences.Last();
            CloseDialog();
        }
    }

    public void LeaveLine() {
        CurrentSentence = -2;
        LeftEarly = true;
        
        _children[1].GetComponent<Text>().text = "";
        foreach (var character in CurrentDialog.LeaveSentence) {
            _children[1].GetComponent<Text>().text += character;
        }
    }

    public void CloseDialog() {
        // IsDialogOpen = false;
        CurrentSentence = -1;
        _moveUp = 2;
    }
}