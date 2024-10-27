using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams param) 
        : base(x =>
            (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search))&&
            (!param.BrandId.HasValue || x.ProductBrandId == param.BrandId)&&
            (!param.TypeId.HasValue || x.ProductTypeId == param.TypeId)
        )

        {

        }
    }
}