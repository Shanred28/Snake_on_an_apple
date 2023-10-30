using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private string _nameEntity;
    public string NameEntity => _nameEntity;

    protected virtual void Update()
    { 
    
    }

    protected virtual void FixedUpdate()
    { 
    
    }
}
