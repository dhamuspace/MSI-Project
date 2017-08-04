


Go

CREATE PROCEDURE [dbo].[sp_SettleCreditCardProcess1]
(@tGrossAmt numeric(18,2),@tNetAmt numeric(18,2),@tDiscount numeric(18,2),@tTotTax numeric(18,2),@RoundValue numeric(18,2),@tempTable Type_gridValue Readonly,@DiscountType varchar(100),@tTxtAmount numeric(18,2),@tUserno numeric(18,0), @tCounter numeric(18,0),@tCreditCardName varchar(400), @tempFreeItem dtSingleFreeSales Readonly,@dt_gridload1 sp_funBtnDolorAlterTable  READONLY)
as
DECLARE @TranCounter INT; 
DECLARE @tStrnNo Numeric(18,0)=0;
DECLARE @tBillNo Numeric(18,0)=0;
DECLARE @tCurrentDate datetime;
DECLARE @tCurrentTime datetime;
DECLARE @tVoucherNo Numeric(18,0)=0;
DECLARE @ItemName varchar(200),@Qty numeric(18,4),@Rate numeric(18,2),@Amt numeric(18,2),@Disc Numeric(18,2);
DECLARE @tStrnSno Numeric(18,0)=0;
DECLARE @tItemNo Numeric(18,0)=0;
DECLARE @tTaxNo Numeric(18,0)=0;
DECLARE @tUnitNo Numeric(18,0)=0;
DECLARE @tTaxPercent Numeric(18,2)=0;
DECLARE @Profit Numeric(18,2)=0; 
DECLARE @tTaxAmt Numeric(18,2)=0;
DECLARE @tNetSalAmt Numeric(18,2)=0;
DECLARE @tSalesCount Numeric(18,0)=0;
DECLARE @tVchRefNo Numeric(18,0)=0;
DECLARE @tTax Numeric(18,2)=0;
DECLARE @tVoucherNoNew Numeric(18,0)=0;
DECLARE @tVoucherSno Numeric(18,0)=0;
DECLARE @tLedgerNo1 Numeric(18,0)=0;
DECLARE @tSingleTaxAmt Numeric(18,2)=0;
DECLARE @tItemCost Numeric(18,2)=0;
DECLARE @tTotItemCost Numeric(18,2)=0;
DECLARE @tTotItemRate Numeric(18,2)=0;
DECLARE @tLedsel_name varchar(100);
DECLARE @tSub varchar(100);
DECLARE @tLedgerNo1New Numeric(18,2);
DECLARE @tSingleTaxAmtNew Numeric(18,2)=0;
Declare @tCHK numeric(18,2)=0;
DECLARE @tSalRecv_Sno numeric(18,2);
DECLARE @tTaxNumber NUMERIC(18,0);
DECLARE @tNetSalRetAmt NUMERIC(18,2);
DECLARE @tOpenItem varchar(50);
DECLARE @tOpenItemCount Numeric(18,0);
DECLARE c CURSOR LOCAL READ_ONLY FOR SELECT ItemName,Qty,Rate,Amt, Disc FROM @tempTable
DECLARE c2 CURSOR LOCAL READ_ONLY FOR SELECT ItemName,Qty,Rate,Amt, Disc FROM @tempTable
DECLARE @tBankLedger_No Numeric(18,2);

	Declare @tItemNameFree varchar(400);
	Declare @tQtyFree numeric(18, 4);
	Declare @tScannedQtyFree varchar(100);
	Declare @tMainItemNameFree varchar(400);
	Declare @tOfferNameFree varchar(400);
	Declare @tOfferFreeQtyFree numeric(18, 2);
	Declare @tTotSaleQtyFree numeric(18, 2);
	DECLARE c3 CURSOR LOCAL READ_ONLY FOR SELECT ItemName,Qty,ScannedQty,MainItemName,OfferName,OfferFreeQty,TotSaleQty FROM @tempFreeItem
DECLARE c7 CURSOR LOCAL READ_ONLY FOR SELECT Type1,RemaininbillAmt,ReceiverAmt,Types2 from @dt_gridload1 
declare @Type1 varchar(200),@RecRemBillAmt numeric(18,2),@RecAmt numeric(18,2),@Types2 as varchar(200);

SET @TranCounter = @@TRANCOUNT;
IF @TranCounter > 0
SAVE TRANSACTION ProcedureSave;
ELSE
   BEGIN TRANSACTION;
   BEGIN TRY
  --Select @tBankLedger_No=BankLedger_No from CreditCard_Table where Card_Name=@tCreditCardName
  Select @tBankLedger_No=Ledger_no from Ledger_table where Ledger_name=@tCreditCardName
   --Select @tCreditCardName=Ledger_name from Ledger_table where Ledger_no=@tBankLedger_No
open c2
fetch from c2 into @ItemName,@Qty,@Rate,@Amt,@Disc
while @@fetch_status=0
begin
    Select @tItemNo=Item_no,@tItemCost=Item_cost from Item_table where Item_name=@ItemName;
    Set @tTotItemCost=@tTotItemCost+(@tItemCost*@Qty);
    Set @tTotItemRate=@tTotItemRate+(@Amt);
    fetch next from c2 into @ItemName,@Qty,@Rate,@Amt, @Disc
end
close c2
deallocate c2
 if exists (Select * from Tempsalmas_table where smas_rtno=0)
 Select @tBillNo=max(smas_billno)+1 from Tempsalmas_table where smas_rtno=0;
 else
 Select @tBillNo=max(smas_billno)+1 from SalMas_table where smas_rtno=0;
  Set @Profit=0;
   set @Profit=@tTotItemRate-@tTotItemCost;
   select @tSalRecv_Sno=(max(SalRecv_Sno)+1),@tStrnNo=(MAX(StrnNo)+1),@tVoucherNo=(max(VoucherNo)+1) from NumberTable;
   
   --Select @tBillNo=(count(*)+1) from SalMas_table where smas_rtno=0;
   
  
  if @tBillNo is Null
 Set @tBillNo=1
   --INSERT INTO SalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tStrnNo,'14',@tTxtAmount,'0','0')
  -- INSERT INTO TempSalRecv_table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tBillNo,@tBankLedger_No,@tTxtAmount,'0','0')
   open c7
  fetch from c7 into @Type1,@RecRemBillAmt,@RecAmt,@Types2
  while @@FETCH_STATUS=0
  Begin
   --INSERT INTO SalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tStrnNo,''5'',@tTxtAmount,(@tValue-@tTxtAmount),''0'')
   INSERT INTO TempSalRecv_table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tBillNo,@Type1,@RecRemBillAmt,@RecAmt,'0')
   set @tSalRecv_Sno=@tSalRecv_Sno+1
   fetch Next from c7 into @Type1,@RecRemBillAmt,@RecAmt,@Types2
  End
  close c7
  deallocate c7
  Update NumberTable set SalRecv_Sno=@tSalRecv_Sno;
 
   
   --if(@tBillNo=0) 
   --set @tBillNo=1;
   select @tCurrentDate=CONVERT(DATE,DATEADD(day,1,EndOfDay),103) from EndOFday_Table where Id=(select EndOfDayId from NumberTable)
   select @tCurrentTime=convert(time,GETDATE(),100); 
      if @tCounter is null
  set @tCounter=1
   if @tUserno is null
  set @tUserno=1
   Insert into Tempsalmas_table (smas_no,smas_slno,smas_rtno,smas_billprefix,smas_billsuffix,smas_billno,Smas_Bill,smas_billdate,smas_billtime,smas_days,Mechanic_no,MechCommi_Amt,MechCommi_Per,SmanCommi_Amt,SManCommi_Per,MechCommiTax_Per,Smas_SmanNo,dc_no,dc_date,order_no,reference_date,reference_no,Vehicle_no,Smas_Others1,Smas_others2,smas_others3,smas_others4,UserNo,ctr_no,Godown_no,party_no,Customer_no,smas_name,smas_add1,smas_add2,smas_add3,smas_add4,smas_add5,smas_st,smas_cst,smas_cashmode,Smas_salmode,smas_saltype,smas_ordertype,smas_addled1,smas_addled2,smas_addled3,smas_addled4,smas_adddet1,smas_adddet2,smas_adddet3,smas_adddet4,smas_adddisc1,smas_adddisc2,smas_adddisc3,smas_adddisc4,smas_adddiscr1,smas_adddiscr2,smas_adddiscr3,smas_adddiscr4,smas_addamt1,smas_addamt2,smas_addamt3,smas_addamt4,smas_Gross,smas_GrossAmount,BankCharge,smas_NetAmount,smas_rcvdamount,smas_remarks,smas_Cancel,smas_cremark,smas_rounded,CashReceived,Profit,Update_Flag,Tax_Refund,Print_no,smas_point,smas_issue,smas_paidAmt,smas_Touch,smas_TotalCash,smas_TotalCredit,smas_TotalNets,Loaditem,Smas_columns,VoucherSno,SalesVchNo,ReceiptVchNo,CrNoteVchNo,CardSno,CardNo) values 
   (@tStrnNo,'0','0','','',@tBillNo,@tBillNo,@tCurrentDate,@tCurrentTime,'0','1','0','0','0','0','0','0','0','1999-01-01','','','','','','','','',@tUserno,@tCounter,'2',@tBankLedger_No,'0',@tCreditCardName,'','','','','','','','0','0','0','0','0','0','0','0','','','','','0','0','0','0','0','0','0','0','0','0','0','0',@tGrossAmt,@tGrossAmt,'0',@tNetAmt,@tNetAmt,'','0','',@RoundValue,'0',@Profit,'0','0','0','0','1',@tNetAmt,'True','0','0',@tNetAmt,'0','',@tVoucherNo,@tBillNo,'0','0','0','')
   
   
   
 Select @tStrnNo=(max(strnno)+1) from Numbertable
Declare @tFreeSno Numeric(18,2)=0;
Declare @tCtl_FreeQty  bit=0;
Declare @tSaleQtyFrom numeric(18,2);
Declare @tFreeQtyCount int=0;
Declare @tRprop varchar(100)='NoTax';
open c
fetch from c into @ItemName,@Qty,@Rate,@Amt, @Disc
while @@fetch_status=0
begin
  Select @tStrnSno=(max(strnsno)+1) from Numbertable
  Select @tItemNo=Item_no,@tTaxNo=Tax_no,@tUnitNo=Unit_no from Item_table where Item_name=@ItemName;
  SELECT @tTaxPercent=Nt_Percent,@tLedgerNo1=NtLedger_No from Tax_Table where Tax_No=@tTaxNo;
  Set @tTaxAmt=((@Qty*@Rate)*(@tTaxPercent/100));
  
  Select @tRprop=Rprop from Rptset where Rdesc='Display Tax Type'
  if @tRprop='NoTax'
  begin
  Set @tTaxAmt=0
  set @tTaxNo=1
  set @tTaxPercent=0
  end
  
   --Free Item Code Start
  Select @tCtl_FreeQty= Ctl_FreeQty from Control_table
  if @tCtl_FreeQty=1
  begin 
   set @tFreeQtyCount=0;
  --set @tSaleQtyFrom=0;
  if Exists (Select * from tempView where FreeType='Item Price' and Item_Name=@ItemName)
  Begin  
   set @tFreeSno=(Select Top 1 (FreeSno) from tempView where FreeType='Item Price' and Item_Name=@ItemName)
   Select @tSaleQtyFrom=SaleQtyFrom from tempView where FreeType='Item Price' and Item_Name=@ItemName
  set @tFreeQtyCount=@Qty/@tSaleQtyFrom;
    if @tFreeQtyCount=0
    set @tFreeSno=0
  End
  Else  
  Begin  
   set @tFreeSno=(Select Top 1 (FreeSno) from tempView where FreeType<>'Item Price' and Item_Name=@ItemName)
    Select @tSaleQtyFrom=SaleQtyFrom from tempView where FreeType<>'Item Price' and Item_Name=@ItemName
    set @tFreeQtyCount=@Qty/@tSaleQtyFrom;
    if @tFreeQtyCount=0
    set @tFreeSno=0
  End
  if  @tFreeSno is null
  set @tFreeSno=0
  End
  
  if @tFreeSno=0
  set @tFreeQtyCount=0
  -- Free Item Code End
 
  
  Select @tItemNo=Item_no,@tItemCost=Item_cost,@tOpenItem=OpenItem from Item_table where Item_name=@ItemName;
    Set @tTotItemCost=(@tItemCost*@Qty);
    Set @tTotItemRate=(@Amt);  
    if @tOpenItem='True'
  select @tOpenItemCount=COUNT(*) from Tempstktrn_table where item_no=(select item_no from Item_table where Item_name=@ItemName) and strn_no=@tStrnNo  
  ELSE
  set @tOpenItemCount=0    
    
  INSERT INTO Tempstktrn_table (strn_sno,strn_no,strn_rtno,strn_type,strn_date,Godown_BillNo,StrnParty_no,Grn_no,OrderSno,Dc_no,item_no,ctr_no,godown_no,Unit_no,Unit_Ratio,QtyInPieces,nt_qty,tx_qty,Short_qty,rnt_qty,rtx_qty,Invnt_qty,Invtx_qty,QtyDetails,Rate,Tax_Rate,CurrencyNo,CurrencyValue,Amount,Tax_No,Disc_PerQty,Disc_Per,Disc_Amt,Adldisc_Per,Adldisc_Amt,Othdisc_Amt,OthPurdisc,Ed_PerQty,Ed_Per,Ed_Amt,Cess_Per,Cess_Amt,SHECess_Per,SHECess_Amt,HL_Per,HL_Amt,CST_per,CST_amt,tax_Flag,tax_per,tax_amt,Sur_per,Sur_amt,CommiPer,Commi,SmanPer,SmanAmt,spl_discamt,tot_amt,alp1,alp2,alp3,alp4,ala1,ala2,ala3,ala4,Net_Amt,Other_Exp,BillOther_Exp,strn_remarks,Strn_Cancel,Order_Ack,Cost,Mrsp,Margin,Margin_No,Srate,Frtx_Qty,RFrnt_Qty,RFrtx_Qty,Frnt_Qty,FreeQty,FreeItemNo,Profit,Item_Point,Mech_no,PurRate,OpenItem,OpenItemCount) values
   (@tStrnSno,@tStrnNo,'0','1', @tCurrentDate,'0',@tBankLedger_No,'0','0','0',@tItemNo,@tCounter,'2',@tUnitNo,'1','0',@Qty,'0','0','0','0','0','0','',@Rate,'0','0','0',@Amt,@tTaxNo,'0','0',@Disc,'0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0',@tTaxPercent,@tTaxAmt,'0','0','0','0','0','0','0',(Case when @tRprop='Inclusive' then @Amt Else (@Amt+@tTaxAmt) END),'0','0','0','0','0','0','0','0',(Case when @tRprop='Inclusive' then @Amt Else (@Amt+@tTaxAmt) END),'0','0',@tRprop,'0','0','0','0','0','0','0','0','0','0','0',@tFreeQtyCount,@tFreeSno,(@tTotItemRate-@tTotItemCost),'0','0','0',@tOpenItem,@tOpenItemCount);
  update Numbertable set strnsno=strnsno+1;
   select @tNetSalAmt=sum(net_amt) from Tempstktrn_table where item_no=@tItemNo and strn_type=1
  select @tNetSalRetAmt=sum(net_amt) from Tempstktrn_table where item_no=@tItemNo and strn_type=2
    if @tNetSalAmt is null
  set @tNetSalAmt=0;
  if @tNetSalRetAmt is null
  set @tNetSalRetAmt=0;
  update Item_Table set nt_salqty=nt_salqty+@Qty,nt_cloqty=nt_cloqty-@Qty,Nt_Salval=(@tNetSalAmt-@tNetSalAmt),Nt_SalRetval=@tNetSalAmt where Item_no=@tItemNo;
  fetch next from c into @ItemName,@Qty,@Rate,@Amt, @Disc
