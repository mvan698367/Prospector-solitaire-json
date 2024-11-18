using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JSONMultiplier {
    public float    x;
    public float    y;
}
[System.Serializable]
public class JSONSlot {
    public int          id;
    public float        x;
    public float        y;
    public bool         faceUp = false;
    public string       layer = "Default";
    public string       hiddenByString;
}
[System.Serializable]
public class JSONPile {
    public float        x;
    public float        y;
    public string       layer = "Default";
    public float        xStagger;
}
[System.Serializable]
public class JSONSlotDef {
    public JSONMultiplier   multiplier;
    public JSONSlot[]       slots;
    public JSONPile         drawPile;
    public JSONPile         discardPile;
}

[System.Serializable]
public class SlotDef {
    public float        x;
    public float        y;
    public bool         faceUp = false;
    public string       layerName = "Default";
    public int          layerID = 0;
    public int          id;
    public List<int>    hiddenBy = new List<int>();
    public string       type = "slot";
    public Vector2      stagger;
}

public class Layout : MonoBehaviour {
    [Header("Set Dynamically")]
    public JSONSlotDef      jsonr;
    public Vector2          multiplier;
    public List<SlotDef>    slotDefs;
    public SlotDef          drawPile;
    public SlotDef          discardPile;

    public void ReadLayout(string jsonText) {
        jsonr = JsonUtility.FromJson<JSONSlotDef>(jsonText);

        multiplier.x = jsonr.multiplier.x;
        multiplier.y = jsonr.multiplier.y;

        SlotDef tSD;
        JSONSlot[] slots = jsonr.slots;
        for (int i = 0; i < slots.Length; i++) {
            tSD = new SlotDef();
            tSD.type = "slot";
            tSD.x = slots[i].x;
            tSD.y = slots[i].y;
            int.TryParse(slots[i].layer[^1].ToString(), out tSD.layerID);
            tSD.layerName = slots[i].layer;
            tSD.faceUp = slots[i].faceUp;
            tSD.id = slots[i].id;
            string[] hiding = slots[i].hiddenByString.Split(',');
            foreach (string s in hiding) {
                int hiddenBy;
                if (int.TryParse(s, out hiddenBy)) {
                    tSD.hiddenBy.Add(hiddenBy);
                }                
            }
            slotDefs.Add(tSD);
        }

        drawPile.x = jsonr.drawPile.x;
        drawPile.y = jsonr.drawPile.y;
        drawPile.stagger.x = jsonr.drawPile.xStagger;
        drawPile.type = "drawpile";
        drawPile.layerID = 4;
        drawPile.layerName = jsonr.drawPile.layer;

        discardPile.x = jsonr.discardPile.x;
        discardPile.y = jsonr.discardPile.y;
        discardPile.stagger.x = jsonr.discardPile.xStagger;
        discardPile.type = "discardpile";
        discardPile.layerID = 5;
        discardPile.layerName = jsonr.discardPile.layer;
    }
}