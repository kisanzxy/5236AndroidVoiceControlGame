using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class CharacterSpriteFactory 
{
    private static Dictionary<int, Sprite> map = new Dictionary<int , Sprite>();
    private static Dictionary<int, Sprite> mover_map = new Dictionary<int, Sprite>();
    public static Sprite spriteFactory(int id) {
        string path = "Sprites/";
        
        if (map.ContainsKey(id))
        {
            return map[id];
        }
        else {
            if (id == (int)CharacterSprite.normal)
            {
                path += "Normal";
            }
            else if (id == (int)CharacterSprite.curl)
            {
                path += "Curl";
            }
            else if (id == (int)CharacterSprite.unicorn)
            {
                path += "Unicorn";
            }
            else {
                path += "Dinasour";
            }
            Sprite sp = Resources.Load<Sprite>(path);
            map.Add(id, sp);
            return sp;
        }
    }

    public static Sprite moveSpriteFactory(int id)
    {
        string path = "Sprites/";

        if (mover_map.ContainsKey(id))
        {
            return mover_map[id];
        }
        else
        {
            if (id == (int)CharacterSprite.normal)
            {
                path += "Normal";
            }
            else if (id == (int)CharacterSprite.curl)
            {
                path += "Curl";
            }
            else if (id == (int)CharacterSprite.unicorn)
            {
                path += "Unicorn";
            }
            else
            {
                path += "Dinasour";
            }
            path += "_move";
            Sprite sp = Resources.Load<Sprite>(path);
            mover_map.Add(id, sp);
            return sp;
        }
    }
}
