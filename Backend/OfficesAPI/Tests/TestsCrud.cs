using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using NUnit.Framework;
using OfficesAPI.Application.Contracts.Models;
using OfficesAPI.Application.Contracts.Models.Requests;
using OfficesAPI.Application.Contracts.Models.Responses;
using OfficesAPI.Application.Contracts.Services;
using OfficesAPI.Application.Services;
using OfficesAPI.Domain.Models;
using Shared.Domain.Abstractions;
using Shared.Domain.Models;
using Shared.Misc;

namespace OfficesAPI.Tests;

public class TestsCrud {
    Dictionary<Guid, Office> dictionaryRepo;
    List<Office> updatedRepo;

    IOfficeService officeService;
    Mock<IRepository<Office>> mockRepo;
    Mock<IUnitOfWork> mockUnitOfWork;

    OfficeCreateRequest GenValidDto() => new OfficeCreateRequest() {
        Address = new AddressDto {
            City = "New York",
            HouseNumber = "146",
            Street = "Bowery",
            OfficeNumber = "245a",
        },
        IsActive = true,
        RegistryPhoneNumber = "+9458347621145"
    };

    [SetUp]
    public void SetUp() {
        dictionaryRepo = new();
        updatedRepo = new();

        mockRepo = new();
        mockRepo
            .Setup(repo => repo.Create(It.IsAny<Office>()))
            .Callback<Office>(office => {
                office.Id = Guid.NewGuid();
                dictionaryRepo.Add(office.Id, office);
            });

        mockRepo
            .Setup(x => x.GetPageAsync(It.IsAny<IPageDesc>(), It.IsAny<CancellationToken>()))
            .Returns<IPageDesc, CancellationToken>((pageDesc, _) => {
                return Task.Run(() => updatedRepo.Paginate(pageDesc).ToList().AsEnumerable());
            });

        mockRepo
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns<Guid, CancellationToken>((id, _) => {
                return Task.Run(() => updatedRepo.Find(x => x.Id == id));
            });

        mockUnitOfWork = new();
        mockUnitOfWork
            .Setup(o => o.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Callback<CancellationToken>(_ => Task.Run(() => {
                var prevCount = updatedRepo.Count;
                updatedRepo = dictionaryRepo.Select(pair => pair.Value).ToList();
                return Math.Abs(prevCount - updatedRepo.Count);
            }));


        officeService = new OfficeService(mockRepo.Object, mockUnitOfWork.Object);
    }

    [Test]
    [CancelAfter(5000)]
    public async Task TestCreationValidData(CancellationToken cancellationToken) {
        var validCreateDto = GenValidDto();

        var id = await officeService.CreateAsync(validCreateDto, cancellationToken);

        mockRepo.Verify(x => x.Create(It.IsAny<Office>()), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        Assert.That(id, Is.Not.Default);
    }

    [Test]
    [CancelAfter(5000)]
    public async Task TestGetEmptyPage(CancellationToken cancellationToken) {
        var pageDesc = new PageDesc {
            PageNumber = 1,
            PageSize = 50,
        };

        var list = await officeService.GetPageAsync(pageDesc, cancellationToken);

        mockRepo.Verify(x => x.GetPageAsync(It.IsAny<IPageDesc>(), It.IsAny<CancellationToken>()), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());

        Assert.That(list.Any(), Is.False);
    }

    [Test]
    [CancelAfter(5000)]
    public async Task TestGetPage(CancellationToken cancellationToken) {
        var pageDesc = new PageDesc {
            PageNumber = 1,
            PageSize = 50,
        };

        _ = await officeService.CreateAsync(GenValidDto(), cancellationToken);

        var list = await officeService.GetPageAsync(pageDesc, cancellationToken);

        mockRepo.Verify(x => x.GetPageAsync(It.IsAny<IPageDesc>(), It.IsAny<CancellationToken>()), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        Assert.That(list.Any(), Is.True);
    }

    [Test]
    [CancelAfter(5000)]
    public async Task TestGetByIdValid(CancellationToken cancellationToken) {
        var officeCreateDto = GenValidDto();
        var officeDto = officeCreateDto.Adapt<OfficeResponse>();

        var id = await officeService.CreateAsync(officeCreateDto, cancellationToken);

        var officeDtoRet = await officeService.GetByIdAsync(id, cancellationToken);

        mockRepo.Verify(x => x.GetPageAsync(It.IsAny<IPageDesc>(), It.IsAny<CancellationToken>()), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        officeDto = officeDto with {
            Id = officeDtoRet.Id,
        };

        Assert.That(officeDto, Is.EqualTo(officeDtoRet));
    }
}