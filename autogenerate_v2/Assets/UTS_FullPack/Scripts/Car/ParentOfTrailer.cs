using UnityEngine;

public class ParentOfTrailer : MonoBehaviour
{
    private GameObject par;

    public GameObject PAR
    {
        get 
        {
            if(par != null)
                return par;
            else
                return null;
        }

        set {par = value;}
    }

    public void InitTag()
    {
        gameObject.tag = par.tag;
    }
}