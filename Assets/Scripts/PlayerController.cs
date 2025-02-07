using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController charController;
    public bool drawDebug;
    private Ray ray;
    private RaycastHit hit;
    private bool isHitting;
    private float currentHitDistance;
    public List<GameObject> inventario = new();
    private Dictionary<string, string> teclasInventario = new Dictionary<string, string>();
    public GameObject textoColeta;
    private string nomeHittado;
    
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        ray.origin = transform.position + (transform.forward * 0.7f);
        ray.direction = transform.forward;

        isHitting = Physics.SphereCast(ray, charController.height / 4, out hit, 0.5f);
        
        currentHitDistance = isHitting ? hit.distance : 0.5f;
        
        if (isHitting && hit.transform.name != "Plane")
        {
            nomeHittado = hit.transform.name;
            if (Input.GetKeyDown(KeyCode.E) && hit.transform.gameObject.layer == 6)
            {
                inventario.Add(hit.transform.gameObject);
                hit.transform.gameObject.SetActive(false);

                UIController.instance.CallMensageBox($"Pegou o item \"{nomeHittado}\"");
            }
        }
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.green;

        if (drawDebug) {
            Debug.DrawLine(ray.origin, ray.origin + (ray.direction * currentHitDistance));
            Gizmos.DrawWireSphere(ray.origin + (ray.direction * currentHitDistance), charController.height / 4);
        }
    }
}