using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Database
{
    public static class Creator
    {
        async public static Task CreateDataBase(SqlCommand command)
        {
            command.CommandText = "CREATE DATABASE Parser";
            await command.ExecuteNonQueryAsync();

            // устанавливаем нашу базу в качастве основной для выполнения команд
            command.CommandText = "USE Parser;";
            await command.ExecuteNonQueryAsync();
        }

        async public static Task CreateTables(SqlCommand command)
        {
            // создаем таблицу для моделей машин
            command.CommandText =
                "CREATE TABLE Model (" +
                "Id INT PRIMARY KEY IDENTITY(1,1)," +
                "Name NVARCHAR(50) NOT NULL)";
            await command.ExecuteNonQueryAsync();

            // создаем таблицу описания модификаций
            command.CommandText =
                "CREATE TABLE Equipment (" +
                "Id INT PRIMARY KEY IDENTITY(1,1)," +
                "Code NVARCHAR(50) NOT NULL," +
                "Date NVARCHAR(50) NOT NULL," +
                "AllCodes NVARCHAR(50) NOT NULL," +
                "ModelId INT REFERENCES Model(Id) ON DELETE CASCADE)";
            await command.ExecuteNonQueryAsync();

            // создаем таблицу подробного описания модификаций
            command.CommandText =
                "CREATE TABLE EquipmentInfo (" +
                "Id INT PRIMARY KEY IDENTITY(1,1)," +
                "Name NVARCHAR(50) NOT NULL," +
                "Date NVARCHAR(50) NOT NULL," +
                "ENGINE NVARCHAR(50) NOT NULL," +
                "BODY NVARCHAR(50) NOT NULL," +
                "GRADE NVARCHAR(50) NOT NULL," +
                "ATM_MTM NVARCHAR(50) NOT NULL," +
                "GEAR_SHIFT_TYPE NVARCHAR(50) NOT NULL," +
                "CAB NVARCHAR(50) NOT NULL," +
                "TRANSMISSION_MODEL NVARCHAR(50) NOT NULL," +
                "LOADING_CAPACITY NVARCHAR(50) NOT NULL," +
                "EquipmentId INT REFERENCES Equipment(Id) ON DELETE CASCADE)";
            await command.ExecuteNonQueryAsync();

            // создаем таблицу для категорий товаров
            command.CommandText =
                "CREATE TABLE Groups (" +
                "Id INT PRIMARY KEY IDENTITY(1,1)," +
                "Name NVARCHAR(50) NOT NULL)";
            await command.ExecuteNonQueryAsync();

            // создаем таблицу для подкатегорий товаров
            command.CommandText =
                "CREATE TABLE Subgroup (" +
                "Id INT PRIMARY KEY IDENTITY(1,1)," +
                "Name NVARCHAR(50) NOT NULL," +
                "GroupsId INT REFERENCES Groups(Id) ON DELETE CASCADE)";
            await command.ExecuteNonQueryAsync();

            // создаем таблицу для товаров
            command.CommandText =
                "CREATE TABLE Product (" +
                "Id INT PRIMARY KEY IDENTITY(1,1)," +
                "Code NVARCHAR(50) NOT NULL," +
                "Count INT," +
                "Date NVARCHAR(50) NOT NULL," +
                "Info NVARCHAR(50) NOT NULL," +
                "Tree_code NVARCHAR(50) NOT NULL," +
                "Tree NVARCHAR(50) NOT NULL," +
                "SubgroupId INT REFERENCES Subgroup(Id) ON DELETE CASCADE)";
            await command.ExecuteNonQueryAsync();

            // создаем таблицу для изображений подкатегорий
            command.CommandText =
                "CREATE TABLE Image (" +
                "Id INT PRIMARY KEY IDENTITY(1,1)," +
                "Name NVARCHAR(50) NOT NULL," +
                "SubgroupId INT REFERENCES Subgroup(Id) ON DELETE CASCADE)";
            await command.ExecuteNonQueryAsync();

            // так как, по моему мнению, один продукт может быть в разных подкатегориях модефикаций
            // создадим тоблицу Product_EquipmentInfo для осуществления связи многие ко многим

            command.CommandText =
               "CREATE TABLE Product_EquipmentInfo (" +
               "EquipmentInfoId INT REFERENCES EquipmentInfo(Id) ON DELETE CASCADE," +
               "ProductId INT REFERENCES Product(Id) ON DELETE CASCADE," +
               "PRIMARY KEY(EquipmentInfoId,ProductId))";
            await command.ExecuteNonQueryAsync();
        }

        async public static Task CreateStoredProcedureByModel(SqlCommand command)
        {
            command.CommandText = @"
                CREATE PROCEDURE AddModel
                @name NVARCHAR(50)
                AS
                INSERT INTO Model(Name)
                VALUES(@name)";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE UpdateModel
            @Id INT,
            @name NVARCHAR(50)
            AS
            UPDATE Model
            SET Name = @name
            WHERE Model.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE GetSingleModel
            @Id INT
            AS
            SELECT * FROM Model
            WHERE Model.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
           CREATE PROCEDURE GetAllModel
           AS
           SELECT * FROM Model";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE DeleteModel
            @Id INT
            AS
            DELETE Model
            WHERE Model.Id = @Id";
            await command.ExecuteNonQueryAsync();
        }

        async public static Task CreateStoredProcedureByEquipment(SqlCommand command)
        {
            command.CommandText = @"
            CREATE PROCEDURE AddEquipment
            @сode NVARCHAR(50),
            @date NVARCHAR(50),
            @allCodes NVARCHAR(50),
            @modelId INT 
            AS
            INSERT INTO Equipment(Code, Date, AllCodes, ModelId)
            VALUES(@сode, @date, @allCodes, (SELECT Id FROM Model WHERE Model.Id = @modelId))";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE UpdateEquipment
            @Id INT,
            @code NVARCHAR(50),
            @date NVARCHAR(50),
            @allCodes NVARCHAR(50),
            @modelId INT
            AS
            UPDATE Equipment
            SET Code = @code, Date = @date, AllCodes = @allCodes, ModelId = (SELECT Id FROM Model WHERE Model.Id = @modelId)
            WHERE Equipment.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE GetSingleEquipment
            @Id INT
            AS
            SELECT * FROM Equipment
            WHERE Equipment.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
           CREATE PROCEDURE GetEquipmentByModelId
           @Id INT
           AS
           SELECT * FROM Equipment
           WHERE Equipment.ModelId = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE DeleteEquipment
            @Id INT
            AS
            DELETE Equipment
            WHERE Equipment.Id = @Id";
            await command.ExecuteNonQueryAsync();
        }

        async public static Task CreateStoredProcedureByEquipmentInfo(SqlCommand command)
        {
            command.CommandText = @"
            CREATE PROCEDURE AddEquipmentInfo
            @name NVARCHAR(50),
            @date NVARCHAR(50),
            @eNGINE NVARCHAR(50),
            @bODY NVARCHAR(50),
            @gRADE NVARCHAR(50),
            @aTM_MTM NVARCHAR(50),
            @gEAR_SHIFT_TYPE NVARCHAR(50),
            @cAB NVARCHAR(50),
            @tRANSMISSION_MODEL NVARCHAR(50),
            @lOADING_CAPACITY NVARCHAR(50),
            @equipmentId INT
            AS
            INSERT INTO EquipmentInfo(Name, Date, ENGINE, BODY, GRADE, ATM_MTM, GEAR_SHIFT_TYPE, CAB, TRANSMISSION_MODEL, LOADING_CAPACITY, EquipmentId)
            VALUES(@name, @date, @eNGINE, @bODY, @gRADE, @aTM_MTM, @gEAR_SHIFT_TYPE, @cAB, @tRANSMISSION_MODEL, @lOADING_CAPACITY, 
            (SELECT Id FROM Equipment WHERE Equipment.Id = @equipmentId))";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE UpdateEquipmentInfo
            @Id INT,
            @name NVARCHAR(50),
            @date NVARCHAR(50),
            @eNGINE NVARCHAR(50),
            @bODY NVARCHAR(50),
            @gRADE NVARCHAR(50),
            @aTM_MTM NVARCHAR(50),
            @gEAR_SHIFT_TYPE NVARCHAR(50),
            @cAB NVARCHAR(50),
            @tRANSMISSION_MODEL NVARCHAR(50),
            @lOADING_CAPACITY NVARCHAR(50),
            @equipmentId INT
            AS
            UPDATE EquipmentInfo
            SET Name = @name, Date = @date, ENGINE = @eNGINE, BODY = @bODY, GRADE = @gRADE, 
                              ATM_MTM = @aTM_MTM, GEAR_SHIFT_TYPE = @gEAR_SHIFT_TYPE, CAB = @cAB, 
                              TRANSMISSION_MODEL = @tRANSMISSION_MODEL, LOADING_CAPACITY = @lOADING_CAPACITY, 
                              EquipmentId = (SELECT Id FROM Equipment WHERE Equipment.Id = @equipmentId)
            WHERE EquipmentInfo.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE GetSingleEquipmentInfo
            @Id INT
            AS
            SELECT * FROM EquipmentInfo
            WHERE EquipmentInfo.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
           CREATE PROCEDURE GetEquipmentInfoByEquipmentId
           @Id INT
           AS
           SELECT * FROM EquipmentInfo
           WHERE EquipmentInfo.EquipmentId = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
           CREATE PROCEDURE DeleteEquipmentInfo
           @Id INT
           AS
           DELETE EquipmentInfo
           WHERE EquipmentInfo.Id = @Id";
            await command.ExecuteNonQueryAsync();
        }

        async public static Task CreateStoredProcedureByGroups(SqlCommand command)
        {
            command.CommandText = @"
                CREATE PROCEDURE AddGroups
                @name NVARCHAR(50)
                AS
                INSERT INTO Groups(Name)
                VALUES(@name)";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE UpdateGroups
            @Id INT,
            @name NVARCHAR(50)
            AS
            UPDATE Groups
            SET Name = @name
            WHERE Groups.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE GetSingleGroups
            @Id INT
            AS
            SELECT * FROM Groups
            WHERE Groups.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
           CREATE PROCEDURE GetAllGroups
           AS
           SELECT * FROM Groups";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE DeleteGroups
            @Id INT
            AS
            DELETE Groups
            WHERE Groups.Id = @Id";
            await command.ExecuteNonQueryAsync();
        }

        async public static Task CreateStoredProcedureBySubgroup(SqlCommand command)
        {
            command.CommandText = @"
            CREATE PROCEDURE AddSubgroup
            @name NVARCHAR(50),
            @groupsId INT
            AS
            INSERT INTO Subgroup(Name, GroupsId)
            VALUES(@name, (SELECT Id FROM Groups WHERE Groups.Id = @groupsId))";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE UpdateSubgroup
            @Id INT,
            @name NVARCHAR(50),
            @groupsId INT
            AS
            UPDATE Subgroup
            SET Name = @name, GroupsId = (SELECT Id FROM Groups WHERE Groups.Id = @groupsId)
            WHERE Subgroup.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE GetSingleSubgroup
            @Id INT
            AS
            SELECT * FROM Subgroup
            WHERE Subgroup.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
           CREATE PROCEDURE GetSubgroupByGroupsId
           @Id INT
           AS
           SELECT * FROM Subgroup
           WHERE Subgroup.GroupsId = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE DeleteSubgroup
            @Id INT
            AS
            DELETE Subgroup
            WHERE Subgroup.Id = @Id";
            await command.ExecuteNonQueryAsync();
        }

        async public static Task CreateStoredProcedureByImage(SqlCommand command)
        {
            command.CommandText = @"
            CREATE PROCEDURE AddImage
            @name NVARCHAR(50),
            @subgroupId INT
            AS
            INSERT INTO Image(Name, SubgroupId)
            VALUES(@name, (SELECT Id FROM Subgroup WHERE Subgroup.Id = @subgroupId))";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE UpdateImage
            @Id INT,
            @name NVARCHAR(50),
            @subgroupId INT
            AS
            UPDATE Image
            SET Name = @name, SubgroupId = (SELECT Id FROM Subgroup WHERE Subgroup.Id = @subgroupId)
            WHERE Image.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE GetAllImage
            AS
            SELECT * FROM Image";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
           CREATE PROCEDURE GetImageBySubgroupId
           @Id INT
           AS
           SELECT * FROM Image
           WHERE Image.SubgroupId = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
            CREATE PROCEDURE DeleteImage
            @Id INT
            AS
            DELETE Image
            WHERE Image.Id = @Id";
            await command.ExecuteNonQueryAsync();
        }

        async public static Task CreateStoredProcedureByProduct(SqlCommand command)
        {
            command.CommandText = @"
                CREATE PROCEDURE AddProduct
                @code NVARCHAR(50),
                @count INT,
                @date NVARCHAR(50),
                @info NVARCHAR(50),
                @tree_code NVARCHAR(50),
                @tree NVARCHAR(50),
                @subgroupId INT,
                @equipmentInfoId INT
                AS
                INSERT INTO Product(Code, Count, Date, Info, Tree_code, Tree, SubgroupId)
                VALUES(@code, @count, @date, @info, @tree_code, @tree, (SELECT Id FROM Subgroup WHERE Subgroup.Id = @subgroupId))
                INSERT INTO Product_EquipmentInfo(EquipmentInfoId, ProductId)
                VALUES((SELECT Id FROM EquipmentInfo WHERE EquipmentInfo.Id = @equipmentInfoId), (SELECT IDENT_CURRENT('Product')))";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
                CREATE PROCEDURE UpdateProduct
                @Id INT,
                @code NVARCHAR(50),
                @count INT,
                @date NVARCHAR(50),
                @info NVARCHAR(50),
                @tree_code NVARCHAR(50),
                @tree NVARCHAR(50),
                @subgroupId INT
                AS
                UPDATE Product
                SET Code = @code, Count = @count, Date = @date, Info = @info, Tree_code = @tree_code, Tree = @tree, SubgroupId = (SELECT Id FROM Subgroup WHERE Subgroup.Id = @subgroupId)
                WHERE Product.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
                CREATE PROCEDURE GetSingleProduct
                @Id INT
                AS
                SELECT * FROM Product
                WHERE Product.Id = @Id";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
                CREATE PROCEDURE GetProductBySubgroupId_EquipmentInfoId
                @subgroupId INT,
                @equipmentInfoId INT
                AS
                SELECT * FROM Product
                JOIN Product_EquipmentInfo ON Product_EquipmentInfo.ProductId = Product.Id
                WHERE Product_EquipmentInfo.EquipmentInfoId = @equipmentInfoId AND Product.SubgroupId = @subgroupId";
            await command.ExecuteNonQueryAsync();

            command.CommandText = @"
                CREATE PROCEDURE DeleteProduct
                @Id INT
                AS
                DELETE Product
                WHERE Product.Id = @Id";
            await command.ExecuteNonQueryAsync();
        }
    }
}
