using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;


namespace SalesProject.WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void btnCashButtonHome(string lblTotAmt, string lblNetAmt, string lblTaxAmt, string tUserNo, string tCounter, DataTable dt, string lblDiscount, string DiscountType, DataTable dtSingleFree, string tSmenNo, string tsmanRemarks,DataTable dtserial);

        [OperationContract]
        void btnNETSButtonHome(string lblTotAmt, string lblNetAmt, string lblTaxAmt, string tUserNo, string tCounter, DataTable dt, string lblDiscount, string DiscountType, DataTable dtSingleFree, string tSmenNo, string tsmanRemarks, DataTable dtserial);

       // [OperationContract]
        //void sp_funBtnDolor1(string lblTotAmt, string lblNetAmt, string lblTaxAmt, string tUserNo, string tCounter, DataTable dt, string lblDiscount, string DiscountType);
    }
}
