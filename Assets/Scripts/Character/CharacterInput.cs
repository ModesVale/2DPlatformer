using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public float Move {  get; private set; }
    public bool Jump { get; private set; }

    private void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");
        Jump = Input.GetButtonDown("Jump");
    }
}
