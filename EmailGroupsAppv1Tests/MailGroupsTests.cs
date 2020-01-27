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
    [DataRow("1")]
    [DataRow("x")]
    [DataRow("")]
    public async Task Get_Mail_Group(string name)
    {
      //arrange
      var data = new List<MailGroup>
      {
        new MailGroup{Name="2"},
        new MailGroup{Name="3"},
        new MailGroup{Name="1"}
      }.AsQueryable();

      var mockMailGroups = GetMock(data);
      mockMailGroups.Setup(x => x.FindAsync(name)).ReturnsAsync(() =>
      {
        return mockMailGroups.Object.FirstOrDefault(x => x.Name == name);
      });
      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailGroups).Returns(mockMailGroups.Object);

      var service = new MailGroupsController(mockContext.Object);

      //act
      var response = await service.GetMailGroup(name);

      //assert
      if (data.Any(x => x.Name == name))
      {
        Assert.IsNotNull(response.Value);
        Assert.AreEqual(response.Value.Name, name);
      }
      else
      {
        Assert.IsNull(response.Value);
      }
    }

    [TestMethod]
    [DataRow("1")]
    [DataRow("x")]
    [DataRow("")]
    public async Task Delete_Mail_Group(string name)
    {
      //arrange
      var data = new List<MailGroup>
      {
        new MailGroup{Name="2"},
        new MailGroup{Name="3"},
        new MailGroup{Name="1"}
      }.AsQueryable();

      var mockMailGroups = GetMock(data);
      mockMailGroups.Setup(x => x.FindAsync(name)).ReturnsAsync(() =>
      {
        return mockMailGroups.Object.FirstOrDefault(x => x.Name == name);
      });
      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailGroups).Returns(mockMailGroups.Object);
      var service = new MailGroupsController(mockContext.Object);

      //act
      var response = await service.DeleteMailGroup(name);

      //assert
      if (data.Any(x => x.Name == name))
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
      var newObject = new MailGroup { Name = "1", Description = "1" };

      var mockContext = new Mock<MailGroupsContext>();
      var service = new MailGroupsController(mockContext.Object);

      //act
      await service.PutMailGroup("x", newObject);

      //assert
      mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
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
