using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public GameObject DialogObject;
    public GameObject Button;
    public bool IsDialogOpen;

    private GameObject _currentDialog;
    public Dialog CurrentDialog;

    private GameObject _canvas;
    public int CurrentSentenceIndex;
    public string CurrentSentence;
    public int CurrentCharacterIndex;

    public bool RequiresCharacter;
    
    public float MovementSpeed;
    private Vector3 _positionOriginal;
    private Vector3 _positionIncrease;
    private Vector3 _velocity;
    private int _moveUp;

    private float _textCounter;

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
                if (CurrentSentenceIndex >= -1) {
                    NextLine();
                }
            }

            if (_textCounter > 0) {
                _textCounter -= Time.deltaTime;
            }
            else {
                _textCounter = 0.04f;
                RequiresCharacter = true;
            }
            
            if (LeftEarly) {
                if (LeaveCounter > 0) {
                    LeaveCounter -= Time.deltaTime;
                }
                else {
                    _moveUp = 2;
                }
            }

            if (RequiresCharacter) {
                RequiresCharacter = false;

                if (CurrentCharacterIndex + 1 <= CurrentSentence.Length) {
                    _children[1].GetComponent<Text>().text += CurrentSentence[CurrentCharacterIndex];
                    CurrentCharacterIndex++;
                }
            }

            if (Math.Abs(_currentDialog.transform.position.y - _positionOriginal.y) < 2f && _moveUp == 2) {
                ForceCloseDialog();
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
        CurrentSentenceIndex = 0;
        CurrentCharacterIndex = 0;

        _currentDialog = Instantiate(DialogObject, _canvas.transform);
        // 0 = Name, 1 = Content, 2 = ResponseButtons
        _children = new List<GameObject>();

        foreach (Transform child in _currentDialog.transform) {
            _children.Add(child.gameObject);
        }
        
        _children[0].GetComponent<Text>().text = string.Join(", ", CurrentDialog.Names);

        // Create the buttons
        foreach (var b in CurrentDialog.Options) {
            var button = Instantiate(Button, _children[2].transform);
            button.transform.Find("Text").GetComponent<Text>().text = b.Name;
            button.transform.GetComponent<Button>().onClick.AddListener(() => b.Function.Invoke());
        }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public void NextLine() {
        CurrentCharacterIndex = 0;
        _children[1].GetComponent<Text>().text = "";
        
        if (CurrentSentenceIndex < CurrentDialog.Sentences.Length) {
            CurrentSentence = CurrentDialog.Sentences[CurrentSentenceIndex];
            CurrentSentenceIndex++;

            _textCounter = 0.04f;
        }
        else {
            _children[1].GetComponent<Text>().text = CurrentDialog.Sentences.Last();
            CloseDialog();
        }
    }

    public void LeaveLine() {
        CurrentCharacterIndex = 0;
        CurrentSentenceIndex = -1;
        _children[1].GetComponent<Text>().text = "";
        
        LeftEarly = true;
        
        // _children[1].GetComponent<Text>().text = "";
        // foreach (var character in CurrentDialog.LeaveSentence) {
        //     _children[1].GetComponent<Text>().text += character;
        // }

        CurrentSentence = CurrentDialog.LeaveSentence;
        _textCounter = 0.04f;
    }

    public void CloseDialog() {
        // IsDialogOpen = false;
        CurrentSentenceIndex = -1;
        CurrentSentence = "";
        _textCounter = 0;
        _moveUp = 2;
    }

    public void ForceCloseDialog() {
        IsDialogOpen = false;
        Destroy(_currentDialog);
    }
}