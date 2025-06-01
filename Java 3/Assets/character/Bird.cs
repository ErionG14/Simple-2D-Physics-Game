using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
   private Vector3 _initialPosition;
   private bool _birdWasLaunched;
   private float _timeSittingAround;
   [SerializeField] private float _launchPower = 500;
   public int editorOrderInLayer = -2; // Order in Layer in the editor
   public int gameOrderInLayer = 0;   // Order in Layer during gameplay

   private LineRenderer _lineRenderer;

   private void Awake()
   {
      _initialPosition = transform.position;
      _lineRenderer = GetComponent<LineRenderer>();

#if UNITY_EDITOR
      if (!Application.isPlaying)
      {
         _lineRenderer.sortingOrder = editorOrderInLayer;
      }
      else
#endif
      {
         _lineRenderer.sortingOrder = gameOrderInLayer;
      }
   }

   private void Update()
   {
      _lineRenderer.SetPosition(1, _initialPosition);
      _lineRenderer.SetPosition(0, transform.position);

      if (_birdWasLaunched && GetComponent<Rigidbody2D>().linearVelocity.magnitude <= 0.1f)
      {
         _timeSittingAround += Time.deltaTime;
      }

      if (transform.position.y > 10 || transform.position.y < -10 ||
          transform.position.x > 10 || transform.position.x < -10 ||
          _timeSittingAround > 3)
      {
         string currentScene = SceneManager.GetActiveScene().name;
         SceneManager.LoadScene(currentScene);
      }
   }

   private void OnMouseDown()
   {
      GetComponent<SpriteRenderer>().color = Color.red;
      _lineRenderer.enabled = true;
   }

   private void OnMouseUp()
   {
      GetComponent<SpriteRenderer>().color = Color.white;
      Vector2 directionToInitialPosition = _initialPosition - transform.position;
      GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
      GetComponent<Rigidbody2D>().gravityScale = 1;
      _birdWasLaunched = true;
      _lineRenderer.enabled = false;
   }

   private void OnMouseDrag()
   {
      Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      transform.position = new Vector3(newPosition.x, newPosition.y);
   }
}
