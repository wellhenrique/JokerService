using System.Xml;

namespace JokerService.SyncXmlWithCloud.UseCase
{
  public class XmlParser
  {
    private readonly XmlDocument xmlDoc;

    public XmlParser(string xmlFilePath)
    {
      xmlDoc = new();
      xmlDoc.Load(xmlFilePath);
    }

    public string? GetTpEvento()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? tpEventoValue = xmlDoc.SelectSingleNode("//nfe:tpEvento", namespaceManager);

      if (tpEventoValue != null)
      {
        string tpEvento = tpEventoValue.InnerText;
        return tpEvento;
      }
      return null;
    }

    public string? GetChNFe()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? ChNFeValue = xmlDoc.SelectSingleNode("//nfe:chNFe", namespaceManager);

      if (ChNFeValue != null)
      {
        string ChNFe = ChNFeValue.InnerText;
        return ChNFe;
      }
      return null;
    }

    public string? GetCnpjEmit()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? cnpjEmitterValue = xmlDoc.SelectSingleNode("//nfe:CNPJ", namespaceManager);
      if (cnpjEmitterValue != null)
      {
        string cnpjValue = cnpjEmitterValue.InnerText;
        return cnpjValue;
      }
      return null;
    }

    public string? GetxNomeEmit()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? xNomeEmitValue = xmlDoc.SelectSingleNode("//nfe:emit/nfe:xNome", namespaceManager);
      if (xNomeEmitValue != null)
      {
        string xNomeValue = xNomeEmitValue.InnerText;
        return xNomeValue;
      }
      return null;
    }

    public string? GetxEnderEmitMun()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? xMunEmitValue = xmlDoc.SelectSingleNode("//nfe:emit/nfe:enderEmit/nfe:xMun", namespaceManager);
      if (xMunEmitValue != null)
      {
        string xMunValue = xMunEmitValue.InnerText;
        return xMunValue;
      }
      return null;
    }

    public string? GetxEnderEmitUF()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? xUFEmitValue = xmlDoc.SelectSingleNode("//nfe:emit/nfe:enderEmit/nfe:UF", namespaceManager);
      if (xUFEmitValue != null)
      {
        string xUFValue = xUFEmitValue.InnerText;
        return xUFValue;
      }
      return null;
    }

    public string? GetCnpjDest()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? cnpjReceiverValue = xmlDoc.SelectSingleNode("//nfe:dest/nfe:CNPJ", namespaceManager);
      if (cnpjReceiverValue != null)
      {
        string cnpjValue = cnpjReceiverValue.InnerText;
        return cnpjValue;
      }
      return null;
    }

    public string? GetxNomeDest()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? xNomeDestValue = xmlDoc.SelectSingleNode("//nfe:dest/nfe:xNome", namespaceManager);
      if (xNomeDestValue != null)
      {
        string xNomeValue = xNomeDestValue.InnerText;
        return xNomeValue;
      }
      return null;
    }

    public string? GetxEnderDestMun()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? xMunDestValue = xmlDoc.SelectSingleNode("//nfe:dest/nfe:enderDest/nfe:xMun", namespaceManager);
      if (xMunDestValue != null)
      {
        string xMunValue = xMunDestValue.InnerText;
        return xMunValue;
      }
      return null;
    }

    public string? GetxEnderDestUF()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? xUFEmitValue = xmlDoc.SelectSingleNode("//nfe:dest/nfe:enderDest/nfe:UF", namespaceManager);
      if (xUFEmitValue != null)
      {
        string xUFValue = xUFEmitValue.InnerText;
        return xUFValue;
      }
      return null;
    }

    public string? GetDhEmit()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? dhEmitValue = xmlDoc.SelectSingleNode("//nfe:dhEmi", namespaceManager);
      if (dhEmitValue != null)
      {
        string dhEmit = dhEmitValue.InnerText;
        return dhEmit;
      }
      return null;
    }

    public string? GetNumDocument()
    {
      XmlNamespaceManager namespaceManager = new(xmlDoc.NameTable);
      namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");

      XmlNode? numSerieValue = xmlDoc.SelectSingleNode("//nfe:serie", namespaceManager);
      if (numSerieValue == null) return null;
      string numSerie = numSerieValue.InnerText;

      XmlNode? nNFValue = xmlDoc.SelectSingleNode("//nfe:nNF", namespaceManager);
      if (nNFValue == null) return null;
      string numNFe = numSerieValue.InnerText;

      return $"{numSerie}/{numNFe}";
    }
  }
}