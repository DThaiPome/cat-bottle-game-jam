using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TVBehavior : MonoBehaviour
{
    [SerializeField]
    private Sprite normalTV;
    [SerializeField]
    private Sprite brokenTV;

    public bool broken = false;
    public LevelManager levelM;

    private SpriteRenderer ren;

    // Start is called before the first frame update
    void Start()
    {
        this.ren = this.GetComponent<SpriteRenderer>();

        this.levelM = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (broken && !this.gameOver())
        {
            levelM.LevelBeat();
        }

        if (broken)
        {
            this.ren.sprite = this.brokenTV;
        } else
        {
            this.ren.sprite = this.normalTV;
        }
    }

    bool gameOver()
    {

        if (levelM == null)
        {

            return false;
        }
        else
        {

            return levelM.isGameOver;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameOver())
        {

            //call cat Function
            if (other.gameObject.CompareTag("Player"))
            {
                broken = true;
            }
        }

    }
}
