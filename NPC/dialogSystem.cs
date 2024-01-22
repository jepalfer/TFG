using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogSystem : MonoBehaviour
{
    private int _dialogIndex;
    [SerializeField] private int _dialogQuantity;
    [SerializeField] private int _NPCIndex;
    [TextArea]
    [SerializeField] List<string> _dialogues;

    public int getDialogIndex()
    {
        return _dialogIndex;
    }

    public int getDialogQuantity()
    {
        return _dialogQuantity;
    }
    public int getNPCIndex()
    {
        return _NPCIndex;
    }
    public List<string> getDialogs()
    {
        return _dialogues;
    }







}
