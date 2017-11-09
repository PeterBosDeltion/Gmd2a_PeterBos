using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour {
    public GameObject winPanel;
    public GameObject losePanel;

    public static List<GameObject> enims = new List<GameObject>();

    public Texture2D defCurs;
    public Texture2D dragCurs;
    public Texture2D castCurs;
    public Texture2D pointCurs;

    public GameObject freezeParticles;
    public bool canFreeze;

    public GameObject firePrefab;
    public bool targeting;
    public bool canWall;

    public LineRenderer ln;
    // Use this for initialization
    void Start() {
        GameObject[] e = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject g in e)
        {
            enims.Add(g);
        }
        canFreeze = true;
        ln = GetComponent<LineRenderer>();
        canWall = true;
    }

    // Update is called once per frame
    void Update() {
        Win();
        Lose();

        FireDome();

    }

    public void Win()
    {
        if (TortManager.infoGathered >= TortManager.goalInfo)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
    }

    public void Lose()
    {
        if (enims.Count <= 0)
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Freeze()
    {
        if (canFreeze)
        {
            freezeParticles.SetActive(true);
            StartCoroutine(FreezeCool());
            foreach (GameObject g in enims)
            {
                g.GetComponent<Enemy>().Frozen();
            }
            canFreeze = false;
        }
    }

    IEnumerator FreezeCool()
    {
        yield return new WaitForSeconds(0.5F);
        freezeParticles.SetActive(false);

        yield return new WaitForSeconds(35);
        canFreeze = true;
    }

    public void ToggleFireDome()
    {
        if(!targeting && canWall)
        {
            targeting = true;
            Cursor.SetCursor(castCurs, Vector2.zero, CursorMode.Auto);
        }
        else if (!targeting)
        {
            targeting = false;
            Cursor.SetCursor(defCurs, Vector2.zero, CursorMode.Auto);
        }
    }

    public void FireDome()
    {
        if (Input.GetMouseButtonDown(1))
        {
            targeting = false;
            Cursor.SetCursor(defCurs, Vector2.zero, CursorMode.Auto);
        }
        if (targeting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 23));
                GameObject g = Instantiate(firePrefab, pos, Quaternion.identity);
                targeting = false;
                canWall = false;
                Cursor.SetCursor(defCurs, Vector2.zero, CursorMode.Auto);
                Destroy(g, 5);
                StartCoroutine(FireCool());
            }
        }
    }

    public IEnumerator FireCool()
    {
        yield return new WaitForSeconds(20);
        canWall = true;

    }
}