end
close c
deallocate c
Update NumberTable set strnno=strnno+1;

Select @tSalesCount=(Max(Vch_BillNo)) from VoucherNo_table where Vch_type='10'
--Select @tVchRefNo=(max(Ref_no)+1) from TempVch_table
if exists (Select * from TempVch_table)
Select @tVchRefNo=(max(Ref_no)+1) from TempVch_table
else
Select @tVchRefNo=(max(Ref_no)+1) from Vch_table

if @tVchRefNo is Null
Set @tVchRefNo=1;
--DECLARE c1 CURSOR LOCAL READ_ONLY FOR select Distinct(Tax_Per) As Tax from stktrn_table where strn_no=@tStrnNo
select @tVoucherNoNew=(max(VoucherNo)+1) from NumberTable

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1',@tBankLedger_No,@tVchRefNo,'1',@tCurrentDate,'10','5',@tLedgerNo1,@tNetAmt,'0','','','0','','0')
Update NumberTable set VoucherSno=VoucherSno+1;

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable;

Set @tCHK=(@tNetAmt-@tDiscount)-(@tGrossAmt+@tTotTax);
if(@tCHK<0)
Insert into TempVch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1',@tBankLedger_No,@tVchRefNo,'1',@tCurrentDate,'10','6','5',-@RoundValue,'0','','','0','','0')
else if(@tCHK>0)
Insert into TempVch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1',@tBankLedger_No,@tVchRefNo,'1',@tCurrentDate,'10','6','5','0',@RoundValue,'','','0','','0')

Update NumberTable set VoucherSno=VoucherSno+1;

DECLARE c1 CURSOR LOCAL READ_ONLY FOR select Tax_Per As Tax,Tax_No from Tempstktrn_table where strn_no=@tStrnNo group by tax_per,Tax_No
open c1
fetch from c1 into @tTax,@tTaxNumber
while @@fetch_status=0
begin
select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
select @tLedgerNo1=NtLedger_No from Tax_table where Nt_Percent=@tTax and Tax_no=@tTaxNumber
select @tSingleTaxAmt=sum(Tax_Amt) from Tempstktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1',@tBankLedger_No,@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1,'5','0',@tSingleTaxAmt,'','','0','','0')
  set @tSingleTaxAmtNew=0;
 if @tTax <>0
 select @tSingleTaxAmtNew=sum(Rate*nt_qty) from Tempstktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
 --set @tSingleTaxAmtNew=((@tSingleTaxAmt*100)/@tTax);
Update NumberTable set VoucherSno=VoucherSno+1;

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
select @tLedsel_name=Ledsel_name from Ledsel_table where Ledger_no=@tLedgerNo1;
IF @tLedsel_name is null
BEGIN
SET @tLedgerNo1New='0'
SET @tLedgerNo1New=0;
select @tSingleTaxAmtNew=sum(Rate*nt_qty) from Tempstktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
END
ELSE
BEGIN
IF CHARINDEX ('%',@tLedsel_name)=0
SET @tSub=@tLedsel_name
ELSE
SELECT @tSub=LEFT(@tLedsel_name, CHARINDEX('%',@tLedsel_name)-1);
Select @tLedgerNo1New=Ledger_no from Ledsel_table where Ledsel_name like @tSub+'%' and Ledger_no<>@tLedgerNo1
END
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1',@tBankLedger_No,@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1New,'5','0',@tSingleTaxAmtNew,'','','0','','0')


Update NumberTable set VoucherSno=VoucherSno+1;
fetch next from c1 into @tTax,@tTaxNumber
end
close c1
deallocate c1


--Free Item Code Start   
open c3
Declare @tFreeItemNo numeric(18,0);
Declare @tFreeMainItemNo numeric(18,0);
Declare @tFreeOfferNo numeric(18,0);
fetch from c3 into @tItemNameFree,@tQtyFree,@tScannedQtyFree,@tMainItemNameFree,@tOfferNameFree,@tOfferFreeQtyFree,@tTotSaleQtyFree
while @@fetch_status=0
begin
Select @tFreeItemNo=Item_no from Item_table where Item_name=@tItemNameFree
Select @tFreeMainItemNo=Item_no from Item_table where Item_name=@tMainItemNameFree
select @tFreeOfferNo=FreeSno from FreeItemMaster_table where OfferName=@tOfferNameFree
INSERT INTO [dbo].[SalFreeItemDetail_table]([smas_no] ,[smas_billno],[FreeItem_no],[TotFreeQty],[TotScannedQty],[MainItem_no],[OfferNo],[OfferFreeQty],[TotSaleQty],[smas_BillDate], [Ctr_no])
VALUES(@tStrnNo,@tBillNo,@tFreeItemNo,@tQtyFree,@tScannedQtyFree,@tFreeMainItemNo,@tFreeOfferNo,@tOfferFreeQtyFree,@tTotSaleQtyFree,@tCurrentDate,@tCounter)
fetch Next from c3 into @tItemNameFree,@tQtyFree,@tScannedQtyFree,@tMainItemNameFree,@tOfferNameFree,@tOfferFreeQtyFree,@tTotSaleQtyFree
End
close c3
deallocate c3
--Free Item Code End   

--open c1
--fetch from c1 into @tTax
--while @@fetch_status=0
--begin
--select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
--select @tLedgerNo1=NtLedger_No from Tax_table where Nt_Percent=@tTax
--select @tSingleTaxAmt=sum(Tax_Amt) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
--Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
-- (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1,'5','0',@tSingleTaxAmt,'','','0','','0')
-- set @tSingleTaxAmtNew=0;
-- if @tTax <>0
-- set @tSingleTaxAmtNew=((@tSingleTaxAmt*100)/@tTax);
--Update NumberTable set VoucherSno=VoucherSno+1;

--select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
--select @tLedsel_name=Ledsel_name from Ledsel_table where Ledger_no=@tLedgerNo1;
--SELECT @tSub=LEFT(@tLedsel_name, CHARINDEX('%',@tLedsel_name)-1);
--Select @tLedgerNo1New=Ledger_no from Ledsel_table where Ledsel_name like @tSub+'%' and Ledger_no<>@tLedgerNo1
--Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
-- (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1New,'5','0',@tSingleTaxAmtNew,'','','0','','0')


--Update NumberTable set VoucherSno=VoucherSno+1;
--fetch next from c1 into @tTax
--end
--close c1
--deallocate c1 
   
   
   DECLARE @tDiscountId numeric(18,0);
   select @tDiscountId=(MAX(DiscountEntry_Id)+1) from NumberTable;
  INSERT INTO TempDiscountDetail_table([Discount_Id],[Type],[Date],[Bill_no],[Amount],[GrossAmount]) VALUES
           (@tDiscountId,@DiscountType,@tCurrentDate,@tBillNo,@tDiscount,@tGrossAmt);

   Update VoucherNo_table set Vch_BillNo=Vch_BillNo+1 where Vch_Type='10';
   Update Numbertable set VoucherNo=VoucherNo+3,DiscountEntry_Id=DiscountEntry_Id+1;
   
   IF @TranCounter = 0           
      COMMIT TRANSACTION;
   END TRY
   
   BEGIN CATCH
   IF @TranCounter = 0         
      ROLLBACK TRANSACTION;
   ELSE
      IF XACT_STATE() <> -1        
         ROLLBACK TRANSACTION ProcedureSave;        
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;

      SELECT @ErrorMessage = ERROR_MESSAGE();
      SELECT @ErrorSeverity = ERROR_SEVERITY();
      SELECT @ErrorState = ERROR_STATE();

      RAISERROR (@ErrorMessage, -- Message text.
                 @ErrorSeverity, -- Severity.
                 @ErrorState -- State.
                 );
    END CATCH;



	
GO

CREATE PROCEDURE [dbo].[sp_SettleCreditCardProcess2](@tValue numeric(18,2),@tCreditCardName varchar(400))
AS
DECLARE @tStrnNo Numeric(18,0)=0;
DECLARE @tSalRecv_Sno numeric(18,2);
DECLARE @tBankLedger_No Numeric(18,2);
BEGIN
DECLARE @tBillNo NUMERIC(18,2);
  -- Select @tBankLedger_No=BankLedger_No from CreditCard_Table where Card_Name=@tCreditCardName
  Select @tBankLedger_No=Ledger_no from Ledger_table where Ledger_name=@tCreditCardName
  
   --Select @tBillNo=(count(*)+1) from SalMas_table where smas_rtno=0;
    if exists (Select * from Tempsalmas_table where smas_rtno=0)
 Select @tBillNo=max(smas_billno)+1 from Tempsalmas_table where smas_rtno=0;
 else
 Select @tBillNo=max(smas_billno)+1 from SalMas_table where smas_rtno=0;
  SELECT @tSalRecv_Sno=(max(SalRecv_Sno)+1),@tStrnNo=(MAX(StrnNo)+1) from NumberTable;
   --INSERT INTO SalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tStrnNo,'14',@tValue,'0','0')   
   INSERT INTO TempSalRecv_table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tBillNo,@tBankLedger_No,@tValue,'0','0')   
   Update NumberTable set SalRecv_Sno=SalRecv_Sno+1
END;



GO


CREATE PROCEDURE [dbo].[sp_SettleDiscount](@tDiscount NUMERIC(18,2),@tGrossAmt numeric(18,2))
  AS
  BEGIN  
  DECLARE @tDiscountId numeric(18,0);
  DECLARE @tCurrentDate Datetime;
  DECLARE @tBillNo NUMERIC(18,0);
  select @tDiscountId=(MAX(DiscountEntry_Id)+1) from NumberTable;
   Select @tBillNo=(count(*)+1) from SalMas_table where smas_rtno=0;
  select @tCurrentDate=DATEADD(day,1,EndOfDay) from EndOFday_Table where Id=(select EndOfDayId from NumberTable);
  INSERT INTO [dbo].[DiscountDetail_table]([Discount_Id],[Type],[Date],[Bill_no],[Amount],[GrossAmount]) VALUES
           (@tDiscountId,'Amount',@tCurrentDate,@tBillNo,@tDiscount,@tGrossAmt);
  Update Numbertable set DiscountEntry_Id=DiscountEntry_Id+1;      
  END;


  
GO


