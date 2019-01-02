using UnityEngine;
using UnityEditor;

public class CallbackMethods : HAMainEditorWindow
{


    #region generic menu callback methods
    public void FocusCallback()
    {
        //Focus object.
      
        SceneView.lastActiveSceneView.LookAt(focusObj.transform.position);
    }

    public void RemoveFromFilteredCallback()
    {
        Debug.Log("Removed from filtered.");
        filteredGameObjects.Remove(focusObj.gameObject);
    }

    public void SelectObjCallback()
    {
        //Select object in hierarchy window.
        Selection.activeTransform = focusObj.transform;
    }

    public void NewParentCallback()
    {
        MiniWindow.page = 0;
        MiniWindow windoww = (MiniWindow)EditorWindow.GetWindow(typeof(MiniWindow));

        windoww.Show();

    }

    public void NewNameCallback()
    {
        MiniWindow.page = 1;
        MiniWindow windoww = (MiniWindow)EditorWindow.GetWindow(typeof(MiniWindow));
        windoww.Show();

    }

    public void AddStaticObjectsCallback()
    {
        GameObject[] allObjs = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < allObjs.Length; i++)
        {
            if (allObjs[i].gameObject.isStatic == true)
            {
                if (!filteredGameObjects.Contains(allObjs[i]))
                {
                   filteredGameObjects.Add(allObjs[i].gameObject);
                }
            }  
        }
    }

    public void ReplaceNewPrefabCallback()
    {
        MiniWindow.page = 3;
        MiniWindow windoww = (MiniWindow)EditorWindow.GetWindow(typeof(MiniWindow));

        windoww.Show();
    }
    

    #endregion

}
