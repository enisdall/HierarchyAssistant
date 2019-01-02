//This class lists filtered objects from HAMainEditorWindow.

using UnityEngine;
using UnityEditor;
using System.Linq;

public class ListFilteredObjects : HAMainEditorWindow
{

    public void ListFilteredObjectsMethod()
    {
        for (int i = 0; i < filteredGameObjects.Count; i++)
        {
          

                //Create a button for detect every object.
                if (GUILayout.Button(filteredGameObjects[i].gameObject.name))
                {             

                focusObj = filteredGameObjects[i].transform;

                    CallbackMethods callbackmethods = CreateInstance<CallbackMethods>();
                    //Create new generic menu.
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Select"), false, callbackmethods.SelectObjCallback);
                    menu.AddItem(new GUIContent("Focus"), false, callbackmethods.FocusCallback);
                    menu.AddItem(new GUIContent("Remove"), false, callbackmethods.RemoveFromFilteredCallback);
                    menu.AddItem(new GUIContent("Rename"), false, callbackmethods.NewNameCallback);
                    menu.ShowAsContext();
                }
                        
        }

        //Remove duplicates.
        filteredGameObjects = filteredGameObjects.Distinct().ToList();

    }


   


}
