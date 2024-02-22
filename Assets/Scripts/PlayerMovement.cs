using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private float speed = 5f;
    private Vector2 movementLast;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Koristi Unity Input System za dobivanje ulaznih podataka
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Normaliziraj input vektor kako bi se osiguralo ravnomjerno kretanje u svim smjerovima
        movementInput.Normalize();

        // Primijeni input na igra?evu poziciju
        rb.velocity = movementInput * speed;

        // Postavi parametre Animatora na temelju inputa za Blend Tree
        animator.SetFloat("xInput", movementInput.x);
        animator.SetFloat("yInput", movementInput.y);

        if (movementInput.x != 0 || movementInput.y != 0)
        {
            movementLast.x = movementInput.x;
            movementLast.y = movementInput.y; 
        }

        // Ako igra? klika na tipku, postavi zadnji smjer na smjer kretanja
        if (movementInput.magnitude == 0f)
        {
            animator.SetFloat("xIdle", movementLast.x);
            animator.SetFloat("yIdle", movementLast.y);
        }
        else
        {
            animator.SetFloat("xIdle", 0f);
            animator.SetFloat("yIdle", 0f);
        }


        // Ako igra? nije u pokretu, postavi stanje na "idle"
        if (movementInput.magnitude == 0f)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            // Ako je igra? u pokretu, postavi stanje na "move"
            animator.SetBool("isMoving", true);
        }
    }

    
}
