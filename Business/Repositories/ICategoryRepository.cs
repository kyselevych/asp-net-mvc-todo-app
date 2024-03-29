﻿using Business.Entities;

namespace Business.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> GetList();

        CategoryModel GetById(int id);

        void Delete(int id);

        int Create(CategoryModel model);

        void Update(CategoryModel model);
    }
}
