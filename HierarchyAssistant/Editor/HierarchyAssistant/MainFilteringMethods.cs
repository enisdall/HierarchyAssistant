using UnityEngine;

public class MainFilteringMethods : HAMainEditorWindow
{

    
    public void WithTag()
    {

        foundedGameObjectsWithTag = GameObject.FindGameObjectsWithTag(tag_method_str);

        for (int i = 0; i < foundedGameObjectsWithTag.Length; i++)
        {
            if (!filteredGameObjects.Contains(foundedGameObjectsWithTag[i]))
            {
                filteredGameObjects.Add(foundedGameObjectsWithTag[i].gameObject);
            }
        }
        
       

    }

    public void WithLayer()
    {
        GameObject[] allObjs = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < allObjs.Length; i++)
        {
            if (allObjs[i].gameObject.layer == layer_method_int)
            {
                filteredGameObjects.Add(allObjs[i].gameObject);
            }
        }
    }


}
