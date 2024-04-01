using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using NUnit.Framework;
using OfficesAPI.Application.Contracts.Models;
using OfficesAPI.Application.Contracts.Services;
using OfficesAPI.Application.Services;
using OfficesAPI.Domain.Models;
using Shared.Domain.Abstractions;

namespace OfficesAPI.Tests;

public class TestsCRUD
{
    Dictionary<Guid, Office> dictionaryRepo = new();
    List<Office> updatedRepo = new();

    IOfficeService officeService;
    Mock<IRepository<Office>> mockRepo;
    Mock<IUnitOfWork> mockUnitOfWork;

    [SetUp]
    public void SetUp()
    {
        mockRepo = new();
        mockRepo
            .Setup(repo => repo.Create(It.IsAny<Office>()))
            .Callback<Office>(office =>
            {
                office.Id = Guid.NewGuid();
                dictionaryRepo.Add(office.Id, office);
            });

        mockUnitOfWork = new();
        mockUnitOfWork
            .Setup(o => o.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Callback<CancellationToken>(ct => Task.Run(() =>
            {
                var prevCount = updatedRepo.Count;
                updatedRepo = dictionaryRepo.Select(pair => pair.Value).ToList();
                return Math.Abs(prevCount - updatedRepo.Count);
            }));


        officeService = new OfficeService(mockRepo.Object, mockUnitOfWork.Object);
    }

    [Test]
    [CancelAfter(5000)]
    public async Task TestCreationValidData(CancellationToken cancellationToken)
    {
        var validCreateDto = new OfficeCreateDto()
        {
            Address = new AddressDto
            {
                City = "New York",
                HouseNumber = "146",
                Street = "Bowery",
                OfficeNumber = "245a",
            },
            IsActive = true,
            RegistryPhoneNumber = "+9458347621145"
        };

        var id = await officeService.CreateAsync(validCreateDto, cancellationToken);

        Assert.That(id, Is.Not.Default);
    }
}