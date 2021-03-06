using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private IPlayerStateMachine states;
    private SpriteRenderer renderer;
    public Sprite standingsprite;
    public Sprite lyingsprite;

    // Start is called before the first frame update
    void Start()
    {
        this.states = this.GetComponent<IPlayerStateMachine>();
        this.renderer = this.GetComponent<SpriteRenderer>();
        this.states.OnStateEnter(this.changeSpriteStanding, PlayerState.Standing);
        this.states.OnStateEnter(this.changeSpriteSitting, PlayerState.Looking);
    }

    void changeSpriteStanding()
    {
        renderer.sprite = standingsprite;
    }

    void changeSpriteSitting()
    {
        renderer.sprite = lyingsprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
