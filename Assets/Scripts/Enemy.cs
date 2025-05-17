using UnityEngine;

public class Enemy : MonoBehaviour
{
    public const float DefaultSpeed = 10f;
    public const float StoppingDistance = 0.1f;
    public Transform target;

    private Vector3 _targetOffset = new(20f, 0f, 0f); 

    private CharacterController _controller;
    private float _speed;
    private Vector3 _velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // instanciando a classe CharacterController, com ela podemos realizar o movimento 
        // do personagem e também o pulo
        _controller = GetComponent<CharacterController>();
        _speed = DefaultSpeed;
        
        // criando o objeto no qual o inimigo irá correr atrás
        // SERÁ TROCADO PARA O PLAYER
        var fakeTargetPointObject = new GameObject("FakeTarget")
        {
            transform =
            {
                position = transform.position + _targetOffset
            }
        };

        target = fakeTargetPointObject.transform;
        
        // Logs para controle e debug
        Debug.Log("FakeTarget position: ");
        Debug.Log(target.position.x);
        
        Debug.Log("Enemy position: ");
        Debug.Log(transform.position.x);
    }

    // Update is called once per frame
    private void Update()
    {
        // verifica se chegamos no nosso objetivo, caso contrário calculamos
        // a direcao e velocidade para onde iremos
        var distance = Vector3.Distance(transform.position, target.position);
        
        // if (distance > StoppingDistance)
        // {
        //     
        //     var dir = (target.position - transform.position).normalized;
        //     _velocity = new Vector3(dir.x, 0f, dir.z) * _speed;
        //     
        //     Debug.DrawRay(transform.position, dir * 2f, Color.red);
        //     
        //     // adiciona gravidade caso ele nao esteja no chao
        //     if (!_controller.isGrounded)
        //     {
        //         _velocity.y += Physics.gravity.y * Time.deltaTime;
        //     }
        //
        //     // move ele até o objeto
        //     _controller.Move(_velocity * Time.deltaTime);
        //     // Debug para verificar se está se movendo
        //     Debug.Log("Moving with velocity: " + _velocity + " Distance: " + distance);
        // }
        // else
        // {
        //     // reseta o vetor de velocidade para nao correr o risco do personagem entrar em loop,
        //     // correndo em circulos
        //     _velocity = Vector3.zero;
        //     Debug.Log("Stopped at target. Distance: " + distance);
        // }
    }
    
    void OnDrawGizmosSelected()
    {
        // esse método eh apenas para desenhar o ponto.
        if (target is not null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawWireSphere(target.position, 0.3f);
        }
        else
        {
            // Mostrar onde o alvo será criado mesmo antes de iniciar o jogo
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position + _targetOffset, 0.3f);
        }
    }
}