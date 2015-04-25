using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("UserConfiguration")]
public class UserConfiguration 
{
    public int lifeArmoredTower;
    public int speedArmoredTower;

    public int bestTime;
    public int tutorial;
    
    public int totalExplosiveBullet;
    public int totalTraps;
    public int totalPulses;

    public int totalPlasma;

    public int totalSpiderKilled;
    public int totalWaspKilled;
}
