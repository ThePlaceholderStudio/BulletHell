using System;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    Shotgun,
    SMG,
    Rifle,
    None
}

public enum Utility
{
    LifeRegen,
    Shield,
    Magnet,
    None
}

public enum ItemType
{
    Utility,
    Weapon,
    WeaponUgrade,
    Trait
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    None
}

[CreateAssetMenu]
public class EquippableItem : Item
{
    public event Action<EquippableItem> OnItemEquippedEvent;

    public float MaxHpBonus;
    public float LifeRegenBonus;
    public float ArmorBonus;
    public float DashCoolDownBonus;
    public float DashRangeBonus;
    public float MoveSpeedBonus;
    public float DamageBonus;
    public float FireRateBonus;
    public float ReloadSpeedBonus;
    public float CriticalChanceBonus;
    public float CriticalDamageBonus;
    public float PickUpRadiusBonus;
    public float XPGainBonus;
    [Space]
    public float MaxHpPercentBonus;
    public float LifeRegenPercentBonus;
    public float ArmorPercentBonus;
    public float DashCooldownPercentBonus;
    public float DashRangePercentBonus;
    public float MoveSpeedPercentBonus;
    public float DamagePercentBonus;
    public float FireRatePercentBonus;
    public float ReloadSpeedPercentBonus;
    public float CriticalChancePercentBonus;
    public float CriticalDamagePercentBonus;
    public float PickUpRadiusPercentBonus;
    public float XPGainPercentBonus;
    [Space]
    public Utility Utility;
    public ItemType ItemType;
    public Rarity EquipmentRarity;
    [Space]
    [Header("Weapon Specs")]
    public WeaponType WeaponType;
    public int Range;
    public int MuzzleVelocity;
    public int ReloadTime;
    public int MagazineSize;
    public int RPM;
    public int ProjectileCount;
    public int TargetPenetration;
    public int ConicalAngle;
    [Space]
    [Header("Weapon Bonuses")]
    public int RangePercentBonus;
    public int ReloadTimePercentBonus;
    public int MagazineBonus;
    public int RPMPercentBonus;
    public int ProjecticleCountBonus;
    public int TargetPenetrationBonus;
    public int ConicalAnglePercentBonus;


