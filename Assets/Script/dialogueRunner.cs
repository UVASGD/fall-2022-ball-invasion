using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using System.Linq;

// This class is using a dialog system imported from the asset store. Here's the documentation: https://github.com/DoublSB/UnityDialogAsset
public class dialogueRunner : MonoBehaviour
{
    public DialogManager manager;
    public List<string> lines;
    // Start is called before the first frame update
    void Start()
    {
        List<DialogData> dialogList = new List<DialogData>();
        foreach(string line in lines){
            dialogList.Add(new DialogData(line, "Scientist"));
        }
        dialogList.Last().Callback = () => {
            FindObjectOfType<enemySpawner>().startSpawning();
            Destroy(gameObject);
            };
        manager.Show(dialogList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
