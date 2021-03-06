using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Moq;
using EmailGroupsAppv1.Models;
using EmailGroupsAppv1.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace EmailGroupsAppv1Tests
{
  [TestClass]
  public class MailGroupsTests
  {
    [TestMethod]
    public async Task Create_Mail_Group()
    {
      //arrange
      var data = new List<MailGroup>().AsQueryable();
      var newObject = new MailGroup { Name = "1" };

      var mockMailGroups = GetMock(data);
      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailGroups).Returns(mockMailGroups.Object);
      var service = new MailGroupsController(mockContext.Object);

      //act
      await service.PostMailGroup(newObject);

      //assert
      mockMailGroups.Verify(x => x.Add(It.IsAny<MailGroup>()), Times.Once);
      mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Get_Mail_Groups()
    {
      //arrange
      var data = new List<MailGroup>
      {
        new MailGroup{Name="2"},
        new MailGroup{Name="3"},
        new MailGroup{Name="1"}
      }.AsQueryable();

      var mockMailGroups = GetMock(data);
      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailGroups).Returns(mockMailGroups.Object);
      var service = new MailGroupsController(mockContext.Object);

      //act
      var response = await service.GetMailGroups();

      //assert
      Assert.IsNotNull(response.Value);
      Assert.AreEqual(response.Value.Count(), 3);
      Assert.AreEqual(response.Value.ElementAt(0).Name, "1");
      Assert.AreEqual(response.Value.ElementAt(1).Name, "2");
      Assert.AreEqual(response.Value.ElementAt(2).Name, "3");
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(-1)]
    [DataRow(4)]
    public async Task Get_Mail_Group(int id)
    {
      //arrange
      var data = new List<MailGroup>
      {
        new MailGroup{Id=2},
        new MailGroup{Id=3},
        new MailGroup{Id=1}
      }.AsQueryable();

      var mockMailGroups = GetMock(data);
      mockMailGroups.Setup(x => x.FindAsync(id)).ReturnsAsync(() =>
      {
        return mockMailGroups.Object.FirstOrDefault(x => x.Id == id);
      });
      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailGroups).Returns(mockMailGroups.Object);

      var service = new MailGroupsController(mockContext.Object);

      //act
      var response = await service.GetMailGroup(id);

      //assert
      if (data.Any(x => x.Id == id))
      {
        Assert.IsNotNull(response.Value);
        Assert.AreEqual(response.Value.Id, id);
      }
      else
      {
        Assert.IsNull(response.Value);
      }
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(-1)]
    [DataRow(4)]
    public async Task Delete_Mail_Group(int id)
    {
      //arrange
      var data = new List<MailGroup>
      {
        new MailGroup{Id=2},
        new MailGroup{Id=3},
        new MailGroup{Id=1}
      }.AsQueryable();

      var mockMailGroups = GetMock(data);
      mockMailGroups.Setup(x => x.FindAsync(id)).ReturnsAsync(() =>
      {
        return mockMailGroups.Object.FirstOrDefault(x => x.Id == id);
      });
      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailGroups).Returns(mockMailGroups.Object);
      var service = new MailGroupsController(mockContext.Object);

      //act
      var response = await service.DeleteMailGroup(id);

      //assert
      if (data.Any(x => x.Id == id))
      {
        mockMailGroups.Verify(x => x.Remove(It.IsAny<MailGroup>()), Times.Once);
        mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsNotNull(response.Value);
      }
      else
      {
        mockMailGroups.Verify(x => x.Remove(It.IsAny<MailGroup>()), Times.Never);
        mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        Assert.IsNull(response.Value);
      }
    }

    [TestMethod]
    public async Task Update_Mail_Group()
    {
      //arrange
      var newObject = new MailGroup { Id = 1, Name = "1" };

      var mockContext = new Mock<MailGroupsContext>();
      var service = new MailGroupsController(mockContext.Object);

      //act
      await service.PutMailGroup(2, newObject);

      //assert
      mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    [DataRow(1, 1)]
    [DataRow(1, 2)]
    [DataRow(2, 1)]
    public async Task Create_Mail_Address(int groupId, int addressGroupId)
    {
      //arrange
      var data = new List<MailAddress>().AsQueryable();
      var newObject = new MailAddress { GroupId = addressGroupId, Name = "1" };

      var mockMailAddresses = GetMock(data);
      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailAddresses).Returns(mockMailAddresses.Object);
      var service = new MailGroupsController(mockContext.Object);

      //act
      await service.PostMailAddress(groupId, newObject);

      //assert
      if (groupId == addressGroupId)
      {
        mockMailAddresses.Verify(x => x.Add(It.IsAny<MailAddress>()), Times.Once);
        mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
      }
      else
      {
        mockMailAddresses.Verify(x => x.Add(It.IsAny<MailAddress>()), Times.Never);
        mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
      }
    }

    [TestMethod]
    [DataRow(1, 1, 2)]
    [DataRow(1, 2, 1)]
    public async Task Update_Mail_Address(int groupId, int id, int mailAddressGroupId)
    {
      //arrange
      var newObject = new MailAddress { Id = 1, GroupId = mailAddressGroupId };

      var mockContext = new Mock<MailGroupsContext>();
      var service = new MailGroupsController(mockContext.Object);

      //act
      await service.PutMailAddress(groupId, id, newObject);

      //assert
      mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    [DataRow(1, 1)]
    [DataRow(1, 2)]
    [DataRow(2, 1)]
    [DataRow(2, 4)]
    public async Task Delete_Mail_Address(int groupId, int id)
    {
      //arrange
      var data = new List<MailAddress>
      {
        new MailAddress{Id=1, GroupId=1},
        new MailAddress{Id=2, GroupId=1},
        new MailAddress{Id=3, GroupId=1}
      }.AsQueryable();

      var mockMailGroups = GetMock(data);
      mockMailGroups.Setup(x => x.FindAsync(id)).ReturnsAsync(() =>
      {
        return mockMailGroups.Object.FirstOrDefault(x => x.Id == id);
      });
      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailAddresses).Returns(mockMailGroups.Object);
      var service = new MailGroupsController(mockContext.Object);

      //act
      var response = await service.DeleteMailAddress(groupId, id);

      //assert
      if (data.Any(x => x.Id == id && x.GroupId == groupId))
      {
        mockMailGroups.Verify(x => x.Remove(It.IsAny<MailAddress>()), Times.Once);
        mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsNotNull(response.Value);
      }
      else
      {
        mockMailGroups.Verify(x => x.Remove(It.IsAny<MailAddress>()), Times.Never);
        mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        Assert.IsNull(response.Value);
      }
    }

    private Mock<DbSet<TEntity>> GetMock<TEntity>(IQueryable<TEntity> data) where TEntity : class
    {
      var enumerable = new TestAsyncEnumerable<TEntity>(data);
      var mockMailGroups = new Mock<DbSet<TEntity>>();
      mockMailGroups.As<IAsyncEnumerable<TEntity>>()
        .Setup(x => x.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
        .Returns(enumerable.GetAsyncEnumerator());
      mockMailGroups.As<IQueryable<TEntity>>().Setup(x => x.Provider).Returns(enumerable.Provider);
      mockMailGroups.As<IQueryable<TEntity>>().Setup(x => x.Expression).Returns(data.Expression);
      mockMailGroups.As<IQueryable<TEntity>>().Setup(x => x.ElementType).Returns(enumerable.ElementType);
      mockMailGroups.As<IQueryable<TEntity>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());
      mockMailGroups.Setup(x => x.AsQueryable()).Returns(mockMailGroups.Object);
      mockMailGroups.Setup(x => x.AsAsyncEnumerable()).Returns(mockMailGroups.Object);
      return mockMailGroups;
    }
  }
}
