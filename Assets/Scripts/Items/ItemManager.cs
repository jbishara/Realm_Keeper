using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] public Dictionary<Items, GameObject> ItemDatabase;
    




    public enum Items
    {
        LuckyDice,
        ToxicFlask,
        ImpsHand,
        Sword,
        Dagger,
        Bow,
        TargetSign,
        KnightsHelmet,
        Apple,
        VampireFangs,
        RangersHat,
        ChestPiece,
        SpikedShoulderPads,

    }
}

