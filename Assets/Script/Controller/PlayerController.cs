using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 5.0f;
    private Vector3 _destination;
    private float Run_Wait_Ratio = 0.0f;
    private float Jump_Height = 2.0f;
    private bool Jumping = false;

    private Constant.State CharecterState = Constant.State.Idle;
    /*private void MoveBitch()
    {
        if(CharecterState == Constant.State.Die){ return; }
        if (Input.GetKey(KeyCode.W))
        {
            // transform.position += new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime * _speed;
            // transform.position += Vector3.forward * Time.deltaTime * _speed;
            // transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);

            // transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
            // transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
            // transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
            // transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
        }
    }*/


    /*public void Jump()
    {
       if (transform.position.y == 0)
        {
            Jumping = true;
        }
        if(Jumping == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 100);
            float height = transform.position.y;
            if(height == 2.2)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 100);
            }
            else if(height == 0)
            {
                Jumping = false;
                return;
            }
        }
        else
        {
            return;
        }
    }*/
    private void Move()
    {
        Vector3 MoveVector = _destination - transform.position;
        Debug.DrawRay(Camera.main.transform.position, _destination, Color.red, 1.0f);
        if (MoveVector.magnitude < 0.00001f)
        {
            CharecterState = Constant.State.Idle;
            return;
        }
        if (MoveVector.magnitude < (MoveVector.normalized * _speed * Time.deltaTime).magnitude)
        {
            transform.position += MoveVector;
        }
        else
        {
            transform.position += MoveVector.normalized * _speed * Time.deltaTime;

        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveVector), 0.4f);

        Animator Movement = GetComponent<Animator>();
        //Movement.SetFloat("Run_Wait_Ratio", 1.0f);
        Run_Wait_Ratio = Mathf.Lerp(Run_Wait_Ratio, 1.0f, 0.2f);
        Movement.SetFloat("Run_Wait_Ratio", Run_Wait_Ratio);
        Movement.Play("Run_Wait");
    }
    private void Jump()
    {
        //transform.Translate(Vector3.up * Time.deltaTime * 100);
        float height = 0;
        height = Mathf.Lerp(height, 2.2f, 0.2f);
        if (height == 2.2)
        {
            height = Mathf.Lerp(height, 0.0f, 0.2f);
        }
        transform.position = new Vector3(0.0f, height, 1.0f);
    }
    private void KeyBoard_Action(Constant.KeyBoardEvent evt)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Jumping == true)
            {
                return;
            }
            else
            {
                if (transform.position.y == 0)
                {
                    Jump();
                    Animator Movement = GetComponent<Animator>();
                    Movement.Play("Jump");
                    Jumping = true;
                }
            }

        }
    }
    private void Die() { }
    private void Idle() {
        Jumping = false;
    Animator Movement = GetComponent<Animator>();
        //Movement.SetFloat("Run_Wait_Ratio", 0.0f);
        Run_Wait_Ratio = Mathf.Lerp(Run_Wait_Ratio, 0.0f, 0.2f);
        Movement.SetFloat("Run_Wait_Ratio", Run_Wait_Ratio);
        Movement.Play("Run_Wait");
    }
    private void OnMouseClick(Constant.MouseEvent evt)
    {
        //Vector3 point_3d = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        // Vector3 DirVtr = point_3d - transform.position;
        //DirVtr = DirVtr.normalized;
        if(CharecterState == Constant.State.Die){ return; }
        RaycastHit onHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition) ;
        Debug.Log(Physics.Raycast(ray, out onHit, 200, LayerMask.GetMask("Land")));
        //Debug.DrawRay(Camera.main.transform.position, ray.direction*500.0f, Color.red, 1.0f);
        if(Physics.Raycast(ray, out onHit, 200, LayerMask.GetMask("Land")) == true)
        {
            _destination = onHit.point;
            CharecterState = Constant.State.Move;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        Managers.Input.KeyBoardAction -= KeyBoard_Action;
        Managers.Input.KeyBoardAction += KeyBoard_Action;
        Managers.Input.MouseAction -= OnMouseClick;
        Managers.Input.MouseAction += OnMouseClick;
    }

    // Update is called once per frame
    void Update()
    {
        switch (CharecterState) {
            case Constant.State.Die:  
                Die();
                break;
            case Constant.State.Move: 
                Move();
                break;
            case Constant.State.Idle: 
                Idle();
                break;
        }
    }
}
