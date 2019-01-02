using UnityEngine;

public class ApplyMethods : HAMainEditorWindow
{

    #region ApplyMethods
    public void ApplyParent(string name)
    {

        GameObject pn = new GameObject(name);

        int number1 = filteredGameObjects.Count;

        for (int i = 0; i < number1; i++)
        {
            filteredGameObjects[i].transform.parent = pn.transform;

            if (i == number1)
            {
                break;
            }
        }

    }

    public void ApplyNewName(string name)
    {
        focusObj.name = name;
    }
    #endregion


}
