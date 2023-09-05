using JokerService.SyncXmlWithCloud.Models;
using JokerService.SyncXmlWithCloud.Services;
using JokerService.SyncXmlWithCloud.UseCase;

public class XmlToCloudSyncer
{
    private FileSystemWatcher? watcher;
    private CompanyConfig companyConfig;
    private readonly string xmlFolderPath;
    private readonly DeliveryAPIService _deliveryAPI;

    public XmlToCloudSyncer(string _xmlFolderPath, CompanyConfig _companyConfig)
    {
        _deliveryAPI = new();
        xmlFolderPath = _xmlFolderPath;
        companyConfig = _companyConfig;
    }

    /** Configura o FileSystemWatcher para monitorar a pasta de XMLs. */
    public FileSystemWatcher InitializeWatcher()
    {
        watcher = new FileSystemWatcher(xmlFolderPath, "*.xml")
        {
            NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName,
            IncludeSubdirectories = true,
        };

        /** Associa o manipulador de eventos para o evento "Created" */
        watcher.Created += HandleFileCreatedEvent;

        watcher.EnableRaisingEvents = true;
        return watcher;
    }

    /** Método chamado quando um novo arquivo XML é criado na pasta monitorada */
    private void HandleFileCreatedEvent(object sender, FileSystemEventArgs e)
    {
        string xmlFileFullPath = e.FullPath;

        /** Verifica se o XML deve ser filtrado com base no CNPJ das empresas */
        if (ShouldFilterXmlByCompanyCNPJ(xmlFileFullPath))
        {
            return;
        }

        /** Verifica se o evento é relevante (criação de arquivo) 
            e não é um arquivo já comprovado/invalido (com sufixo _comprova)
         */
        if (!IsFileCreatedEventRelevant(e))
        {
            return;
        }

        /** Verifica se o caminho do XML deve ser ignorado com base nas configurações da empresa */
        if (
            ShouldIgnoreFilePath(xmlFileFullPath)
        )
        {
            /** Adiciona sufixo "_comprova" ao nome do arquivo para não ser mais assistido */
            RenameXmlFileWithSufix(xmlFileFullPath);
            return;
        }

        XmlParser parsedXml = new(xmlFileFullPath);
        /** Envia o XML para a nuvem após as validações */
        SendXmlToCloud(parsedXml);
    }

    private bool ShouldFilterXmlByCompanyCNPJ(string xmlFileFullPath)
    {
        if (!companyConfig.DocumentSetup.FoldersSetup.FilterByCNPJ) return false;
        if (companyConfig?.Cnpj == null) return false;

        XmlParser parsedXml = new(xmlFileFullPath);
        string? cnpjEmitter = parsedXml.GetCnpjEmit();

        if (cnpjEmitter == null)
            return false;

        if (companyConfig.Cnpj != cnpjEmitter)
        {
            CompanyConfig? xmlEmittingCompany = LoadCompanyResponsibleForXml(cnpjEmitter);
            if (xmlEmittingCompany == null)
                return false;

            companyConfig = xmlEmittingCompany;
        }

        return true;
    }

    private static CompanyConfig? LoadCompanyResponsibleForXml(string cnpjEmitter)
    {
        try
        {
            List<CompanyConfig>? companiesConfigFiles = ConectaFacilDirectoryHelper.LoadConfiguredCompaniesOnConfigsFolder();

            if (companiesConfigFiles != null)
            {
                foreach (CompanyConfig companyConfig in companiesConfigFiles)
                {
                    if (companyConfig.Cnpj == cnpjEmitter)
                        return companyConfig;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the XML file: {ex.Message}");
        }
        return null;
    }

    private static bool IsFileCreatedEventRelevant(FileSystemEventArgs e)
    {
        return e.ChangeType == WatcherChangeTypes.Created &&
                e.Name != null &&
                !e.Name.Contains("comprova");
    }

    private bool ShouldIgnoreFilePath(string xmlFileFullPath)
    {
        return companyConfig?.DocumentSetup.FoldersSetup.IsPathIgnored(xmlFileFullPath) ?? false;
    }

    private void RenameXmlFileWithSufix(string sourceXmlFileName)
    {
        try
        {
            string? directory = Path.GetDirectoryName(sourceXmlFileName);
            if (!Directory.Exists(directory))
            {
                return;
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceXmlFileName);
            string extension = Path.GetExtension(sourceXmlFileName);

            string newFileName = $"{fileNameWithoutExtension}_comprova{extension}";
            string destinationXmlFilename = Path.Combine(directory, newFileName);

            while (true)
            {
                Thread.Sleep(500);
                if (XmlFileIsAvailable(sourceXmlFileName))
                {
                    File.Move(sourceXmlFileName, destinationXmlFilename);
                    Console.WriteLine("Added 'comprova' sufix to filename.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while renaming the file: {ex.Message}");
        }
    }

    private bool XmlFileIsAvailable(string xmlFileFullPath)
    {
        try
        {
            using FileStream fileInfo = new(xmlFileFullPath, FileMode.Open, FileAccess.Read, FileShare.None);
            return true;
        }
        catch (IOException)
        {
            return false;
        }
    }

    //verify xml type
    private void SendXmlToCloud(XmlParser parsedXml)
    {
        bool xmlIsCancellationEvent = XmlEventTypeIsCancellation(parsedXml);
        if (xmlIsCancellationEvent)
        {
            /** Extrai a chave da NFe e solicitação de cancelamento para a API */
            ExtractChNFeAndRequestCancellation(parsedXml);
            return;
        }
        /** Extrai os valores da NFe e solicitação a criação de uma entrega para a API */
        ExtractNFeValuesAndRequestCreationDelivery(parsedXml);
    }

    private static bool XmlEventTypeIsCancellation(XmlParser parsedXml)
    {
        string? eventType = parsedXml.GetTpEvento();
        return eventType == "110111";
    }

    private async void ExtractChNFeAndRequestCancellation(XmlParser parsedXml)
    {
        string? keyNFe = parsedXml.GetChNFe();
        if (keyNFe == null) return;

        await _deliveryAPI.RequestCancellation(keyNFe);
        return;
    }

    private async void ExtractNFeValuesAndRequestCreationDelivery(XmlParser parsedXml)
    {
        XmlToDelivery xmlToDelivery = new(parsedXml);
        Delivery? delivery = xmlToDelivery.CreateDelivery();
        if (delivery == null) return;

        await _deliveryAPI.RequestCreateNewDelivery(delivery, companyConfig.Token);
    }
}