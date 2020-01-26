using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Moq;
using EmailGroupsAppv1.Models;
using EmailGroupsAppv1.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;

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
      var data = new List<MailGroup>
      {
        new MailGroup{Name="2"},
        new MailGroup{Name="3"},
        new MailGroup{Name="1"}
      }.AsQueryable();

      var mockMailGroups = new Mock<DbSet<MailGroup>>();
      mockMailGroups.As<IDbAsyncEnumerable<MailGroup>>()
        .Setup(x => x.GetAsyncEnumerator())
        .Returns(new TestAsyncEnumerator<MailGroup>(data.GetEnumerator()));

      mockMailGroups.As<IQueryable<MailGroup>>()
        .Setup(x => x.Provider)
        .Returns(new TestAsyncQueryProvider<MailGroup>(data.Provider));

      mockMailGroups.As<IQueryable<MailGroup>>().Setup(x => x.Expression).Returns(data.Expression);
      mockMailGroups.As<IQueryable<MailGroup>>().Setup(x => x.ElementType).Returns(data.ElementType);
      mockMailGroups.As<IQueryable<MailGroup>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

      var mockContext = new Mock<MailGroupsContext>();
      mockContext.Setup(x => x.MailGroups).Returns(mockMailGroups.Object);

      var service = new MailGroupsController(mockContext.Object);
      var response = await service.GetMailGroups();

      Assert.IsInstanceOfType(response, typeof(ActionResult<IEnumerable<MailGroup>>));
    }
  }
}
