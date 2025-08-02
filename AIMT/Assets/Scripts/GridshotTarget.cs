using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GridshotTarget : MonoBehaviour
{
    public Gridshot gridshot;
    public AudioSource hitSound;
    private bool active = true;
    private float lifeTime = 3f;
    private float timer = 0f;
    private void Start()
    {
        gridshot = GameObject.FindGameObjectWithTag("Logic").GetComponent<Gridshot>();
        if (gridshot == null)
        {
            Debug.LogError("Gridshot component not found in the scene.");
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            
            GetComponent<SpriteRenderer>().color = Color.red;
            if (gridshot != null && active)
            {
                active = false;
                gridshot.OnTargetMissed();
                gridshot.RemoveTarget(gameObject);
            }
            else if(active)
            {
                active = false;
                Destroy(gameObject, 0.1f);
            }
        }
    }
    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        if (gridshot != null && active)
        {
            active = false;
            gridshot.OnTargetHit();
            gridshot.RemoveTarget(gameObject);
        }
        else if (active)
        {
            active = false;
            Destroy(gameObject, 0.1f);
        }

        if (hitSound != null)
        {
            hitSound.Stop();
            hitSound.time = 0f;
            hitSound.Play();
        }
    }
}
