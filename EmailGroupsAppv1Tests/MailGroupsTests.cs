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
      var mockMailGroups = new Mock<DbSet<MailGroup>>();
      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailGroups).Returns(mockMailGroups.Object);

      var service = new MailGroupsController(mockContext.Object);
      await service.PostMailGroup(new MailGroup() { Name = "1" });

      mockMailGroups.Verify(x => x.Add(It.IsAny<MailGroup>()), Times.Once);
      mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
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

      //expect
      var enumerable = new TestAsyncEnumerable<MailGroup>(data);
      var mockMailGroups = new Mock<DbSet<MailGroup>>();
      mockMailGroups.As<IAsyncEnumerable<MailGroup>>()
        .Setup(x => x.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
        .Returns(enumerable.GetAsyncEnumerator());
      mockMailGroups.As<IQueryable<MailGroup>>().Setup(x => x.Provider).Returns(enumerable.Provider);
      mockMailGroups.As<IQueryable<MailGroup>>().Setup(x => x.Expression).Returns(data.Expression);
      mockMailGroups.As<IQueryable<MailGroup>>().Setup(x => x.ElementType).Returns(enumerable.ElementType);
      mockMailGroups.As<IQueryable<MailGroup>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());
      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailGroups).Returns(mockMailGroups.Object);
      var service = new MailGroupsController(mockContext.Object);

      //act
      var response = await service.GetMailGroups();

      //assert
      Assert.IsInstanceOfType(response, typeof(ActionResult<IEnumerable<MailGroup>>));

      //act
      var responseValue = response.Value as IEnumerable<MailGroup>;

      //assert
      Assert.AreEqual(responseValue.Count(), 3);
      Assert.AreEqual(responseValue.ElementAt(0).Name, "1");
      Assert.AreEqual(responseValue.ElementAt(1).Name, "2");
      Assert.AreEqual(responseValue.ElementAt(2).Name, "3");
    }
  }
}
