using UnityEngine;
using UnityEditor;

public class MiniWindow : HAMainEditorWindow
{

    public string nameStr = "";
    public static int page;
    [SerializeField] private GameObject prefab;
    
    private void OnEnable()
    {
       maxSize = new Vector2(300, 100);
       minSize = new Vector2(300, 100);
      
    }

    private void OnGUI()
    {
        if (page == 0)
        {
            Parenting();
        }

        if (page == 1)
        {  
            RenameWindow();
        }

        if (page == 3)
        {
            ReplaceNewPrefab();
        }

    }

    public void Parenting()
    {
      
        GUILayout.Space(15f);
            GUILayout.Label("Give a parent object name", EditorStyles.boldLabel);
            nameStr = GUILayout.TextField(nameStr);
            GUILayout.Space(5f);

            GUILayout.BeginHorizontal();
            GUI.color = Color.green;
            if (GUILayout.Button("Apply"))
            {
            ApplyMethods applymethods = CreateInstance<ApplyMethods>();
            applymethods.ApplyParent(nameStr);
                this.Close();
            }

            GUI.color = Color.red;
            if (GUILayout.Button("Cancel"))
            {
                this.Close();
            }

            GUILayout.EndHorizontal();
        }

  
    public void RenameWindow()
    {
    
            GUILayout.Space(15f);
            GUILayout.Label("Give a new name", EditorStyles.boldLabel);
            nameStr = GUILayout.TextField(nameStr);
            GUILayout.Space(5f);

            GUILayout.BeginHorizontal();
            GUI.color = Color.green;

            if (GUILayout.Button("Apply"))
            {
                ApplyMethods applymethods = CreateInstance<ApplyMethods>();
                applymethods.ApplyNewName(nameStr);
                this.Close();
            }

            GUI.color = Color.red;
            if (GUILayout.Button("Cancel"))
            {
                this.Close();
            }

            GUILayout.EndHorizontal();
        
        
    }

    public void ReplaceNewPrefab()
    {

        GUILayout.Space(15f);
        GUILayout.Label("Replace With", EditorStyles.boldLabel);
        //Create object field
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        GUILayout.Space(5f);

        GUILayout.BeginHorizontal();
        GUI.color = Color.green;

        if (GUILayout.Button("Apply"))
        {

            var filteredObjs = filteredGameObjects;

            for (var i = filteredObjs.Count - 1; i >= 0; --i)
            {

                var currentFilteredObj = filteredGameObjects[i];
               
                GameObject newObj;

                newObj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

                Undo.RegisterCompleteObjectUndo(newObj,"Replace Prefabs");
                newObj.transform.parent = currentFilteredObj.transform.parent;
                newObj.transform.localPosition = currentFilteredObj.transform.localPosition;
                newObj.transform.localRotation = currentFilteredObj.transform.localRotation;
                newObj.transform.localScale = currentFilteredObj.transform.localScale;
                newObj.transform.SetSiblingIndex(currentFilteredObj.transform.GetSiblingIndex());
                Undo.DestroyObjectImmediate(currentFilteredObj);
            }

            this.Close();
        }

        GUI.color = Color.red;
        if (GUILayout.Button("Cancel"))
        {
            this.Close();
        }

        GUILayout.EndHorizontal();
    }


}