CREATE PROCEDURE [dbo].[sp_SettleHouseACProcess1]
(@tGrossAmt numeric(18,2),@tNetAmt numeric(18,2),@tDiscount numeric(18,2),@tTotTax numeric(18,2),@RoundValue numeric(18,2),@tempTable Type_gridValue Readonly,@DiscountType varchar(100),@tTxtAmount numeric(18,2),@tUserno numeric(18,0), @tCounter numeric(18,0),@tCreditCardName varchar(400), @tempFreeItem dtSingleFreeSales Readonly,@dt_gridload1 sp_funBtnDolorAlterTable  READONLY)
as
DECLARE @TranCounter INT; 
DECLARE @tStrnNo Numeric(18,0)=0;
DECLARE @tBillNo Numeric(18,0)=0;
DECLARE @tCurrentDate datetime;
DECLARE @tCurrentTime datetime;
DECLARE @tVoucherNo Numeric(18,0)=0;
DECLARE @ItemName varchar(200),@Qty numeric(18,4),@Rate numeric(18,2),@Amt numeric(18,2),@Disc Numeric(18,2);
DECLARE @tStrnSno Numeric(18,0)=0;
DECLARE @tItemNo Numeric(18,0)=0;
DECLARE @tTaxNo Numeric(18,0)=0;
DECLARE @tUnitNo Numeric(18,0)=0;
DECLARE @tTaxPercent Numeric(18,2)=0;
DECLARE @Profit Numeric(18,2)=0; 
DECLARE @tTaxAmt Numeric(18,2)=0;
DECLARE @tNetSalAmt Numeric(18,2)=0;
DECLARE @tSalesCount Numeric(18,0)=0;
DECLARE @tVchRefNo Numeric(18,0)=0;
DECLARE @tTax Numeric(18,2)=0;
DECLARE @tVoucherNoNew Numeric(18,0)=0;
DECLARE @tVoucherSno Numeric(18,0)=0;
DECLARE @tLedgerNo1 Numeric(18,0)=0;
DECLARE @tSingleTaxAmt Numeric(18,2)=0;
DECLARE @tItemCost Numeric(18,2)=0;
DECLARE @tTotItemCost Numeric(18,2)=0;
DECLARE @tTotItemRate Numeric(18,2)=0;
DECLARE @tLedsel_name varchar(100);
DECLARE @tSub varchar(100);
DECLARE @tLedgerNo1New Numeric(18,2);
DECLARE @tSingleTaxAmtNew Numeric(18,2)=0;
Declare @tCHK numeric(18,2)=0;
DECLARE @tSalRecv_Sno numeric(18,2);
DECLARE @tTaxNumber NUMERIC(18,0);
DECLARE @tNetSalRetAmt NUMERIC(18,2);
DECLARE @tOpenItem varchar(50);
DECLARE @tOpenItemCount Numeric(18,0);
DECLARE c CURSOR LOCAL READ_ONLY FOR SELECT ItemName,Qty,Rate,Amt, Disc FROM @tempTable
DECLARE c2 CURSOR LOCAL READ_ONLY FOR SELECT ItemName,Qty,Rate,Amt, Disc FROM @tempTable
DECLARE @tBankLedger_No Numeric(18,2);

	Declare @tItemNameFree varchar(400);
	Declare @tQtyFree numeric(18, 4);
	Declare @tScannedQtyFree varchar(100);
	Declare @tMainItemNameFree varchar(400);
	Declare @tOfferNameFree varchar(400);
	Declare @tOfferFreeQtyFree numeric(18, 2);
	Declare @tTotSaleQtyFree numeric(18, 2);
	DECLARE c3 CURSOR LOCAL READ_ONLY FOR SELECT ItemName,Qty,ScannedQty,MainItemName,OfferName,OfferFreeQty,TotSaleQty FROM @tempFreeItem
DECLARE c7 CURSOR LOCAL READ_ONLY FOR SELECT Type1,RemaininbillAmt,ReceiverAmt,Types2 from @dt_gridload1 
declare @Type1 varchar(200),@RecRemBillAmt numeric(18,2),@RecAmt numeric(18,2),@Types2 as varchar(200);


SET @TranCounter = @@TRANCOUNT;
IF @TranCounter > 0
SAVE TRANSACTION ProcedureSave;
ELSE
   BEGIN TRANSACTION;
   BEGIN TRY
   Select @tBankLedger_No=ledger_no from Ledger_Table where Ledger_name=@tCreditCardName
open c2
fetch from c2 into @ItemName,@Qty,@Rate,@Amt, @Disc
while @@fetch_status=0
begin
    Select @tItemNo=Item_no,@tItemCost=Item_cost from Item_table where Item_name=@ItemName;
    Set @tTotItemCost=@tTotItemCost+(@tItemCost*@Qty);
    Set @tTotItemRate=@tTotItemRate+(@Amt);
    fetch next from c2 into @ItemName,@Qty,@Rate,@Amt, @Disc
end
close c2
deallocate c2
 -- Select @tBillNo=(count(*)+1) from SalMas_table where smas_rtno=0;
    if exists (Select * from Tempsalmas_table where smas_rtno=0)
 Select @tBillNo=max(smas_billno)+1 from Tempsalmas_table where smas_rtno=0;
 else
 Select @tBillNo=max(smas_billno)+1 from SalMas_table where smas_rtno=0;
  Set @Profit=0;
   set @Profit=@tTotItemRate-@tTotItemCost;
   select @tSalRecv_Sno=(max(SalRecv_Sno)+1),@tStrnNo=(MAX(StrnNo)+1),@tVoucherNo=(max(VoucherNo)+1) from NumberTable;
   
 
  if @tBillNo is Null
 Set @tBillNo=1
   --INSERT INTO SalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tStrnNo,'14',@tTxtAmount,'0','0')
  -- INSERT INTO tempSalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tBillNo,@tBankLedger_No,@tTxtAmount,'0','0')
   open c7
  fetch from c7 into @Type1,@RecRemBillAmt,@RecAmt,@Types2
  while @@FETCH_STATUS=0
  Begin
   --INSERT INTO SalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tStrnNo,''''5'''',@tTxtAmount,(@tValue-@tTxtAmount),''''0'''')
   INSERT INTO TempSalRecv_table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tBillNo,@Type1,@RecRemBillAmt,@RecAmt,'0')
   set @tSalRecv_Sno=@tSalRecv_Sno+1
   fetch Next from c7 into @Type1,@RecRemBillAmt,@RecAmt,@Types2
  End
  close c7
  deallocate c7
  Update NumberTable set SalRecv_Sno=@tSalRecv_Sno;
 
   
   
   --if(@tBillNo=0) 
   --set @tBillNo=1;
   select @tCurrentDate=CONVERT(DATE,DATEADD(day,1,EndOfDay),103) from EndOFday_Table where Id=(select EndOfDayId from NumberTable)
   select @tCurrentTime=convert(time,GETDATE(),100); 
      if @tCounter is null
  set @tCounter=1
   if @tUserno is null
  set @tUserno=1
   Insert into Tempsalmas_table (smas_no,smas_slno,smas_rtno,smas_billprefix,smas_billsuffix,smas_billno,Smas_Bill,smas_billdate,smas_billtime,smas_days,Mechanic_no,MechCommi_Amt,MechCommi_Per,SmanCommi_Amt,SManCommi_Per,MechCommiTax_Per,Smas_SmanNo,dc_no,dc_date,order_no,reference_date,reference_no,Vehicle_no,Smas_Others1,Smas_others2,smas_others3,smas_others4,UserNo,ctr_no,Godown_no,party_no,Customer_no,smas_name,smas_add1,smas_add2,smas_add3,smas_add4,smas_add5,smas_st,smas_cst,smas_cashmode,Smas_salmode,smas_saltype,smas_ordertype,smas_addled1,smas_addled2,smas_addled3,smas_addled4,smas_adddet1,smas_adddet2,smas_adddet3,smas_adddet4,smas_adddisc1,smas_adddisc2,smas_adddisc3,smas_adddisc4,smas_adddiscr1,smas_adddiscr2,smas_adddiscr3,smas_adddiscr4,smas_addamt1,smas_addamt2,smas_addamt3,smas_addamt4,smas_Gross,smas_GrossAmount,BankCharge,smas_NetAmount,smas_rcvdamount,smas_remarks,smas_Cancel,smas_cremark,smas_rounded,CashReceived,Profit,Update_Flag,Tax_Refund,Print_no,smas_point,smas_issue,smas_paidAmt,smas_Touch,smas_TotalCash,smas_TotalCredit,smas_TotalNets,Loaditem,Smas_columns,VoucherSno,SalesVchNo,ReceiptVchNo,CrNoteVchNo,CardSno,CardNo) values 
   (@tStrnNo,'0','0','','',@tBillNo,@tBillNo,@tCurrentDate,@tCurrentTime,'0','1','0','0','0','0','0','0','0','1999-01-01','','','','','','','','',@tUserno,@tCounter,'2',@tBankLedger_No,'0',@tCreditCardName,'','','','','','','','0','0','0','0','0','0','0','0','','','','','0','0','0','0','0','0','0','0','0','0','0','0',@tGrossAmt,@tGrossAmt,'0',@tNetAmt,@tNetAmt,'','0','',@RoundValue,'0',@Profit,'0','0','0','0','1',@tNetAmt,'True','0','0',@tNetAmt,'0','',@tVoucherNo,@tBillNo,'0','0','0','')
   
   
   
 Select @tStrnNo=(max(strnno)+1) from Numbertable
Declare @tFreeSno Numeric(18,2)=0;
Declare @tCtl_FreeQty  bit=0;
Declare @tRprop varchar(100)='NoTax';
open c
fetch from c into @ItemName,@Qty,@Rate,@Amt, @Disc
while @@fetch_status=0
begin
  Select @tStrnSno=(max(strnsno)+1) from Numbertable
  Select @tItemNo=Item_no,@tTaxNo=Tax_no,@tUnitNo=Unit_no from Item_table where Item_name=@ItemName;
  SELECT @tTaxPercent=Nt_Percent,@tLedgerNo1=NtLedger_No from Tax_Table where Tax_No=@tTaxNo;
  Set @tTaxAmt=((@Qty*@Rate)*(@tTaxPercent/100));
  
  Select @tRprop=Rprop from Rptset where Rdesc='Display Tax Type'
  if @tRprop='NoTax'
  begin
  Set @tTaxAmt=0
  set @tTaxNo=1
  set @tTaxPercent=0
  end
  
   --Free Item Code Start
  Select @tCtl_FreeQty= Ctl_FreeQty from Control_table
  if @tCtl_FreeQty=1
  begin 
  if Exists (Select * from tempView where FreeType='Item Price' and Item_Name=@ItemName)
  Begin
   set @tFreeSno=(Select Top 1 (FreeSno) from tempView where FreeType='Item Price' and Item_Name=@ItemName)
  End
  Else  
  Begin
   set @tFreeSno=(Select Top 1 (FreeSno) from tempView where FreeType<>'Item Price' and Item_Name=@ItemName)
  End
  if  @tFreeSno is null
  set @tFreeSno=0
  End
  -- Free Item Code End
  
  Select @tItemNo=Item_no,@tItemCost=Item_cost,@tOpenItem=OpenItem from Item_table where Item_name=@ItemName;
    Set @tTotItemCost=(@tItemCost*@Qty);
    Set @tTotItemRate=(@Rate*@Qty);  
    if @tOpenItem='True'
  select @tOpenItemCount=COUNT(*) from Tempstktrn_table where item_no=(select item_no from Item_table where Item_name=@ItemName) and strn_no=@tStrnNo  
  ELSE
  set @tOpenItemCount=0    
    
  INSERT INTO Tempstktrn_table (strn_sno,strn_no,strn_rtno,strn_type,strn_date,Godown_BillNo,StrnParty_no,Grn_no,OrderSno,Dc_no,item_no,ctr_no,godown_no,Unit_no,Unit_Ratio,QtyInPieces,nt_qty,tx_qty,Short_qty,rnt_qty,rtx_qty,Invnt_qty,Invtx_qty,QtyDetails,Rate,Tax_Rate,CurrencyNo,CurrencyValue,Amount,Tax_No,Disc_PerQty,Disc_Per,Disc_Amt,Adldisc_Per,Adldisc_Amt,Othdisc_Amt,OthPurdisc,Ed_PerQty,Ed_Per,Ed_Amt,Cess_Per,Cess_Amt,SHECess_Per,SHECess_Amt,HL_Per,HL_Amt,CST_per,CST_amt,tax_Flag,tax_per,tax_amt,Sur_per,Sur_amt,CommiPer,Commi,SmanPer,SmanAmt,spl_discamt,tot_amt,alp1,alp2,alp3,alp4,ala1,ala2,ala3,ala4,Net_Amt,Other_Exp,BillOther_Exp,strn_remarks,Strn_Cancel,Order_Ack,Cost,Mrsp,Margin,Margin_No,Srate,Frtx_Qty,RFrnt_Qty,RFrtx_Qty,Frnt_Qty,FreeQty,FreeItemNo,Profit,Item_Point,Mech_no,PurRate,OpenItem,OpenItemCount) values
   (@tStrnSno,@tStrnNo,'0','1', @tCurrentDate,'0',@tBankLedger_No,'0','0','0',@tItemNo,@tCounter,'2',@tUnitNo,'1','0',@Qty,'0','0','0','0','0','0','',@Rate,'0','0','0',@Amt,@tTaxNo,'0','0',@Disc,'0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0',@tTaxPercent,@tTaxAmt,'0','0','0','0','0','0','0',(Case when @tRprop='Inclusive' then @Amt Else (@Amt+@tTaxAmt) END),'0','0','0','0','0','0','0','0',(Case when @tRprop='Inclusive' then @Amt Else (@Amt+@tTaxAmt) END),'0','0',@tRprop,'0','0','0','0','0','0','0','0','0','0','0','0',@tFreeSno,(@tTotItemRate-@tTotItemCost),'0','0','0',@tOpenItem,@tOpenItemCount);
  update Numbertable set strnsno=strnsno+1;
   select @tNetSalAmt=sum(net_amt) from Tempstktrn_table where item_no=@tItemNo and strn_type=1
  select @tNetSalRetAmt=sum(net_amt) from Tempstktrn_table where item_no=@tItemNo and strn_type=2
    if @tNetSalAmt is null
  set @tNetSalAmt=0;
  if @tNetSalRetAmt is null
  set @tNetSalRetAmt=0;
  update Item_Table set nt_salqty=nt_salqty+@Qty,nt_cloqty=nt_cloqty-@Qty,Nt_Salval=(@tNetSalAmt-@tNetSalAmt),Nt_SalRetval=@tNetSalAmt where Item_no=@tItemNo;
  fetch next from c into @ItemName,@Qty,@Rate,@Amt, @Disc
