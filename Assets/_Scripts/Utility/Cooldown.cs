using UnityEngine;
using System.Collections;

public class Cooldown
{
    private float _cooldownEndTime
    {
        get { return _cooldownStartTime + _cooldownDuration; }
    }
    private float _cooldownDuration;
    private float _cooldownStartTime;
    private bool _isOnCooldown;

    public float Percentage
    {
        get
        {
            float percentage = 1 - (_cooldownEndTime - Time.time) / _cooldownDuration;
            return percentage;
        }
    }

    public float Duration
    {
        get { return _cooldownDuration; }
        set { _cooldownDuration = Mathf.Abs(value); }
    }

    public bool IsOnCooldown
    {
        get
        {
            return Time.time < _cooldownEndTime;
        }
        set
        {
            if (value)
            {
                _cooldownStartTime = Time.time;
            }
            else
            {
                _cooldownStartTime = Time.time - _cooldownDuration;
            }
        }

    }

    public Cooldown(float cooldownDuration)
    {
        _cooldownDuration = cooldownDuration;
        _cooldownStartTime = Time.time;
    }
}
