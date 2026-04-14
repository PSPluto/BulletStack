using UnityEngine;

public class BtnAnimation : MonoBehaviour
{
    private Animator animater;
    private void Start()
    {
        animater = GetComponent<Animator>();
    }
    public void Show()
    {
        animater.SetBool("Show", true);
    }
    public void Hide()
        {
            animater.SetBool("Show", false);
    }
}