end
close c
deallocate c
Update NumberTable set strnno=strnno+1;

Select @tSalesCount=(Max(Vch_BillNo)) from VoucherNo_table where Vch_type='10'
--Select @tVchRefNo=(max(Ref_no)+1) from TempVch_table
if exists (Select * from TempVch_table)
Select @tVchRefNo=(max(Ref_no)+1) from TempVch_table
else
Select @tVchRefNo=(max(Ref_no)+1) from Vch_table
if @tVchRefNo is Null
Set @tVchRefNo=1;
--DECLARE c1 CURSOR LOCAL READ_ONLY FOR select Distinct(Tax_Per) As Tax from stktrn_table where strn_no=@tStrnNo
select @tVoucherNoNew=(max(VoucherNo)+1) from NumberTable

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1',@tBankLedger_No,@tVchRefNo,'1',@tCurrentDate,'10','5',@tLedgerNo1,@tNetAmt,'0','','','0','','0')
Update NumberTable set VoucherSno=VoucherSno+1;

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable;

Set @tCHK=(@tNetAmt-@tDiscount)-(@tGrossAmt+@tTotTax);
if(@tCHK<0)
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1',@tBankLedger_No,@tVchRefNo,'1',@tCurrentDate,'10','6','5',-@RoundValue,'0','','','0','','0')
else if(@tCHK>0)
Insert into TempVch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1',@tBankLedger_No,@tVchRefNo,'1',@tCurrentDate,'10','6','5','0',@RoundValue,'','','0','','0')

Update NumberTable set VoucherSno=VoucherSno+1;

DECLARE c1 CURSOR LOCAL READ_ONLY FOR select Tax_Per As Tax,Tax_No from Tempstktrn_table where strn_no=@tStrnNo group by tax_per,Tax_No
open c1
fetch from c1 into @tTax,@tTaxNumber
while @@fetch_status=0
begin
select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
select @tLedgerNo1=NtLedger_No from Tax_table where Nt_Percent=@tTax and Tax_no=@tTaxNumber
select @tSingleTaxAmt=sum(Tax_Amt) from Tempstktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1',@tBankLedger_No,@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1,'5','0',@tSingleTaxAmt,'','','0','','0')
  set @tSingleTaxAmtNew=0;
 if @tTax <>0
 select @tSingleTaxAmtNew=sum(Rate*nt_qty) from Tempstktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
 --set @tSingleTaxAmtNew=((@tSingleTaxAmt*100)/@tTax);
Update NumberTable set VoucherSno=VoucherSno+1;

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
select @tLedsel_name=Ledsel_name from Ledsel_table where Ledger_no=@tLedgerNo1;
IF @tLedsel_name is null
BEGIN
SET @tLedgerNo1New='0'
SET @tLedgerNo1New=0;
select @tSingleTaxAmtNew=sum(Rate*nt_qty) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
END
ELSE
BEGIN
IF CHARINDEX ('%',@tLedsel_name)=0
SET @tSub=@tLedsel_name
ELSE
SELECT @tSub=LEFT(@tLedsel_name, CHARINDEX('%',@tLedsel_name)-1);
Select @tLedgerNo1New=Ledger_no from Ledsel_table where Ledsel_name like @tSub+'%' and Ledger_no<>@tLedgerNo1
END
Insert into TempVch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1',@tBankLedger_No,@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1New,'5','0',@tSingleTaxAmtNew,'','','0','','0')


Update NumberTable set VoucherSno=VoucherSno+1;
fetch next from c1 into @tTax,@tTaxNumber
end
close c1
deallocate c1


--Free Item Code Start   
open c3
Declare @tFreeItemNo numeric(18,0);
Declare @tFreeMainItemNo numeric(18,0);
Declare @tFreeOfferNo numeric(18,0);
fetch from c3 into @tItemNameFree,@tQtyFree,@tScannedQtyFree,@tMainItemNameFree,@tOfferNameFree,@tOfferFreeQtyFree,@tTotSaleQtyFree
while @@fetch_status=0
begin
Select @tFreeItemNo=Item_no from Item_table where Item_name=@tItemNameFree
Select @tFreeMainItemNo=Item_no from Item_table where Item_name=@tMainItemNameFree
select @tFreeOfferNo=FreeSno from FreeItemMaster_table where OfferName=@tOfferNameFree
INSERT INTO [dbo].[SalFreeItemDetail_table]([smas_no] ,[smas_billno],[FreeItem_no],[TotFreeQty],[TotScannedQty],[MainItem_no],[OfferNo],[OfferFreeQty],[TotSaleQty],[smas_BillDate],[Ctr_no])
VALUES(@tStrnNo,@tBillNo,@tFreeItemNo,@tQtyFree,@tScannedQtyFree,@tFreeMainItemNo,@tFreeOfferNo,@tOfferFreeQtyFree,@tTotSaleQtyFree,@tCurrentDate,@tCounter)
fetch Next from c3 into @tItemNameFree,@tQtyFree,@tScannedQtyFree,@tMainItemNameFree,@tOfferNameFree,@tOfferFreeQtyFree,@tTotSaleQtyFree
End
close c3
deallocate c3
--Free Item Code End   

--open c1
--fetch from c1 into @tTax
--while @@fetch_status=0
--begin
--select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
--select @tLedgerNo1=NtLedger_No from Tax_table where Nt_Percent=@tTax
--select @tSingleTaxAmt=sum(Tax_Amt) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
--Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
-- (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1,'5','0',@tSingleTaxAmt,'','','0','','0')
-- set @tSingleTaxAmtNew=0;
-- if @tTax <>0
-- set @tSingleTaxAmtNew=((@tSingleTaxAmt*100)/@tTax);
--Update NumberTable set VoucherSno=VoucherSno+1;

--select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
--select @tLedsel_name=Ledsel_name from Ledsel_table where Ledger_no=@tLedgerNo1;
--SELECT @tSub=LEFT(@tLedsel_name, CHARINDEX('%',@tLedsel_name)-1);
--Select @tLedgerNo1New=Ledger_no from Ledsel_table where Ledsel_name like @tSub+'%' and Ledger_no<>@tLedgerNo1
--Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
-- (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1New,'5','0',@tSingleTaxAmtNew,'','','0','','0')


--Update NumberTable set VoucherSno=VoucherSno+1;
--fetch next from c1 into @tTax
--end
--close c1
--deallocate c1 
   
   
   DECLARE @tDiscountId numeric(18,0);
   select @tDiscountId=(MAX(DiscountEntry_Id)+1) from NumberTable;
  INSERT INTO TempDiscountDetail_table([Discount_Id],[Type],[Date],[Bill_no],[Amount],[GrossAmount]) VALUES
           (@tDiscountId,@DiscountType,@tCurrentDate,@tBillNo,@tDiscount,@tGrossAmt);

   Update VoucherNo_table set Vch_BillNo=Vch_BillNo+1 where Vch_Type='10';
   Update Numbertable set VoucherNo=VoucherNo+3,DiscountEntry_Id=DiscountEntry_Id+1;
   
   IF @TranCounter = 0           
      COMMIT TRANSACTION;
   END TRY
   
   BEGIN CATCH
   IF @TranCounter = 0         
      ROLLBACK TRANSACTION;
   ELSE
      IF XACT_STATE() <> -1        
         ROLLBACK TRANSACTION ProcedureSave;        
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;

      SELECT @ErrorMessage = ERROR_MESSAGE();
      SELECT @ErrorSeverity = ERROR_SEVERITY();
      SELECT @ErrorState = ERROR_STATE();

      RAISERROR (@ErrorMessage, -- Message text.
                 @ErrorSeverity, -- Severity.
                 @ErrorState -- State.
                 );
    END CATCH;

GO


CREATE PROCEDURE [dbo].[sp_SettleHouseACProcess2](@tValue numeric(18,2),@tCreditCardName varchar(400))
AS
DECLARE @tStrnNo Numeric(18,0)=0;
DECLARE @tSalRecv_Sno numeric(18,2);
DECLARE @tBankLedger_No Numeric(18,2);
BEGIN
DECLARE @tBillNo NUMERIC(18,2);
   Select @tBankLedger_No=Ledger_No from Ledger_table where Ledger_name=@tCreditCardName
       if exists (Select * from Tempsalmas_table where smas_rtno=0)
 Select @tBillNo=max(smas_billno)+1 from Tempsalmas_table where smas_rtno=0;
 else
 Select @tBillNo=max(smas_billno)+1 from SalMas_table where smas_rtno=0;
   SELECT @tSalRecv_Sno=(max(SalRecv_Sno)+1),@tStrnNo=(MAX(StrnNo)+1) from NumberTable;
  -- Select @tBillNo=(count(*)+1) from SalMas_table where smas_rtno=0;

   --INSERT INTO SalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tStrnNo,'14',@tValue,'0','0')   
   INSERT INTO TempSalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tBillNo,@tBankLedger_No,@tValue,'0','0')   
   Update NumberTable set SalRecv_Sno=SalRecv_Sno+1
END;



GO


CREATE PROCEDURE [dbo].[sp_SettleNETSProcess1]
(@tGrossAmt numeric(18,2),@tNetAmt numeric(18,2),@tDiscount numeric(18,2),@tTotTax numeric(18,2),@RoundValue numeric(18,2),@tempTable Type_gridValue Readonly,@DiscountType varchar(100),@tTxtAmount numeric(18,2),@tUserno numeric(18,0), @tCounter numeric(18,0),@tempFreeItem dtSingleFreeSales Readonly,@dt_gridload1 sp_funBtnDolorAlterTable  READONLY)
as
DECLARE @TranCounter INT; 
DECLARE @tStrnNo Numeric(18,0)=0;
DECLARE @tBillNo Numeric(18,0)=0;
DECLARE @tCurrentDate datetime;
DECLARE @tCurrentTime datetime;
DECLARE @tVoucherNo Numeric(18,0)=0;
DECLARE @ItemName varchar(200),@Qty numeric(18,4),@Rate numeric(18,2),@Amt numeric(18,2),@Disc Numeric(18,2);
DECLARE @tStrnSno Numeric(18,0)=0;
DECLARE @tItemNo Numeric(18,0)=0;
DECLARE @tTaxNo Numeric(18,0)=0;
DECLARE @tUnitNo Numeric(18,0)=0;
DECLARE @tTaxPercent Numeric(18,2)=0;
DECLARE @Profit Numeric(18,2)=0; 
DECLARE @tTaxAmt Numeric(18,2)=0;
DECLARE @tNetSalAmt Numeric(18,2)=0;
DECLARE @tSalesCount Numeric(18,0)=0;
DECLARE @tVchRefNo Numeric(18,0)=0;
DECLARE @tTax Numeric(18,2)=0;
DECLARE @tVoucherNoNew Numeric(18,0)=0;
DECLARE @tVoucherSno Numeric(18,0)=0;
DECLARE @tLedgerNo1 Numeric(18,0)=0;
DECLARE @tSingleTaxAmt Numeric(18,2)=0;
DECLARE @tItemCost Numeric(18,2)=0;
DECLARE @tTotItemCost Numeric(18,2)=0;
DECLARE @tTotItemRate Numeric(18,2)=0;
DECLARE @tLedsel_name varchar(100);
DECLARE @tSub varchar(100);
DECLARE @tLedgerNo1New Numeric(18,2);
DECLARE @tSingleTaxAmtNew Numeric(18,2)=0;
Declare @tCHK numeric(18,2)=0;
DECLARE @tSalRecv_Sno numeric(18,2);
DECLARE @tTaxNumber NUMERIC(18,0);
DECLARE @tNetSalRetAmt NUMERIC(18,2);
DECLARE @tOpenItem varchar(50);
DECLARE @tOpenItemCount Numeric(18,0);
DECLARE c CURSOR LOCAL READ_ONLY FOR SELECT ItemName,Qty,Rate,Amt,Disc FROM @tempTable
DECLARE c2 CURSOR LOCAL READ_ONLY FOR SELECT ItemName,Qty,Rate,Amt,Disc FROM @tempTable

	Declare @tItemNameFree varchar(400);
	Declare @tQtyFree numeric(18, 4);
	Declare @tScannedQtyFree varchar(100);
	Declare @tMainItemNameFree varchar(400);
	Declare @tOfferNameFree varchar(400);
	Declare @tOfferFreeQtyFree numeric(18, 2);
	Declare @tTotSaleQtyFree numeric(18, 2);
	DECLARE c3 CURSOR LOCAL READ_ONLY FOR SELECT ItemName,Qty,ScannedQty,MainItemName,OfferName,OfferFreeQty,TotSaleQty FROM @tempFreeItem
	DECLARE c7 CURSOR LOCAL READ_ONLY FOR SELECT Type1,RemaininbillAmt,ReceiverAmt,Types2 from @dt_gridload1 
