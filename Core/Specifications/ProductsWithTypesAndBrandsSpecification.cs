using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams param)
        : base(x => 
            (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search)) &&
            (!param.BrandId.HasValue || x.ProductBrandId == param.BrandId) &&
            (!param.TypeId.HasValue || x.ProductTypeId == param.TypeId) 
        )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            
            ApplyPaging(param.PageSize * (param.PageIndex - 1), param.PageSize);

            if (!string.IsNullOrEmpty(param.Sort))
            {
                switch (param.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
            
            
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}