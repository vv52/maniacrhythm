using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LS_input : MonoBehaviour
{
    public RawImage lsGraphic;
    public Texture twb_ls;
    public Texture treasure_ls;

    public List<string> songs = new List<string>();
    public List<Texture> songImgs = new List<Texture>();
    
    public int cursor = 0;

    void Awake()
    {
        songs.Add("twb");
        songs.Add("treasure");
        songImgs.Add(twb_ls);
        songImgs.Add(treasure_ls);
    }

    void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            if (cursor == 0)
            {
                cursor = songs.Count - 1;
            }
            else
            {
                cursor -= 1;
            }
        }
        if (Input.GetKeyDown("f"))
        {
        	SceneManager.LoadScene("title", LoadSceneMode.Single);
        }
        if (Input.GetKeyDown("j"))
        {
        	SceneManager.LoadScene(songs[cursor], LoadSceneMode.Single);
        }
        if (Input.GetKeyDown("k"))
        {
        	if (cursor == songs.Count - 1)
            {
                cursor = 0;
            }
            else
            {
                cursor += 1;
            }
        }

        lsGraphic.texture = songImgs[cursor];
    }
}