declare @Type1 varchar(200),@RecRemBillAmt numeric(18,2),@RecAmt numeric(18,2),@Types2 as varchar(200);



SET @TranCounter = @@TRANCOUNT;
IF @TranCounter > 0
SAVE TRANSACTION ProcedureSave;
ELSE
   BEGIN TRANSACTION;
   BEGIN TRY
   
open c2
fetch from c2 into @ItemName,@Qty,@Rate,@Amt, @Disc
while @@fetch_status=0
begin
    Select @tItemNo=Item_no,@tItemCost=Item_cost from Item_table where Item_name=@ItemName;
    Set @tTotItemCost=@tTotItemCost+(@tItemCost*@Qty);
    Set @tTotItemRate=@tTotItemRate+(@Amt);
    fetch next from c2 into @ItemName,@Qty,@Rate,@Amt,@Disc
end
close c2
deallocate c2
-- Select @tBillNo=(count(*)+1) from SalMas_table where smas_rtno=0;
  
   if exists (Select * from Tempsalmas_table where smas_rtno=0)
 Select @tBillNo=max(smas_billno)+1 from Tempsalmas_table where smas_rtno=0;
 else
 Select @tBillNo=max(smas_billno)+1 from SalMas_table where smas_rtno=0;
  Set @Profit=0;
   set @Profit=@tTotItemRate-@tTotItemCost;
   select @tSalRecv_Sno=(max(SalRecv_Sno)+1),@tStrnNo=(MAX(StrnNo)+1),@tVoucherNo=(max(VoucherNo)+1) from NumberTable;
   
  
  if @tBillNo is Null
 Set @tBillNo=1
   --INSERT INTO SalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tStrnNo,'14',@tTxtAmount,'0','0')
   --INSERT INTO TempSalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tBillNo,'14',@tTxtAmount,'0','0')
    open c7
  fetch from c7 into @Type1,@RecRemBillAmt,@RecAmt,@Types2
  while @@FETCH_STATUS=0
  Begin
   --INSERT INTO SalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tStrnNo,''''5'''',@tTxtAmount,(@tValue-@tTxtAmount),''''0'''')
   INSERT INTO TempSalRecv_table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tBillNo,@Type1,@RecRemBillAmt,@RecAmt,'0')
   set @tSalRecv_Sno=@tSalRecv_Sno+1
   fetch Next from c7 into @Type1,@RecRemBillAmt,@RecAmt,@Types2
  End
  close c7
  deallocate c7
  Update NumberTable set SalRecv_Sno=@tSalRecv_Sno;
 
   
   --if(@tBillNo=0) 
   --set @tBillNo=1;
   select @tCurrentDate=CONVERT(DATE,DATEADD(day,1,EndOfDay),103) from EndOFday_Table where Id=(select EndOfDayId from NumberTable)
   select @tCurrentTime=convert(time,GETDATE(),100); 
      if @tCounter is null
  set @tCounter=1
   if @tUserno is null
  set @tUserno=1
   Insert into Tempsalmas_table (smas_no,smas_slno,smas_rtno,smas_billprefix,smas_billsuffix,smas_billno,Smas_Bill,smas_billdate,smas_billtime,smas_days,Mechanic_no,MechCommi_Amt,MechCommi_Per,SmanCommi_Amt,SManCommi_Per,MechCommiTax_Per,Smas_SmanNo,dc_no,dc_date,order_no,reference_date,reference_no,Vehicle_no,Smas_Others1,Smas_others2,smas_others3,smas_others4,UserNo,ctr_no,Godown_no,party_no,Customer_no,smas_name,smas_add1,smas_add2,smas_add3,smas_add4,smas_add5,smas_st,smas_cst,smas_cashmode,Smas_salmode,smas_saltype,smas_ordertype,smas_addled1,smas_addled2,smas_addled3,smas_addled4,smas_adddet1,smas_adddet2,smas_adddet3,smas_adddet4,smas_adddisc1,smas_adddisc2,smas_adddisc3,smas_adddisc4,smas_adddiscr1,smas_adddiscr2,smas_adddiscr3,smas_adddiscr4,smas_addamt1,smas_addamt2,smas_addamt3,smas_addamt4,smas_Gross,smas_GrossAmount,BankCharge,smas_NetAmount,smas_rcvdamount,smas_remarks,smas_Cancel,smas_cremark,smas_rounded,CashReceived,Profit,Update_Flag,Tax_Refund,Print_no,smas_point,smas_issue,smas_paidAmt,smas_Touch,smas_TotalCash,smas_TotalCredit,smas_TotalNets,Loaditem,Smas_columns,VoucherSno,SalesVchNo,ReceiptVchNo,CrNoteVchNo,CardSno,CardNo) values 
   (@tStrnNo,'0','0','','',@tBillNo,@tBillNo,@tCurrentDate,@tCurrentTime,'0','1','0','0','0','0','0','0','0','1999-01-01','','','','','','','','',@tUserno,@tCounter,'2','14','0','NETS','','','','','','','','0','0','0','0','0','0','0','0','','','','','0','0','0','0','0','0','0','0','0','0','0','0',@tGrossAmt,@tGrossAmt,'0',@tNetAmt,@tNetAmt,'','0','',@RoundValue,'0',@Profit,'0','0','0','0','1',@tNetAmt,'True','0','0',@tNetAmt,'0','',@tVoucherNo,@tBillNo,'0','0','0','')
   
   
   
 Select @tStrnNo=(max(strnno)+1) from Numbertable
Declare @tFreeSno Numeric(18,2)=0;
Declare @tCtl_FreeQty  bit=0;
Declare @tRprop varchar(100)='NoTax';
open c
fetch from c into @ItemName,@Qty,@Rate,@Amt,@Disc
while @@fetch_status=0
begin
  Select @tStrnSno=(max(strnsno)+1) from Numbertable
  Select @tItemNo=Item_no,@tTaxNo=Tax_no,@tUnitNo=Unit_no from Item_table where Item_name=@ItemName;
  SELECT @tTaxPercent=Nt_Percent,@tLedgerNo1=NtLedger_No from Tax_Table where Tax_No=@tTaxNo;
  Set @tTaxAmt=((@Qty*@Rate)*(@tTaxPercent/100));
  
  Select @tRprop=Rprop from Rptset where Rdesc='Display Tax Type'
  if @tRprop='NoTax'
  begin
  Set @tTaxAmt=0
  set @tTaxNo=1
  set @tTaxPercent=0
  end
     --Free Item Code Start
  Select @tCtl_FreeQty= Ctl_FreeQty from Control_table
  if @tCtl_FreeQty=1
  begin 
  if Exists (Select * from tempView where FreeType='Item Price' and Item_Name=@ItemName)
  Begin
   set @tFreeSno=(Select Top 1 (FreeSno) from tempView where FreeType='Item Price' and Item_Name=@ItemName)
  End
  Else  
  Begin
   set @tFreeSno=(Select Top 1 (FreeSno) from tempView where FreeType<>'Item Price' and Item_Name=@ItemName)
  End
  if  @tFreeSno is null
  set @tFreeSno=0
  End
  -- Free Item Code End
  
  Select @tItemNo=Item_no,@tItemCost=Item_cost,@tOpenItem=OpenItem from Item_table where Item_name=@ItemName;
    Set @tTotItemCost=(@tItemCost*@Qty);
    Set @tTotItemRate=(@Amt);  
    if @tOpenItem='True'
  select @tOpenItemCount=COUNT(*) from Tempstktrn_table where item_no=(select item_no from Item_table where Item_name=@ItemName) and strn_no=@tStrnNo  
  ELSE
  set @tOpenItemCount=0    
    
  INSERT INTO Tempstktrn_table (strn_sno,strn_no,strn_rtno,strn_type,strn_date,Godown_BillNo,StrnParty_no,Grn_no,OrderSno,Dc_no,item_no,ctr_no,godown_no,Unit_no,Unit_Ratio,QtyInPieces,nt_qty,tx_qty,Short_qty,rnt_qty,rtx_qty,Invnt_qty,Invtx_qty,QtyDetails,Rate,Tax_Rate,CurrencyNo,CurrencyValue,Amount,Tax_No,Disc_PerQty,Disc_Per,Disc_Amt,Adldisc_Per,Adldisc_Amt,Othdisc_Amt,OthPurdisc,Ed_PerQty,Ed_Per,Ed_Amt,Cess_Per,Cess_Amt,SHECess_Per,SHECess_Amt,HL_Per,HL_Amt,CST_per,CST_amt,tax_Flag,tax_per,tax_amt,Sur_per,Sur_amt,CommiPer,Commi,SmanPer,SmanAmt,spl_discamt,tot_amt,alp1,alp2,alp3,alp4,ala1,ala2,ala3,ala4,Net_Amt,Other_Exp,BillOther_Exp,strn_remarks,Strn_Cancel,Order_Ack,Cost,Mrsp,Margin,Margin_No,Srate,Frtx_Qty,RFrnt_Qty,RFrtx_Qty,Frnt_Qty,FreeQty,FreeItemNo,Profit,Item_Point,Mech_no,PurRate,OpenItem,OpenItemCount) values
   (@tStrnSno,@tStrnNo,'0','1', @tCurrentDate,'0','14','0','0','0',@tItemNo,@tCounter,'2',@tUnitNo,'1','0',@Qty,'0','0','0','0','0','0','',@Rate,'0','0','0',@Amt,@tTaxNo,'0','0',@Disc,'0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0',@tTaxPercent,@tTaxAmt,'0','0','0','0','0','0','0',(Case when @tRprop='Inclusive' then @Amt Else (@Amt+@tTaxAmt) END),'0','0','0','0','0','0','0','0',(Case when @tRprop='Inclusive' then @Amt Else (@Amt+@tTaxAmt) END),'0','0',@tRprop,'0','0','0','0','0','0','0','0','0','0','0','0',@tFreeSno,(@tTotItemRate-@tTotItemCost),'0','0','0',@tOpenItem,@tOpenItemCount);
  update Numbertable set strnsno=strnsno+1;
   select @tNetSalAmt=sum(net_amt) from Tempstktrn_table where item_no=@tItemNo and strn_type=1
  select @tNetSalRetAmt=sum(net_amt) from Tempstktrn_table where item_no=@tItemNo and strn_type=2
    if @tNetSalAmt is null
  set @tNetSalAmt=0;
  if @tNetSalRetAmt is null
  set @tNetSalRetAmt=0;
  update Item_Table set nt_salqty=nt_salqty+@Qty,nt_cloqty=nt_cloqty-@Qty,Nt_Salval=(@tNetSalAmt-@tNetSalAmt),Nt_SalRetval=@tNetSalAmt where Item_no=@tItemNo;
  fetch next from c into @ItemName,@Qty,@Rate,@Amt,@Disc
end
close c
deallocate c
Update NumberTable set strnno=strnno+1;

Select @tSalesCount=(Max(Vch_BillNo)) from VoucherNo_table where Vch_type='10'
--Select @tVchRefNo=(max(Ref_no)+1) from TempVch_table

if exists (Select * from TempVch_table)
Select @tVchRefNo=(max(Ref_no)+1) from TempVch_table
else
Select @tVchRefNo=(max(Ref_no)+1) from Vch_table

if @tVchRefNo is Null
Set @tVchRefNo=1;
--DECLARE c1 CURSOR LOCAL READ_ONLY FOR select Distinct(Tax_Per) As Tax from stktrn_table where strn_no=@tStrnNo
select @tVoucherNoNew=(max(VoucherNo)+1) from NumberTable

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1','14',@tVchRefNo,'1',@tCurrentDate,'10','5',@tLedgerNo1,@tNetAmt,'0','','','0','','0')
Update NumberTable set VoucherSno=VoucherSno+1;

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable;

Set @tCHK=(@tNetAmt-@tDiscount)-(@tGrossAmt+@tTotTax);
if(@tCHK<0)
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1','14',@tVchRefNo,'1',@tCurrentDate,'10','6','5',-@RoundValue,'0','','','0','','0')
else if(@tCHK>0)
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1','14',@tVchRefNo,'1',@tCurrentDate,'10','6','5','0',@RoundValue,'','','0','','0')

Update NumberTable set VoucherSno=VoucherSno+1;

DECLARE c1 CURSOR LOCAL READ_ONLY FOR select Tax_Per As Tax,Tax_No from Tempstktrn_table where strn_no=@tStrnNo group by tax_per,Tax_No
open c1
fetch from c1 into @tTax,@tTaxNumber
while @@fetch_status=0
begin
select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
select @tLedgerNo1=NtLedger_No from Tax_table where Nt_Percent=@tTax and Tax_no=@tTaxNumber
select @tSingleTaxAmt=sum(Tax_Amt) from Tempstktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1','14',@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1,'5','0',@tSingleTaxAmt,'','','0','','0')
  set @tSingleTaxAmtNew=0;
 if @tTax <>0
 select @tSingleTaxAmtNew=sum(Rate*nt_qty) from Tempstktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
 --set @tSingleTaxAmtNew=((@tSingleTaxAmt*100)/@tTax);
