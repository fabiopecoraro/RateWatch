using System.Text;
using System.Xml.Serialization;
using RateWatch.Domain.ECB;
//using RateWatch.Infrastructure.ExternalServices;
using Xunit;

namespace RateWatch.UnitTests.Infrastructure;

public class EcbXmlDeserializationTests
{
    [Fact]
    public void Should_Deserialize_EcbXml_To_Envelope()
    {
        // Arrange: XML di esempio (mockato)
        const string xml = """
        <?xml version="1.0" encoding="UTF-8"?>
        <gesmes:Envelope xmlns:gesmes="http://www.gesmes.org/xml/2002-08-01"
                         xmlns="http://www.ecb.int/vocabulary/2002-08-01/eurofxref">
          <gesmes:subject>Reference rates</gesmes:subject>
          <gesmes:Sender>
            <gesmes:name>European Central Bank</gesmes:name>
          </gesmes:Sender>
          <Cube>
            <Cube time="2025-04-03">
              <Cube currency="USD" rate="1.0874"/>
              <Cube currency="GBP" rate="0.8526"/>
            </Cube>
          </Cube>
        </gesmes:Envelope>
        """;

        var serializer = new XmlSerializer(typeof(Envelope));

        // Act
        using var reader = new StringReader(xml);
        var envelope = serializer.Deserialize(reader) as Envelope;

        // Assert
        Assert.NotNull(envelope);
        Assert.Equal("Reference rates", envelope.Subject);
        Assert.Equal("European Central Bank", envelope.Sender.Name);

        var dayCube = envelope.Cube.Cubes.Single();
        Assert.Equal("2025-04-03", dayCube.Time);
        Assert.Equal(2, dayCube.Rates.Count);
        Assert.Contains(dayCube.Rates, r => r.Currency == "USD" && r.Rate == "1.0874");
        Assert.Contains(dayCube.Rates, r => r.Currency == "GBP" && r.Rate == "0.8526");
    }
}
