using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public virtual string Name { get; }
    public virtual string Description { get; }
    public virtual bool IsImmediatelyUse { get; }
    public virtual string ImagePath { get; } // TODO Addressableからロードするパス

    protected MeteorSpawner meteorSpawner;
    protected Turret turret;
    protected Ground ground;

    public void Init(MeteorSpawner meteorSpawner, Turret turret, Ground ground)
    {
        this.meteorSpawner = meteorSpawner;
        this.turret = turret;
        this.ground = ground;
    }

    public virtual void Use() { }
}

public class Clean : Item, IDisposable
{
    public override string Name => "クリーナー";
    public override string Description => "画面上の隕石を一掃する。";
    public override bool IsImmediatelyUse => false;
    public override string ImagePath => "image_cleaner";

    public override void Use()
    {
        meteorSpawner.BreakAllMeteors();
    }

    public void Dispose() { }
}
public class DoubleBeam : Item, IDisposable
{
    public override string Name => "ダブルビーム";
    public override string Description => "ビームが2本になる";
    public override bool IsImmediatelyUse => true;
    public override string ImagePath => "image_doubleBeam";

    public override void Use()
    {
        turret.SetBeamMode(Turret.BeamMode.Double);
    }

    public void Dispose() { }
}
public class CureTown : Item, IDisposable
{
    public override string Name => "街の復興";
    public override string Description => "街のライフが1回復する";
    public override bool IsImmediatelyUse => true;
    public override string ImagePath => "image_cureTown";

    public override void Use()
    {
        ground.Cure(1);
    }

    public void Dispose() { }
}