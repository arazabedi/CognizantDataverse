using System;
using System.Collections.Generic;
using CognizantDataverse.Models;
using CognizantDataverse.Services;
using Microsoft.Xrm.Sdk;
using Moq;
using Xunit;

public class AccountControllerTests
{
    private readonly Mock<IAccountService> _mockAccountService;
    private readonly Mock<IConsoleUI> _mockConsoleUI;
    private readonly AccountController _accountController;

    public AccountControllerTests()
    {
        _mockAccountService = new Mock<IAccountService>();
        _mockConsoleUI = new Mock<IConsoleUI>();
        _accountController = new AccountController(_mockAccountService.Object, _mockConsoleUI.Object);
    }

    [Fact]
    public void HandleCreateAccount_ShouldCreateAccount_WhenValidInputIsProvided()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        _mockConsoleUI.Setup(ui => ui.PromptForInput("Enter Account Name:")).Returns("Test Account");
        _mockConsoleUI.Setup(ui => ui.PromptForInput("Enter Phone Number:")).Returns("1234567890");
        _mockConsoleUI.Setup(ui => ui.PromptForInput("Enter City:")).Returns("Test City");
        _mockAccountService.Setup(service => service.CreateAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                           .Returns(accountId);

        // Act
        _accountController.HandleCreateAccount();

        // Assert
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"Account created with ID: {accountId}"), Times.Once);
    }

    [Fact]
    public void HandleReadAccount_ShouldDisplayAccountDetails_WhenValidAccountIdIsProvided()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var accountEntity = new Entity("account")
        {
            ["name"] = "Test Account",
            ["telephone1"] = "1234567890",
            ["address1_city"] = "Test City"
        };

        _mockConsoleUI.Setup(ui => ui.PromptForInput("Enter Account ID:")).Returns(accountId.ToString());
        _mockAccountService.Setup(service => service.ReadAccount(accountId)).Returns(accountEntity);

        // Act
        _accountController.HandleReadAccount();

        // Assert
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"Account Name: {accountEntity["name"]}"), Times.Once);
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"Telephone: {accountEntity["telephone1"]}"), Times.Once);
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"City: {accountEntity["address1_city"]}"), Times.Once);
    }

    [Fact]
    public void HandleUpdateAccount_ShouldUpdateAccount_WhenValidAccountIdAndNameAreProvided()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        _mockConsoleUI.Setup(ui => ui.PromptForInput("Enter Account ID:")).Returns(accountId.ToString());
        _mockConsoleUI.Setup(ui => ui.PromptForInput("Enter New Account Name:")).Returns("Updated Account Name");

        // Act
        _accountController.HandleUpdateAccount();

        // Assert
        _mockAccountService.Verify(service => service.UpdateAccount(accountId, "Updated Account Name"), Times.Once);
        _mockConsoleUI.Verify(ui => ui.DisplayMessage("Account updated successfully."), Times.Once);
    }

    [Fact]
    public void HandleDeleteAccount_ShouldDeleteAccount_WhenValidAccountIdIsProvided()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        _mockConsoleUI.Setup(ui => ui.PromptForInput("Enter Account ID:")).Returns(accountId.ToString());

        // Act
        _accountController.HandleDeleteAccount();

        // Assert
        _mockAccountService.Verify(service => service.DeleteAccount(accountId), Times.Once);
        _mockConsoleUI.Verify(ui => ui.DisplayMessage("Account deleted successfully."), Times.Once);
    }

    [Fact]
    public void HandleViewAllAccounts_ShouldDisplayAllAccounts_WhenAccountsExist()
    {
        // Arrange
        var accounts = new List<Account>
        {
            new Account { Id = Guid.NewGuid(), Name = "Account 1", Phone = "1111111111", City = "City 1" },
            new Account { Id = Guid.NewGuid(), Name = "Account 2", Phone = "2222222222", City = "City 2" }
        };

        _mockAccountService.Setup(service => service.GetAllAccounts()).Returns(accounts.ToArray());

        // Act
        _accountController.HandleViewAllAccounts();

        // Assert
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"Account ID: {accounts[0].Id}"), Times.Once);
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"Name: {accounts[0].Name}"), Times.Once);
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"Phone: {accounts[0].Phone}"), Times.Once);
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"City: {accounts[0].City}"), Times.Once);

        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"Account ID: {accounts[1].Id}"), Times.Once);
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"Name: {accounts[1].Name}"), Times.Once);
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"Phone: {accounts[1].Phone}"), Times.Once);
        _mockConsoleUI.Verify(ui => ui.DisplayMessage($"City: {accounts[1].City}"), Times.Once);
    }

    [Fact]
    public void HandleViewAllAccounts_ShouldDisplayNoAccountsMessage_WhenNoAccountsExist()
    {
        // Arrange
        _mockAccountService.Setup(service => service.GetAllAccounts()).Returns(Array.Empty<Account>());

        // Act
        _accountController.HandleViewAllAccounts();

        // Assert
        _mockConsoleUI.Verify(ui => ui.DisplayMessage("No accounts found."), Times.Once);
    }
}