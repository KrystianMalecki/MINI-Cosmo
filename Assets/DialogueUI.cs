using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueUI : UIBase
{
    public Image icon;
    public TextMeshProUGUI namer;
    public TextMeshProUGUI txt;
    public GameObject ResponseBase;
    public Transform mainTransform;
    public List<ResponseBox> boxes = new List<ResponseBox>();
    public SODialogue dialogueData;
    public UNode current;
    public UIManager uimanager;
    public override void OpenThis()
    {
        base.OpenThis();
        current = dialogueData.nodes[0];
        DoCurrent();
    }
    public void Progress(int id)
    {
        if (id >= dialogueData.nodes.Count||id==-1)
        {
            uimanager.HideAll();

            uimanager.CloseMenu("Dialogue");
          //  Debug.LogError("leave");
            return;
        }
     //   Debug.Log(current.type+">"+ dialogueData.nodes[id].type);
        current = dialogueData.nodes[id];

        DoCurrent();
    }
    public void DoCurrent()
    {
        switch (current.type)
        {

            case UNodeType.Error:
                {
                    CloseThis();
                    break;
                }
            case UNodeType.Function:
                {
                   // Debug.Log("funcy");
                    /* UnityEvent con = JsonUtility.FromJson<UnityEvent>(current.funcData);
                     con.Invoke();*/
                    current.funcData2.Invoke();
                    Progress(current.outs[0]);

                    break;

                }
            case UNodeType.Start:
                {
                    Progress(current.outs[0]);
                    break;
                }
            case UNodeType.Text:
                {
                    DisplayInfo(current);
                    break;
                }

            case UNodeType.If:
                {
                    /* conditioner con = JsonUtility.FromJson<conditioner>(current.ifData);
                      bool b = con.Invoke();*/
                    bool b = current.ifData2.Invoke(); 

                    if (b)
                    {
                        Progress(current.outs[0]);
                    }
                    else
                    {
                        Progress(current.outs[1]);
                    }
                    break;
                }
            case UNodeType.Exit:
                {
                    uimanager.HideAll();

                    uimanager.CloseMenu("Dialogue");
                    break;
                }
        }
    }
    public void Clicked(int id)
    {
        Progress(current.outs[id]);

    }
    public void DisplayInfo(UNode node)
    {
        icon.sprite = node.charSpr ;
        namer.text = StaticDataManager.instance.TMProFormater + node.characterName;
        txt.text = StaticDataManager.instance.TMProFormater + node.text;
        int offset = 0;
        
            for (int a = 0; a < boxes.Count; a++)
            {
                if (node.responses.Count > a)
                {
                    boxes[a].Setup(node.responses[a], this, a);

                    offset++;
                }
                else
                {
                    boxes[a].Hide();
                }
            }
        if (node.responses.Count > 0)
        {
            for (int a = offset; a < node.responses.Count; a++)
            {
                GameObject go = Instantiate(ResponseBase, mainTransform);
                ResponseBox ib = go.GetComponent<ResponseBox>();
                ib.Setup(node.responses[a], this, a);
                boxes.Add(ib);
            }
        }
    }
}
