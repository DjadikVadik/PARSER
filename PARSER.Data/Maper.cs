using PARSER.Data.Models;
using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data
{
    public static class Maper
    {
        public static Model ToModel(ModelDomain modelDomain) => new Model 
        {   
            Id = modelDomain.Id, 
            Name = modelDomain.Name 
        };

        public static Equipment ToModel(EquipmentDomain equipmentDomain) => new Equipment 
        {   Id = equipmentDomain.Id, 
            Code= equipmentDomain.Code, 
            Date = equipmentDomain.Date, 
            AllCodes = equipmentDomain.AllCodes, 
            ModelId = equipmentDomain.ModelDomainId 
        };

        public static EquipmentInfo ToModel(EquipmentInfoDomain equipmentInfoDomain) => new EquipmentInfo
        {
            Id = equipmentInfoDomain.Id,
            Name = equipmentInfoDomain.Name,
            Date = equipmentInfoDomain.Date,
            ENGINE = equipmentInfoDomain.ENGINE,
            BODY = equipmentInfoDomain.BODY,
            GRADE = equipmentInfoDomain.GRADE,
            ATM_MTM = equipmentInfoDomain.ATM_MTM,
            GEAR_SHIFT_TYPE = equipmentInfoDomain.GEAR_SHIFT_TYPE,
            CAB = equipmentInfoDomain.CAB,
            TRANSMISSION_MODEL = equipmentInfoDomain.TRANSMISSION_MODEL,
            LOADING_CAPACITY = equipmentInfoDomain.LOADING_CAPACITY,
            EquipmentId = equipmentInfoDomain.EquipmentDomainId
        };

        public static Group ToModel(GroupDomain groupDomain) => new Group
        {
            Id = groupDomain.Id,
            Name = groupDomain.Name,
        };

        public static Subgroup ToModel(SubgroupDomain subgroupDomain) => new Subgroup
        {
            Id = subgroupDomain.Id,
            Name = subgroupDomain.Name,
            GroupId = subgroupDomain.GroupDomainId
        };

        public static Product ToModel(ProductDomain productDomain) => new Product
        {
            Id = productDomain.Id,
            Code = productDomain.Code,
            Count = productDomain.Count,
            Date = productDomain.Date,
            Info = productDomain.Info,
            Tree_code = productDomain.Tree_code,
            Tree = productDomain.Tree,
            SubgroupId = productDomain.SubgroupDomainId
        };

        public static Image ToModel(ImageDomain imageDomain) => new Image
        {
            Id = imageDomain.Id,
            Name = imageDomain.Name,
            SubgroupId = imageDomain.SubgroupDomainId
        };

        public static IEnumerable<Model> ToModel(IEnumerable<ModelDomain> modelDomains)
        {
            var models = new List<Model>();

            foreach(var modelDomain in modelDomains)
            {
                models.Add(ToModel(modelDomain));
            }
            return models;
        }

        public static IEnumerable<Equipment> ToModel(IEnumerable<EquipmentDomain> equipmentDomains)
        {
            var equipments = new List<Equipment>();

            foreach(var equipmentDomain in equipmentDomains)
            {
                equipments.Add(ToModel(equipmentDomain));
            }

            return equipments;
        }

        public static IEnumerable<EquipmentInfo> ToModel(IEnumerable<EquipmentInfoDomain> equipmentInfoDomains)
        {
            var equipmentInfos = new List<EquipmentInfo>();

            foreach (var equipmentInfoDomain in equipmentInfoDomains)
            {
                equipmentInfos.Add(ToModel(equipmentInfoDomain));
            }

            return equipmentInfos;
        }

        public static IEnumerable<Group> ToModel(IEnumerable<GroupDomain> groupDomains)
        {
            var groups = new List<Group>();

            foreach (var groupDomain in groupDomains)
            {
                groups.Add(ToModel(groupDomain));
            }

            return groups;
        }

        public static IEnumerable<Subgroup> ToModel(IEnumerable<SubgroupDomain> subgroupDomains)
        {
            var subgroups = new List<Subgroup>();

            foreach (var subgroupDomain in subgroupDomains)
            {
                subgroups.Add(ToModel(subgroupDomain));
            }

            return subgroups;
        }

        public static IEnumerable<Product> ToModel(IEnumerable<ProductDomain> productDomains)
        {
            var products = new List<Product>();

            foreach (var productDomain in productDomains)
            {
                products.Add(ToModel(productDomain));
            }

            return products;
        }

        public static IEnumerable<Image> ToModel(IEnumerable<ImageDomain> imageDomains)
        {
            var images = new List<Image>();

            foreach (var imageDomain in imageDomains)
            {
                images.Add(ToModel(imageDomain));
            }

            return images;
        }

        public static ModelDomain ToDomain(Model model) => new ModelDomain
        {
            Id = model.Id,
            Name = model.Name
        };

        public static EquipmentDomain ToDomain(Equipment equipment) => new EquipmentDomain
        {
            Id = equipment.Id,
            Code = equipment.Code,
            Date = equipment.Date,
            AllCodes = equipment.AllCodes,
            ModelDomainId = equipment.ModelId
        };

        public static EquipmentInfoDomain ToDomain(EquipmentInfo equipmentInfo) => new EquipmentInfoDomain
        {
            Id = equipmentInfo.Id,
            Name = equipmentInfo.Name,
            Date = equipmentInfo.Date,
            ENGINE = equipmentInfo.ENGINE,
            BODY = equipmentInfo.BODY,
            GRADE = equipmentInfo.GRADE,
            ATM_MTM = equipmentInfo.ATM_MTM,
            GEAR_SHIFT_TYPE = equipmentInfo.GEAR_SHIFT_TYPE,
            CAB = equipmentInfo.CAB,
            TRANSMISSION_MODEL = equipmentInfo.TRANSMISSION_MODEL,
            LOADING_CAPACITY = equipmentInfo.LOADING_CAPACITY,
            EquipmentDomainId = equipmentInfo.EquipmentId
        };

        public static GroupDomain ToDomain(Group group) => new GroupDomain
        {
            Id = group.Id,
            Name = group.Name,
        };

        public static SubgroupDomain ToDomain(Subgroup subgroup) => new SubgroupDomain
        {
            Id = subgroup.Id,
            Name = subgroup.Name,
            GroupDomainId = subgroup.GroupId
        };

        public static ProductDomain ToDomain(Product product) => new ProductDomain
        {
            Id = product.Id,
            Code = product.Code,
            Count = product.Count,
            Date = product.Date,
            Info = product.Info,
            Tree_code = product.Tree_code,
            Tree = product.Tree,
            SubgroupDomainId = product.SubgroupId
        };

        public static ImageDomain ToDomain(Image image) => new ImageDomain
        {
            Id = image.Id,
            Name = image.Name,
            SubgroupDomainId = image.SubgroupId
        };

        public static IEnumerable<ModelDomain> ToDomain(IEnumerable<Model> models)
        {
            var modelDomains = new List<ModelDomain>();

            foreach (var model in models)
            {
                modelDomains.Add(ToDomain(model));
            }

            return modelDomains;
        }

        public static IEnumerable<EquipmentDomain> ToDomain(IEnumerable<Equipment> equipments)
        {
            var equipmentDomains = new List<EquipmentDomain>();

            foreach (var equipment in equipments)
            {
                equipmentDomains.Add(ToDomain(equipment));
            }

            return equipmentDomains;
        }

        public static IEnumerable<EquipmentInfoDomain> ToDomain(IEnumerable<EquipmentInfo> equipmentInfos)
        {
            var equipmentInfoDomains = new List<EquipmentInfoDomain>();

            foreach (var equipmentInfo in equipmentInfos)
            {
                equipmentInfoDomains.Add(ToDomain(equipmentInfo));
            }

            return equipmentInfoDomains;
        }

        public static IEnumerable<GroupDomain> ToDomain(IEnumerable<Group> groups)
        {
            var groupDomains = new List<GroupDomain>();

            foreach (var group in groups)
            {
                groupDomains.Add(ToDomain(group));
            }

            return groupDomains;
        }

        public static IEnumerable<SubgroupDomain> ToDomain(IEnumerable<Subgroup> subgroups)
        {
            var subgroupDomains = new List<SubgroupDomain>();

            foreach (var subgroup in subgroups)
            {
                subgroupDomains.Add(ToDomain(subgroup));
            }

            return subgroupDomains;
        }

        public static IEnumerable<ProductDomain> ToDomain(IEnumerable<Product> products)
        {
            var productsDomains = new List<ProductDomain>();

            foreach (var product in products)
            {
                productsDomains.Add(ToDomain(product));
            }

            return productsDomains;
        }

        public static IEnumerable<ImageDomain> ToDomain(IEnumerable<Image> images)
        {
            var imagesDomains = new List<ImageDomain>();

            foreach (var image in images)
            {
                imagesDomains.Add(ToDomain(image));
            }

            return imagesDomains;
        }
    }
}
