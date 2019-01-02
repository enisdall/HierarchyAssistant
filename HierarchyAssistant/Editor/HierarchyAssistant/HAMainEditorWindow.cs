using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HAMainEditorWindow : EditorWindow
{
    #region Variables

    #region Textures
    Texture2D headerSectionTexture;
   // Texture2D backgroundSectionTexture;
    #endregion

    #region Rect variables
    Rect headerSectionRect;
    Rect filterMethodSectionRect;
    Rect mainFunctionTabSectionRect;
    Rect filteredObjsSectionRect;

   // Rect windowRect = new Rect(20, 20, 120, 50);
    #endregion

    #region Toolbars
    public int toolbarInt = 0;
    public string[] toolbarStrings = { "Tag", "Layer" };
    #endregion

    #region guiSkins
    GUISkin guiSkin;
    #endregion

    #region Foldout section position bools
   
    //Filter objects page foldouts.
    bool methodSectionshowPosition = true;
   // bool methodSectionHighlithPosition = true;
    bool methodSettingsSectionPosition = true;
    bool filteredObjectsSectionPosition = true;

    //This is sub foldoud named "Main methods" under filtering methods.
    bool filterMethod_mainMethods_sub_foldout = true;
    //This is sub foldoud named "Secondary methods" under filtering methods.
    bool filterMethod_secondaryMethods_sub_foldout = true;

    Vector2 scrollposition;

    #endregion

    #region  Main method public variables
    public static string tag_method_str = "Untagged";
    public static int layer_method_int = 0;
    #endregion

    #region Filtered objects variables
    public static GameObject[] foundedGameObjectsWithTag;
    public static List<GameObject> filteredGameObjects = new List<GameObject>();   
    public static Transform focusObj;
    #endregion

    #endregion // Variables end

    #region Other methods
    [MenuItem("Window/Hierarchy Assistant")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        HAMainEditorWindow window = (HAMainEditorWindow)EditorWindow.GetWindow(typeof(HAMainEditorWindow));
        // Give minimum size value to window.
        window.minSize = new Vector2(300, 500);
        window.Show();
    }
    
    //Repaint
    public void OnInspectorUpdate()
    {
        this.Repaint();
    }
    
    //When editor window is opened
    void OnEnable()
    {
        headerSectionTexture = Resources.Load<Texture2D>("GUI/header1");

        if (guiSkin == null)
        {
            guiSkin = Resources.Load<GUISkin>("AutoParentingGUIskin");
        }
    }

    private void Update()
    {
        //Always remove null objects from list.
        for (int i = 0; i < filteredGameObjects.Count; i++)
        {
            if (filteredGameObjects[i] == null)
            {
                filteredGameObjects.RemoveAt(i);
            }
        }
    }

    void RectValues()
    {

        //rect positions
        headerSectionRect.x = 0;
        headerSectionRect.y = 0;

        mainFunctionTabSectionRect.x = 0;
        mainFunctionTabSectionRect.y = 45;

        filteredObjsSectionRect.x = 0;
        filteredObjsSectionRect.y = 60;
        //sizes
        headerSectionRect.width = Screen.width;
        headerSectionRect.height = 40;

        mainFunctionTabSectionRect.width = Screen.width;
        mainFunctionTabSectionRect.height = 50;

        filteredObjsSectionRect.width = Screen.width / 2;
        filteredObjsSectionRect.height = 60;

        //draw
        GUI.DrawTexture(headerSectionRect, headerSectionTexture);

        filteredObjsSectionRect = new Rect(0, Screen.height, Screen.width / 2, Screen.height);

    }

   
    #endregion

    #region Main methods

    void OnGUI()
    {
       RectValues();
       //Filter methods section rect values.
       filterMethodSectionRect = new Rect(0, Screen.height / 10, Screen.width, Screen.height);
     
       GUILayout.BeginArea(headerSectionRect);
       // Write a header.
       GUILayout.Label("Hierarchy Assistant V1.0", guiSkin.label);
       GUILayout.EndArea();
   
       GUILayout.BeginArea(mainFunctionTabSectionRect);
      
       GUILayout.EndArea();

        #region Main pages.
        // if page one is active
       
            GUILayout.BeginArea(filterMethodSectionRect, EditorStyles.inspectorFullWidthMargins);
            
            GUILayout.Space(5f);
            methodSectionshowPosition = EditorGUILayout.Foldout(methodSectionshowPosition, "Filter Methods", EditorStyles.boldFont);

            GUILayout.Space(5f);
            
            if (methodSectionshowPosition)
            {
               
                //Increase indent level;
                EditorGUI.indentLevel++;
                
                filterMethod_mainMethods_sub_foldout = EditorGUILayout.Foldout(filterMethod_mainMethods_sub_foldout, "Main Methods", EditorStyles.boldFont);
                if (filterMethod_mainMethods_sub_foldout)
                {
                    toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
                }

                GUILayout.Space(5f);
                filterMethod_secondaryMethods_sub_foldout = EditorGUILayout.Foldout(filterMethod_secondaryMethods_sub_foldout, "Secondary Method", EditorStyles.boldFont);
                if (filterMethod_secondaryMethods_sub_foldout)
                {
                    GUILayout.BeginHorizontal();

               
                if (GUILayout.Button("..."))
                    {
                        CallbackMethods callbackmethods = CreateInstance <CallbackMethods>();
                        GenericMenu menu = new GenericMenu();
                        menu.AddItem(new GUIContent("Add Static Objects"), false, callbackmethods.AddStaticObjectsCallback);
                        //menu.AddItem(new GUIContent("Add Inactive Objects(Hierarchy)"), false, callbackmethods.AddInactiveInHierarchyCallback);
                        menu.ShowAsContext();
                    }
                   
                  //  GUILayout.Button("Add Inactive Objects");
                    GUILayout.EndHorizontal();
                }

                //Normalize indent level;
                EditorGUI.indentLevel--;

            GUILayout.Space(5f);

              
           

            }

            GUILayout.Space(5f);
            methodSettingsSectionPosition = EditorGUILayout.Foldout(methodSettingsSectionPosition, "Filter Settings", EditorStyles.boldFont);
            #region filtering methods
            if (methodSettingsSectionPosition)
            {
                //Filter method with tag.
                if (toolbarInt == 0)
                {
                    tag_method_str = EditorGUILayout.TagField("Select tag", tag_method_str);

                    GUILayout.Space(5f);

                    if (GUILayout.Button("Add to filtered objects"))
                    {
                        MainFilteringMethods mainfilteringmethods = CreateInstance<MainFilteringMethods>();
                        mainfilteringmethods.WithTag();
                    }

                }

                //Filter method with layer.
                if (toolbarInt == 1)
                {
                    layer_method_int = EditorGUILayout.LayerField("Select Layer", layer_method_int);

                    GUILayout.Space(5f);

                    if (GUILayout.Button("Add to filtered objects"))
                    {
                        MainFilteringMethods mainfilteringmethods = CreateInstance<MainFilteringMethods>();
                        mainfilteringmethods.WithLayer();
                    }

                }

                
            }
            #endregion

            GUILayout.Space(5f);
           
            filteredObjectsSectionPosition = EditorGUILayout.Foldout(filteredObjectsSectionPosition, "Filtered Objects", EditorStyles.boldFont);
             //if filtered object section foldout is open:
            if (filteredObjectsSectionPosition)
            {
           
                //Create new scrool view.
                scrollposition = GUILayout.BeginScrollView(scrollposition, GUILayout.Height(150));
                
                ListFilteredObjects listfilteredobjs = CreateInstance<ListFilteredObjects>();
                //Call method which lists filtered objects in ListFilteredObjects class.
                listfilteredobjs.ListFilteredObjectsMethod();

                GUILayout.Space(5f);
               
                GUILayout.BeginHorizontal();
                GUI.backgroundColor = Color.red;
            
            if (GUILayout.Button("Remove all"))
                {
                    filteredGameObjects.Clear(); //Clear filtered objects.
                }

                GUI.backgroundColor = Color.white;
                if (GUILayout.Button("Select all"))
                {    
                    GameObject[] selectallobjs = filteredGameObjects.ToArray();
                    Selection.objects = selectallobjs;

                }

             //Operations window.
            if (GUILayout.Button("..."))
            {
                CallbackMethods callbackmethods = CreateInstance<CallbackMethods>();
                //Create new generic menu.
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Create New Parent"), false, callbackmethods.NewParentCallback);
                menu.AddItem(new GUIContent("Replace New Prefab"), false ,callbackmethods.ReplaceNewPrefabCallback);
                menu.ShowAsContext();
            }

            GUILayout.EndScrollView();
                //   this.Repaint();
                GUILayout.EndHorizontal();
            }

            
            
            GUILayout.EndArea();
        #endregion
    }

    
    #endregion



}