Update NumberTable set VoucherSno=VoucherSno+1;

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
select @tLedsel_name=Ledsel_name from Ledsel_table where Ledger_no=@tLedgerNo1;
IF @tLedsel_name is null
BEGIN
SET @tLedgerNo1New='0'
SET @tLedgerNo1New=0;
select @tSingleTaxAmtNew=sum(Rate*nt_qty) from Tempstktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
END
ELSE
BEGIN
IF CHARINDEX ('%',@tLedsel_name)=0
SET @tSub=@tLedsel_name
ELSE
SELECT @tSub=LEFT(@tLedsel_name, CHARINDEX('%',@tLedsel_name)-1);
Select @tLedgerNo1New=Ledger_no from Ledsel_table where Ledsel_name like @tSub+'%' and Ledger_no<>@tLedgerNo1
END
Insert into TempVch_table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,@tCounter,@tUserno,'1','14',@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1New,'5','0',@tSingleTaxAmtNew,'','','0','','0')


Update NumberTable set VoucherSno=VoucherSno+1;
fetch next from c1 into @tTax,@tTaxNumber
end
close c1
deallocate c1

--Free Item Code Start   
open c3
Declare @tFreeItemNo numeric(18,0);
Declare @tFreeMainItemNo numeric(18,0);
Declare @tFreeOfferNo numeric(18,0);
fetch from c3 into @tItemNameFree,@tQtyFree,@tScannedQtyFree,@tMainItemNameFree,@tOfferNameFree,@tOfferFreeQtyFree,@tTotSaleQtyFree
while @@fetch_status=0
begin
Select @tFreeItemNo=Item_no from Item_table where Item_name=@tItemNameFree
Select @tFreeMainItemNo=Item_no from Item_table where Item_name=@tMainItemNameFree
select @tFreeOfferNo=FreeSno from FreeItemMaster_table where OfferName=@tOfferNameFree
INSERT INTO [dbo].[SalFreeItemDetail_table]([smas_no] ,[smas_billno],[FreeItem_no],[TotFreeQty],[TotScannedQty],[MainItem_no],[OfferNo],[OfferFreeQty],[TotSaleQty],[smas_BillDate],[Ctr_no])
VALUES(@tStrnNo,@tBillNo,@tFreeItemNo,@tQtyFree,@tScannedQtyFree,@tFreeMainItemNo,@tFreeOfferNo,@tOfferFreeQtyFree,@tTotSaleQtyFree,@tCurrentDate,@tCounter)
fetch Next from c3 into @tItemNameFree,@tQtyFree,@tScannedQtyFree,@tMainItemNameFree,@tOfferNameFree,@tOfferFreeQtyFree,@tTotSaleQtyFree
End
close c3
deallocate c3
--Free Item Code End   


--open c1
--fetch from c1 into @tTax
--while @@fetch_status=0
--begin
--select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
--select @tLedgerNo1=NtLedger_No from Tax_table where Nt_Percent=@tTax
--select @tSingleTaxAmt=sum(Tax_Amt) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
--Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
-- (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1,'5','0',@tSingleTaxAmt,'','','0','','0')
-- set @tSingleTaxAmtNew=0;
-- if @tTax <>0
-- set @tSingleTaxAmtNew=((@tSingleTaxAmt*100)/@tTax);
--Update NumberTable set VoucherSno=VoucherSno+1;

--select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
--select @tLedsel_name=Ledsel_name from Ledsel_table where Ledger_no=@tLedgerNo1;
--SELECT @tSub=LEFT(@tLedsel_name, CHARINDEX('%',@tLedsel_name)-1);
--Select @tLedgerNo1New=Ledger_no from Ledsel_table where Ledsel_name like @tSub+'%' and Ledger_no<>@tLedgerNo1
--Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
-- (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1New,'5','0',@tSingleTaxAmtNew,'','','0','','0')


--Update NumberTable set VoucherSno=VoucherSno+1;
--fetch next from c1 into @tTax
--end
--close c1
--deallocate c1 
   
   
   DECLARE @tDiscountId numeric(18,0);
   select @tDiscountId=(MAX(DiscountEntry_Id)+1) from NumberTable;
  INSERT INTO TempDiscountDetail_table([Discount_Id],[Type],[Date],[Bill_no],[Amount],[GrossAmount]) VALUES
           (@tDiscountId,@DiscountType,@tCurrentDate,@tBillNo,@tDiscount,@tGrossAmt);

   Update VoucherNo_table set Vch_BillNo=Vch_BillNo+1 where Vch_Type='10';
   Update Numbertable set VoucherNo=VoucherNo+3,DiscountEntry_Id=DiscountEntry_Id+1;
   
   IF @TranCounter = 0           
      COMMIT TRANSACTION;
   END TRY
   
   BEGIN CATCH
   IF @TranCounter = 0         
      ROLLBACK TRANSACTION;
   ELSE
      IF XACT_STATE() <> -1        
         ROLLBACK TRANSACTION ProcedureSave;        
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;

      SELECT @ErrorMessage = ERROR_MESSAGE();
      SELECT @ErrorSeverity = ERROR_SEVERITY();
      SELECT @ErrorState = ERROR_STATE();

      RAISERROR (@ErrorMessage, -- Message text.
                 @ErrorSeverity, -- Severity.
                 @ErrorState -- State.
                 );
    END CATCH;

GO

CREATE PROCEDURE [dbo].[sp_SettleNETSProcess2](@tValue numeric(18,2))
AS
DECLARE @tStrnNo Numeric(18,0)=0;
DECLARE @tSalRecv_Sno numeric(18,2);
BEGIN
DECLARE @tBillNo NUMERIC(18,2);
  -- Select @tBillNo=(count(*)+1) from SalMas_table where smas_rtno=0;
   if exists (Select * from Tempsalmas_table where smas_rtno=0)
 Select @tBillNo=max(smas_billno)+1 from Tempsalmas_table where smas_rtno=0;
 else
 Select @tBillNo=max(smas_billno)+1 from SalMas_table where smas_rtno=0;
   SELECT @tSalRecv_Sno=(max(SalRecv_Sno)+1),@tStrnNo=(MAX(StrnNo)+1) from NumberTable;

   --INSERT INTO SalRecv_Table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tStrnNo,'14',@tValue,'0','0')   
   INSERT INTO TempSalRecv_table (SalRecv_Sno,SalRecv_Salno,SalRecv_Led,SalRecv_Amt,SalRecv_Refund,SalRecv_Cancel) values(@tSalRecv_Sno,@tBillNo,'14',@tValue,'0','0')   
   Update NumberTable set SalRecv_Sno=SalRecv_Sno+1
END;


GO

CREATE PROCEDURE [dbo].[sp_StockAdjCreate]
(
@tCtr_name varchar(200),
@tLedger_name varchar(200),
@temp_Table type_StockAdjCreate READONLY,
@tAdjNO INT,
@tDate DATETIME,
@tInnNo INT,
@tDt_inv DATETIME

)
as
DECLARE @TranCounter INT;
DECLARE @vCtr_no INT;
DECLARE @vLedger_no INT;
DECLARE @vCtr_no2 INT;
DECLARE @vStrnSno INT;
DECLARE @ItemCode varchar(100),@ItemName varchar(100),@Unit varchar(100),@LessQty numeric(18,2),@AddQty numeric(18,2),@Rate numeric(18,2),@Amount numeric(18,2);
DECLARE @vItemNo INT;
DECLARE @vTaxNo INT;
DECLARE @vNt_percent numeric(18,2);
DECLARE @vTaxValue numeric(18,2);
DECLARE @vUnitNo varchar(100);
DECLARE @vAmount numeric(18,2);
DECLARE c1 CURSOR LOCAL READ_ONLY FOR SELECT ItemCode,ItemName,unit,LessQty,AddQty,Rate,Amount FROM @temp_Table 

SET @TranCounter = @@TRANCOUNT;
IF @TranCounter > 0
SAVE TRANSACTION ProcedureSave;
ELSE
   BEGIN TRANSACTION;
   BEGIN TRY
  
  select @vCtr_no=ctr_no from counter_table where ctr_name=@tCtr_name
  select @vLedger_no=Ledger_no from Ledger_table where Ledger_name=@tLedger_name
  select @vCtr_no2=ctr_no from counter_table where ctr_name=@tCtr_name
  
  open c1
  fetch from c1 into @ItemCode,@ItemName,@Unit,@LessQty,@AddQty,@Rate,@Amount
  while @@FETCH_STATUS=0
  begin 
   IF (@ItemCode<>'' or @ItemName<>'')
   BEGIN
     IF (@AddQty<>'0' or @LessQty<>'0')
     BEGIN
       select @vStrnSno=Max(StrnSno)+1 from NumberTable
       Update NumberTable set StrnSno=StrnSno + 1
	   select @vItemNo=Item_no from Item_table where Item_name=@ItemName
       select @vTaxNo=Tax_no from Item_table where Item_name=@ItemName
       select @vNt_percent=Nt_percent from Tax_table where Tax_no=@vTaxNo
  
       if @vNt_percent<>0
       begin
          set @vTaxValue=@vNt_percent
       end
       else
       begin
          set @vTaxValue=0
       end 
  
     select @vUnitNo=Unit_no from Item_table where Item_name=@ItemName
     set @vAmount=@Rate*@vTaxValue/100;
  
     IF @AddQty='0' and @LessQty<>0
     BEGIN
        INSERT INTO stktrn_table (strn_sno,strn_no,strn_rtno,strn_type,strn_date,Godown_BillNo,StrnParty_no,Grn_no,OrderSno,Dc_no,item_no,ctr_no,godown_no,Unit_no,Unit_Ratio,QtyInPieces,nt_qty,tx_qty,Short_qty,rnt_qty,rtx_qty,Invnt_qty,Invtx_qty,Rate,Tax_Rate,CurrencyNo,CurrencyValue,Amount,Tax_No,Disc_PerQty,Disc_Per,Disc_Amt,Adldisc_Per,Adldisc_Amt,Othdisc_Amt,OthPurdisc,Ed_PerQty,Ed_Per,Ed_Amt,Cess_Per,Cess_Amt,SHECess_Per,SHECess_Amt,HL_Per,HL_Amt,CST_per,CST_amt,tax_Flag,tax_per,tax_amt,Sur_per,Sur_amt,CommiPer,Commi,SmanPer,SmanAmt,spl_discamt,tot_amt,alp1,alp2,alp3,alp4,ala1,ala2,ala3,ala4,Net_Amt,Other_Exp,BillOther_Exp,strn_remarks,Strn_Cancel,Order_Ack,Cost,Mrsp,Margin,Margin_No,Srate,Frtx_Qty,RFrnt_Qty,RFrtx_Qty,Frnt_Qty,FreeQty,FreeItemNo,Profit,Item_Point,Mech_no,PurRate,InvoiceNo,InvoiceDate)
                    VALUES(@vStrnSno,@tAdjNO,'0','11',@tDate,'0',@vLedger_no,'0','0','0',@vItemNo,@vCtr_no2,'2',@vUnitNo,'1','0',@LessQty,'0','0','0','0','0','0',@Rate,'0','0','0',@Amount,@vTaxNo,'0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0',0,'0','0','0','0','0','0','0','0','0',@Amount,'0','0','0','0','0','0','0','0',@Amount,'0','0','',0,0,'0','0','0','0','0','0','0','0','0','0','0','0','0','0','0',@tInnNo,@tDt_inv)
	               
     END
  
  IF @LessQty='0' and @AddQty<>0
  begin
  INSERT INTO stktrn_table (strn_sno,strn_no,strn_rtno,strn_type,strn_date,Godown_BillNo,StrnParty_no,Grn_no,OrderSno,Dc_no,item_no,ctr_no,godown_no,Unit_no,Unit_Ratio,QtyInPieces,nt_qty,tx_qty,Short_qty,rnt_qty,rtx_qty,Invnt_qty,Invtx_qty,Rate,Tax_Rate,CurrencyNo,CurrencyValue,Amount,Tax_No,Disc_PerQty,Disc_Per,Disc_Amt,Adldisc_Per,Adldisc_Amt,Othdisc_Amt,OthPurdisc,Ed_PerQty,Ed_Per,Ed_Amt,Cess_Per,Cess_Amt,SHECess_Per,SHECess_Amt,HL_Per,HL_Amt,CST_per,CST_amt,tax_Flag,tax_per,tax_amt,Sur_per,Sur_amt,CommiPer,Commi,SmanPer,SmanAmt,spl_discamt,tot_amt,alp1,alp2,alp3,alp4,ala1,ala2,ala3,ala4,Net_Amt,Other_Exp,BillOther_Exp,strn_remarks,Strn_Cancel,Order_Ack,Cost,Mrsp,Margin,Margin_No,Srate,Frtx_Qty,RFrnt_Qty,RFrtx_Qty,Frnt_Qty,FreeQty,FreeItemNo,Profit,Item_Point,Mech_no,PurRate,InvoiceNo,InvoiceDate)
                    VALUES(@vStrnSno,@tAdjNO,'0','12',@tDate,'0',@vLedger_no,'0','0','0',@vItemNo,@vCtr_no2,'2',@vUnitNo,'1','0',@AddQty,'0','0','0','0','0','0',@Rate,'0','0','0',@Amount,@vTaxNo,'0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0',0,'0','0','0','0','0','0','0','0','0',@Amount,'0','0','0','0','0','0','0','0',@Amount,'0','0','',0,0,'0','0','0','0','0','0','0','0','0','0','0','0','0','0','0',@tInnNo,@tDt_inv)
 
  end
  END
  END
  fetch next from c1 into @ItemCode,@ItemName,@Unit,@LessQty,@AddQty,@Rate,@Amount
  end
  close c1
  deallocate c1
  
   IF @TranCounter = 0          
      COMMIT TRANSACTION;
   END TRY
  
   BEGIN CATCH
   IF @TranCounter = 0        
      ROLLBACK TRANSACTION;
   ELSE
      IF XACT_STATE() <> -1       
         ROLLBACK TRANSACTION ProcedureSave;       
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;
 
      SELECT @ErrorMessage = ERROR_MESSAGE();
      SELECT @ErrorSeverity = ERROR_SEVERITY();
      SELECT @ErrorState = ERROR_STATE();
 
      RAISERROR (@ErrorMessage, -- Message text.
                 @ErrorSeverity, -- Severity.
                 @ErrorState -- State.
                 );
    END CATCH;


