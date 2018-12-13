﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public static float health = 10.0f;
    public static float hunger = 10.0f;
    public static float height; //defines jumping
    public static float y = -1.184f;

    public static int timer = 0;
    Rigidbody2D rb;
    Collider2D col;
    Animator anim;
    public static float move_speed = 0.008f;
    float sidestep_speed = 0.4f;
    float jump_speed = 16.0f;
    //AudioSource sound;
    float hor;
    float ver;
    bool jump = false;
    const float distance_speed = 0.00001f;
    const float sky_speed = 0.0025f;


    SpriteRenderer health_sprite;
    SpriteRenderer hunger_sprite;

    // Use this for initialization
    void Start () {
        health_sprite = GameObject.Find("health").GetComponent<SpriteRenderer>();
        hunger_sprite = GameObject.Find("hunger").GetComponent<SpriteRenderer>();

        anim = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        col = gameObject.GetComponentInChildren<Collider2D>();
        y = transform.position.y;
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (game.map.transform.position.y < -38 && game.flying_enemy_o.activeSelf == false) {
            game.flying_enemy_o.SetActive(true);
            move_speed = 0f;
        }

        //new speedup added on the stack

        health_sprite.size = new Vector2(0.07f * health, 0.06f);
        hunger_sprite.size = new Vector2(0.07f * hunger, 0.06f);


        anim.SetBool("jump", jump);
        anim.SetFloat("ver", ver);
        anim.SetFloat("hor", hor);

        height = col.transform.position.y;
        
        bool ground = col.IsTouchingLayers(LayerMask.GetMask("Ground"));

        // JUMP
        if (Input.GetKey(KeyCode.Space) && ground)
        {
            rb.AddForce(new Vector2(0,  jump_speed));
            jump = true;
            hunger -= 0.02f;
        } 
        else if (jump && ground) {
            jump = false;
        }

        // FORWARD BACKWARDS
        if (Input.GetKey(KeyCode.W) && game.map.transform.position.y > -40) {
            game.map.transform.position = new Vector2(game.map.transform.position.x, game.map.transform.position.y - move_speed);
            ver = 1;
            hunger -= 0.002f;
        } else if (Input.GetKey(KeyCode.S) && game.map.transform.position.y < 0) {
            game.map.transform.position = new Vector2(game.map.transform.position.x, game.map.transform.position.y + move_speed);
            ver = -1;
            hunger -= 0.002f;
        } else if (ver != 0) {
            ver = 0;
        }

        // SIDESTEP
        if (Input.GetKey(KeyCode.D))
        {
       	    if (transform.position.x < 1.8f)
				transform.position += Vector3.right * sidestep_speed * Time.deltaTime;     
           
            hunger -= 0.002f;
            hor = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -1.8f)
				transform.position += Vector3.left * sidestep_speed * Time.deltaTime;
            
            hunger -= 0.002f;
            hor = -1;
        } else if (hor != 0) {
            hor = 0;
        }
    }

    public static bool compare_values(float f1, float f2, float f3) {
        Debug.Log(f1 + "    " + f2);
        if ((f1 <= (f2 + f3)) && (f1 >= (f2 - f3))) {
            return true;
        }

        return false;
    }

}
