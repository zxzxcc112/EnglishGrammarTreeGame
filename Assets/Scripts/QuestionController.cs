using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    [SerializeField] private Transform nextQuestion;
    [SerializeField] private Transform currentQuestion;
    [SerializeField] private Transform postQuestion;
    [SerializeField] private Transform nextPosition;
    [SerializeField] private Transform currentPosition;
    [SerializeField] private Transform postPosition;

    [SerializeField] private float t = 0.1f;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private bool canControl = true;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canControl)
        {
            postQuestion = currentQuestion;
            currentQuestion = nextQuestion;
            nextQuestion = null;
            canControl = false;
        }
    }

    private void FixedUpdate()
    {
        if(currentQuestion != null)
        {
            currentQuestion.position = Vector3.MoveTowards(currentQuestion.position,
            Vector3.Lerp(currentQuestion.position, currentPosition.position, t),
            speed);
        }
        
        if(postQuestion != null)
        {
            postQuestion.position = Vector3.MoveTowards(postQuestion.position,
            postPosition.position,
            speed);
            
            if (Vector3.Distance(postQuestion.position, postPosition.position) <= 0.01f)
            {
                nextQuestion = postQuestion;
                nextQuestion.position = nextPosition.position;
                postQuestion = null;
                canControl = true;
            }
        }
        
    }

}
