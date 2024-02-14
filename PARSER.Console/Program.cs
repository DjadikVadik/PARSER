using Microsoft.Data.SqlClient;
using PARSER.Console;
using PARSER.Data.Models;
using PARSER.Database;
using PARSER.Domain.ModelsDomain;
using PARSER.Infrastructure;
using PARSER.Parser;
using PARSER.Parser.Implementation;

internal class Program
{
    async static Task Main(string[] args)
    {
        /* проэкт состоит из: 
         - консольного приложения PARSER.Console
         - библиотеки PARSER.Database в которой находятся все SQL команды для построения базы данных
         - библиотеки PARSER.Data в ней находятся модели и репозитории для работы с базой данных, а также мапер для преобразования в бизнесс модель
         - библиотеки PARSER.Domain в ней находятся бизнесс модели
         - библиотеки PARSER.Infosructure в ней находится бизнесс логика
         - библиотеки PARSER.Parser в ней находится непосредственно парсер данных*/

        // в конце выполнения программы я удаляю созданную базу, что бы можно было проверить код и не создавать ее у себя
        // удаляю базу прямо здесь, что бы можно было закоментировать и посмотреть как создается база

        // создадим conection и conection string и попытаемся открыть соединение с базой данных

        var conectString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;";
        using var conection = new SqlConnection(conectString);

        try
        {
            conection.Open();
            Console.WriteLine("Connection is open!");
        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.Message);
        }

        // создадим SQL команду
        // все манипуляции выполняю здесь для наглядности 

        using var command = conection.CreateCommand();

        // создадим нужную нам базу данных
        await Creator.CreateDataBase(command);

        // создадим нужные нам таблицы
        await Creator.CreateTables(command);

        // создадим хранимые процедуры для таблицы Model 
        await Creator.CreateStoredProcedureByModel(command);

        // создадим хранимые процедуры для таблицы Equipment
        await Creator.CreateStoredProcedureByEquipment(command);

        // создадим хранимые процедуры для таблицы EquipmentInfo
        await Creator.CreateStoredProcedureByEquipmentInfo(command);

        // создадим хранимые процедуры для таблицы Group
        await Creator.CreateStoredProcedureByGroups(command);

        // создадим хранимые процедуры для таблицы Subgroup
        await Creator.CreateStoredProcedureBySubgroup(command);

        // создадим хранимые процедуры для таблицы Product
        await Creator.CreateStoredProcedureByProduct(command);

        // создадим хранимые процедуры для таблицы Image
        await Creator.CreateStoredProcedureByImage(command);

        // так как я не знаю как будет использоваться база, подумал что кластеризированных индексов, что создаст база будет достаточно

        // создаем контроллеры для работы с базой данных

        var modelController = new ModelController(command);
        var equipmentController = new EquipmentController(command);
        var equipmentInfoController = new EquipmentInfoController(command);
        var groupController = new GroupController(command);
        var subgroupController = new SubgroupController(command);
        var productController = new ProductController(command);
        var imageController = new ImageController(command);

        // создаем клиент и настройки для работы парсера
        using var client = new HttpClient();
        var setings = new ParserSetings(client);

        // пытаемся спарсить данные в базу
        var listModels = new ModelEquipmentDomainParser(setings).GetModelDomains();
        if (await modelController.AddRangeAsync(listModels)) Console.WriteLine("Model loaded!!!");

        var listEquipment = new ModelEquipmentDomainParser(setings).GetEquipmentDomains();
        if (await equipmentController.AddRangeAsync(listEquipment)) Console.WriteLine("Equipment loaded!!!");

        setings.URL = "https://www.ilcats.ru/toyota/?function=getComplectations&market=EU&model=671440&startDate=198308&endDate=198903";

        var listEquipmentInfo = new EquipmentInfoDomainParser(setings).GetEquipmentInfos();
        if (await equipmentInfoController.AddRangeAsync(listEquipmentInfo)) Console.WriteLine("EquipmentInfos loaded!!!");

        setings.URL = "https://www.ilcats.ru/toyota/?function=getGroups&market=EU&model=671440&modification=LN51L-KRA&complectation=001";

        var listGroup = new GroupDomainParser(setings).GetGroupDomains();
        if (await groupController.AddRangeAsync(listGroup)) Console.WriteLine("Group loaded!!!");

        setings.URL = "https://www.ilcats.ru/toyota/?function=getSubGroups&market=EU&model=671440&modification=LN51L-KRA&complectation=001&group=1";

        var listSubgroup = new SubgroupDomainParser(setings).GetSubgroupDomains();
        if (await subgroupController.AddRangeAsync(listSubgroup)) Console.WriteLine("Subgroup loaded!!!");

        setings.URL = "https://www.ilcats.ru/toyota/?function=getParts&market=EU&model=671440&modification=LN51L-KRA&complectation=001&group=1&subgroup=0901";

        var listProduct = new ProductDomainParser(setings).GetProductDomains();
        if (await productController.AddRangeAsync(listProduct, 1)) Console.WriteLine("Product loaded!!!");

        var listImage = new ImageDomainParser(setings).GetImageDomain();
        if (await imageController.AddSingleAsync(listImage)) Console.WriteLine("Image loaded!!!");

        // забераем данные с нашей базы

        var models = await modelController.GetAllAsync();
        if (models != null) Printer.Print(models);

        var equipments = await equipmentController.GetAllAsync(10);
        if (equipments != null) Printer.Print(equipments);

        var equipmentsInfos = await equipmentInfoController.GetAllAsync(1);
        if (equipmentsInfos != null) Printer.Print(equipmentsInfos);

        var groups = await groupController.GetAllAsync();
        if (groups != null) Printer.Print(groups);

        var subgroups = await subgroupController.GetAllAsync(1);
        if (subgroups != null) Printer.Print(subgroups);

        var products = await productController.GetAllAsync(1, 1);
        if (products != null) Printer.Print(products);

        var images = await imageController.GetAllAsync();
        if (images != null) Printer.Print(images);

        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = "USE master; DROP DATABASE Parser";
        await command.ExecuteNonQueryAsync();
    }
}
