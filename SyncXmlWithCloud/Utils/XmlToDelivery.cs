
using JokerService.SyncXmlWithCloud.Models;
using JokerService.SyncXmlWithCloud.UseCase;

class XmlToDelivery
{
  readonly XmlParser _parsedXml;

  public XmlToDelivery(XmlParser parsedXml)
  {
    _parsedXml = parsedXml;
  }

  public Delivery? CreateDelivery()
  {
    string? chNFe = _parsedXml.GetChNFe();
    if (chNFe == null) return null;

    string? dhEmit = _parsedXml.GetDhEmit();
    if (dhEmit == null) return null;

    string? document = _parsedXml.GetNumDocument();
    if (document == null) return null;

    string? emitterName = _parsedXml.GetxNomeEmit();
    if (emitterName == null) return null;

    string? emitterDocument = _parsedXml.GetCnpjEmit();
    if (emitterDocument == null) return null;

    string? emitterCity = _parsedXml.GetxEnderEmitMun();
    if (emitterCity == null) return null;

    string? emitterState = _parsedXml.GetxEnderEmitUF();
    if (emitterState == null) return null;

    Emitter emitter = new()
    {
      name = emitterName,
      document = emitterDocument,
      city = emitterCity,
      state = emitterState,
    };

    string? receiverName = _parsedXml.GetxNomeDest();
    if (receiverName == null) return null;

    string? receiverDocument = _parsedXml.GetCnpjDest();
    if (receiverDocument == null) return null;

    string? receiverCity = _parsedXml.GetxEnderDestMun();
    if (receiverCity == null) return null;

    string? receiverState = _parsedXml.GetxEnderDestUF();
    if (receiverState == null) return null;

    Receiver receiver = new()
    {
      name = receiverName,
      document = receiverDocument,
      city = receiverCity,
      state = receiverState,
    };

    string? carrierName = _parsedXml.GetxNomeDest();
    string? carrierDocument = _parsedXml.GetCnpjDest();
    string? carrierCity = _parsedXml.GetxEnderDestMun();
    string? carrierState = _parsedXml.GetxEnderDestUF();

    Carrier? carrier = null;
    if (carrierName != null && carrierDocument != null)
    {
      carrier = new()
      {
        name = carrierName,
        document = carrierDocument,
        city = carrierCity,
        state = carrierState,
      };
    }

    Delivery delivery = new()
    {
      key = chNFe,
      receiver = receiver,
      emitter = emitter,
      document = document,
      issueDate = dhEmit,
      carrier = carrier
    };

    return delivery;
  }
}