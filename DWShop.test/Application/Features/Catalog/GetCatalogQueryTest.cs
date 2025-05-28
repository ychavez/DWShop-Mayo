using AutoMapper;
using DWShop.Application.Features.Catalog.Queries;
using DWShop.Application.Responses.Catalog;
using DWShop.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.test.Application.Features.Catalog
{
    public class GetCatalogQueryTest
    {

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryAsync<Domain.Entities.Catalog, int>> _repositoryAsync;

        public GetCatalogQueryTest()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryAsync = new Mock<IRepositoryAsync<Domain.Entities.Catalog, int>>();
        }

        [Fact]
        private async Task Handle_Should_Return_Result()
        {
            //Arrange
            var query = new GetCatalogQuery();
            var catalogs = new List<Domain.Entities.Catalog>()
            {
                new Domain.Entities.Catalog
                {
                    Id = 1,
                    Name = "Catalog 1",
                    Category = "Category A",
                    Description = "Description for Catalog 1",
                    Summary = "Summary for Catalog 1",
                    Price = 100.00m,
                    PhotoURL = "http://example.com/catalog1.jpg"
                },
                new Domain.Entities.Catalog
                {
                    Id = 2,
                    Name = "Catalog 2",
                    Category = "Category B",
                    Description = "Description for Catalog 2",
                    Summary = "Summary for Catalog 2",
                    Price = 200.00m,
                    PhotoURL = "http://example.com/catalog2.jpg"
                },
                new Domain.Entities.Catalog
                {
                    Id = 3,
                    Name = "Catalog 3",
                    Category = "Category C",
                    Description = "Description for Catalog 3",
                    Summary = "Summary for Catalog 3",
                    Price = 300.00m,
                    PhotoURL = "http://example.com/catalog3.jpg"
                },
                new Domain.Entities.Catalog
                {
                    Id = 4,
                    Name = "Catalog 4",
                    Category = "Category D",
                    Description = "Description for Catalog 4",
                    Summary = "Summary for Catalog 4",
                    Price = 400.00m,
                    PhotoURL = "http://example.com/catalog4.jpg"
                }
            };

            var response = new List<CatalogResponse>
            {
                new CatalogResponse
                {
                    Id = 1,
                    Name = "Catalog 1",
                    Category = "Category A",
                    Description = "Description for Catalog 1",
                    Summary = "Summary for Catalog 1",
                    Price = 100.00m,
                    PhotoURL = "http://example.com/catalog1.jpg"
                },
                new CatalogResponse
                {
                    Id = 2,
                    Name = "Catalog 2",
                    Category = "Category B",
                    Description = "Description for Catalog 2",
                    Summary = "Summary for Catalog 2",
                    Price = 200.00m,
                    PhotoURL = "http://example.com/catalog2.jpg"
                },
                new CatalogResponse
                {
                    Id = 3,
                    Name = "Catalog 3",
                    Category = "Category C",
                    Description = "Description for Catalog 3",
                    Summary = "Summary for Catalog 3",
                    Price = 300.00m,
                    PhotoURL = "http://example.com/catalog3.jpg"
                },
                new CatalogResponse
                {
                    Id = 4,
                    Name = "Catalog 4",
                    Category = "Category D",
                    Description = "Description for Catalog 4",
                    Summary = "Summary for Catalog 4",
                    Price = 400.00m,
                    PhotoURL = "http://example.com/catalog4.jpg"
                }
            };


            _mapperMock.Setup(x => x.Map<List<CatalogResponse>>(catalogs)).Returns(response);

            _repositoryAsync.Setup(x => x.GetAllAsync()).ReturnsAsync(catalogs);

            var handler = new GetCatalogQueryHandler(_mapperMock.Object, _repositoryAsync.Object);


            //Act
            var result = await handler.Handle(query, default);


            //Assert
            Assert.NotNull(result);
            Assert.Equal(response, result.Data);
            Assert.True(result.Succeded);

            _mapperMock.Verify(x =>
            x.Map<List<CatalogResponse>>(It.IsAny<List<Domain.Entities.Catalog>>()), Times.Once);

            _repositoryAsync.Verify(x => x.GetAllAsync(), Times.Once);

        }


    }
}
