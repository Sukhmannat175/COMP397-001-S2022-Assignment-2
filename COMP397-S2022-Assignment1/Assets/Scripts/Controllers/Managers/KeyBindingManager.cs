/*  Filename:           KeyBindingController.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        June 6, 2022
 *  Description:        Key Binding Controller.
 *  Revision History:   June 6, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

public class KeyBindingManager : MonoBehaviour
{
    public enum KeyAction { NONE = 0, UP = 1, LEFT = 2, DOWN = 3, RIGHT = 4 }

    [Header("Default")]
    [SerializeField] private KeyCode defaultUp = KeyCode.W;
    [SerializeField] private KeyCode defaultLeft = KeyCode.A;
    [SerializeField] private KeyCode defaultDown = KeyCode.S;
    [SerializeField] private KeyCode defaultRight = KeyCode.D;

    [Header("Inverted X")]
    [SerializeField] private KeyCode invertedLeft = KeyCode.D;
    [SerializeField] private KeyCode invertedRight = KeyCode.A;

    [Header("Inverted Y")]
    [SerializeField] private KeyCode invertedUp = KeyCode.S;
    [SerializeField] private KeyCode invertedDown = KeyCode.W;

    [Header("Adopted")]
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode right;
    [SerializeField] private bool selectedNormalXAxis = true;
    [SerializeField] private bool selectedNormalYAxis = true;


    public static KeyBindingManager instance;

    public KeyCode Up { get { return up; } }
    public KeyCode Left { get { return left; } }
    public KeyCode Down { get { return down; } }
    public KeyCode Right { get { return right; } }
    public bool SelectedNormalXAxis { get { return selectedNormalXAxis; } }
    public bool SelectedNormalYAxis { get { return selectedNormalYAxis; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        UseNormalXAxis();
        UseNormalYAxis();
    }

    public KeyCode GetKey(KeyAction keyAction)
    {
        switch (keyAction)
        {
            case KeyAction.UP:
                return up;
            case KeyAction.LEFT:
                return left;
            case KeyAction.DOWN:
                return down;
            case KeyAction.RIGHT:
                return right;
            default:
                Debug.LogError("Please update the key action");
                return KeyCode.None;
        }
    }

    public void Rebind(KeyAction keyAction, KeyCode keyCode)
    {
        switch (keyAction)
        {
            case KeyAction.UP:
                up = keyCode;
                break;
            case KeyAction.LEFT:
                left = keyCode;
                break;
            case KeyAction.DOWN:
                down = keyCode;
                break;
            case KeyAction.RIGHT:
                right = keyCode;
                break;
            default:
                Debug.LogError("Please update the key action");
                break;
        }
    }

    public void UseInvertedXAxis()
    {
        left = invertedLeft;
        right = invertedRight;
        selectedNormalXAxis = false;
    }

    public void UseInvertedYAxis()
    {
        up = invertedUp;
        down = invertedDown;
        selectedNormalYAxis = false;
    }

    public void UseNormalXAxis()
    {
        left = defaultLeft;
        right = defaultRight;
        selectedNormalXAxis = true;
    }

    public void UseNormalYAxis()
    {
        up = defaultUp;
        down = defaultDown;
        selectedNormalYAxis = true;
    }
}
