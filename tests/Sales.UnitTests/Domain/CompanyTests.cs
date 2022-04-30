using Sales.Implementation.Domain;
using System;
using FluentAssertions;
using Xunit;

namespace Sales.UnitTests.Domain;

public class CompanyTests {

    [Fact]
    public void ShouldAddMultipleRole() {
        var company = new Company(0, "Name");
        company.AddRole(CompanyRole.Customer);
        company.Roles.Should().HaveCount(1);
        company.Roles.Should().Contain(CompanyRole.Customer);

        company.AddRole(CompanyRole.Vendor);
        company.Roles.Should().HaveCount(2);
        company.Roles.Should().Contain(CompanyRole.Vendor);

        company.AddRole(CompanyRole.Supplier);
        company.Roles.Should().HaveCount(3);
        company.Roles.Should().Contain(CompanyRole.Supplier);
    }

    [Fact]
    public void ShouldNotAddDuplicateRole() {
        var company = new Company(0, "Name");
        var addRole = () => company.AddRole(CompanyRole.Customer);
        addRole();
        addRole.Should().Throw<InvalidOperationException>();
    }

}