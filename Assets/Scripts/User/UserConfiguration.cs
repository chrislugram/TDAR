using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("UserConfiguration")]
public class UserConfiguration 
{
     public int damageArmoredTower;
     public int speedArmoredTower;
     public int bestTimeInEasy;
     public int bestTimeInMedium;
     public int bestTimeInHard;
     public int totalMissiles;
     public int totalTraps;
     public int totalPulses;
}
