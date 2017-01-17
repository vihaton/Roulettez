using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Xml.Serialization;

public class FeelingSaveData {

    [XmlAttribute("date")]
     public DateTime date;
     public TunneStruct feeling;
}

