using Business.Entities;
using Business.Repositories;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

namespace StorageXml.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private XElement? xmlRootElement;

        private XElement? xmlCategoriesElement;

        private XElement? xmlMetaElement;

        private readonly string categoriesXmlFilePath;

        public CategoryRepository(IConfiguration configuration)
        {
            categoriesXmlFilePath = configuration.GetSection("XmlStorage")["CategoriesFilePath"];

            InitTasksXmlFile();
        }

        public int Create(CategoryModel categoryModel)
        {
            int idNewCategory = GetNextCategoryIdAndIncrement();

            var newCategoryElement = new XElement("Category",
               new XAttribute("id", idNewCategory),
               new XElement("Name", categoryModel.Name)
           );

            xmlCategoriesElement?.Add(newCategoryElement);
            SaveXmlDocument();

            return idNewCategory;
        }

        public void Delete(int id)
        {
            var categoryElement = xmlCategoriesElement?.Elements("Category")
                .SingleOrDefault(category => (string?)category.Attribute("id") == id.ToString());

            categoryElement?.Remove();
            SaveXmlDocument();
        }

        public CategoryModel? GetById(int id)
        {
            var categoryElement = xmlCategoriesElement?.Elements("Category")
                .SingleOrDefault(category => (string?)category.Attribute("id") == id.ToString());

            if (categoryElement == null) return null;

            return ParseXmlElementToCategoryModel(categoryElement);
        }

        public IEnumerable<CategoryModel> GetList()
        {
            var categoriesList = xmlCategoriesElement?.Elements("Category")
                .Select(category => ParseXmlElementToCategoryModel(category));

            return categoriesList;
        }

        public void Update(CategoryModel categoryModel)
        {
            var categoryElement = xmlCategoriesElement?.Elements("Category")
                .SingleOrDefault(category => (string?)category.Attribute("id") == categoryModel.Id.ToString());

            categoryElement?.SetElementValue("Name", categoryModel.Name);
            SaveXmlDocument();
        }

        internal static CategoryModel ParseXmlElementToCategoryModel(XElement categoryElement)
        {
            var categoryIdAttribute = categoryElement.Attribute("id");
            var categoryNameElement = categoryElement.Element("Name");

            int id = int.Parse((string)categoryIdAttribute);
            string name = (string)categoryNameElement;

            var categoryModel = new CategoryModel()
            {
                Id = id,
                Name = name
            };

            return categoryModel;
        }

        private int GetNextCategoryIdAndIncrement()
        {
            var nextCategoryIdElement = xmlMetaElement?.Element("NextCategoryId");
            int id = (int)nextCategoryIdElement;

            nextCategoryIdElement.Value = Convert.ToString(id + 1);
            SaveXmlDocument();

            return id;
        }

        private void SaveXmlDocument()
        {
            xmlRootElement?.Save(categoriesXmlFilePath);
        }

        private void InitTasksXmlFile()
        {
            XDocument xmlDocument;

            try
            {
                xmlDocument = XDocument.Load(categoriesXmlFilePath);
            }
            catch (FileNotFoundException)
            {
                xmlDocument = new XDocument();

                var rootElement = new XElement("Root",
                    new XElement("Meta", new XElement("NextCategoryId", 1)),
                    new XElement("Categories")
                );

                xmlDocument?.Add(rootElement);
                xmlDocument?.Save(categoriesXmlFilePath);
            }

            xmlRootElement = xmlDocument?.Root;
            xmlCategoriesElement = xmlRootElement?.Element("Categories");
            xmlMetaElement = xmlRootElement?.Element("Meta");
        }
    }
}
