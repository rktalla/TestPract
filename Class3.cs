using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    [TestClass]
    [DeploymentItem("dependency.config")]
    public class IncomingMessagesUnitTest : LoanAppDomainServiceTestBase
    {
        LoanService _loanService;
        IncomingMessageService _incomingMessageService;

        public IncomingMessagesUnitTest()
        {
            _loanService = new LoanService();
            _incomingMessageService = new IncomingMessageService();
        }

        [TestMethod, TestCategory("DomainService.IncomingMessageServiceUnitTest"), TestCategory("DomainServiceUnitTest")]
        public void SaveActivityCategorization_ForCopyTemplate_Success()
        {

            ServiceBase.UnitOfWork = GetMockUnitofWork();
            LoanAppUserContextTest.SetupUserDetail("LNSAAT05", "", "");
            var loanDealResponse = new LoanDealGetLoanDealResponse()
            {
                LoanDeal = new LoanDeal
                {
                    CustomerName = "GLAS BEHRENS RESORTS",
                    TransactionAmount = 125412,
                    TransactionCurrencyCode = "USD",
                    ValueDate = DateTime.Now,
                    CustomerCode = "5089"
                }
            };

            var mockServiceLocator =
                Mock.Of<IServiceLocator>(
                    i =>

                              i.GetInstance<IUserContextService>().Current == LoanAppUserContextTest.PopulateUserContextMock()

                             && i.GetInstance<IUserRequestContextService>().Current == LoanAppUserContextTest.PopulateRequestContextMock()
                             && i.GetInstance<ILoanService>().StartLoanSetupWorkflow(It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             && i.GetInstance<ILoanService>().GetTemplateDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LoanInputSheetViewModel>()) == 20453


                             && i.GetInstance<ILoanDealBpmService>().GetLoanDeal(It.IsAny<LoanDealGetLoanDealRequest>()) == loanDealResponse
                             && i.GetInstance<ICustomerService>().GetCustomerIdByCifNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()) == 5089
                             && i.GetInstance<IUserService>().GetOrCreateCurrentEmployeeIdFromAD(It.IsAny<string>(), It.IsAny<bool>()) == 375
                             && i.GetInstance<IReferenceDataSelectorService>().GetReferenceDataCodeByName(It.IsAny<ReferenceDataSelector>(), It.IsAny<string>()) == string.Empty);

            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mockServiceLocator));



            var result = _incomingMessageService.SaveActivityCategorization(IncomingMessageVM_DummyEntities.GetIncomingMessageActivityViewModel().FirstOrDefault(d => d.TemplateId == "00000011"));
            Assert.IsTrue(result == 20453);

        }
        [TestMethod, TestCategory("DomainService.IncomingMessageServiceUnitTest"), TestCategory("DomainServiceUnitTest")]
        public void SaveActivityCategorization_ForCopyTemplate_fail()
        {

            ServiceBase.UnitOfWork = GetMockUnitofWork();
            LoanAppUserContextTest.SetupUserDetail("LNSAAT05", "", "");
            var loanDealResponse = new LoanDealGetLoanDealResponse()
            {
                LoanDeal = new LoanDeal
                {
                    CustomerName = "GLAS BEHRENS RESORTS",
                    TransactionAmount = 125412,
                    TransactionCurrencyCode = "USD",
                    ValueDate = DateTime.Now,
                    CustomerCode = "5089"
                }
            };

            var mockServiceLocator =
                Mock.Of<IServiceLocator>(
                    i =>

                              i.GetInstance<IUserContextService>().Current == LoanAppUserContextTest.PopulateUserContextMock()

                             && i.GetInstance<IUserRequestContextService>().Current == LoanAppUserContextTest.PopulateRequestContextMock()
                             && i.GetInstance<ILoanService>().StartLoanSetupWorkflow(It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             && i.GetInstance<ILoanService>().GetTemplateDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LoanInputSheetViewModel>()) == 20453


                             && i.GetInstance<ILoanDealBpmService>().GetLoanDeal(It.IsAny<LoanDealGetLoanDealRequest>()) == loanDealResponse
                             && i.GetInstance<ICustomerService>().GetCustomerIdByCifNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()) == 5089
                             && i.GetInstance<IUserService>().GetOrCreateCurrentEmployeeIdFromAD(It.IsAny<string>(), It.IsAny<bool>()) == 375
                             && i.GetInstance<IReferenceDataSelectorService>().GetReferenceDataCodeByName(It.IsAny<ReferenceDataSelector>(), It.IsAny<string>()) == string.Empty);
            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mockServiceLocator));



            var result = _incomingMessageService.SaveActivityCategorization(IncomingMessageVM_DummyEntities.GetIncomingMessageActivityViewModel().FirstOrDefault(d => d.TemplateId == "00000011"));
            Assert.IsFalse(result != 20453);

        }

        [TestMethod, TestCategory("DomainService.IncomingMessageServiceUnitTest"), TestCategory("DomainServiceUnitTest")]
        public void SaveActivityCategorization_GetTemplateDetails_ForCopyTemplate_Success()
        {

            ServiceBase.UnitOfWork = GetMockUnitofWork();
            LoanAppUserContextTest.SetupUserDetail("LNSAAT05", "", "");
            var loanDealResponse = new LoanDealGetLoanDealResponse()
            {
                LoanDeal = new LoanDeal
                {
                    CustomerName = "GLAS BEHRENS RESORTS",
                    TransactionAmount = 125412,
                    TransactionCurrencyCode = "USD",
                    ValueDate = DateTime.Now,
                    CustomerCode = "5089"
                }
            };
            int refint = 0;
            var mockServiceLocator =
                Mock.Of<IServiceLocator>(
                    i =>
                             i.GetInstance<IUserContextService>().Current == LoanAppUserContextTest.PopulateUserContextMock()
                             && i.GetInstance<IWorkflowService>().StartBpmWorkflow(It.IsAny<LoansApp.Common.Web.WorkflowType>(), It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Func<InitiateWorkflowRequestViewModel, string>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             && i.GetInstance<IWorkflowService>().CheckUpdateTransactionNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int?>()) == TransactionNumberDMDummyEntities.GeTransactionNumbers().FirstOrDefault(j => j.TransactionNumberId == 0003)
                             && i.GetInstance<IUserRequestContextService>().Current == LoanAppUserContextTest.PopulateRequestContextMock()
                             && i.GetInstance<ILoanService>().StartLoanSetupWorkflow(It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             //&& i.GetInstance<ILoanService>().GetTemplateDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LoanInputSheetViewModel>()) == 20453
                             && i.GetInstance<ILoanService>().SaveLoanDeal(It.IsAny<LoanDeal>(), ref refint, It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()) == 20543
                             && i.GetInstance<ILoanDealBpmService>().GetLoanDeal(It.IsAny<LoanDealGetLoanDealRequest>()) == loanDealResponse
                             && i.GetInstance<ICustomerService>().GetCustomerIdByCifNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()) == 5089
                             && i.GetInstance<IUserService>().GetOrCreateCurrentEmployeeIdFromAD(It.IsAny<string>(), It.IsAny<bool>()) == 375
                             && i.GetInstance<IReferenceDataSelectorService>().GetReferenceDataCodeByName(It.IsAny<ReferenceDataSelector>(), It.IsAny<string>()) == string.Empty);
            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mockServiceLocator));

            var incomingMessageActivityVM = IncomingMessageVM_DummyEntities.GetIncomingMessageActivityViewModel().FirstOrDefault(d => d.TemplateId == "00000011");
            var loanInputsheetViewModel = new LoanInputSheetViewModel
            {
                CustomerCode = incomingMessageActivityVM.CIFNumber,
                CustomerName = incomingMessageActivityVM.CustomerName,
                FundingRequestViewModel = new FundingRequestViewModel
                {
                    TransactionAmount = incomingMessageActivityVM.Amount,
                    ValueDate = incomingMessageActivityVM.ValueDate,
                    TransactionCurrencyCode = incomingMessageActivityVM.CurrencyCode
                },
                WorkflowAdminUID = incomingMessageActivityVM.AdminUID,
                WorkflowAdminName = incomingMessageActivityVM.AdminName
            };
            var result = _loanService.GetTemplateDetails(incomingMessageActivityVM.TemplateId, incomingMessageActivityVM.TransactionNumber, incomingMessageActivityVM.CostCenterCode, loanInputsheetViewModel);
            Assert.IsTrue(result == 20543);

        }

        [TestMethod, TestCategory("DomainService.IncomingMessageServiceUnitTest"), TestCategory("DomainServiceUnitTest")]
        public void SaveActivityCategorization_GetTemplateDetails_ForCopyTemplate_fail()
        {

            ServiceBase.UnitOfWork = GetMockUnitofWork();
            LoanAppUserContextTest.SetupUserDetail("LNSAAT05", "", "");
            var loanDealResponse = new LoanDealGetLoanDealResponse()
            {
                LoanDeal = new LoanDeal
                {
                    CustomerName = "GLAS BEHRENS RESORTS",
                    TransactionAmount = 125412,
                    TransactionCurrencyCode = "USD",
                    ValueDate = DateTime.Now,
                    CustomerCode = "5089"
                }
            };
            int refint = 0;
            var mockServiceLocator =
                Mock.Of<IServiceLocator>(
                    i =>
                             i.GetInstance<IUserContextService>().Current == LoanAppUserContextTest.PopulateUserContextMock()
                             && i.GetInstance<IWorkflowService>().StartBpmWorkflow(It.IsAny<LoansApp.Common.Web.WorkflowType>(), It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Func<InitiateWorkflowRequestViewModel, string>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             && i.GetInstance<IWorkflowService>().CheckUpdateTransactionNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int?>()) == TransactionNumberDMDummyEntities.GeTransactionNumbers().FirstOrDefault(j => j.TransactionNumberId == 0003)
                             && i.GetInstance<IUserRequestContextService>().Current == LoanAppUserContextTest.PopulateRequestContextMock()
                             && i.GetInstance<ILoanService>().StartLoanSetupWorkflow(It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             //&& i.GetInstance<ILoanService>().GetTemplateDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LoanInputSheetViewModel>()) == 20453
                             && i.GetInstance<ILoanService>().SaveLoanDeal(It.IsAny<LoanDeal>(), ref refint, It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()) == 20543
                             && i.GetInstance<ILoanDealBpmService>().GetLoanDeal(It.IsAny<LoanDealGetLoanDealRequest>()) == loanDealResponse
                             && i.GetInstance<ICustomerService>().GetCustomerIdByCifNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()) == 5089
                             && i.GetInstance<IUserService>().GetOrCreateCurrentEmployeeIdFromAD(It.IsAny<string>(), It.IsAny<bool>()) == 375
                             && i.GetInstance<IReferenceDataSelectorService>().GetReferenceDataCodeByName(It.IsAny<ReferenceDataSelector>(), It.IsAny<string>()) == string.Empty);
            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mockServiceLocator));

            var incomingMessageActivityVM = IncomingMessageVM_DummyEntities.GetIncomingMessageActivityViewModel().FirstOrDefault(d => d.TemplateId == "00000011");
            var loanInputsheetViewModel = new LoanInputSheetViewModel
            {
                CustomerCode = incomingMessageActivityVM.CIFNumber,
                CustomerName = incomingMessageActivityVM.CustomerName,
                FundingRequestViewModel = new FundingRequestViewModel
                {
                    TransactionAmount = incomingMessageActivityVM.Amount,
                    ValueDate = incomingMessageActivityVM.ValueDate,
                    TransactionCurrencyCode = incomingMessageActivityVM.CurrencyCode
                },
                WorkflowAdminUID = incomingMessageActivityVM.AdminUID,
                WorkflowAdminName = incomingMessageActivityVM.AdminName
            };
            var result = _loanService.GetTemplateDetails(incomingMessageActivityVM.TemplateId, incomingMessageActivityVM.TransactionNumber, incomingMessageActivityVM.CostCenterCode, loanInputsheetViewModel);
            Assert.IsFalse(result != 20543);

        }

        [TestMethod, TestCategory("DomainService.IncomingMessageServiceUnitTest"), TestCategory("DomainServiceUnitTest")]
        public void SaveActivityCategorization_ForCopyLoan_Success()
        {

            ServiceBase.UnitOfWork = GetMockUnitofWork();
            LoanAppUserContextTest.SetupUserDetail("LNSAAT05", "", "");
            var loanDealResponse = new LoanDealGetLoanDealResponse()
            {
                LoanDeal = new LoanDeal
                {
                    CustomerName = "GLAS BEHRENS RESORTS",
                    TransactionAmount = 125412,
                    TransactionCurrencyCode = "USD",
                    ValueDate = DateTime.Now,
                    CustomerCode = "5089"
                }
            };

            var mockServiceLocator =
                Mock.Of<IServiceLocator>(
                    i =>

                              i.GetInstance<IUserContextService>().Current == LoanAppUserContextTest.PopulateUserContextMock()

                             && i.GetInstance<IUserRequestContextService>().Current == LoanAppUserContextTest.PopulateRequestContextMock()
                             && i.GetInstance<ILoanService>().StartLoanSetupWorkflow(It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             && i.GetInstance<ILoanService>().CopyFromLoan(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LoanInputSheetViewModel>()) == 20453


                             && i.GetInstance<ILoanDealBpmService>().GetLoanDeal(It.IsAny<LoanDealGetLoanDealRequest>()) == loanDealResponse
                             && i.GetInstance<ICustomerService>().GetCustomerIdByCifNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()) == 5089
                             && i.GetInstance<IUserService>().GetOrCreateCurrentEmployeeIdFromAD(It.IsAny<string>(), It.IsAny<bool>()) == 375
                             && i.GetInstance<IReferenceDataSelectorService>().GetReferenceDataCodeByName(It.IsAny<ReferenceDataSelector>(), It.IsAny<string>()) == string.Empty);
            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mockServiceLocator));



            var result = _incomingMessageService.SaveActivityCategorization(IncomingMessageVM_DummyEntities.GetIncomingMessageActivityViewModel().FirstOrDefault(d => d.LoanTransactionNumber == "LN-0000900      -000"));
            Assert.IsTrue(result == 20453);

        }

        [TestMethod, TestCategory("DomainService.IncomingMessageServiceUnitTest"), TestCategory("DomainServiceUnitTest")]
        public void SaveActivityCategorization_ForCopyLoan_fail()
        {

            ServiceBase.UnitOfWork = GetMockUnitofWork();
            LoanAppUserContextTest.SetupUserDetail("LNSAAT05", "", "");
            var loanDealResponse = new LoanDealGetLoanDealResponse()
            {
                LoanDeal = new LoanDeal
                {
                    CustomerName = "GLAS BEHRENS RESORTS",
                    TransactionAmount = 125412,
                    TransactionCurrencyCode = "USD",
                    ValueDate = DateTime.Now,
                    CustomerCode = "5089"
                }
            };

            var mockServiceLocator =
                Mock.Of<IServiceLocator>(
                    i =>

                              i.GetInstance<IUserContextService>().Current == LoanAppUserContextTest.PopulateUserContextMock()

                             && i.GetInstance<IUserRequestContextService>().Current == LoanAppUserContextTest.PopulateRequestContextMock()
                             && i.GetInstance<ILoanService>().StartLoanSetupWorkflow(It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             && i.GetInstance<ILoanService>().CopyFromLoan(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LoanInputSheetViewModel>()) == 20453


                             && i.GetInstance<ILoanDealBpmService>().GetLoanDeal(It.IsAny<LoanDealGetLoanDealRequest>()) == loanDealResponse
                             && i.GetInstance<ICustomerService>().GetCustomerIdByCifNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()) == 5089
                             && i.GetInstance<IUserService>().GetOrCreateCurrentEmployeeIdFromAD(It.IsAny<string>(), It.IsAny<bool>()) == 375
                             && i.GetInstance<IReferenceDataSelectorService>().GetReferenceDataCodeByName(It.IsAny<ReferenceDataSelector>(), It.IsAny<string>()) == string.Empty);
            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mockServiceLocator));



            var result = _incomingMessageService.SaveActivityCategorization(IncomingMessageVM_DummyEntities.GetIncomingMessageActivityViewModel().FirstOrDefault(d => d.LoanTransactionNumber == "LN-0000900      -000"));
            Assert.IsFalse(result != 20453);

        }

        [TestMethod, TestCategory("DomainService.IncomingMessageServiceUnitTest"), TestCategory("DomainServiceUnitTest")]
        public void SaveActivityCategorization_CopyFromLoan_ForCopyLoan_Success()
        {

            ServiceBase.UnitOfWork = GetMockUnitofWork();
            LoanAppUserContextTest.SetupUserDetail("LNSAAT05", "", "");
            var loanDealResponse = new LoanDealGetLoanDealResponse()
            {
                LoanDeal = new LoanDeal
                {
                    CustomerName = "GLAS BEHRENS RESORTS",
                    TransactionAmount = 125412,
                    TransactionCurrencyCode = "USD",
                    ValueDate = DateTime.Now,
                    CustomerCode = "5089"
                }
            };
            int refint = 0;
            var mockServiceLocator =
                Mock.Of<IServiceLocator>(
                    i =>
                             i.GetInstance<IUserContextService>().Current == LoanAppUserContextTest.PopulateUserContextMock()
                             && i.GetInstance<IWorkflowService>().StartBpmWorkflow(It.IsAny<LoansApp.Common.Web.WorkflowType>(), It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Func<InitiateWorkflowRequestViewModel, string>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             && i.GetInstance<IWorkflowService>().CheckUpdateTransactionNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int?>()) == TransactionNumberDMDummyEntities.GeTransactionNumbers().FirstOrDefault(j => j.TransactionNumberId == 0003)
                             && i.GetInstance<IUserRequestContextService>().Current == LoanAppUserContextTest.PopulateRequestContextMock()
                             && i.GetInstance<ILoanService>().StartLoanSetupWorkflow(It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             && i.GetInstance<ILoanService>().GetLoanDealCanonicalFromOVS(It.IsAny<string>(), It.IsAny<string>()) == loanDealResponse.LoanDeal
                             && i.GetInstance<ILoanService>().SaveLoanDeal(It.IsAny<LoanDeal>(), ref refint, It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()) == 20543
                             && i.GetInstance<ILoanDealBpmService>().GetLoanDeal(It.IsAny<LoanDealGetLoanDealRequest>()) == loanDealResponse
                             && i.GetInstance<ICustomerService>().GetCustomerIdByCifNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()) == 5089
                             && i.GetInstance<IUserService>().GetOrCreateCurrentEmployeeIdFromAD(It.IsAny<string>(), It.IsAny<bool>()) == 375
                             && i.GetInstance<IReferenceDataSelectorService>().GetReferenceDataCodeByName(It.IsAny<ReferenceDataSelector>(), It.IsAny<string>()) == string.Empty);
            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mockServiceLocator));

            var incomingMessageActivityVM = IncomingMessageVM_DummyEntities.GetIncomingMessageActivityViewModel().FirstOrDefault(d => d.LoanTransactionNumber == "LN-0000900      -000");
            var loanInputsheetViewModel = new LoanInputSheetViewModel
            {
                CustomerCode = incomingMessageActivityVM.CIFNumber,
                CustomerName = incomingMessageActivityVM.CustomerName,
                FundingRequestViewModel = new FundingRequestViewModel
                {
                    TransactionAmount = incomingMessageActivityVM.Amount,
                    ValueDate = incomingMessageActivityVM.ValueDate,
                    TransactionCurrencyCode = incomingMessageActivityVM.CurrencyCode
                },
                WorkflowAdminUID = incomingMessageActivityVM.AdminUID,
                WorkflowAdminName = incomingMessageActivityVM.AdminName
            };
            var result = _loanService.CopyFromLoan(incomingMessageActivityVM.TransactionNumber, incomingMessageActivityVM.CostCenterCode, loanInputsheetViewModel);
            Assert.IsTrue(result == 20543);

        }

        [TestMethod, TestCategory("DomainService.IncomingMessageServiceUnitTest"), TestCategory("DomainServiceUnitTest")]
        public void SaveActivityCategorization_CopyFromLoan_ForCopyLoan_fail()
        {

            ServiceBase.UnitOfWork = GetMockUnitofWork();
            LoanAppUserContextTest.SetupUserDetail("LNSAAT05", "", "");
            var loanDealResponse = new LoanDealGetLoanDealResponse()
            {
                LoanDeal = new LoanDeal
                {
                    CustomerName = "GLAS BEHRENS RESORTS",
                    TransactionAmount = 125412,
                    TransactionCurrencyCode = "USD",
                    ValueDate = DateTime.Now,
                    CustomerCode = "5089"
                }
            };
            int refint = 0;
            var mockServiceLocator =
                Mock.Of<IServiceLocator>(
                    i =>
                             i.GetInstance<IUserContextService>().Current == LoanAppUserContextTest.PopulateUserContextMock()
                             && i.GetInstance<IWorkflowService>().StartBpmWorkflow(It.IsAny<LoansApp.Common.Web.WorkflowType>(), It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Func<InitiateWorkflowRequestViewModel, string>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             && i.GetInstance<IWorkflowService>().CheckUpdateTransactionNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int?>()) == TransactionNumberDMDummyEntities.GeTransactionNumbers().FirstOrDefault(j => j.TransactionNumberId == 0003)
                             && i.GetInstance<IUserRequestContextService>().Current == LoanAppUserContextTest.PopulateRequestContextMock()
                             //from Loan Service we are using Service Locator to call loan Service methods for that reason we mocked ILoanService
                             && i.GetInstance<ILoanService>().StartLoanSetupWorkflow(It.IsAny<InitiateWorkflowRequestViewModel>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>(), It.IsAny<Action<InitiateWorkflowRequestViewModel>>()) == 25430
                             && i.GetInstance<ILoanService>().GetLoanDealCanonicalFromOVS(It.IsAny<string>(), It.IsAny<string>()) == loanDealResponse.LoanDeal
                             && i.GetInstance<ILoanService>().SaveLoanDeal(It.IsAny<LoanDeal>(), ref refint, It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()) == 20543
                             && i.GetInstance<ILoanDealBpmService>().GetLoanDeal(It.IsAny<LoanDealGetLoanDealRequest>()) == loanDealResponse
                             && i.GetInstance<ICustomerService>().GetCustomerIdByCifNumber(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()) == 5089
                             && i.GetInstance<IUserService>().GetOrCreateCurrentEmployeeIdFromAD(It.IsAny<string>(), It.IsAny<bool>()) == 375
                             && i.GetInstance<IReferenceDataSelectorService>().GetReferenceDataCodeByName(It.IsAny<ReferenceDataSelector>(), It.IsAny<string>()) == string.Empty);
            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mockServiceLocator));

            var incomingMessageActivityVM = IncomingMessageVM_DummyEntities.GetIncomingMessageActivityViewModel().FirstOrDefault(d => d.LoanTransactionNumber == "LN-0000900      -000");
            var loanInputsheetViewModel = new LoanInputSheetViewModel
            {
                CustomerCode = incomingMessageActivityVM.CIFNumber,
                CustomerName = incomingMessageActivityVM.CustomerName,
                FundingRequestViewModel = new FundingRequestViewModel
                {
                    TransactionAmount = incomingMessageActivityVM.Amount,
                    ValueDate = incomingMessageActivityVM.ValueDate,
                    TransactionCurrencyCode = incomingMessageActivityVM.CurrencyCode
                },
                WorkflowAdminUID = incomingMessageActivityVM.AdminUID,
                WorkflowAdminName = incomingMessageActivityVM.AdminName
            };
            var result = _loanService.CopyFromLoan(incomingMessageActivityVM.TransactionNumber, incomingMessageActivityVM.CostCenterCode, loanInputsheetViewModel);
            Assert.IsFalse(result != 20543);

        }


        private IUnitOfWork GetMockUnitofWork()
        {
            return Mock.Of<IUnitOfWork>(r =>
                 r.GenericRepositoryFor<IncomingRequestActivity>() == Mock.Of<IGenericRepository<IncomingRequestActivity>>(d => d.GetAll() == IncomingRequestActivityDMDummyEntities.GetIncomingRequestActivity().Where(i => i.WorkflowTransactionId == 2543)) &&
                 r.GenericRepositoryFor<IncomingRequest>() == Mock.Of<IGenericRepository<IncomingRequest>>(d => d.GetAll() == IncomingRequestDMDummyEntities.GetIncomingRequest().Where(i => i.IncomingRequestId == 129)) &&
                 r.GenericRepositoryFor<TransactionDataSource>() == Mock.Of<IGenericRepository<TransactionDataSource>>(d => d.GetAll() == TransactionDataSourceDMDummyEntities.GetTransactionDataSource())

                 );

        }
    }

}