GO

CREATE PROCEDURE [dbo].[sp_SupplierDelete](@tSupplierNo numeric(18,0),@tSupplierName varchar(200))
as
begin
Delete from Supplier_table where Supplier_no=@tSupplierNo and Supplier_name=@tSupplierName
end;

Go 

CREATE PROCEDURE [dbo].[sp_TaxInsert](@TaxName varchar(150),@TaxValue varchar(500),@SetDefault varchar(20))
as

declare @tTaxNo int
DECLARE @TranCounter int

begin

SET @TranCounter = @@TRANCOUNT;
IF @TranCounter > 0
SAVE TRANSACTION ProcedureSave;
ELSE
   BEGIN TRANSACTION;
   BEGIN TRY   


select @tTaxNo=TaxId+1 from Number_table

if @SetDefault=1

begin
Update TaxCreation set SetDefault='0'
end

insert into TaxCreation(TaxId,TaxName,TaxValue,SetDefault) values(@tTaxNo,@TaxName,@TaxValue,@SetDefault)

Update Number_table set TaxId=TaxId + 1


      IF @TranCounter = 0          
      COMMIT TRANSACTION;
   END TRY
  
   BEGIN CATCH
   IF @TranCounter = 0        
      ROLLBACK TRANSACTION;
   ELSE
      IF XACT_STATE() <> -1       
         ROLLBACK TRANSACTION ProcedureSave;       
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;
 
      SELECT @ErrorMessage = ERROR_MESSAGE();
      SELECT @ErrorSeverity = ERROR_SEVERITY();
      SELECT @ErrorState = ERROR_STATE();
 
      RAISERROR (@ErrorMessage, -- Message text.
                 @ErrorSeverity, -- Severity.
                 @ErrorState -- State.
                 );
    END CATCH

end;


GO



CREATE PROCEDURE [dbo].[sp_TaxUpdate](@tTaxNo int,@TaxName varchar(150),@TaxValue varchar(500),@SetDefault varchar(20))
as

DECLARE @TranCounter int

begin

SET @TranCounter = @@TRANCOUNT;
IF @TranCounter > 0
SAVE TRANSACTION ProcedureSave;
ELSE
   BEGIN TRANSACTION;
   BEGIN TRY   

if @SetDefault=1

begin
Update TaxCreation set SetDefault='0'
end

Update TaxCreation set TaxName=@TaxName,TaxValue=@TaxValue,SetDefault=@SetDefault where TaxId=@tTaxNo
	
	
	IF @TranCounter = 0          
      COMMIT TRANSACTION;
   END TRY
  
   BEGIN CATCH
   IF @TranCounter = 0        
      ROLLBACK TRANSACTION;
   ELSE
      IF XACT_STATE() <> -1       
         ROLLBACK TRANSACTION ProcedureSave;       
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;
 
      SELECT @ErrorMessage = ERROR_MESSAGE();
      SELECT @ErrorSeverity = ERROR_SEVERITY();
      SELECT @ErrorState = ERROR_STATE();
 
      RAISERROR (@ErrorMessage, -- Message text.
                 @ErrorSeverity, -- Severity.
                 @ErrorState -- State.
                 );
    END CATCH	
	
end;


GO

CREATE PROCEDURE [dbo].[sp_Unit_Insert]
(

@tunit_name as varchar(200),
@tudecimal as float,
@tWeightScale as bit,
@chk as bit OUT
)
as
DECLARE @TranCounter INT;
DECLARE @uno varchar(200);
DECLARE @udec float;
DECLARE @UNameUpper varchar(200)
SET @TranCounter = @@TRANCOUNT;
IF @TranCounter > 0
SAVE TRANSACTION ProcedureSave;
ELSE
   BEGIN TRANSACTION;
   BEGIN TRY
   
  select @uno=max(UnitID)+1 from Numbertable
  
  set @UNameUpper=UPPER(@tunit_name)
  
  IF @tudecimal<>0
  BEGIN
  set @udec=@tudecimal
  END
  ELSE
  set @udec=0
  
  BEGIN
  IF NOT EXISTS(Select * from unit_table where unit_name=@tunit_name)
  
  BEGIN
   INSERT INTO DBO.unit_table(unit_no,unit_name,unit_printname,unit_mtname,unit_alias,unit_flag,WeightScale,unit_Decimals) 
  VALUES(@uno ,@tunit_name ,@UNameUpper,@UNameUpper ,'0','0',@tWeightScale,@udec);
  set @chk=0
  
  END
  
  ELSE
 set @chk=1
  END
  
  update NumberTable set UnitId=UnitId+1  
  
   IF @TranCounter = 0          
      COMMIT TRANSACTION;
   END TRY
  
   BEGIN CATCH
   IF @TranCounter = 0        
      ROLLBACK TRANSACTION;
   ELSE
      IF XACT_STATE() <> -1       
         ROLLBACK TRANSACTION ProcedureSave;       
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;
 
      SELECT @ErrorMessage = ERROR_MESSAGE();
      SELECT @ErrorSeverity = ERROR_SEVERITY();
      SELECT @ErrorState = ERROR_STATE();
 
      RAISERROR (@ErrorMessage, -- Message text.
                 @ErrorSeverity, -- Severity.
                 @ErrorState -- State.
                 );
    END CATCH;


GO

CREATE PROCEDURE [dbo].[sp_Unit_Update]
(
@unit_name as varchar(200),
@unit_mtname as varchar(200),
@unit_Decimals as varchar(200),
@unit_Name2 as varchar(200),
@tWeightScale as bit
)
as
DECLARE @TranCounter INT;
SET @TranCounter = @@TRANCOUNT;
IF @TranCounter > 0
SAVE TRANSACTION ProcedureSave;
ELSE
   BEGIN TRANSACTION;
   BEGIN TRY
  
 UPDATE DBO.unit_table SET unit_name=@unit_name,unit_mtname= @unit_mtname,WeightScale=@tWeightScale,unit_Decimals =@unit_Decimals WHERE unit_name=@unit_Name2  
  
   IF @TranCounter = 0          
      COMMIT TRANSACTION;
   END TRY
  
   BEGIN CATCH
   IF @TranCounter = 0        
      ROLLBACK TRANSACTION;
   ELSE
      IF XACT_STATE() <> -1       
         ROLLBACK TRANSACTION ProcedureSave;       
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;
 
      SELECT @ErrorMessage = ERROR_MESSAGE();
      SELECT @ErrorSeverity = ERROR_SEVERITY();
      SELECT @ErrorState = ERROR_STATE();
 
      RAISERROR (@ErrorMessage, -- Message text.
                 @ErrorSeverity, -- Severity.
                 @ErrorState -- State.
                 );
    END CATCH;

	
GO


CREATE PROCEDURE [dbo].[sp_updateFirstEndOfday] (@tUserno Numeric(18,0),@tCounter numeric(18,0))
AS
DECLARE @tEndOfDayId numeric(18,0);
DECLARE @tCount numeric(18,0);
BEGIN
Select @tEndOfDayId=max(EndOfDayId)+1 from NumberTable
Select @tCount=count(*) from EndOfDay_Table where BeginCashDrawId is null
if @tCount=0 OR @tCount is Null
begin
if @tEndOfDayId=0
BEGIN
insert into EndOFDay_table (Id,EndOfDay,Coin_P05,Coin_P10,Coin_P20,Coin_P50,Coin_1,Coin_2,Coin_5,Coin_10,Coin_20,Coin_50,Coin_100,Coin_1000,Coin_P05amt,Coin_P10amt,Coin_P20amt,Coin_P50amt,Coin_1amt,Coin_2amt,Coin_5amt,Coin_10amt,Coin_20amt,Coin_50amt,Coin_100amt,Coin_1000amt,CoinTotCount,CoinTotAmt,Status,User_no,Ctr_no) values
('1',DATEADD(DAY,-1,getdate()),'0','0','0','0','0','0','0','0','0','0','0','0','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0','0.00','Match',@tUserno,@tCounter)
UPDATE NumberTable set EndOfDayId=EndOfDayId+1;
END
ELSE
BEGIN
insert into EndOFDay_table (Id,EndOfDay,Coin_P05,Coin_P10,Coin_P20,Coin_P50,Coin_1,Coin_2,Coin_5,Coin_10,Coin_20,Coin_50,Coin_100,Coin_1000,Coin_P05amt,Coin_P10amt,Coin_P20amt,Coin_P50amt,Coin_1amt,Coin_2amt,Coin_5amt,Coin_10amt,Coin_20amt,Coin_50amt,Coin_100amt,Coin_1000amt,CoinTotCount,CoinTotAmt,Status,User_no,Ctr_no) values
(@tEndOfDayId,DATEADD(DAY,-1,getdate()),'0','0','0','0','0','0','0','0','0','0','0','0','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0.00','0','0.00','Match',@tUserno,@tCounter)
UPDATE NumberTable set EndOfDayId=EndOfDayId+1;
END
END
INSERT INTO BeginCashDrawerActive_table (Active,User_no,Ctr_no,EndOfDayDate) values (1,@tUserno,@tCounter,getdate());

END;


GO

CREATE PROCEDURE [dbo].[sp_User_Insert]
(
@tUserName as varchar(200),
@tUserType as varchar(100),
@tPassword as varchar(200),
@tCounter as VARCHAR(100),
@tDiscountRange as numeric(18,2),
@tResettle as varchar(50),
@tStopAtQty as varchar(50),@tStopAtRate as varchar(50),
@tAllowVoid as varchar(50),
@tAllowReturn as varchar(50),@tViewReport varchar(50),@LSystemName Varchar(Max))
as
DECLARE @TranCounter INT;
DECLARE @Uno varchar(200);
DECLARE @UNameUpper varchar(200);
DECLARE @vUserType int;
DECLARE @tCounterNo int;

SET @TranCounter = @@TRANCOUNT;
IF @TranCounter > 0
SAVE TRANSACTION ProcedureSave;
ELSE
   BEGIN TRANSACTION;
   BEGIN TRY
   
  Select @Uno=max(User_No)+1 from Numbertable
      
      if @tUserType='Admin'
      begin
      set @vUserType=0
      end
      else if @tUserType='User'
      begin
      set @vUserType=1
      end
  set @UNameUpper=UPPER(@tUserName)
  Select @tCounterNo=Ctr_no from counter_table where ctr_name=@tCounter;
  BEGIN
    --if Not Exists (Select * from User_table where USER_NAME=@tUserName and Ctr_no=@tCounter)
  Begin
    INSERT INTO User_table(User_no,User_name,User_type,User_mtname,User_Pass,Alter_Days,Print_Bills,Ctr_no,DiscountRange,Resettle,StopatQty,StopatRate,AllowVoid,AllowReturn,ViewReport,LSystemName) 
    VALUES(@Uno ,@tUserName ,@vUserType,@UNameUpper,@tPassword ,'0','False',@tCounterNo,@tDiscountRange,@tResettle,@tStopAtQty,@tStopAtRate,@tAllowVoid,@tAllowReturn,@tViewReport,@LSystemName);
  End
 --Else
 --  begin
	--Update User_table Set User_no=@Uno,User_type=@vUserType,Alter_Days=0,Print_Bills='False',DiscountRange=@tDiscountRange,Resettle=@tResettle,StopatQty=@tStopAtQty,StopatRate=@tStopAtRate  where User_name=@tUserName and User_Pass=@tPassword and Ctr_no=@tCounterNo
 --  End  
END  
   update NumberTable set User_No=User_No+1  
   IF @TranCounter = 0          
      COMMIT TRANSACTION;
   END TRY
   BEGIN CATCH
   IF @TranCounter = 0        
      ROLLBACK TRANSACTION;
   ELSE
      IF XACT_STATE() <> -1       
         ROLLBACK TRANSACTION ProcedureSave;       
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;
 
      SELECT @ErrorMessage = ERROR_MESSAGE();
      SELECT @ErrorSeverity = ERROR_SEVERITY();
      SELECT @ErrorState = ERROR_STATE();
 
      RAISERROR (@ErrorMessage, -- Message text.
                 @ErrorSeverity, -- Severity.
                 @ErrorState -- State.
                 );
    END CATCH;

	
GO

CREATE PROCEDURE [dbo].[sp_vchCreation] (@tTax Numeric(18,2),@tSalesCount numeric(18,0),@tCurrentDate datetime,@tTaxNumber NUMERIC(18,0),@CashType VARCHAR(50),@OldVchNumber varchar(100))
AS