    public void Equip(Character c)
    {
        OnItemEquippedEvent?.Invoke(this);

        #region ItemStatsFlat
        if (MaxHpBonus != 0)
            c.MaxHp.AddModifier(new StatModifier(MaxHpBonus, StatModType.Flat, this));

        if (LifeRegenBonus != 0)
            c.LifeRegen.AddModifier(new StatModifier(LifeRegenBonus, StatModType.Flat, this));

        if (ArmorBonus != 0)
            c.Armor.AddModifier(new StatModifier(ArmorBonus, StatModType.Flat, this));

        if (DashCoolDownBonus != 0)
            c.DashCoolDown.AddModifier(new StatModifier(DashCoolDownBonus, StatModType.Flat, this));

        if (DashRangeBonus != 0)
            c.DashRange.AddModifier(new StatModifier(DashRangeBonus, StatModType.Flat, this));

        if (MoveSpeedBonus != 0)
            c.MoveSpeed.AddModifier(new StatModifier(MoveSpeedBonus, StatModType.Flat, this));

        if (DamageBonus != 0)
            c.Damage.AddModifier(new StatModifier(DamageBonus, StatModType.Flat, this));

        if (FireRateBonus != 0)
            c.FireRate.AddModifier(new StatModifier(FireRateBonus, StatModType.Flat, this));

        if (ReloadSpeedBonus != 0)
            c.ReloadSpeed.AddModifier(new StatModifier(ReloadSpeedBonus, StatModType.Flat, this));

        if (CriticalChanceBonus != 0)
            c.CriticalChance.AddModifier(new StatModifier(CriticalChanceBonus, StatModType.Flat, this));

        if (CriticalDamageBonus != 0)
            c.CriticalDamage.AddModifier(new StatModifier(CriticalDamageBonus, StatModType.Flat, this));

        if (PickUpRadiusBonus != 0)
            c.PickUpRadius.AddModifier(new StatModifier(PickUpRadiusBonus, StatModType.Flat, this));

        if (XPGainBonus != 0)
            c.XPGain.AddModifier(new StatModifier(XPGainBonus, StatModType.Flat, this));
        #endregion ItemStatsFlat

        #region ItemStatsPercentage
        if (MaxHpPercentBonus != 0)
            c.MaxHp.AddModifier(new StatModifier(MaxHpPercentBonus, StatModType.PercentMult, this));

        if (LifeRegenPercentBonus != 0)
            c.LifeRegen.AddModifier(new StatModifier(LifeRegenPercentBonus, StatModType.PercentMult, this));

        if (ArmorPercentBonus != 0)
            c.Armor.AddModifier(new StatModifier(ArmorPercentBonus, StatModType.PercentMult, this));

        if (DashCooldownPercentBonus != 0)
            c.DashCoolDown.AddModifier(new StatModifier(DashCooldownPercentBonus, StatModType.PercentMult, this));

        if (DashRangePercentBonus != 0)
            c.DashRange.AddModifier(new StatModifier(DashRangePercentBonus, StatModType.PercentMult, this));

        if (MoveSpeedPercentBonus != 0)
            c.MoveSpeed.AddModifier(new StatModifier(MoveSpeedPercentBonus, StatModType.PercentMult, this));

        if (DamagePercentBonus != 0)
            c.Damage.AddModifier(new StatModifier(DamagePercentBonus, StatModType.PercentMult, this));

        if (FireRatePercentBonus != 0)
            c.FireRate.AddModifier(new StatModifier(FireRatePercentBonus, StatModType.PercentMult, this));

        if (ReloadSpeedPercentBonus != 0)
            c.ReloadSpeed.AddModifier(new StatModifier(ReloadSpeedPercentBonus, StatModType.PercentMult, this));

        if (CriticalChancePercentBonus != 0)
            c.CriticalChance.AddModifier(new StatModifier(CriticalChancePercentBonus, StatModType.PercentMult, this));

        if (CriticalDamagePercentBonus != 0)
            c.CriticalDamage.AddModifier(new StatModifier(CriticalDamagePercentBonus, StatModType.PercentMult, this));

        if (PickUpRadiusPercentBonus != 0)
            c.PickUpRadius.AddModifier(new StatModifier(PickUpRadiusPercentBonus, StatModType.PercentMult, this));

        if (XPGainPercentBonus != 0)
            c.XPGain.AddModifier(new StatModifier(XPGainPercentBonus, StatModType.PercentMult, this));
        #endregion ItemStatsPercentage

        #region WeaponStats
        if (RangePercentBonus != 0)
            c.Range.AddModifier(new StatModifier(RangePercentBonus, StatModType.PercentMult, this));

        if (ReloadTimePercentBonus != 0)
            c.ReloadTime.AddModifier(new StatModifier(ReloadTimePercentBonus, StatModType.PercentMult, this));

        if (MagazineBonus != 0)
            c.MagazineSize.AddModifier(new StatModifier(MagazineBonus, StatModType.PercentMult, this));

        if (RPMPercentBonus != 0)
            c.RPM.AddModifier(new StatModifier(RPMPercentBonus, StatModType.PercentMult, this));

        if (ProjecticleCountBonus != 0)
            c.ProjectileCount.AddModifier(new StatModifier(ProjecticleCountBonus, StatModType.PercentMult, this));

        if (TargetPenetrationBonus != 0)
            c.TargetPenetration.AddModifier(new StatModifier(TargetPenetrationBonus, StatModType.PercentMult, this));

        if (ConicalAnglePercentBonus != 0)
            c.ConicalAngle.AddModifier(new StatModifier(ConicalAnglePercentBonus, StatModType.PercentMult, this));
        #endregion WeaponStats
    }
}
