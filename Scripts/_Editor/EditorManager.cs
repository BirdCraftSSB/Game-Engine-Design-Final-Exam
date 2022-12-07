using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    public GameControls inputAction;

    public Camera mainCam;
    public Camera editorCam;

    public bool editorMode = false;

    Vector3 mousePos;
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject item;
    public bool instantiated = false;

    // Command
    ICommand command;

    // UI Manager
    UIManager ui;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inputAction = PlayerInputController.controller.inputAction;
        mainCam.enabled = true;
        editorCam.enabled = false;

        ui = GetComponent<UIManager>();
    }

    public void EnterEditorMode()
    {
        mainCam.enabled = !mainCam.enabled;
        editorCam.enabled = !editorCam.enabled;

        ui.ToggleEditorUI();
    }

    public void AddItem(int itemId)
    {
        if (editorMode && !instantiated)
        {
            switch (itemId)
            {
                case 1:
                    item = Instantiate(prefab1);
                    //Create boxes that can observe events and give them an event to do
                    Enemy spike1 = new Enemy();
                    //Add the boxes to the list of objects waiting for something to happen
                    break;
                default:
                    break;
            }
            instantiated = true;
        }
    }

    public void DropItem()
    {
        if (editorMode && instantiated)
        {
            if (item.GetComponent<Rigidbody>())
            {
                item.GetComponent<Rigidbody>().useGravity = true;
            }
            item.GetComponent<Collider>().enabled = true;

            // Add item transform to items list
            command = new PlacePelletsCommand(item.transform.position, item.transform);
            CommandInvoker.AddCommand(command);

            instantiated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if we are in editor mode
        if (mainCam.enabled == false && editorCam.enabled == true)
        {
            // Stop all movement in game
            Time.timeScale = 0;
            editorMode = true;

            // Making cursor visible when in editor mode
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            editorMode = false;

            // Making cursor invisible when in play mode
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (instantiated)
        {
            mousePos = Mouse.current.position.ReadValue();
            mousePos = new Vector3(mousePos.x, mousePos.y, 40f);

            item.transform.position = editorCam.ScreenToWorldPoint(mousePos);
        }

    }
}
