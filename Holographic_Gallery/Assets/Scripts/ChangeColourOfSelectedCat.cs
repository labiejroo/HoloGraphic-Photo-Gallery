using UnityEngine;
using SingletonFile;

public class ChangeColourOfSelectedCat : Singleton<ChangeColourOfSelectedCat> {

    [SerializeField]
    private Material _selectedMaterial;

    [SerializeField]
    private Material _nonSelectedMaterial;

    [SerializeField]
    private MeshRenderer[] _meshRenderers;
    
    public void ChangeColour(int index)
    {
        foreach (MeshRenderer item in _meshRenderers)
        {
            item.material = _nonSelectedMaterial;
        }

        _meshRenderers[index].material = _selectedMaterial;
    }
}
