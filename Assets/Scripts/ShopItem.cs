using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem
{
    public virtual string Name { get; }
    public virtual int Price { get; }
    public virtual string Description { get; }
    public virtual int EntryDay { get; }
    public virtual int Weight { get; }

    protected GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public virtual void Exec() { }
}

public class Bomb : ShopItem
{
    public override string Name => "ボム";
    public override int Price => 10;
    public override string Description => "画面上の隕石を一掃する。";
    public override int EntryDay => 1;
    public override int Weight => 10;
    public override void Exec()
    {
        gameManager.BreakAllMeteor();
    }
}

public class AttackUp : ShopItem
{
    public override string Name => "攻撃力アップ";
    public override int Price => 20;
    public override string Description => "攻撃力が1上がる";
    public override int EntryDay => 1;
    public override int Weight => 2;
    public override void Exec()
    {
        gameManager.BreakAllMeteor();
    }
}
