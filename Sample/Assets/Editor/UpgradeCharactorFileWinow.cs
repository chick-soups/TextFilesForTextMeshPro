using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class UpgradeCharactorFileWinow : EditorWindow
{
  [MenuItem("Window/UpgradeCharactorFile")]
  private static void Init(){
    UpgradeCharactorFileWinow ucfw =       GetWindow<UpgradeCharactorFileWinow>(false,"UpgradeCharactorFile",true);
    ucfw.minSize=new Vector2(600,600);
  }

  private string m_CharactorFilePath;
  private string m_targetFile;

  private void OnGUI() {
    GUILayout.BeginHorizontal();
    if(GUILayout.Button("Charactor File:",GUILayout.Width(200))){
        m_CharactorFilePath =     EditorUtility.OpenFilePanel("Charactor File",null,"txt");
    }
    GUILayout.Label(m_CharactorFilePath);
    GUILayout.EndHorizontal();
      GUILayout.BeginHorizontal();
    if(GUILayout.Button("Pick charactor form:",GUILayout.Width(200))){
        m_targetFile =     EditorUtility.OpenFilePanel("Charactor File",null,"txt");
    }
    GUILayout.Label(m_targetFile);
    GUILayout.EndHorizontal();
    if(GUILayout.Button("Run")){
        var sourceChars=GetSortedChars(m_CharactorFilePath);
        var targetChars=GetSortedChars(m_targetFile);
        RemoveSameChars(targetChars);
        PickCharactors(sourceChars,targetChars);
        SaveCharactors(sourceChars,m_CharactorFilePath);
    }
  }

  private List<char> GetSortedChars(string filePath){
     string content= File.ReadAllText(filePath);
     List<char> chars=new List<char>();
     int length=content.Length;
     for (int i = 0; i < length; i++)
     {
        chars.Add(content[i]);
     }
     chars.Sort();
     return chars;
  }

  private void RemoveSameChars(List<char> chars){
    int index=0;
    int length=chars.Count;
    Debug.Log(length);
    while(index<length-1){
         char current=chars[index];
         char next=chars[index+1];
         if(current==next){
          chars.RemoveAt(index+1);
          length-=1;
         }else{
          index+=1;
         }
    }
  }

  private void PickCharactors(List<char> sources,List<char> target){
    for (int i = 0; i < target.Count; i++)
    {
      char item=target[i];
      if(!sources.Contains(item)){
        sources.Add(item);
        Debug.Log("Add charactor:"+item);
      }
    }
  }

  private void SaveCharactors(List<char> source,string savePath){
    string content=new string(source.ToArray());
    File.WriteAllText(savePath,content);
  }
}
