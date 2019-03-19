using System;
using UnityEngine.Events;

[Serializable]
public class Dialog {
    public string[] Names;
    public string[] Sentences;
    public string LeaveSentence;
    public Option[] Options;

    [Serializable]
    public class Option {
        public string Name;
        public UnityEvent Function;
    }
}