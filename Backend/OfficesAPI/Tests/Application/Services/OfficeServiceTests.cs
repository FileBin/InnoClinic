using Mapster;
using Moq;
using NUnit.Framework;
using OfficesAPI.Application.Contracts.Models.Requests;
using OfficesAPI.Application.Contracts.Models.Responses;
using OfficesAPI.Application.Contracts.Services;
using OfficesAPI.Application.Services;
using OfficesAPI.Domain.Models;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Domain.Models;
using InnoClinic.Shared.Exceptions.Models;
using MockQueryable.Moq;

namespace OfficesAPI.Tests;

public class OfficeServiceTests {
    Dictionary<Guid, Office> dictionaryRepo;
    List<Office> updatedRepo;

    IOfficeService officeService;
    Mock<IRepository<Office>> mockRepo;
    Mock<IUnitOfWork> mockUnitOfWork;

    OfficeCreateRequest GenValidCreateRequest() => new OfficeCreateRequest() {
        Address = new AddressRequest {
            City = "New York",
            HouseNumber = "146",
            Street = "Bowery",
            OfficeNumber = "245a",
        },
        IsActive = true,
        RegistryPhoneNumber = "+9458347621145"
    };

    OfficeUpdateRequest GenValidUpdateRequest() => new OfficeUpdateRequest() {
        Address = new AddressRequest {
            City = "Warsaw",
            HouseNumber = "146",
            Street = "Bowery",
            OfficeNumber = "245a",
        },
        IsActive = false,
        RegistryPhoneNumber = "+5453565"
    };

    [SetUp]
    public void SetUp() {
        dictionaryRepo = [];
        updatedRepo = [];

        var queryableMock = dictionaryRepo.Values.BuildMock();

        mockRepo = new();
        mockRepo
            .Setup(repo => repo.Create(It.IsAny<Office>()))
            .Callback<Office>(office => {
                office.Id = Guid.NewGuid();
                dictionaryRepo.Add(office.Id, office);
            });

        mockRepo
            .Setup(x => x.GetAll())
            .Returns(queryableMock);

        mockRepo
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns<Guid, CancellationToken>((id, _) => {
                return Task.Run(() => updatedRepo.Find(x => x.Id == id));
            });

        mockRepo
            .Setup(x => x.Delete(It.IsAny<Office>()))
            .Callback<Office>(office => {
                dictionaryRepo.Remove(office.Id);
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
    public async Task TestCreateValid(CancellationToken cancellationToken) {
        var validCreateDto = GenValidCreateRequest();

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

        mockRepo.Verify(x => x.GetAll(), Times.Once());
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

        _ = await officeService.CreateAsync(GenValidCreateRequest(), cancellationToken);

        var list = await officeService.GetPageAsync(pageDesc, cancellationToken);

        mockRepo.Verify(x => x.GetAll(), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        Assert.That(list.Any(), Is.True);
    }

    [Test]
    [CancelAfter(5000)]
    public async Task TestGetByIdValid(CancellationToken cancellationToken) {
        var officeCreateDto = GenValidCreateRequest();
        var officeDto = officeCreateDto.Adapt<OfficeResponse>();

        var id = await officeService.CreateAsync(officeCreateDto, cancellationToken);

        var officeDtoRet = await officeService.GetByIdAsync(id, cancellationToken);

        mockRepo.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        officeDto = officeDto with {
            Id = officeDtoRet.Id,
        };

        Assert.That(officeDto, Is.EqualTo(officeDtoRet));
    }

    [Test]
    [CancelAfter(5000)]
    public void TestGetByIdNotFound(CancellationToken cancellationToken) {
        var id = Guid.NewGuid();

        Assert.ThrowsAsync(typeof(NotFoundException), async () => {
            await officeService.GetByIdAsync(id, cancellationToken);
        });

        mockRepo.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
    }

    [Test]
    [CancelAfter(5000)]
    public async Task TestUpdateValidAsync(CancellationToken cancellationToken) {
        var officeCreateRequest = GenValidCreateRequest();
        var id = await officeService.CreateAsync(officeCreateRequest, cancellationToken);
        
        var officeUpdateRequest = GenValidUpdateRequest();
        await officeService.UpdateAsync(id, officeUpdateRequest, cancellationToken);

        mockRepo.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce());

        var officeExpect = officeCreateRequest.Adapt<OfficeResponse>() with {
            Id = id,
            Address = (officeUpdateRequest.Address ?? officeCreateRequest.Address).Adapt<AddressResponse>(),
            IsActive = officeUpdateRequest.IsActive ?? officeCreateRequest.IsActive,
            RegistryPhoneNumber = officeUpdateRequest.RegistryPhoneNumber ?? officeCreateRequest.RegistryPhoneNumber,
        };

        var officeActual = await officeService.GetByIdAsync(id, cancellationToken);

        Assert.That(officeActual, Is.EqualTo(officeExpect));
    }

    [Test]
    [CancelAfter(5000)]
    public void TestUpdateNotFound(CancellationToken cancellationToken) {
        Assert.ThrowsAsync(typeof(NotFoundException), async () => {
            await officeService.UpdateAsync(Guid.NewGuid(), GenValidUpdateRequest(), cancellationToken);
        });

        mockRepo.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
    }

    [Test]
    [CancelAfter(5000)]
    public async Task TestDeleteValidAsync(CancellationToken cancellationToken) {
        var officeCreateRequest = GenValidCreateRequest();

        var id = await officeService.CreateAsync(officeCreateRequest, cancellationToken);

        await officeService.DeleteAsync(id, cancellationToken);

        mockRepo.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        mockRepo.Verify(x => x.Delete(It.IsAny<Office>()), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce());

        Assert.ThrowsAsync(typeof(NotFoundException), async () => {
            await officeService.GetByIdAsync(id, cancellationToken);
        });
    }

    [Test]
    [CancelAfter(5000)]
    public void TestDeleteNotFound(CancellationToken cancellationToken) {
        Assert.ThrowsAsync(typeof(NotFoundException), async () => {
            await officeService.DeleteAsync(Guid.NewGuid(), cancellationToken);
        });

        mockRepo.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
        mockRepo.Verify(x => x.Delete(It.IsAny<Office>()), Times.Never());
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
    }
}