using RateWatch.Domain.ECB.Commons;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RateWatch.Domain.ECB;

[XmlRoot(ElementName = "Envelope", Namespace = EcbXmlNamespaces.GesmesNamespace)]
public class Envelope
{
    [XmlElement(ElementName = "subject", Namespace = EcbXmlNamespaces.GesmesNamespace)]
    public string Subject { get; set; } = null!;

    [XmlElement(ElementName = "Sender", Namespace = EcbXmlNamespaces.GesmesNamespace)]
    public Sender Sender { get; set; } = null!;

    [XmlElement(ElementName = "Cube", Namespace = EcbXmlNamespaces.EcbNamespace)]
    public OuterCube Cube { get; set; } = null!;
}

public class Sender
{
    [XmlElement(ElementName = "name", Namespace = EcbXmlNamespaces.GesmesNamespace)]
    public string Name { get; set; } = null!;
}

public class OuterCube
{
    [XmlElement(ElementName = "Cube", Namespace = EcbXmlNamespaces.EcbNamespace)]
    public List<TimeCube> Cubes { get; set; } = [];
}

public class TimeCube
{
    [XmlAttribute(AttributeName = "time")]
    public string Time { get; set; } = null!;

    [XmlElement(ElementName = "Cube", Namespace = EcbXmlNamespaces.EcbNamespace)]
    public List<RateCube> Rates { get; set; } = [];
}

public class RateCube
{
    [XmlAttribute(AttributeName = "currency")]
    public string Currency { get; set; } = null!;

    [XmlAttribute(AttributeName = "rate")]
    public string Rate { get; set; } = null!;
}
