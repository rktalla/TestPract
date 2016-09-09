using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApplication1
{
    public class Class1
    {
        private IUnitOfWork GetMockUnitofWork()
        {
            return Mock.Of<IUnitOfWork>(r =>
                 r.GenericRepositoryFor<IncomingRequest>() == Mock.Of<IGenericRepository<IncomingRequest>>(d => d.GetAll() == IncomingRequestDMDummyEntities.GetIncomingRequest().Where(i => i.IncomingRequestId == 129)) &&
                 r.GenericRepositoryFor<TransactionDataSource>() == Mock.Of<IGenericRepository<TransactionDataSource>>(d => d.GetAll() == TransactionDataSourceDMDummyEntities.GetTransactionDataSource())

                 );
        }
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
                }
            };

            var mockServiceLocator =
                Mock.Of<IServiceLocator>(
                    i =>        i.GetInstance<IUserContextService>().Current == LoanAppUserContextTest.PopulateUserContextMock()
                             && i.GetInstance<IUserRequestContextService>().Current == LoanAppUserContextTest.PopulateRequestContextMock()
                             && i.GetInstance<IUserService>().GetOrCreateCurrentEmployeeIdFromAD(It.IsAny<string>(), It.IsAny<bool>()) == 375
                             && i.GetInstance<IReferenceDataSelectorService>().GetReferenceDataCodeByName(It.IsAny<ReferenceDataSelector>(), It.IsAny<string>()) == string.Empty);

                             ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mockServiceLocator));

            var result = _incomingMessageService.SaveActivityCategorization(IncomingMessageVM_DummyEntities.GetIncomingMessageActivityViewModel().FirstOrDefault(d => d.TemplateId == "00000011"));
            Assert.IsTrue(result == 20453);
        }
    }
}