DECLARE @tBillNo Numeric(18,0)=0;
DECLARE @tVoucherNoNew Numeric(18,0)=0;
DECLARE @tVoucherSno Numeric(18,0)=0;
DECLARE @tLedgerNo1 Numeric(18,0)=0;
DECLARE @tSingleTaxAmt Numeric(18,2)=0;
DECLARE @tSingleTaxAmtNew Numeric(18,2)=0;
DECLARE @tLedsel_name varchar(100);
DECLARE @tSub varchar(100);
DECLARE @tLedgerNo1New Numeric(18,2);
DECLARE @tVchRefNo Numeric(18,0)=0;
DECLARE @tStrnNo numeric(18,0)=0;
BEGIN
--select @tVoucherNoNew=Vch_Sno from Vch_table where Vch_No=@tSalesCount
--select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
--select @tLedgerNo1=NtLedger_No from Tax_table where Nt_Percent=@tTax
--Select @tStrnNo=smas_no from salmas_table where smas_billno=@tSalesCount;
--select @tSingleTaxAmt=sum(Tax_Amt) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
--select @tVchRefNo=smas_no from salmas_table where smas_billno=@tStrnNo;
--Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
-- (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','5',@tVchRefNo,'1',@tCurrentDate,'12',@tLedgerNo1,'5','0',@tSingleTaxAmt,'','','0','','0')
--  set @tSingleTaxAmtNew=0;
-- if @tTax <>0
-- set @tSingleTaxAmtNew=((@tSingleTaxAmt*100)/@tTax);
--Update NumberTable set VoucherSno=VoucherSno+1;

--select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
--select @tLedsel_name=Ledsel_name from Ledsel_table where Ledger_no=@tLedgerNo1;
--SELECT @tSub=LEFT(@tLedsel_name, CHARINDEX('%',@tLedsel_name)-1);
--Select @tLedgerNo1New=Ledger_no from Ledsel_table where Ledsel_name like @tSub+'%' and Ledger_no<>@tLedgerNo1
--Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
-- (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','5',@tVchRefNo,'1',@tCurrentDate,'12',@tLedgerNo1New,'5','0',@tSingleTaxAmtNew,'','','0','','0')
--Update NumberTable set VoucherSno=VoucherSno+1;
if @CashType='Cash'
BEGIN
Select @tStrnNo=smas_no from salmas_table where smas_billno=@tSalesCount;
select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
select @tLedgerNo1=NtLedger_No from Tax_table where Nt_Percent=@tTax and Tax_no=@tTaxNumber
select @tSingleTaxAmt=sum(Tax_Amt) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@OldVchNumber,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','5',@tVchRefNo,'1',@tCurrentDate,'12',@tLedgerNo1,'5','0',@tSingleTaxAmt,'','','0','','0')
  set @tSingleTaxAmtNew=0;
 if @tTax <>0
 select @tSingleTaxAmtNew=sum(Rate*nt_qty) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
 --set @tSingleTaxAmtNew=((@tSingleTaxAmt*100)/@tTax);
Update NumberTable set VoucherSno=VoucherSno+1;

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
select @tLedsel_name=Ledsel_name from Ledsel_table where Ledger_no=@tLedgerNo1;
IF @tLedsel_name is null
BEGIN
SET @tLedgerNo1New='0'
SET @tLedgerNo1New=0;
select @tSingleTaxAmtNew=sum(Rate*nt_qty) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
END
ELSE
BEGIN
IF CHARINDEX ('%',@tLedsel_name)=0
SET @tSub=@tLedsel_name
ELSE
SELECT @tSub=LEFT(@tLedsel_name, CHARINDEX('%',@tLedsel_name)-1);
Select @tLedgerNo1New=Ledger_no from Ledsel_table where Ledsel_name like @tSub+'%' and Ledger_no<>@tLedgerNo1
END
Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@OldVchNumber,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','5',@tVchRefNo,'1',@tCurrentDate,'12',@tLedgerNo1New,'5','0',@tSingleTaxAmtNew,'','','0','','0')


Update NumberTable set VoucherSno=VoucherSno+1;
END
IF @CashType='Credit'
BEGIN
Select @tStrnNo=smas_no from salmas_table where smas_billno=@tSalesCount;
select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
select @tLedgerNo1=NtLedger_No from Tax_table where Nt_Percent=@tTax and Tax_no=@tTaxNumber
select @tSingleTaxAmt=sum(Tax_Amt) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@OldVchNumber,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1,'5','0',@tSingleTaxAmt,'','','0','','0')
  set @tSingleTaxAmtNew=0;
 if @tTax <>0
 select @tSingleTaxAmtNew=sum(Rate*nt_qty) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
 --set @tSingleTaxAmtNew=((@tSingleTaxAmt*100)/@tTax);
Update NumberTable set VoucherSno=VoucherSno+1;

select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
select @tLedsel_name=Ledsel_name from Ledsel_table where Ledger_no=@tLedgerNo1;
IF @tLedsel_name is null
BEGIN
SET @tLedgerNo1New='0'
SET @tLedgerNo1New=0;
select @tSingleTaxAmtNew=sum(Rate*nt_qty) from Stktrn_table where tax_Per=@tTax and strn_no=@tStrnNo;
END
ELSE
BEGIN
IF CHARINDEX ('%',@tLedsel_name)=0
SET @tSub=@tLedsel_name
ELSE
SELECT @tSub=LEFT(@tLedsel_name, CHARINDEX('%',@tLedsel_name)-1);
Select @tLedgerNo1New=Ledger_no from Ledsel_table where Ledsel_name like @tSub+'%' and Ledger_no<>@tLedgerNo1
END
Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@OldVchNumber,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10',@tLedgerNo1New,'5','0',@tSingleTaxAmtNew,'','','0','','0')


Update NumberTable set VoucherSno=VoucherSno+1;
END
END;


GO

CREATE PROCEDURE [dbo].[sp_vchDelete](@tSalesCount numeric(18,0),@tNetAmt numeric(18,2),@tDiscount numeric(18,2),@tGrossAmt numeric(18,2),@tTotTax numeric(18,2),@tCurrentDate datetime,@CashType VARCHAR(50))	
AS
DECLARE @tVch_party NUMERIC(18,0);
DECLARE @tCHK NUMERIC(18,2);
DECLARE @tVoucherNoNew Numeric(18,0)=0;
DECLARE @tVoucherSno Numeric(18,0)=0;
DECLARE @tLedgerNo1 Numeric(18,0)=0;
DECLARE @tSingleTaxAmt Numeric(18,2)=0;
DECLARE @tSingleTaxAmtNew Numeric(18,2)=0;
DECLARE @tLedsel_name varchar(100);
DECLARE @tSub varchar(100);
DECLARE @tLedgerNo1New Numeric(18,2);
DECLARE @tVchRefNo Numeric(18,0)=0;
DECLARE @tStrnNo numeric(18,0)=0;
DECLARE @tOldVchNo numeric(18,0)=0;

BEGIN
select @tVch_party=vch_party from Vch_table where Vch_No=@tSalesCount group by vch_party
select @tOldVchNo=Vch_Sno from  Vch_table where Vch_No=@tSalesCount and Vch_Party=@tVch_party
delete from Vch_table where Vch_No=@tSalesCount and Vch_Party=@tVch_party
select @tVoucherNoNew=Vch_Sno from Vch_table where Vch_No=@tSalesCount
select @tVoucherSno=(max(VoucherSno)+1) from NumberTable
Select @tStrnNo=smas_no from salmas_table where smas_billno=@tSalesCount;
select @tVchRefNo=smas_no from salmas_table where smas_billno=@tStrnNo;
IF @CashType='Cash'
BEGIN
Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tOldVchNo,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','5',@tVchRefNo,'1',@tCurrentDate,'12','5',@tLedgerNo1,@tNetAmt,'0','','','0','','0')
Update NumberTable set VoucherSno=VoucherSno+1;

Set @tCHK=(@tNetAmt-@tDiscount)-(@tGrossAmt+@tTotTax);
if(@tCHK<0)
Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values (@tVoucherSno,@tOldVchNo,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','5',@tVchRefNo,'1',@tCurrentDate,'12','6','5',-@tCHK,'0','','','0','','0')
else if(@tCHK>0)
Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values (@tVoucherSno,@tOldVchNo,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','5',@tVchRefNo,'1',@tCurrentDate,'12','6','5','0',@tCHK,'','','0','','0')

Update NumberTable set VoucherSno=VoucherSno+1;
END
IF @CashType='Credit'
BEGIN
Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values
 (@tVoucherSno,@tVoucherNoNew,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10','5',@tLedgerNo1,@tNetAmt,'0','','','0','','0')
Update NumberTable set VoucherSno=VoucherSno+1;

Set @tCHK=(@tNetAmt-@tDiscount)-(@tGrossAmt+@tTotTax);
if(@tCHK<0)
Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values (@tVoucherSno,@tOldVchNo,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10','6','5',-@tCHK,'0','','','0','','0')
else if(@tCHK>0)
Insert into Vch_Table (Sno,Vch_Sno,Vch_Pre,Vch_NoLong,Vch_Suf,Vch_No,Vch_MtNo,Ctr_no,UserNo,RepNo,Vch_Party,ref_no,ref_det,Vch_Date,Vch_type,ledger_no,ledger_no1,Dr_amount,Cr_amount,Vch_Remarks,Vch_IndRemarks,Vch_Cancel,Vch_CRemarks,Vch_flag) Values (@tVoucherSno,@tOldVchNo,'',@tSalesCount,'',@tSalesCount,@tSalesCount,'1','0','1','14',@tVchRefNo,'1',@tCurrentDate,'10','6','5','0',@tCHK,'','','0','','0')

Update NumberTable set VoucherSno=VoucherSno+1;
END
END;


GO

CREATE PROCEDURE [dbo].[sp_Void](@tBillNo varchar(100),@tReason varchar(400))
AS
SET NOCOUNT ON 
DECLARE @tSmas_no numeric(18,0);
DECLARE @tItemNo NUMERIC(18,0);
DECLARE @tItemQty NUMERIC(18,4);
DECLARE @tNetAmt NUMERIC(18,2);
BEGIN
UPDATE salmas_table set smas_Cancel='True',smas_cremark=@tReason where smas_billno=@tBillNo and smas_rtno=0;
UPDATE stktrn_table set Strn_Cancel='True' where strn_no=(select smas_no from salmas_table where smas_billno=@tBillNo and smas_rtno=0);
UPDATE Vch_table set Vch_Cancel='True' where Vch_Sno=(select VoucherSno from salmas_table where smas_billno=@tBillNo and smas_rtno=0);
DECLARE c1 CURSOR LOCAL READ_ONLY FOR SELECT smas_no FROM salMas_table where smas_billno=@tBillNo and Smas_rtno=0;
open c1
fetch from c1 into @tSmas_no
while @@fetch_status=0
begin
  DECLARE c2 CURSOR LOCAL READ_ONLY FOR Select item_no,nt_qty,Net_Amt from stktrn_table where strn_no=@tSmas_no;    
  open c2
  fetch from c2 into @tItemNo,@tItemQty,@tNetAmt
  WHILE @@FETCH_STATUS=0
  begin
    UPDATE Item_table set nt_salqty=nt_salqty-@tItemQty, nt_cloqty=nt_cloqty+@tItemQty, Nt_Salval=Nt_Salval-@tNetAmt where Item_no=@tItemNo;
    fetch from c2 into @tItemNo,@tItemQty,@tNetAmt
    end
    close c2
    deallocate c2
    fetch next from c1 into @tSmas_no
end
close c1
deallocate c1
END;


GO

CREATE PROCEDURE [dbo].[SPPruchaseSalesDatewiseProfit](@RptType as varchar(20),@DatesType as Varchar(20),@startDate datetime,@endDate datetime)
As 
Begin
SET NOCOUNT ON 
if @RptType='PurRpt'
    Begin  
				select distinct item_table.Item_code As ItemCode,item_table.Item_name As ItemName,sum(nt_qty) As PurQty,Convert(numeric(18,2),Avg(stktrn_table.Rate)) As PurRate,Convert(Numeric(18,2),(Sum(stktrn_table.nt_qty)*(Avg(stktrn_table.Rate))))As TotalPur,'0.00' As SalesQty,'0.00' As SalesRate,'0.00' As SalesTot,sum(nt_qty) As TotalStock from stktrn_table,item_table where stktrn_table.item_no=item_table.item_no and (stktrn_table.strn_type=0 or stktrn_table.strn_type=3) and stktrn_table.strn_date between @startDate and @endDate group by stktrn_table.item_no,item_table.Item_code,item_table.Item_name order by item_name ASC 	    
    End		  
if Ltrim(@RptType)<>'PurRpt'
		Begin
		 
			select distinct item_table.Item_code As ItemCode,item_table.Item_name As ItemName,'0.00' As PurQty,'0.00' As PurRate,'0.00' As TotalPur,sum(nt_qty) As SalesQty,Convert(numeric(18,2),Avg(stktrn_table.Rate)) As SalesRate,Convert(numeric(18,2),(Sum(stktrn_table.nt_qty)*(Avg(stktrn_table.Rate)))) As SalesTot,sum(nt_qty) As TotalStock from stktrn_table,item_table where item_table.item_no=stktrn_table.item_no and   stktrn_table.strn_type=1 and strn_rtno<>1 and Strn_Cancel<>1 and  stktrn_table.strn_date between @startDate and @endDate group by stktrn_table.item_no,item_table.Item_code,item_table.Item_name order by item_name ASC 
			
   			End 	   
End;

Go